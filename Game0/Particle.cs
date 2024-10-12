using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game0
{

    public class Particle
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public float Lifetime;
        public float TotalLifetime;
        public Color Color;
        public float Rotation;

        public Particle(Vector2 position, Vector2 velocity, float lifetime, Color color, float rotation)
        {
            Position = position;
            Velocity = velocity;
            Lifetime = lifetime;
            TotalLifetime = lifetime;
            Color = color;
            Rotation = rotation;
        }

        public void Update(GameTime gameTime)
        {
            Position += Velocity;

            Rotation += 10f;

            Lifetime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public bool IsAlive => Lifetime > 0;
    }
}
