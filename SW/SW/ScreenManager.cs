using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace SW
{
    class ScreenManager
    {
        /// <summary>
        /// Graphic (S)caling factor based on the smalles screen dimension with a preffered smallest dimension of 1080
        /// </summary>
        public static float S;


        public static void CalculateGraphicScaling(GraphicsDeviceManager manager)
        {
            float width;
            float height;
            width = manager.PreferredBackBufferWidth;
            height = manager.PreferredBackBufferHeight;
            if (height > width)
            {
                S = width / 1080.0f;
            }
            else
            {
                S = height / 1080.0f;
            }
            
        }
    }
}
