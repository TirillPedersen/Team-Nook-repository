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


    public MarketStands(int x, int y) :  base("squares.png", 2, 1)
    {
        SetXY(x, y);
    }


    private void getMousePos()
    {
        mouseX = Input.mouseX;
        mouseY = Input.mouseY;
    }

    //void mouseHover()
    //{
    //    if (mouseX == x && mouseY == y)
    //    {
    //        Console.WriteLine("yaay");
    //    }
    //}

    void Update()
    {
        //void mouseHover();
        getMousePos();
        if (mouseX <= x-10 && mouseX >= x + 10 && mouseY <= y - 10 && mouseY >= y + 10)
        {
            Console.WriteLine("on");
        }
        else
        {
            Console.WriteLine("off");
        }
    }
        //for for for loops
        //ctor for constructor
        //cw for console writeline

        //int frame = _currentFrame + 1;
        //if (frame >= _frames) frame = 0;
        //SetFrame(frame);
}

