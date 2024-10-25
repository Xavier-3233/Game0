using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Game0.StateManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace Game0.Screens
{
    public class TitleScreen : MenuScreen
    {
        private readonly GraphicsDeviceManager _graphics;
        //private SpriteBatch _spriteBatch;
        private ContentManager _content;
        private Texture2D _paddle;
        private Vector2 _paddlePlacement;
        private Texture2D _brick;
        private Vector2 _brickPlacement;
        private Texture2D _ball;
        private Vector2 _ballPlacement;
        private Texture2D _breakBall;
        private Vector2 _breakBallPlacement;
        private SpriteFont spriteFont;
        //private StarSprite[] stars;
        private int width = 800;
        private int height = 480;

        public TitleScreen(GraphicsDeviceManager graphics) : base("Main Menu")
        {
            _graphics = graphics;
            var playGameMenuEntry = new MenuEntry("Play Game");
            var playSpaceMenuEntry = new MenuEntry("Play Space");
            var playMine = new MenuEntry("Play Mine");
            var creditsMenuEntry = new MenuEntry("Credits");
            var exitMenuEntry = new MenuEntry("Exit");

            playGameMenuEntry.Selected += PlayGameMenuEntrySelected;
            playSpaceMenuEntry.Selected += PlaySpaceSelected;
            playMine.Selected += PlayMineSelected;
            creditsMenuEntry.Selected += CreditsMenuEntrySelected;
            exitMenuEntry.Selected += ConfirmExitMessageBoxAccepted;

            MenuEntries.Add(playGameMenuEntry);
            MenuEntries.Add(playSpaceMenuEntry);
            MenuEntries.Add(playMine);
            MenuEntries.Add(creditsMenuEntry);
            MenuEntries.Add(exitMenuEntry);

            _paddlePlacement = new Vector2(width / 2 - 66, height - 100);
            _brickPlacement = new Vector2(width / 2 - 16, 0);
            _ballPlacement = new Vector2(width / 2 + 36, height - 135);
            _breakBallPlacement = new Vector2(width / 2 - 66, height - 135);
            System.Random rand = new System.Random();
        }

        private void PlayGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex, new Breakdown(_graphics), null);
        }

        private void PlaySpaceSelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex, new SpaceShooter(_graphics), null);
        }

        private void PlayMineSelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex, new MineBoss(_graphics), null);
        }

        private void CreditsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new CreditsScreen(), e.PlayerIndex);
        }

        public override void Activate()
        {
            if (_content == null)
                _content = new ContentManager(ScreenManager.Game.Services, "Content");

            _paddle = _content.Load<Texture2D>("Paddle");
            _brick = _content.Load<Texture2D>("Brick");
            _ball = _content.Load<Texture2D>("HitTest");
            _breakBall = _content.Load<Texture2D>("BreakBall.png");
            spriteFont = _content.Load<SpriteFont>("PublicPixel");
        }

        /*protected override void OnCancel(PlayerIndex playerIndex)
        {
            const string message = "Are you sure you want to exit this sample?";
            var confirmExitMessageBox = new MessageBoxScreen(message);

            confirmExitMessageBox.Accepted += ConfirmExitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmExitMessageBox, playerIndex);
        }*/

        private void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit();
        }

        /*public override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            /*stars = new StarSprite[]
            {
                new StarSprite(new Vector2((float)rand.NextDouble() * _graphics.GraphicsDevice.Viewport.Width - 32, (float)rand.NextDouble() * _graphics.GraphicsDevice.Viewport.Height - 32)),
                new StarSprite(new Vector2((float)rand.NextDouble() * _graphics.GraphicsDevice.Viewport.Width - 32, (float)rand.NextDouble() * _graphics.GraphicsDevice.Viewport.Height - 32)),
                new StarSprite(new Vector2((float)rand.NextDouble() * _graphics.GraphicsDevice.Viewport.Width - 32, (float)rand.NextDouble() * _graphics.GraphicsDevice.Viewport.Height - 32)),
                new StarSprite(new Vector2((float)rand.NextDouble() * _graphics.GraphicsDevice.Viewport.Width - 32, (float)rand.NextDouble() * _graphics.GraphicsDevice.Viewport.Height - 32)),
                new StarSprite(new Vector2((float)rand.NextDouble() * _graphics.GraphicsDevice.Viewport.Width - 32, (float)rand.NextDouble() * _graphics.GraphicsDevice.Viewport.Height - 32)),
                new StarSprite(new Vector2((float)rand.NextDouble() * _graphics.GraphicsDevice.Viewport.Width - 32, (float)rand.NextDouble() * _graphics.GraphicsDevice.Viewport.Height - 32)),
                new StarSprite(new Vector2((float)rand.NextDouble() * _graphics.GraphicsDevice.Viewport.Width - 32, (float)rand.NextDouble() * _graphics.GraphicsDevice.Viewport.Height - 32)),
                new StarSprite(new Vector2((float)rand.NextDouble() * _graphics.GraphicsDevice.Viewport.Width - 32, (float)rand.NextDouble() * _graphics.GraphicsDevice.Viewport.Height - 32)),
                new StarSprite(new Vector2((float)rand.NextDouble() * _graphics.GraphicsDevice.Viewport.Width - 32, (float)rand.NextDouble() * _graphics.GraphicsDevice.Viewport.Height - 32)),
                new StarSprite(new Vector2((float)rand.NextDouble() * _graphics.GraphicsDevice.Viewport.Width - 32, (float)rand.NextDouble() * _graphics.GraphicsDevice.Viewport.Height - 32)),
                new StarSprite(new Vector2((float)rand.NextDouble() * _graphics.GraphicsDevice.Viewport.Width - 32, (float)rand.NextDouble() * _graphics.GraphicsDevice.Viewport.Height - 32)),
                new StarSprite(new Vector2((float)rand.NextDouble() * _graphics.GraphicsDevice.Viewport.Width - 32, (float)rand.NextDouble() * _graphics.GraphicsDevice.Viewport.Height - 32)),
                new StarSprite(new Vector2((float)rand.NextDouble() * _graphics.GraphicsDevice.Viewport.Width - 32, (float)rand.NextDouble() * _graphics.GraphicsDevice.Viewport.Height - 32))
            };
            base.Initialize();
        }*/

        /*protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _paddle = Content.Load<Texture2D>("Paddle");
            _brick = Content.Load<Texture2D>("Brick");
            _ball = Content.Load<Texture2D>("HitTest");
            _breakBall = Content.Load<Texture2D>("BreakBall.png");
            spriteFont = Content.Load<SpriteFont>("PublicPixel");
            foreach (var star in stars)
            {
                star.LoadContent(Content);
            }
        }*/

        /*public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(_paddle, _paddlePlacement, Color.White);
            _spriteBatch.Draw(_brick, _brickPlacement, Color.White);
            _spriteBatch.Draw(_ball, _ballPlacement, Color.White);
            _spriteBatch.Draw(_breakBall, _breakBallPlacement, Color.White);

            _spriteBatch.DrawString(spriteFont, "Breakdown", new Vector2(_graphics.GraphicsDevice.Viewport.Width / 2 - 127, 100), Color.Gold);
            _spriteBatch.DrawString(spriteFont, "Press 'Esc' to exit the game", new Vector2(25, 200), Color.Gold);
            foreach (var star in stars)
            {
                star.Draw(gameTime, _spriteBatch);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }*/
    }
}
