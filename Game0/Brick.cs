using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game0.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game0
{
    public class Brick
    {
        private Texture2D texture;

        public Color color = Color.White;

        public Vector2 position;

        private BoundingRectangle bounds;

        public BoundingRectangle Bounds => bounds;

        /// <summary>
        /// Determines if the brick has been hit
        /// </summary>
        public bool Hit { get; set; } = false;

        public Brick(Vector2 position)
        {
            this.position = position;

            float scaledWidth = 44;  
            float scaledHeight = 44; 

            // Adjust bounds to match the top-left corner based on center-origin drawing
            Vector2 topLeft = new Vector2(position.X - scaledWidth / 2, position.Y - scaledHeight / 2);

            this.bounds = new BoundingRectangle(topLeft, scaledWidth, scaledHeight);
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Brick");
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Hit) return;
            var origin = new Vector2(texture.Width / 2f, texture.Height / 2f);
            spriteBatch.Draw(texture, position, null, color, 0, origin, 2, SpriteEffects.None, 0);
            
        }

        public bool CollidesWith(BoundingCircle c)
        {
            return CollisionHelper.Collides(c, this.bounds);
        }
    }
}
