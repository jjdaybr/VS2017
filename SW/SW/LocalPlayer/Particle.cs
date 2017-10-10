using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SW
{
    public class FlameParticle
    {
        public Texture2D Texture { get; set; }        // The texture that will be drawn to represent the particle
        public Vector2 Position { get; set; }        // The current position of the particle        
        public Vector2 Velocity { get; set; }        // The speed of the particle at the current instance
        public float Angle { get; set; }            // The current angle of rotation of the particle
        public float AngularVelocity { get; set; }    // The speed that the angle is changing
        public Color Col { get; set; }            // The color of the particle
        public float Size { get; set; }                // The size of the particle
        public float TTL { get; set; }
        public float TTLStart { get; set; }
        //public float ColVelocity { get; set; }

        public FlameParticle(Texture2D texture, Vector2 position, Vector2 velocity,
            float angle, float angularVelocity, Color color, float size, float ttl)
        {
            Texture = texture;
            Position = position;
            Velocity = velocity;
            Angle = angle;
            AngularVelocity = angularVelocity;
            Col = color;
            Size = size;
            TTL = ttl;
            //ColVelocity = colVel;
            TTLStart = ttl;
        }

        public void Update(float elapsed)
        {
            TTL = TTL - elapsed;
            Position += Velocity * elapsed;
            Angle += AngularVelocity * elapsed;
            Vector4 c = Col.ToVector4();
            c.Z = (TTL / (TTLStart * 0.90f));
            c.Y = (TTL / (TTLStart * 0.50f));
            c.X = (TTL / (TTLStart * 0.10f));

            //if (TTL > TTLStart * 0.75f)

            //if (c.Z > 0)
            //    c.Z = c.Z - ColVelocity * elapsed;
            //else if (c.Y > 0)
            //    c.Y = c.Y - ColVelocity * elapsed;
            Col = new Color(c);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
            Vector2 origin = new Vector2(Texture.Width / 2, Texture.Height / 2);

            spriteBatch.Draw(Texture, Position * ScreenManager.S, sourceRectangle, Col,
                Angle, origin, Size * ScreenManager.S, SpriteEffects.None, 0f);
        }


    }
}
