using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GXPEngine;


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
        if (_mouseX >= (x - LevelLoader.character.CalculateCharacterOffset().x) - width / 2 && _mouseX <= (x - LevelLoader.character.CalculateCharacterOffset().x) + width / 2 && _mouseY >= (y - LevelLoader.character.CalculateCharacterOffset().y) - height / 2 && _mouseY <= (y - LevelLoader.character.CalculateCharacterOffset().y) + height / 2) SetFrame(1);
        else SetFrame(0);
    }

    protected void Update()
    {
        getMousePos();
        mouseHover();
    }
}
