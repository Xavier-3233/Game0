using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Game0.StateManagement;
using Game0.Collision;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System.Threading;
using Microsoft.Xna.Framework.Input;
using System.Collections;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Graphics.PackedVector;


namespace Game0.Screens
{
    public class MineBoss : GameScreen
    {
        private GraphicsDeviceManager _graphics;
        private ContentManager _content;
        private SpriteFont _spriteFont;
        private List<Meteor> _meteors;
        private StarShip _ship;
        private Random rand = new Random();
        private ParticleSystem _explosions;
        private readonly InputAction _pauseAction;
        private float _gametimer = 60f;
        //private float _elapsedTime;
        private bool isTimerActive = true;
        private float alpha = 255f;
        private float fadeSpeed = 1f;
        private float playerX;
        //private float offsetX;
        private Texture2D _background;
        Vector2 backgroundPosition1;
        Vector2 backgroundPosition2;
        float backgroundSpeed = 20f;
        private float meteorSpawnTimer;
        private float meteorSpawnInterval = 1f;
        private int score = 0;
        private const int METEOR_POINTS = 100;
        private TileMap _tilemap;

        public MineBoss(GraphicsDeviceManager graphics)
        {
            _graphics = graphics;

            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            _pauseAction = new InputAction(
                new[] { Buttons.Start, Buttons.Back },
                new[] { Keys.Back, Keys.Escape }, true);

            rand = new Random();




            //_meteors = new List<Meteor>();
            _tilemap = new TileMap("MineSweeper.tmj");

        }

        public override void Activate()
        {
            if (_content == null)
                _content = new ContentManager(ScreenManager.Game.Services, "Content");

            _spriteFont = _content.Load<SpriteFont>("PublicPixel");


            _tilemap.LoadContent(_content);
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //   Exit();

            // TODO: Add your update logic here
            base.Update(gameTime, otherScreenHasFocus, false);
            
            

        }

        

        public override void Draw(GameTime gameTime)
        {

            // TODO: Add your drawing code here

            _graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
            ScreenManager.SpriteBatch.Begin();



            _tilemap.DrawTileGrid(gameTime, ScreenManager.SpriteBatch, 680, 480);

            
            
            ScreenManager.SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
