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
    public class CowManager
    {
        private Texture2D texture;

        private class CowObject
        {
            public Vector2 location;
            public Color color;
            public float rotation;
            public float velocity;
            public float accelaration;
        }

        public CowManager()
        {
            
        }

        public void Initialize()
        {

        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("LocalPlayer\\cow");
        }

        public void Update(float elapesed)
        {

        }

        public void Draw(SpriteBatch batch)
        {

        }

    }
}
