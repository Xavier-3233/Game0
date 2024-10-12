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

    public class ParticleSystem
    {
        private List<Particle> particles = new List<Particle>();
        private Texture2D texture;

        public void LoadContent(ContentManager content)
        {

            texture = content.Load<Texture2D>("Explosion"); 
        }

        public void AddParticle(Vector2 position, Vector2 velocity, float lifetime, Color color, float rotation)
        {
            particles.Add(new Particle(position, velocity, lifetime, color, rotation));
        }

        public void Update(GameTime gameTime)
        {
            // Update particles and remove those that are no longer alive
            for (int i = particles.Count - 1; i >= 0; i--)
            {
                particles[i].Update(gameTime);
                if (!particles[i].IsAlive)
                {
                    particles.RemoveAt(i);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var particle in particles)
            {
                float alpha = particle.Lifetime / particle.TotalLifetime;
                Color particleColor = new Color(particle.Color, alpha); // Fade out based on lifetime
                spriteBatch.Draw(texture, particle.Position, null, particleColor, particle.Rotation, Vector2.Zero, 0.02f, SpriteEffects.None, 0f);
            }
        }
    }
}