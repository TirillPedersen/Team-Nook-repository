using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class VegetableFruitStand : MarketStand
    {
        private Sprite _buyMenu;
        private bool _menuShown;

        public VegetableFruitStand(float givenX, float givenY, float givenRotation = 0) : base(givenX, givenY, "VegetableFruitsStand.png", givenRotation)
        {
            _buyMenu = new Sprite("vegetableBuyScreen.png");
            _buyMenu.SetOrigin(_buyMenu.width / 2, _buyMenu.height / 2);
            _buyMenu.SetXY(game.width - _buyMenu.width, game.height - _buyMenu.height);

            _menuShown = false;
        }

        private void BuyMenu()
        {
            if (hoveringOverStand && Input.GetMouseButtonDown(0) && !_menuShown)
            {
                LevelLoader.hud.AddChild(_buyMenu);
                _menuShown = true;
            }
            else if (hoveringOverStand && _menuShown && Input.GetMouseButtonDown(0))
            {
                LevelLoader.hud.RemoveChild(_buyMenu);
                _menuShown = false;
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
