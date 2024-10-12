using Game0.Collision;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game0
{
    public class StarShip
    {
        private KeyboardState keyboardState;

        private Texture2D texture;

        private Texture2D beamTexture;

        public Color color;

        public Vector2 position;

        private List<Beam> beams;

        private float beamSpeed = 500f;

        double timeSinceLastShot = 0;
        const double shotCooldown = 0.25; 

        public List<Beam> GetBeams()
        {
            return beams;
        }

        

        public StarShip(Texture2D beamText)
        {
            beams = new List<Beam>();
            beamTexture = beamText;
        }


        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Ship");
            
        }

        public void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            timeSinceLastShot += gameTime.ElapsedGameTime.TotalSeconds;

            if ((keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W)) && position.Y > 0)
            {
                position += new Vector2(0, -2);
            }
            if ((keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S)) && position.Y < 480 - 34)
            {
                position += new Vector2(0, 2);
            }
            if ((keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A)) && position.X > 0)
            {
                position += new Vector2(-2, 0);
            }
            if ((keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D)))
            {
                position += new Vector2(2, 0);
            }
            if (keyboardState.IsKeyDown(Keys.Space) && timeSinceLastShot >= shotCooldown)
            {
                Shoot();
                timeSinceLastShot = 0; // Reset cooldown
            }
            foreach (var beam in beams)
            {
                beam.Update(gameTime);
            }
            beams.RemoveAll(b => !b.IsActive);
        }

        private void Shoot()
        {
            Vector2 beamPosition = position + new Vector2(30, 8.5f);
            Vector2 beamVelocity = new Vector2(beamSpeed, 0);
            beams.Add(new Beam(beamTexture, beamPosition, beamVelocity));
            
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, float layerDepth)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, layerDepth);
            foreach (var beam in beams)
            {
                beam.Draw(spriteBatch);
            }
        }
    }
}
