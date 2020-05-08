using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;


class Fonty : AnimationSprite
{
private int _text;
    public Fonty(int xVar, int yVar, int textVar, float scaleVar = 1.0f, uint colorVar = 0xFFFFFF) : base("Font.png", 26, 3)
    {
        x = xVar;
        y = yVar;

        _text = textVar;
        printText();

        scale = scaleVar;
        color = colorVar;
    }

    //=======================================================================================
    //                                    >  print Text  <
    //=======================================================================================
    //Sets the frames for each letter, so it prints out correctly
    private void printText()
    {

        //Capitals
        if (_text > 64 && _text < 91) { SetFrame(_text - 65); }

        //Lower Case
        else if (_text > 96 && _text < 123) { SetFrame(_text - 97 + 26); }

        //Numbers
        else if (_text > 47 && _text < 58) { SetFrame(_text - 48 + 52); }

        //space
        else if (_text == 32) { SetFrame(77); }

        //Make the rest transparent
        else { alpha = 0; }
    }

    void Update()
    {
    }
}
