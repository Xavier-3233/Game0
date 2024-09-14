using Game0.Collision;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Game0
{
    public class Paddle
    {

        private KeyboardState keyboardState;

        private Texture2D texture;

        public Color color;

        public Vector2 position;

        private BoundingRectangle bounds = new BoundingRectangle(new Vector2(200, 500-29), 132, 40);

        public BoundingRectangle Bounds => bounds;


        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Paddle");
        }

        public void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            if ((keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A)) && position.X > 0)
            {
                position += new Vector2(-2, 0);
            }
            if ((keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D)) && position.X + 132 < 480)
            {
                position += new Vector2(2, 0);
            }

           

            //Notes: put bounds here
            bounds.X = position.X;
            bounds.Y = position.Y;
            
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
