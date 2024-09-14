using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game0
{
    public class StarSprite
    {

        private const float ANIMATION_SPEED = 0.1f;

        private double animationTimer;

        private int animationFrame;

        private Vector2 position;

        private Texture2D texture;

        /// <summary>
        /// This is to determine when the star animation should play
        /// </summary>
        public bool Go { get; set; } = false;

        public StarSprite(Vector2 position)
        {
            this.position = position;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("StarMan");
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;



            if (animationTimer > ANIMATION_SPEED)
            {
                animationFrame++;
                if (animationFrame > 13) animationFrame = 0;
                animationTimer -= ANIMATION_SPEED;
            }
            int currentRow = animationFrame / 4;
            int currentColumn = animationFrame % 4;

            var source = new Rectangle(currentColumn * 32, currentRow * 32, 32, 32);
            spriteBatch.Draw(texture, position, source, Color.White);
        }
    }
}
