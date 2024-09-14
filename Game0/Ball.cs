using Game0.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game0
{
    public class Ball //NOTE: USE THIS CLASS FOR BOUNDING PURPOSES!!!!! REPLACE WITH STUFF IN MAIN
    {

        /// <summary>
        /// Color of the ball
        /// </summary>
        public Color color;

        private Texture2D texture;

        public Vector2 Position;

        public Vector2 Velocity;

        // + new Vector2(8, 8), 8

        private BoundingCircle boundingCircle;

        public BoundingCircle Bounds => boundingCircle;

        public bool HitBottom { get; set; } = false;

        public Ball(Vector2 position)
        {
            this.boundingCircle = new BoundingCircle(position + new Vector2(10, 10), 10);
        }
        

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("BreakBall");
        }

        public void Update(GameTime gameTime)
        {
            boundingCircle.Center = Position + new Vector2(10, 10);
        }
        

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (HitBottom)
            {
                return;
            }
            var origin = new Vector2(texture.Width / 2f, texture.Height / 2f);
            
            spriteBatch.Draw(texture, Position + origin, null, Color.White, 0, origin, 1f, SpriteEffects.None, 0);
        }

        public bool CollidesWith(BoundingRectangle r)
        {
            return CollisionHelper.Collides(this.boundingCircle, r);
        }
    }
}
