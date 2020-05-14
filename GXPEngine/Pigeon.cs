using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Pigeon : AnimationSprite
{
    private short _currentlyUsedFrame, _frameCounter;
    float speed = 11.1f;
    private bool AllowPigeonsToSpawn;

    public Pigeon(float givenX, float givenY, float givenRotation) : base("pigeon.png", 3, 2)
    {
        SetXY(givenX, givenY);
        SetOrigin(width / 2, height / 2);
        rotation = givenRotation;
        AllowPigeonsToSpawn = true;
    }

    private void ScarePigeon()
    {
        Vec2 DistanceToPigeons = new Vec2(x - LevelLoader.Character.Position.x, y - LevelLoader.Character.Position.y);
        if (DistanceToPigeons.Length() < 100)
        {
            AllowPigeonsToSpawn = false;
            Move(speed, 0);
        }
    }

    private void destroySelf()
    {
        if (x < LevelLoader.Character.Position.x - game.width / 2 || x > LevelLoader.Character.Position.x + game.width / 2 || y > LevelLoader.Character.Position.y - game.height / 2 || y < LevelLoader.Character.Position.y + game.height / 2)
        {
            Console.WriteLine(x);
            if(!AllowPigeonsToSpawn) LateDestroy();
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
    }
}

