using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SW
{
    public class FlameParticleEngine
    {
        private Random random;
        public Vector2 EmitterLocation { get; set; }
        private List<FlameParticle> particles;
        private List<Texture2D> textures;

        public bool emit = false;

        public FlameParticleEngine(List<Texture2D> textures, Vector2 location)
        {
            EmitterLocation = location;
            this.textures = textures;
            this.particles = new List<FlameParticle>();
            random = new Random();
        }

        private FlameParticle GenerateNewParticle(float forceAngle)
        {
            Texture2D texture = textures[random.Next(textures.Count)];
            Vector2 position = EmitterLocation;
            //Vector2 velocity = new Vector2(
            //        100.0f * (float)(random.NextDouble() * 2 - 1),
            //        100.0f * (float)(random.NextDouble() * 2 - 1));

            //Vector2 velocity = new Vector2(
            //        (100.0f * (float)Math.Sin(forceAngle) * ((float)random.NextDouble() * 2.0f - 1.0f)),
            //        (100.0f * (float)Math.Cos(forceAngle) * ((float)random.NextDouble() * 2.0f - 1.0f)));

            Vector2 velocity = new Vector2(-200.0f * (float)Math.Cos(forceAngle) + (50.0f * (float)random.NextDouble() * 2.0f - 1.0f) ,
                                           -200.0f * (float)Math.Sin(forceAngle) + (50.0f * (float)random.NextDouble() * 2.0f - 1.0f));

            float angle = 0;
            float angularVelocity = 0.1f * (float)(random.NextDouble() * 2 - 1);
            //Color color = new Color(
            //        (float)random.NextDouble(),
            //        (float)random.NextDouble(),
            //        (float)random.NextDouble());
            //Color color = new Color(1.0f, 1.0f, 0.0f); // yellow
            Color color = new Color(1.0f, 1.0f, 1.0f);
            float size = (float)random.NextDouble() * 0.5f;
            //int ttl = 20 + random.Next(40);
            float ttl = 0.5f; //+ (float)random.Next(100)/100.0f;

            return new FlameParticle(texture, position, velocity, angle, angularVelocity, color, size, ttl);
        }

        public void Update(float elapsed, float forceAngle)
        {
            if (emit)
            {
                int total = 10;

                for (int i = 0; i < total; i++)
                {
                    particles.Add(GenerateNewParticle(forceAngle));
                }
            }

            for (int particle = 0; particle < particles.Count; particle++)
            {
                if (particles[particle].Col.G >= 0)
                particles[particle].Update(elapsed);
                if (particles[particle].TTL <= 0)
                {
                    particles.RemoveAt(particle);
                    particle--;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Begin();
            for (int index = 0; index < particles.Count; index++)
            {
                particles[index].Draw(spriteBatch);
            }
            //spriteBatch.End();
        }
        


    }
}
