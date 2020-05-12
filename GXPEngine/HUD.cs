using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class HUD : GameObject
    {
        private EasyDraw _proximityInfo;
        private bool _proximityGiven;
        private bool _textOnceAdded;

        public HUD()
        {
            //Proximity information
            _proximityInfo = new EasyDraw(1920, 1080, addCollider: false);
            _proximityInfo.SetColor(0, 0, 0);
            _proximityInfo.TextSize(24);
            _textOnceAdded = false;
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
            else if(!_proximityGiven)
            {
                RemoveChild(_proximityInfo);
                _textOnceAdded = false;
            }
        }

        protected void Update()
        {
            _proximityInfo.Clear(Color.Transparent);
            ShowProximityInfo();
        }
    }
}
