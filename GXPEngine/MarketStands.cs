﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;


public class MarketStands : AnimationSprite
{
    // variables
    private float _mouseX;
    private float _mouseY;
    public Vec2 _position;

    public MarketStands(float givenX, float givenY) : base("vegetableFruitsStand.png", 2, 1)
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
        if (_mouseX >= x - width / 2 && _mouseX <= x + width / 2 && _mouseY >= y - height / 2 && _mouseY <= y + height / 2)
        {

            //Console.WriteLine("on");
            SetFrame(1);
            
        }
        else
        {
            //Console.WriteLine("off");
            SetFrame(0);
        }
    }

    void Update()
    {
        getMousePos();
        mouseHover();
    }
}
