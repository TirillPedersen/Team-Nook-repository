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

    public MarketStand(float givenX, float givenY, string fileName, byte cols = 2, byte rows = 1) : base(fileName, cols, rows)
    {
        _position = new Vec2(givenX, givenY);
        SetXY(givenX, givenY);
        SetOrigin(width / 2, height / 2);
    }


    private void getMousePos()
    {
        _mouseX = Input.mouseX;
        _mouseY = Input.mouseY;
    }

    void mouseHover()
    {
        Vec2 hoverDistanceMouseCharacter = new Vec2((_mouseX + LevelLoader.character.CalculateCharacterOffset().x) - LevelLoader.character.Position.x, (_mouseY + LevelLoader.character.CalculateCharacterOffset().y) - LevelLoader.character.Position.y);

        if (_mouseX >= (x - LevelLoader.character.CalculateCharacterOffset().x) - width / 2 && _mouseX <= (x - LevelLoader.character.CalculateCharacterOffset().x) + width / 2
            && _mouseY >= (y - LevelLoader.character.CalculateCharacterOffset().y) - height / 2
            && _mouseY <= (y - LevelLoader.character.CalculateCharacterOffset().y) + height / 2 && hoverDistanceMouseCharacter.Length() < 300) SetFrame(1);
        else SetFrame(0);
    }

    protected void Update()
    {
        getMousePos();
        mouseHover();
    }
}
