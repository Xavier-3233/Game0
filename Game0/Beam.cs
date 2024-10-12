using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game0.Collision;

namespace Game0
{
    public class Beam
    {
        public Vector2 Position;

        public Vector2 Velocity;

        private Texture2D _texture;

        public bool IsActive;  // To check if the beam is still on screen or needs removal

        private BoundingRectangle bounds;

        public BoundingRectangle Bounds => bounds;

        public Beam(Texture2D texture, Vector2 position, Vector2 velocity)
        {
            _texture = texture;
            Position = position;
            Velocity = velocity;
            IsActive = true;
            bounds = new BoundingRectangle(Position, 20, 1);
        }

        public void Update(GameTime gameTime)
        {
            // Move the beam
            Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Position.Y > 800)  //800 is the screen
            {
                IsActive = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!IsActive)
            {
                return;
            }
            spriteBatch.Draw(_texture, Position, Color.White);
        }

        public bool CollidesWith(BoundingCircle c)
        {
            return CollisionHelper.Collides(c, this.bounds);
        }
    }
}
