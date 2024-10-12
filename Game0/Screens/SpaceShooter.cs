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
    public class SpaceShooter : GameScreen
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

        public SpaceShooter(GraphicsDeviceManager graphics)
        {
            _graphics = graphics;

            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            _pauseAction = new InputAction(
                new[] { Buttons.Start, Buttons.Back },
                new[] { Keys.Back, Keys.Escape }, true);

            rand = new Random();



            backgroundPosition1 = new Vector2(0, 0);
            backgroundPosition2 = new Vector2(780, 0);
            _meteors = new List<Meteor>();
            
        }

        public override void Activate()
        {
            if (_content == null)
                _content = new ContentManager(ScreenManager.Game.Services, "Content");

            _spriteFont = _content.Load<SpriteFont>("PublicPixel");
            //_spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Texture2D tempTexture = _content.Load<Texture2D>("Beam");
            _ship = new StarShip(tempTexture)
            {
                position = new Vector2(200, 200)
            };
            _ship.LoadContent(_content);
            _spriteFont = _content.Load<SpriteFont>("PublicPixel");
            _background = _content.Load<Texture2D>("Space");
            _explosions = new ParticleSystem();
            _explosions.LoadContent(_content);


        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //   Exit();

            // TODO: Add your update logic here
            base.Update(gameTime, otherScreenHasFocus, false);
            if (isTimerActive)
            {
                _gametimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_gametimer <= 0)
                {
                    isTimerActive = false;
                }
                _ship.position.X = MathHelper.Clamp(_ship.position.X, 100, 700);
                playerX = _ship.position.X;

                // Calculate the scrolling offset based on the player's movement
                float offsetX = 1.5f * backgroundSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                // Scroll the backgrounds left
                backgroundPosition1.X -= offsetX;
                backgroundPosition2.X -= offsetX;

                //Moves background
                if (backgroundPosition1.X <= -_background.Width)
                {
                    backgroundPosition1.X = backgroundPosition2.X + _background.Width;
                }
                if (backgroundPosition2.X <= -_background.Width)
                {
                    backgroundPosition2.X = backgroundPosition1.X + _background.Width;
                }


                if (IsActive)
                {
                    if (alpha > 0)
                    {
                        alpha -= fadeSpeed;
                    }
                    else
                    {
                        alpha = 0;
                    }



                    _ship.Update(gameTime);
                    meteorSpawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (meteorSpawnTimer >= meteorSpawnInterval)
                    {
                        SpawnMeteor(); 
                        meteorSpawnTimer = 0f; 
                    }
                    foreach (var meteor in _meteors)
                    {
                        meteor.Update(gameTime);

                    }


                    foreach (var beam in _ship.GetBeams())
                    {
                        foreach (var meteor in _meteors)
                        {
                            if (beam.Bounds.CollidesWith(meteor.Bounds))
                            {
                                meteor.IsActive = false;
                                beam.IsActive = false;
                                for (int i = 0; i < 30; i++) 
                                {
                                    Vector2 velocity = new Vector2(
                                        RandomHelper.NextFloat(-25, 25), // Random horizontal velocity
                                        RandomHelper.NextFloat(-50, 0)  // Random vertical velocity
                                    );

                                    _explosions.AddParticle(meteor.Position, velocity, 1.0f, Color.White, RandomHelper.NextFloat()); 
                                }

                                score += METEOR_POINTS;
                            }
                        }
                    }
                }


                _explosions.Update(gameTime);
                

                _meteors.RemoveAll(m => !m.IsActive);





                //base.Update(gameTime);
            }

        }

        /// <summary>
        /// A special method to spawn in the meteors
        /// </summary>
        private void SpawnMeteor()
        {
            float randomY = (float)(rand.NextDouble() * 480);

            float spawnX = 800 + 30; 

            // Create the meteor at the random position
            Meteor newMeteor = new Meteor(new Vector2(spawnX, randomY));
            newMeteor.LoadContent(_content);

            _meteors.Add(newMeteor);
        }

        public override void Draw(GameTime gameTime)
        {

            // TODO: Add your drawing code here
            
            float playerX = MathHelper.Clamp(_ship.position.X, 300, 700);
            float offsetX = 300 - playerX;

            Matrix transform;

            // Background
            transform = Matrix.CreateTranslation(offsetX * 0.333f, 0, 0);
            ScreenManager.SpriteBatch.Begin(transformMatrix: transform);
            ScreenManager.SpriteBatch.Draw(_background, backgroundPosition1, Color.White);
            ScreenManager.SpriteBatch.Draw(_background, backgroundPosition2, Color.White);
            ScreenManager.SpriteBatch.End();

            
            ScreenManager.SpriteBatch.Begin();
            if (!isTimerActive) //When game ends
            {
                string finalScoreText = $"Final Score: {score}";
                ScreenManager.SpriteBatch.DrawString(_spriteFont, finalScoreText, new Vector2(200, 240), Color.Red);
            }
            string scoreText = $"Score: {score}"; 
            ScreenManager.SpriteBatch.DrawString(_spriteFont, scoreText, new Vector2(10, 30), Color.White);
            int minutes = (int)(_gametimer / 60);
            int seconds = (int)(_gametimer % 60);
            string timeText = $"Time Left: {minutes:D2}:{seconds:D2}"; 


            ScreenManager.SpriteBatch.DrawString(_spriteFont, timeText, new Vector2(10, 10), Color.White);



            _ship.Draw(gameTime, ScreenManager.SpriteBatch, 0.1f);

            _explosions.Draw(ScreenManager.SpriteBatch);

            
            





            if (alpha > 0)
            {
                Color fadeColor = new Color(255, 215, 0, (int)alpha);
                ScreenManager.SpriteBatch.DrawString(_spriteFont, "SpaceShooter", new Vector2(480 / 2 - 80, 100), fadeColor);
                ScreenManager.SpriteBatch.DrawString(_spriteFont, "Use up and down keys to move.", new Vector2(480 / 2 - 200, 200), fadeColor);
                ScreenManager.SpriteBatch.DrawString(_spriteFont, "Use space bar to shoot.", new Vector2(480 / 2 - 200, 250), fadeColor);

                //Used for testing purposes
               /* string heightText = $"Height: {_graphics.PreferredBackBufferHeight}";
                string widthText = $"Width: {_graphics.PreferredBackBufferWidth}";
                string positionX = $"X: {ship.position.X}";
                string positionY = $"Y: {ship.position.Y}";*/
                

            }
            foreach(var meteor in _meteors)
            {
                meteor.Draw(gameTime, ScreenManager.SpriteBatch);
            }
            ScreenManager.SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
