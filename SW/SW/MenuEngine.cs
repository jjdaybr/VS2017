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
    public class MenuEngine
    {
        private bool isVisible;
        private List<MenuStructure> menuList;
        private int menuIndex;
        public MenuEngine()
        {
            menuList = new List<MenuStructure>();
            isVisible = false;
            menuIndex = -1;
        }
        
        public void Initialize()
        {
            menuList = new List<MenuStructure>();
            MenuStructure ms = new MenuStructure(new Vector2(0, 0));
            ms.Title = "Main Menu";
            ms.MenuEntries.Add("Start Game");
            ms.Close = "Quit";
            menuList.Add(ms);
            menuIndex = 0;
        }

        public void LoadContent(ContentManager content)
        {
            foreach(MenuStructure m in menuList)
            {
                m.LoadContent(content);
            }
        }

        public void Draw(SpriteBatch batch)
        {
            if(isVisible)
            {
                menuList[menuIndex].Draw(batch);
            }
        }

        public void Show()
        {
            isVisible = true;
        }

        public void Hide()
        {
            isVisible = false;
        }
    }
}
