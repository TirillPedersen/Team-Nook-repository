using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GXPEngine;

//this is the superclass for all stands

public class MarketStand : AnimationSprite
{
    // variables
    private float _mouseX;
    private float _mouseY;
    public Vec2 _position;

    public MarketStand(float givenX, float givenY, string fileName, int givenRotation = 0, byte cols = 2, byte rows = 1) : base(fileName, cols, rows)
    {
        _position = new Vec2(givenX, givenY);
        SetXY(givenX, givenY);
        SetOrigin(width / 2, height / 2);
        rotation = givenRotation;
    }


    private void getMousePos()
    {
        _mouseX = LevelLoader.Character.CharacterOffset.x +((Input.mouseX - game.width / 2) + LevelLoader.Character.CalculateCharacterOffset().x);
        _mouseY = LevelLoader.Character.CharacterOffset.y +((Input.mouseY - game.height / 2) + LevelLoader.Character.CalculateCharacterOffset().y);
    }

    void mouseHover()
    {
        Vec2 hoverDistanceMouseCharacter = new Vec2(_mouseX - LevelLoader.Character.Position.x, _mouseY - LevelLoader.Character.Position.y);

        if (_mouseX >= x - width / 2
            && _mouseX <= x + width / 2
            && _mouseY >= y - height / 2
            && _mouseY <= y + height / 2
            && hoverDistanceMouseCharacter.Length() < 450) SetFrame(1);
        else SetFrame(0);
    }

    protected void Update()
    {
        getMousePos();
        mouseHover();
    }
}
