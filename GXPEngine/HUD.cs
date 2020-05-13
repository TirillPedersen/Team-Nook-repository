using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TiledMapParser;

namespace GXPEngine
{
    class HUD : GameObject
    {
        private EasyDraw _proximityInfo;
        private EasyDraw _clock;
        private EasyDraw _rectClock;
        private EasyDraw _rectProximityInfo;
        private bool _proximityGiven;
        private bool _textOnceAdded;
        private Sprite basket;

        public HUD()
        {
            //Proximity information
            _rectProximityInfo = new EasyDraw(1920, 1080, addCollider: false);
            _rectProximityInfo.alpha = 0.9f;
            _rectProximityInfo.color = 0x333333;
            AddChild(_rectProximityInfo);

            _proximityInfo = new EasyDraw(1920, 1080, addCollider: false);
            _proximityInfo.color = 0x45884A;
            _proximityInfo.TextSize(24);
            _proximityInfo.TextFont("C:\\Users\\Mauri\\Desktop\\Team-Nook-repository\\GXPEngine\\bin\\Debug\\kenyan_coffee_rg.ttf", 24);

            _textOnceAdded = false;

            _rectClock = new EasyDraw(1920, 1080, addCollider: false);
            _rectClock.Rect(game.width - 85, 90, 150, 100);
            _rectClock.alpha = 0.9f;
            _rectClock.color = 0x333333;
            AddChild(_rectClock);

            //Clock
            _clock = new EasyDraw(1920, 1080, addCollider: false);
            _clock.color = 0x45884A;
            _clock.TextSize(40);
            _clock.TextFont("C:\\Users\\Mauri\\Desktop\\Team - Nook - repository\\GXPEngine\\bin\\Debug\\kenyan_coffee_rg.ttf", 40);
            AddChild(_clock);


            basket = new Sprite("List.png");
            basket.scale = 0.65f;
            basket.SetXY(game.width - basket.width + 60, 225);
            basket.SetOrigin(basket.width / 2, basket.height / 2);
            AddChild(basket);
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
            _clock.Text(DateTime.Now.ToShortTimeString(), game.width - 160, 125);
        }

        private void MenuHover()
        {
            if (Input.mouseX >= basket.x - basket.width / 2 && Input.mouseX <= basket.x + basket.width / 2 && Input.mouseY >= basket.y - basket.height / 2 && Input.mouseY <= basket.y + basket.height / 2) Console.WriteLine("Hovering over basket");
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
