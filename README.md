# Twizzle-Game-Engine
A C# game engine similar to the [OLC game engine](https://github.com/OneLoneCoder/videos/blob/master/olcConsoleGameEngine.h).

This project makes use of [RawConsoleInput](https://github.com/zolrath/RawConsoleInput).

Be aware that this project is still under developement.

# Tutorial
To begin using this engine add TGE.dll under your projects dependencies.
```c#
    using System;
    using TGE;
    
    class Program
    {
        static void Main(string[] args)
        {
            //Create a new Instance of your game
            MyGame game = new MyGame();
            //Start the game by initializing a new display and specifying the type of display,
            //font to use, width and height as well as font width and font height.
            //Currently only console displays are supported.
            game.Run(new Display(DisplayType.Console, "terminal", 160, 124, 8, 8));
        }
    }
    //Create class deriving from TGE.Game and generate overrides
    class MyGame : Game
    {
        public override void Initialize()
        {

        }
        public override void Start()
        {
        
        }
        public override void Update()
        {

        }
        public override void LateUpdate()
        {
        
        }
        public override void Draw()
        {
        
        }
    }
```

# Getting input
```c#
public override void Update()
{
    //Currently only console input is supported.
    MouseInputEvent mouse = ConsoleInput.GetMouse();
    if (mouse.IsLeftClick)
    {
        //Do something
    }
    //GetKey lets you see if a key was pressed this frame, is held this frame or was released this frame
    if (ConsoleInput.GetKey('W').Pressed)
    {
        //Do something
    }
}
```

# Drawing
```c#
public override void Draw()
{
    //This will clear the screenbuffer.
    Screen.Clear();
    
    //Draws text to the screenbuffer at a given x and y position with a specific color.
    Screen.Draw("Test", 0, 0, 15);
    
    //Draws a filled Circle with a specific char and radius to the screenbuffer.
    Screen.FillCircle('█', 43, 54, 10, 15);
    
    //Draws a line between two points to the screenbuffer.
    Screen.DrawLine('-', 23, 12, 32, 43, 15);
    
    //Displays everything that was drawn to the screen since the last Clear() .
    Screen.Print();
}
```


