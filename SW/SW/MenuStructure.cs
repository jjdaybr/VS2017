using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SW
{
    public class MenuStructure
    {
        private SpriteFont font;
        public string Title;
        public string Close;
        public List<string> MenuEntries;
        public Vector2 Origin;

        public MenuStructure(Vector2 origin)
        {
            MenuEntries = new List<string>();
            Origin = origin;
            Title = "Menu";
            Close = "Close";
        }


        public void LoadContent(ContentManager manager)
        {
            font = manager.Load<SpriteFont>("menu");
        }

        public void Draw(SpriteBatch batch)
        {
            Color drawColor;
            Vector2 textDimensions;
            Vector2 drawLocation;
            Rectangle textRectangle;
            Rectangle mouseRectangle;
            int verticalOffset = 0;
            batch.DrawString(font, Title, (Origin + new Vector2(0, verticalOffset)) * ScreenManager.S, Color.White, 0.0f, Vector2.Zero, ScreenManager.S, SpriteEffects.None, 1.0f);
            verticalOffset = verticalOffset + font.LineSpacing;
            Console.WriteLine(">>");
            foreach (string s in MenuEntries)
            {
                drawColor = Color.White;
                textDimensions = font.MeasureString(s) * ScreenManager.S;
                drawLocation = (Origin + new Vector2(0, verticalOffset)) * ScreenManager.S;
                textRectangle = new Rectangle((int)drawLocation.X,
                                              (int)drawLocation.Y,
                                              (int)textDimensions.X, (int)textDimensions.Y);
                mouseRectangle = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1);
                if (textRectangle.Intersects(mouseRectangle))
                    drawColor = Color.Red;
                batch.DrawString(font, s, drawLocation, drawColor, 0.0f, Vector2.Zero, ScreenManager.S, SpriteEffects.None, 1.0f);
                verticalOffset = verticalOffset + font.LineSpacing;
            }
            textDimensions = font.MeasureString(Close) * ScreenManager.S;
            drawLocation = (Origin + new Vector2(0, verticalOffset)) * ScreenManager.S;
            textRectangle = new Rectangle((int)drawLocation.X,
                                                    (int)drawLocation.Y,
                                                    (int)textDimensions.X, (int)textDimensions.Y);
            mouseRectangle = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1);
            drawColor = Color.White;
            if (textRectangle.Intersects(mouseRectangle))
                drawColor = Color.Red;
            batch.DrawString(font, Close, (Origin + new Vector2(0, verticalOffset)) * ScreenManager.S, drawColor, 0.0f, Vector2.Zero, ScreenManager.S, SpriteEffects.None, 1.0f);
            verticalOffset = verticalOffset + font.LineSpacing;
        }

    }
}
