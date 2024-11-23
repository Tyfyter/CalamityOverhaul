﻿using CalamityOverhaul.Common;
using InnoVault.UIHandles;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;

namespace CalamityOverhaul.Content.UIs.SupertableUIs
{
    internal class DragButton : UIHandle, ICWRLoader
    {
        public override Texture2D Texture => CWRUtils.GetT2DValue("CalamityOverhaul/Assets/UIs/SupertableUIs/HotbarRadial_0");
        public static DragButton Instance;
        public override float RenderPriority => 0;
        public override bool Active {
            get {
                if (SupertableUI.Instance == null) {
                    return false;
                }

                return SupertableUI.Instance.Active;
            }
        }
        public Vector2 supPos => SupertableUI.Instance.DrawPosition;
        public Vector2 InSupPosOffset => new Vector2(554, 380);
        public Rectangle mainRec;
        public Vector2 InPosOffsetDragToPos;
        public Vector2 DragVelocity;
        public bool onMain;
        public static bool onDrag;
        public override void Load() => Instance = this;
        void ICWRLoader.UnLoadData() => Instance = null;
        public void Initialize() {
            DrawPosition = SupertableUI.Instance.DrawPosition + InSupPosOffset;
            mainRec = new Rectangle((int)DrawPosition.X, (int)DrawPosition.Y, 48, 48);
            onMain = mainRec.Intersects(new Rectangle((int)MousePosition.X, (int)MousePosition.Y, 1, 1));
        }

        public override void Update() {
            if (SupertableUI.Instance == null) {
                return;
            }

            Initialize();

            //int museS = DownStartL();//获取按钮点击状态
            int museS = (int)keyLeftPressState;
            if (onMain) {
                if (museS == 1 && !onDrag) {//如果玩家刚刚按下鼠标左键，并且此时没有开启拖拽状态
                    onDrag = true;
                    InPosOffsetDragToPos = DrawPosition.To(MousePosition);//记录此时的偏移向量
                    if (Main.myPlayer == player.whoAmI)
                        SoundEngine.PlaySound(SoundID.MenuTick);
                }
            }

            if (onDrag) {
                if (museS == 2) {
                    onDrag = false;
                }
                DragVelocity = (DrawPosition + InPosOffsetDragToPos).To(MousePosition);//更新拖拽的速度
                SupertableUI.Instance.DrawPosition += DragVelocity;
            }
            else {
                DragVelocity = Vector2.Zero;
            }

            Prevention();
        }

        public void Prevention() {
            if (SupertableUI.Instance.DrawPosition.X < 0) {
                SupertableUI.Instance.DrawPosition.X = 0;
            }
            if (SupertableUI.Instance.DrawPosition.X + SupertableUI.Instance.Texture.Width > Main.screenWidth) {
                SupertableUI.Instance.DrawPosition.X = Main.screenWidth - SupertableUI.Instance.Texture.Width;
            }
            if (SupertableUI.Instance.DrawPosition.Y < 0) {
                SupertableUI.Instance.DrawPosition.Y = 0;
            }
            if (SupertableUI.Instance.DrawPosition.Y + SupertableUI.Instance.Texture.Height > Main.screenHeight) {
                SupertableUI.Instance.DrawPosition.Y = Main.screenHeight - SupertableUI.Instance.Texture.Height;
            }
        }

        public void ThisDraw(SpriteBatch spriteBatch) {
            if (onDrag) {
                Initialize();
            }
            Texture2D value = CWRUtils.GetT2DValue("CalamityOverhaul/Assets/UIs/SupertableUIs/TexturePackButtons");
            spriteBatch.Draw(Texture, DrawPosition, null, Color.White, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0);//绘制出UI主体
            Rectangle r1 = new Rectangle(0, 0, 32, 32);
            Rectangle r2 = new Rectangle(32, 0, 32, 32);
            Rectangle r3 = new Rectangle(0, 32, 32, 32);
            Rectangle r4 = new Rectangle(32, 32, 32, 32);
            Color dragColor = Color.Red;
            Color c1 = DragVelocity.Y >= 0 ? Color.White : dragColor;
            Color c2 = DragVelocity.Y <= 0 ? Color.White : dragColor;
            Color c3 = DragVelocity.X >= 0 ? Color.White : dragColor;
            Color c4 = DragVelocity.X <= 0 ? Color.White : dragColor;
            spriteBatch.Draw(value, DrawPosition + new Vector2(16.5f, 0), r1, c1, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0);//上
            spriteBatch.Draw(value, DrawPosition + new Vector2(16.5f, 32), r2, c2, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0);//下
            spriteBatch.Draw(value, DrawPosition + new Vector2(0, 16.5f), r3, c3, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0);//左
            spriteBatch.Draw(value, DrawPosition + new Vector2(32, 16.5f), r4, c4, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0);//右
            if (onMain) {
                Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, VaultUtils.Translation("左键拖动", "left-drag"), DrawPosition.X - 8, DrawPosition.Y - 16, Color.BlueViolet, Color.Black, Vector2.Zero, 0.8f);
            }
        }
    }
}
