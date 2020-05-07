using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;


public class MarketStands : AnimationSprite
{
    // variables
    private float mouseX;
    private float mouseY;


    public MarketStands(float givenX, float givenY) : base("vegetableFruitsStand.png", 1, 1)
    {
        SetXY(givenX, givenY);
        SetOrigin(width / 2, height / 2);
    }


    private void getMousePos()
    {
        mouseX = Input.mouseX;
        mouseY = Input.mouseY;
    }

    void mouseHover()
    {
        if (mouseX >= x - width / 2 && mouseX <= x + width / 2 && mouseY >= y - height / 2 && mouseY <= y + height / 2)
        {
            Console.WriteLine("on");
        }
        else
        {
            Console.WriteLine("off");
        }
    }

    void Update()
    {
        getMousePos();
        mouseHover();
    }
    //for for for loops
    //ctor for constructor
    //cw for console writeline

    //int frame = _currentFrame + 1;
    //if (frame >= _frames) frame = 0;
    //SetFrame(frame);
}

