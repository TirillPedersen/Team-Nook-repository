using System;									// System contains a lot of default C# libraries 
using System.Drawing;                           // System.Drawing contains a library used for canvas drawing below
using GXPEngine;								// GXPEngine contains the engine

public class MyGame : Game
{
    // Create a window that's 1056, 800 / No fullscreen / No vsync

    public MyGame() : base(1920, 1080, false, false)
    {
        //Set FPS to 60 for consistency
        targetFps = 60;

        RenderMain = false;

        Ground ground = new Ground(0, 0);
        AddChild(ground);

        //Fonty font = new Fonty(500, 500, 10);
        //AddChild(font);

        LevelLoader levelLoader = new LevelLoader("MarketMap.tmx");
        AddChild(levelLoader);
    }
    static void Main()                          // Main() is the first method that's called when the program is run
    {
        new MyGame().Start();                   // Create a "MyGame" and start it
    }
}