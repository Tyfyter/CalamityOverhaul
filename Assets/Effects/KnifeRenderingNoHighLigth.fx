//���ž���
matrix transformMatrix;
//����ͼ�����ǵ���Ҷ�ͼ
texture sampleTexture;
//��ɫͼ�������һ�ź����ɫͼ
texture gradientTexture;
int Time;

sampler2D samplerTex = sampler_state
{
    texture = <sampleTexture>;
    magfilter = LINEAR;
    minfilter = LINEAR;
    mipfilter = LINEAR;
    AddressU = wrap;
    AddressV = wrap; //ѭ��UV
};

sampler2D gradientTex = sampler_state
{
    texture = <gradientTexture>;
    magfilter = LINEAR;
    minfilter = LINEAR;
    mipfilter = LINEAR;
    AddressU = wrap;
    AddressV = wrap; //ѭ��UV
};

struct VertexShaderInput
{
    float4 Position : POSITION;
    float2 TexCoords : TEXCOORD0;
    float4 Color : COLOR0;
};

struct VertexShaderOutput
{
    float4 Position : POSITION;
    float2 TexCoords : TEXCOORD0;
    float4 Color : COLOR0;
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;
    
    output.Color = input.Color;
    output.TexCoords = input.TexCoords;
    output.Position = mul(input.Position, transformMatrix);

    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
    float4 color = tex2D(samplerTex, input.TexCoords).xyzw; //��ȡ����Ҷ�ͼ
    float4 color2 = tex2D(gradientTex, float2(input.TexCoords.y, 0)).xyzw; //��ȡ��ɫͼ
    // �������Ҷ�ͼ�ϵ�r����0.8f��Ҳ��������ɫ�Ƚϰ׵Ļ���ô�͸����ӵظ�����
    float3 color3 = float3(0, 0, 0);
    float3 bright = color.xyz * color2.xyz + color3;
    //͸�������ɴ�����ɫ��͸���ȳ��⵶��Ҷ�ͼ��r
    return float4(bright, input.Color.w * color.r);
}

technique Technique1
{
    pass KnifeRenderingNoHighLigthPass
    {
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
};