# Twizzle-Game-Engine
A C# game engine similar to the OLC game engine


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
