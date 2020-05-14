using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;


public class CheeseStand : MarketStand
{
    private Sprite _buyMenu;
    private bool _menuShown;

    public CheeseStand(float givenX, float givenY, float givenRotation = 0) : base(givenX, givenY, "cheeseStand.png", givenRotation)
    {
        _buyMenu = new Sprite("cheeseStandBuyScreen.png");
        _buyMenu.SetOrigin(_buyMenu.width / 2, _buyMenu.height / 2);
        _buyMenu.SetXY(game.width - _buyMenu.width , game.height - _buyMenu.height);
        _menuShown = false;
        scale = 0.85f;
    }

    private void BuyMenu()
    {
        if (hoveringOverStand && Input.GetMouseButtonDown(0) && !_menuShown && !MarketStand.MenuCurrentlyOpened)
        {
            LevelLoader.hud.AddChild(_buyMenu);
            _menuShown = true;
            MarketStand.MenuCurrentlyOpened = true;
        }
        else if(hoveringOverStand && _menuShown && Input.GetMouseButtonDown(0))
        {
            LevelLoader.hud.RemoveChild(_buyMenu);
            _menuShown = false;
            MarketStand.MenuCurrentlyOpened = false;
        }
    }

    protected void Update()
    {
        getMousePos();
        mouseHover();
        BuyMenu();
    }
}
