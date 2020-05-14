using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Pigeon : AnimationSprite
{
    private short _currentlyUsedFrame, _frameCounter;
    float speed = 11.1f;

    public Pigeon(float givenX, float givenY, float givenRotation) : base("pigeon.png", 3, 2)
    {
        SetXY(givenX, givenY);
        SetOrigin(width / 2, height / 2);
        rotation = givenRotation;
    }

    private void ScarePigeon()
    {
        //if (x - LevelLoader.Character.Position.x => -100 && x - Player.X =< 0) || (x - Player.X => 100 && x - Player.X => 0)
        //{
        //    Move(speed, 0);
        //}
    }

    private void destroySelf() 
    {
        if (x < 0 || x > 1920 || y > 1080 || y < 0)
        {
            LateDestroy();
        }
    }


    protected void Update()
    {
        if (_currentlyUsedFrame >= 6) _currentlyUsedFrame = 0;
        currentFrame = _currentlyUsedFrame;

        if (_frameCounter == 1)
        {
            _currentlyUsedFrame++;
            _frameCounter = 0;
        }
        else _frameCounter++;

        ScarePigeon();
        destroySelf();
        Console.WriteLine(x);
    }
}

