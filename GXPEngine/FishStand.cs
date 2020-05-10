using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;


public class FishStand : AnimationSprite
{
    // variables
    private float _mouseX;
    private float _mouseY;
    public Vec2 _position;

    public FishStand(float givenX, float givenY) : base("FishShop.png", 2, 1)
    {
        _position = new Vec2(givenX, givenY);
        SetXY(givenX, givenY);
        SetOrigin(width / 2, height / 2);
    }


    private void getMousePos()
    {
        _mouseX = Input.mouseX + LevelLoader.character.x;
        _mouseY = Input.mouseY + LevelLoader.character.y;
    }

    void mouseHover()
    {
        if (_mouseX >= x - width / 2 && _mouseX <= x + width / 2 && _mouseY >= y - height / 2 && _mouseY <= y + height / 2) SetFrame(1);
        else SetFrame(0);
    }

    protected void Update()
    {
        //getMousePos();
        mouseHover();
    }
}
