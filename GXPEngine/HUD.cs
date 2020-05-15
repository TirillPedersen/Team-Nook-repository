using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private EasyDraw _rectProximityInfo;
        private bool _proximityGiven;
        private bool _textOnceAdded;
        private Sprite _shoppingList;
        private Sprite _clockBackground;
        private Sprite _settingsButton;
        private Sprite _chatBox;
        PrivateFontCollection PFC;
        private Font _ownFontClock;
        private Font _ownFontText;
        private Sprite tutorial;
     

        private Sound _backgroundMusic;
        private SoundChannel _backgroundSC;

        public HUD()
        {
            //Font
            PFC = new PrivateFontCollection();
            PFC.AddFontFile(@"kenyan_coffee_rg.ttf");
            _ownFontClock = new Font(PFC.Families[0], 32);
            _ownFontText = new Font(PFC.Families[0], 32);

            //Proximity information
            _rectProximityInfo = new EasyDraw(1920, 1080, addCollider: false);
            _rectProximityInfo.alpha = 0.9f;
            _rectProximityInfo.color = 0x262626;
            AddChild(_rectProximityInfo);

            _proximityInfo = new EasyDraw(1920, 1080, addCollider: false);
            _proximityInfo.color = 0x56b25d;
            _proximityInfo.TextFont(_ownFontText);

            _textOnceAdded = false;

            //Clock Background
            _clockBackground = new Sprite("timeBackground.png");
            _clockBackground.SetXY(5, 15);
            AddChild(_clockBackground);

            //Clock
            _clock = new EasyDraw(1920, 1080, addCollider: false);
            _clock.color = 0x56b25d;
            _clock.TextFont(_ownFontClock);
            AddChild(_clock);

            //Shopping list
            _shoppingList = new Sprite("List.png");
            _shoppingList.scale = 0.65f;
            _shoppingList.SetXY(_shoppingList.width / 2.5f, 300);
            _shoppingList.SetOrigin(_shoppingList.width / 2, _shoppingList.height / 2);
            AddChild(_shoppingList);

            //Settings button
            _settingsButton = new Sprite("settingsButton.png");
            _settingsButton.SetXY(225, 13);
            AddChild(_settingsButton);

            //Chatbox
            _chatBox = new Sprite("chatBox.png");
            _chatBox.SetOrigin(_chatBox.width / 2, _chatBox.height / 2);
            _chatBox.SetXY(game.width/2, game.height - 50);
            AddChild(_chatBox);

            //Sound
            _backgroundMusic = new Sound("backgroundSounds.mp3", true, true);
            _backgroundSC = _backgroundMusic.Play();
            _backgroundSC.Volume = 0.35f;

            //Tutorial
            tutorial = new Sprite("Tutorial.png");
            tutorial.SetOrigin(tutorial.width/2, tutorial.height/2);
            tutorial.SetXY(game.width / 2, game.height / 2);
            AddChild(tutorial);
        }

        public void ShowProximityInfo()
        {
            _proximityGiven = false;
            _proximityInfo.Text("Walk towards the stand to interact!", Input.mouseX - 250, Input.mouseY);
            _rectProximityInfo.Rect(Input.mouseX - 15, Input.mouseY - 30, 475, 50);

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
            _clock.Text(DateTime.Now.ToShortTimeString(), 75, 87);
        }

        public static bool MenuHover(Sprite UIElement)
        {
            if (Input.mouseX >= UIElement.x && Input.mouseX <= UIElement.x + UIElement.width && Input.mouseY >= UIElement.y && Input.mouseY <= UIElement.y + UIElement.height) return true;
            else return false;
        }

        protected void Update()
        {
            _rectProximityInfo.Clear(Color.Transparent);
            _proximityInfo.Clear(Color.Transparent);
            _clock.Clear(Color.Transparent);
            ShowProximityInfo();
            drawClock();
            //PlayMusic();
            if (Input.GetMouseButtonDown(0)) RemoveChild(tutorial);
        }
    }
}
