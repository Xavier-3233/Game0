using Game0.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Game0.StateManagement;
using System;
using System.Threading;
using System.Transactions;
using Microsoft.Xna.Framework.Content;



namespace Game0.Screens
{
    public class Breakdown : GameScreen
    {
        private GraphicsDeviceManager _graphics;
        private ContentManager _content;
        //private SpriteBatch _spriteBatch;
        private SpriteFont _spriteFont;
        private Ball ball;
        private SoundEffect _brickhit;
        private Song _song;
        private readonly InputAction _pauseAction;

        /// <summary>
        /// Collection of bricks to place at the top
        /// </summary>
        private Brick[] bricks;

        /// <summary>
        /// Paddle to hit the ball
        /// </summary>
        private Paddle paddle;

        /// <summary>
        /// An int to track the amount of bricks left
        /// </summary>
        private int bricksLeft;

        /// <summary>
        /// Alpha to control the opacity of text
        /// </summary>
        private float alpha = 255f;

        /// <summary>
        /// How fast the text fades
        /// </summary>
        private float fadeSpeed = 1f;

        /// <summary>
        /// The star sprites
        /// </summary>
        private StarSprite[] stars;

        /// <summary>
        /// A timer for the star animations
        /// </summary>
        private double currentTime;

        private int height = 600;

        private int width = 484;






        public Breakdown(GraphicsDeviceManager graphics)
        {
            _graphics = graphics;

            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            _pauseAction = new InputAction(
                new[] { Buttons.Start, Buttons.Back },
                new[] { Keys.Back, Keys.Escape }, true);

            Random rand = new Random();
            //Changes the screen proportions 
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.PreferredBackBufferWidth = 484;
            _graphics.ApplyChanges();



            #region Ball
            Vector2 positionBall = new Vector2(width / 2 - 16, height / 2 - 16);

            ball = new Ball(positionBall);

            ball.Position.X = width / 2 - 16;
            ball.Position.Y = height / 2 - 16;
            ball.Velocity = new Vector2((float)rand.NextDouble() * 2 -1, 1.0f);
            if (ball.Velocity.X == 0) ball.Velocity.X = 1;
            ball.Velocity.Normalize();
            ball.Velocity *= 100;
            #endregion
            paddle = new Paddle()
            {
                position = new Vector2(200, 500),

            };

            #region BrickSection
            Vector2 position = new Vector2(22, 11);
            bricks = new Brick[33];
            Color newColor = Color.Red;
            bool flip = false;
            for (int i = 0; i < 33; i++) //This section is for creating the bricks
            {
                if (i > 1 && bricks[i - 1].position.X + 22 >= width)
                {
                    position.Y += 22;
                    newColor = Color.Green;
                    position.X = width - 23;
                    flip = true;
                }
                else if (i > 1 && bricks[i - 1].position.X - 22 <= 0)
                {
                    position.Y += 22;
                    newColor = Color.Blue;
                    position.X = 23;
                    flip = false;
                }
                Brick brick = new Brick(position) { color = newColor };
                bricks[i] = brick;
                if (flip)
                {
                    position.X -= 44;
                }
                else
                {
                    position.X += 44;
                }


            }
            #endregion
            #region Stars
            stars = new StarSprite[]
            {
                new StarSprite(new Vector2((float)rand.NextDouble() * width - 32, (float)rand.NextDouble() * height - 32)),
                new StarSprite(new Vector2((float)rand.NextDouble() * width - 32, (float)rand.NextDouble() * height - 32)),
                new StarSprite(new Vector2((float)rand.NextDouble() * width - 32, (float)rand.NextDouble() * height - 32)),
                new StarSprite(new Vector2((float)rand.NextDouble() * width - 32, (float)rand.NextDouble() * height - 32)),
                new StarSprite(new Vector2((float)rand.NextDouble() * width - 32, (float)rand.NextDouble() * height - 32))
            };
            #endregion
            bricksLeft = bricks.Length;
        }

        /*protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            Random rand = new Random();
            //Changes the screen proportions 
            _graphics.PreferredBackBufferHeight = 640;
            _graphics.PreferredBackBufferWidth = 480;
            _graphics.ApplyChanges();

            #region Ball
            Vector2 positionBall = new Vector2(GraphicsDevice.Viewport.Width / 2 - 16, GraphicsDevice.Viewport.Height / 2 - 16);

            ball = new Ball(positionBall);

            ball.Position.X = GraphicsDevice.Viewport.Width / 2 - 16;
            ball.Position.Y = GraphicsDevice.Viewport.Height / 2 - 16;
            ball.Velocity = new Vector2((float)rand.NextDouble(), 1.0f);
            if (ball.Velocity.X == 0) ball.Velocity.X = 1;
            ball.Velocity.Normalize();
            ball.Velocity *= 100;
            #endregion
            paddle = new Paddle()
            {
                position = new Vector2(200, 500),

            };

            #region BrickSection
            Vector2 position = new Vector2(22, 11);
            bricks = new Brick[33];
            Color newColor = Color.Red;
            bool flip = false;
            for (int i = 0; i < 33; i++) //This section is for creating the bricks
            {
                if (i > 1 && bricks[i - 1].position.X + 20 >= GraphicsDevice.Viewport.Width)
                {
                    position.Y += 22;
                    newColor = Color.Green;
                    position.X = GraphicsDevice.Viewport.Width - 22;
                    flip = true;
                }
                else if (i > 1 && bricks[i - 1].position.X - 20 <= 0)
                {
                    position.Y += 22;
                    newColor = Color.Blue;
                    position.X = 22;
                    flip = false;
                }
                Brick brick = new Brick(position) { color = newColor };
                bricks[i] = brick;
                if (flip)
                {
                    position.X -= 44;
                }
                else
                {
                    position.X += 44;
                }


            }
            #endregion
            #region Stars
            stars = new StarSprite[]
            {
                new StarSprite(new Vector2((float)rand.NextDouble() * GraphicsDevice.Viewport.Width - 32, (float)rand.NextDouble() * GraphicsDevice.Viewport.Height - 32)),
                new StarSprite(new Vector2((float)rand.NextDouble() * GraphicsDevice.Viewport.Width - 32, (float)rand.NextDouble() * GraphicsDevice.Viewport.Height - 32)),
                new StarSprite(new Vector2((float)rand.NextDouble() * GraphicsDevice.Viewport.Width - 32, (float)rand.NextDouble() * GraphicsDevice.Viewport.Height - 32)),
                new StarSprite(new Vector2((float)rand.NextDouble() * GraphicsDevice.Viewport.Width - 32, (float)rand.NextDouble() * GraphicsDevice.Viewport.Height - 32)),
                new StarSprite(new Vector2((float)rand.NextDouble() * GraphicsDevice.Viewport.Width - 32, (float)rand.NextDouble() * GraphicsDevice.Viewport.Height - 32))
            };
            #endregion
            bricksLeft = bricks.Length;

            base.Initialize();
        }*/

