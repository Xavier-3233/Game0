using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Game0.StateManagement;

namespace Game0.Screens
{
    public class CreditsScreen : MenuScreen
    {
        private ContentManager _content;
        private SpriteFont _spriteFont;

        public CreditsScreen() : base("Credits")
        {       

            var back = new MenuEntry("Back");


            back.Selected += OnCancel;

            MenuEntries.Add(back);
        }

        public override void Activate()
        {
            if (_content == null)
                _content = new ContentManager(ScreenManager.Game.Services, "Content");
            _spriteFont = _content.Load<SpriteFont>("PublicPixel");
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.SpriteBatch.Begin();
            ScreenManager.SpriteBatch.DrawString(_spriteFont, "Most of the Artwork was created by Keenan Melton", new Vector2(20, 100), Color.Gold);
            ScreenManager.SpriteBatch.DrawString(_spriteFont, "Explosion art free from Freepik", new Vector2(60, 150), Color.Gold);
            ScreenManager.SpriteBatch.DrawString(_spriteFont, "Composer: Jesse Spillane", new Vector2(480 / 2, 200), Color.Gold);
            ScreenManager.SpriteBatch.DrawString(_spriteFont, "Press Enter to exit", new Vector2(480 / 2, 400), Color.Gold);
            ScreenManager.SpriteBatch.End();
        }

        

    }
}
