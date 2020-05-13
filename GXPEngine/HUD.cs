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
        private bool _proximityGiven;
        private bool _textOnceAdded;
        private Sprite map;
        private Sprite basket;

        public HUD()
        {
            //Proximity information
            _proximityInfo = new EasyDraw(1920, 1080, addCollider: false);
            _proximityInfo.SetColor(0, 0, 0);
            _proximityInfo.TextSize(24);
            _proximityInfo.TextFont("Bahnschrift", 24);

            _textOnceAdded = false;

            _clock = new EasyDraw(1920, 1080, addCollider: false);
            _clock.SetColor(0, 0, 0);
            _clock.TextSize(40);
            _clock.TextFont("Bahnschrift", 40);
            AddChild(_clock);

            map = new Sprite("square.png");
            map.SetXY(game.width - map.width - 50, 800);
            map.SetOrigin(map.width / 2, map.height / 2);
            AddChild(map);

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
                AddChild(_proximityInfo);
                _textOnceAdded = true;
            }
            else if (!_proximityGiven)
            {
                RemoveChild(_proximityInfo);
                _textOnceAdded = false;
            }
        }

        private void drawClock()
        {
            _clock.Text(DateTime.Now.ToShortTimeString(), game.width - 150, 125);
        }

        private void MenuHover()
        {
            if (Input.mouseX >= map.x - map.width / 2 && Input.mouseX <= map.x + map.width / 2 && Input.mouseY >= map.y - map.height / 2 && Input.mouseY <= map.y + map.height / 2) Console.WriteLine("Hovering over map");
            else if (Input.mouseX >= basket.x - basket.width / 2 && Input.mouseX <= basket.x + basket.width / 2 && Input.mouseY >= basket.y - basket.height / 2 && Input.mouseY <= basket.y + basket.height / 2) Console.WriteLine("Hovering over basket");
        }

        protected void Update()
        {
            _proximityInfo.Clear(Color.Transparent);
            _clock.Clear(Color.Transparent);
            ShowProximityInfo();
            drawClock();
            MenuHover();
        }
    }
}
