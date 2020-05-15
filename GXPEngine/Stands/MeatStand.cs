using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class MeatStand : MarketStand
    {
        private Sprite _buyMenu;
        private Sprite _buyMenu2;
        private Sprite _exitButton;
        private bool _menuShown;
        private EasyDraw _boughtItem;

        public MeatStand(float givenX, float givenY, float givenRotation) : base(givenX, givenY, "MeatStand.png", givenRotation)
        {
            _buyMenu = new Sprite("buyScreen.png");
            _buyMenu2 = new Sprite("buyScreen2.png");
            _exitButton = new Sprite("exitCross.png");
            _buyMenu.SetOrigin(_buyMenu.width / 2, _buyMenu.height / 2);
            _buyMenu.SetXY(game.width - _buyMenu.width + 150, game.height - _buyMenu.height + 150);
            _buyMenu2.SetOrigin(_buyMenu.width / 2, _buyMenu.height / 2);
            _buyMenu2.SetXY(game.width - _buyMenu.width + 150, game.height - _buyMenu.height + 150);
            _exitButton.SetXY(_buyMenu.x + 500, _buyMenu.y - 325);
            _menuShown = false;
            scale = 0.85f;

            _boughtItem = new EasyDraw(1920, 1080);
            _boughtItem.TextSize(16);
            _boughtItem.SetColor(0, 0, 0);

        }

        private void BuyMenu()
        {
            if (hoveringOverStand && Input.GetMouseButtonDown(0) && !_menuShown && !MarketStand.MenuCurrentlyOpened)
            {
                LevelLoader.hud.AddChild(_buyMenu);
                LevelLoader.hud.AddChild(_exitButton);
                _menuShown = true;
                MarketStand.MenuCurrentlyOpened = true;
            }


            else if (MarketStand.MenuCurrentlyOpened && _menuShown && Input.GetMouseButtonDown(0) && Input.mouseX >= 1010 && Input.mouseX <= 1375 && Input.mouseY >= 275 && Input.mouseY <= 800)
            {
                LevelLoader.hud.RemoveChild(_buyMenu);
                LevelLoader.hud.AddChild(_buyMenu2);
                LevelLoader.hud.AddChild(_exitButton);
                MarketStand.MenuCurrentlyOpened = false;
            }

            if (_menuShown && HUD.MenuHover(_exitButton) && Input.GetMouseButtonDown(0))
            {
                LevelLoader.hud.RemoveChild(_buyMenu);
                LevelLoader.hud.RemoveChild(_buyMenu2);
                LevelLoader.hud.RemoveChild(_exitButton);
                _menuShown = false;
                MarketStand.MenuCurrentlyOpened = false;
            }

            if (_menuShown && Input.GetMouseButtonDown(0) && Input.mouseX >= 600 && Input.mouseX <= 650 && Input.mouseY >= 380 && Input.mouseY <= 410)
            {
                _boughtItem.Text("5kg meat bought for 25$", 95, MarketStand.BuyShift);
                Console.WriteLine();
                LevelLoader.hud.AddChild(_boughtItem);
                MarketStand.BuyShift += 26;
            }
        }

        protected void Update()
        {
            getMousePos();
            mouseHover();
            BuyMenu();
        }
    }
}
