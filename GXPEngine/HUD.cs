using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using TiledMapParser;

namespace GXPEngine
{
    class HUD : GameObject
    {
        private EasyDraw _proximityInfo;
        private EasyDraw _clock;
        private EasyDraw _newsText;
        private EasyDraw _rectClock;
        private EasyDraw _rectProximityInfo;
        private EasyDraw _rectLogo;
        private EasyDraw _rectNews;
        private bool _proximityGiven;
        private bool _textOnceAdded;
        private Sprite _shoppingList;
        private Sprite _logo;
        
        //private Font _ownFont = new Font("‪C:\\Users\\Mauri\\Desktop\\Team-Nook-repository\\GXPEngine\bin\\Debug\\kenyan_coffee_rg.ttf", 1);

        //Font
        PrivateFontCollection pfc;

        public HUD()
        {
            //Proximity information
            _rectProximityInfo = new EasyDraw(1920, 1080, addCollider: false);
            _rectProximityInfo.alpha = 0.9f;
            _rectProximityInfo.color = 0x262626;
            AddChild(_rectProximityInfo);

            _proximityInfo = new EasyDraw(1920, 1080, addCollider: false);
            _proximityInfo.color = 0x56b25d;
            _proximityInfo.TextSize(24);
            _proximityInfo.TextFont("C:\\Users\\Mauri\\Desktop\\Team-Nook-repository\\GXPEngine\\bin\\Debug\\kenyan_coffee_rg.ttf", 24);

            _textOnceAdded = false;

            _rectClock = new EasyDraw(1920, 1080, addCollider: false);
            _rectClock.Rect(game.width - 90, 85, 150, 100);
            _rectClock.alpha = 0.9f;
            _rectClock.color = 0x262626;
            AddChild(_rectClock);

            //Clock
            _clock = new EasyDraw(1920, 1080, addCollider: false);
            _clock.color = 0x56b25d;
            _clock.TextSize(40);
            _clock.TextFont("C:\\Users\\Mauri\\Desktop\\Team - Nook - repository\\GXPEngine\\bin\\Debug\\kenyan_coffee_rg.ttf", 40);
            AddChild(_clock);

            //Shopping list
            _shoppingList = new Sprite("List.png");
            _shoppingList.scale = 0.65f;
            _shoppingList.SetXY(game.width - _shoppingList.width + 60, 225);
            _shoppingList.SetOrigin(_shoppingList.width / 2, _shoppingList.height / 2);
            AddChild(_shoppingList);


            //Rect for logo
            _rectLogo = new EasyDraw(1920, 1080, addCollider: false);
            _rectLogo.Rect(95, 85, 150, 100);
            _rectLogo.SetOrigin(0, 0);
            _rectLogo.alpha = 0.9f;
            _rectLogo.color = 0x262626;
            AddChild(_rectLogo);
    
            //Logo
            _logo = new Sprite("Logo.png");
            _logo.scale = 0.15f;
            _logo.SetXY(_logo.width - 50, _logo.height - 50);
            AddChild(_logo);

            //Rect for news
            _rectNews = new EasyDraw(1920, 1080, addCollider: false);
            _rectNews.Rect(920, 85, 1475, 100);
            _rectNews.SetOrigin(0, 0);
            _rectNews.alpha = 0.9f;
            _rectNews.color = 0x262626;
            AddChild(_rectNews);

            //News text
            _newsText = new EasyDraw(1920, 1080, addCollider: false);
            _newsText.color = 0x56b25d;
            _newsText.TextSize(40);
            _newsText.Text("Important news will be displayed here!", 215, 120);
            AddChild(_newsText);

            //Load font
            pfc = new PrivateFontCollection();
            //pfc.AddFontFile("‪C:\\Users\\Mauri\\Desktop\\Team-Nook-repository\\GXPEngine\bin\\Debug\\kenyan_coffee_rg.ttf");

        }

        public void ShowProximityInfo()
        {
            _proximityGiven = false;
            _proximityInfo.Text("Walk towards the stand to interact!", Input.mouseX - 250, Input.mouseY);
            _rectProximityInfo.Rect(Input.mouseX, Input.mouseY - 20, 550, 50);

            foreach (AnimationSprite currentStand in LevelLoader.CollisionObjectList)
            {
                if (currentStand is MarketStand)
                {
                    MarketStand stand = currentStand as MarketStand;
                    if (stand.ProximityToStand)
                    {
                        _proximityGiven = true;
                    }
                }
            }

            if (_proximityGiven && !_textOnceAdded)
            {
                AddChild(_rectProximityInfo);
                AddChild(_proximityInfo);
                _textOnceAdded = true;
            }
            else if (!_proximityGiven)
            {
                RemoveChild(_proximityInfo);
                RemoveChild(_rectProximityInfo);
                _textOnceAdded = false;
            }
        }

        private void drawClock()
        {
            _clock.Text(DateTime.Now.ToShortTimeString(), game.width - 165, 125);
        }

        private void MenuHover()
        {
            if (Input.mouseX >= _shoppingList.x - _shoppingList.width / 2 && Input.mouseX <= _shoppingList.x + _shoppingList.width / 2 && Input.mouseY >= _shoppingList.y - _shoppingList.height / 2 && Input.mouseY <= _shoppingList.y + _shoppingList.height / 2) Console.WriteLine("Hovering over basket");
        }

        protected void Update()
        {
            _rectProximityInfo.Clear(Color.Transparent);
            _proximityInfo.Clear(Color.Transparent);
            _clock.Clear(Color.Transparent);
            ShowProximityInfo();
            drawClock();
            MenuHover();
        }
    }
}
