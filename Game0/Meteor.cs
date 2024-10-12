using Game0.Collision;
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
    public class Meteor
    {
        private const float ANIMATION_SPEED = 0.1f;

        private double animationTimer;

        private int animationFrame;

        private Texture2D texture;

        public Vector2 Position;

        public Vector2 Velocity;

        public bool IsActive = true;

        private BoundingCircle boundingCircle;

        public BoundingCircle Bounds => boundingCircle;

        public bool HitLeft { get; set; } = false;

        public Meteor(Vector2 position)
        {
            Position = position;
            Velocity = new Vector2(200, 0);
            this.boundingCircle = new BoundingCircle(position + new Vector2(7.5f, 7.5f), 7.5f);
        }


        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Meteor");
        }

        public void Update(GameTime gameTime)
        {
            Position.X -= Velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
            boundingCircle.Center = Position + new Vector2(7.5f, 7.5f);
        }


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!IsActive)
            {
                return;
            }
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;



            if (animationTimer > ANIMATION_SPEED)
            {
                animationFrame++;
                if (animationFrame >= 9) animationFrame = 0;
                animationTimer -= ANIMATION_SPEED;
            }
            int currentRow = animationFrame / 3;
            int currentColumn = animationFrame % 3;

            var source = new Rectangle(currentColumn * 32, currentRow * 32, 32, 32);
            spriteBatch.Draw(texture, Position, source, Color.White);

        }

        public bool CollidesWith(BoundingRectangle r)
        {
            return CollisionHelper.Collides(this.boundingCircle, r);
        }
    }
}