        public override void Activate()
        {
            if (_content == null)
                _content = new ContentManager(ScreenManager.Game.Services, "Content");

            _spriteFont = _content.Load<SpriteFont>("PublicPixel");
            //_spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ball.LoadContent(_content);
            foreach (var brick in bricks) brick.LoadContent(_content);
            paddle.LoadContent(_content);
            _spriteFont = _content.Load<SpriteFont>("PublicPixel");
            _brickhit = _content.Load<SoundEffect>("BrickHit2");
            _song = _content.Load<Song>("Jesse Spillane - Sleepy");
            foreach (var star in stars)
            {
                star.LoadContent(_content);
            }

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(_song);
        }

        /*protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ball.LoadContent(_content);
            foreach (var brick in bricks) brick.LoadContent(_content);
            paddle.LoadContent(_content);
            _spriteFont = _content.Load<SpriteFont>("PublicPixel");
            _brickhit = _content.Load<SoundEffect>("BrickHit2");
            _song = _content.Load<Song>("Jesse Spillane - Sleepy");
            foreach (var star in stars)
            {
                star.LoadContent(_content);
            }

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(_song);
        }*/

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //   Exit();

            // TODO: Add your update logic here
            base.Update(gameTime, otherScreenHasFocus, false);

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

                currentTime += gameTime.ElapsedGameTime.TotalSeconds;

                paddle.Update(gameTime);
                ball.Position += ball.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                ball.Update(gameTime);

                if (ball.Position.X < 0 || ball.Position.X >= width - 32)
                {
                    ball.Velocity.X *= -1;
                }

                if (ball.Position.Y < 0)
                {
                    ball.Velocity.Y *= -1;
                }

                if (ball.Position.Y > height - 32)
                {
                    ball.HitBottom = true;
                }

                foreach (var brick in bricks)
                {
                    if (!brick.Hit && brick.Bounds.CollidesWith(ball.Bounds))
                    {
                        brick.Hit = true;
                        _brickhit.Play();
                        ball.Velocity.Y *= (float)-1.1; //This is to speed up the ball as the game goes along
                        bricksLeft--;
                        foreach (var star in stars)
                        {
                            star.Go = true;
                        }
                    }
                }

                if (ball.Bounds.CollidesWith(paddle.Bounds))
                {
                    ball.Velocity.Y *= -1;
                }
            }







            //base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here





            
            ScreenManager.SpriteBatch.Begin();
            ball.Draw(gameTime, ScreenManager.SpriteBatch);
            if (bricksLeft == 0)
            {
                ball.HitBottom = true;
                ScreenManager.SpriteBatch.DrawString(_spriteFont, "You Win!", new Vector2(480 / 2 - 60, 100), Color.Gold);
                ScreenManager.SpriteBatch.DrawString(_spriteFont, "Press esc to exit", new Vector2(480 / 2 - 120, 200), Color.Gold);
                foreach (var star in stars)
                {
                    star.Draw(gameTime, ScreenManager.SpriteBatch);
                    star.Go = true;
                }
            }

            if (ball.HitBottom && bricksLeft != 0)
            {
                ScreenManager.SpriteBatch.DrawString(_spriteFont, "You Lose", new Vector2(ScreenManager.GraphicsDevice.Viewport.Width / 2 - 60, 100), Color.Gold);
                ScreenManager.SpriteBatch.DrawString(_spriteFont, "Press esc to exit", new Vector2(ScreenManager.GraphicsDevice.Viewport.Width / 2 - 120, 200), Color.Gold);
            }

            if (currentTime >= 3)
            {
                foreach (var star in stars)
                {
                    star.Go = false;

                }
                currentTime = 0;
            }
            else
            {
                foreach (var star in stars)
                {
                    if (star.Go)
                    {
                        star.Draw(gameTime, ScreenManager.SpriteBatch);
                    }
                }
            }
            foreach (Brick brick in bricks)
            {
                brick.Draw(gameTime, ScreenManager.SpriteBatch);
            }

            if (alpha > 0)
            {
                Color fadeColor = new Color(255, 215, 0, (int)alpha);
                ScreenManager.SpriteBatch.DrawString(_spriteFont, "Breakdown", new Vector2(480 / 2 - 80, 100), fadeColor);
                ScreenManager.SpriteBatch.DrawString(_spriteFont, "Use <- and -> keys to move", new Vector2(480 / 2 - 200, 200), fadeColor);


            }
            paddle.Draw(gameTime, ScreenManager.SpriteBatch);
            ScreenManager.SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
