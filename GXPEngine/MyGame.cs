using System;									// System contains a lot of default C# libraries 
using System.Drawing;                           // System.Drawing contains a library used for canvas drawing below
using GXPEngine;								// GXPEngine contains the engine

public class MyGame : Game
{
    // Create a window that's 1056, 800 / No fullscreen / No vsync
    public MyGame() : base(1056, 800, false, false)
    {
        //Set FPS to 60 for consistency
        targetFps = 60;

        //MarketStands marketStands = new MarketStands(450, 450);
        //AddChild(marketStands);

        LevelLoader levelLoader = new LevelLoader("MarketMap.tmx");
        AddChild(levelLoader);

    }
    static void Main()                          // Main() is the first method that's called when the program is run
    {
        new MyGame().Start();                   // Create a "MyGame" and start it
    }
}