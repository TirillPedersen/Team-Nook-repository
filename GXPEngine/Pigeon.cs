using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Pigeon : AnimationSprite
{
    private short _currentlyUsedFrame, _frameCounter;
    float speed = 15.0f;
    private bool AllowPigeonsToSpawn;
    private Vec2 PigToPlayer;

    private Sound _flySound;
    private SoundChannel _flySC;

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
        if (DistanceToPigeons.Length() < 150 && AllowPigeonsToSpawn)
        {
            AllowPigeonsToSpawn = false;
            PigToPlayer = new Vec2(x - LevelLoader.Character.Position.x, y - LevelLoader.Character.Position.y);
            PigToPlayer.Normalize();
            PigToPlayer = PigToPlayer * speed;

            _flySound = new Sound("pigeon.mp3", false, false);
            _flySC = _flySound.Play();
            _flySC.Volume = 0.25f;
        }

        if (DistanceToPigeons.Length() > 1000 && !AllowPigeonsToSpawn) LateDestroy();
        else if (DistanceToPigeons.Length() < 1000 && !AllowPigeonsToSpawn)
        {
            x += PigToPlayer.x;
            y += PigToPlayer.y;
            rotation = PigToPlayer.GetAngleDegrees();
          
      
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
    }
}

