using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TiledMapParser;
using GXPEngine;

class LevelLoader : GameObject
{
    ////Initialization
    private static short _currentTile = 0;
    private short _tileWidth, _tileHeight;
    private Map _mapData;
    private short[,] _tileData;
    //Game specific
    public static List<AnimationSprite> CollisionObjectList = new List<AnimationSprite>();
    public static List<NPC> NPCList = new List<NPC>();
    public static Character Character;
    public static HUD hud = new HUD();

    public LevelLoader(string mapName)
    {
        _mapData = MapParser.ReadMap(mapName);
        _tileWidth = (short)_mapData.TileWidth;
        _tileHeight = (short)_mapData.TileHeight;

        //Calls both methods to create tiles and objects
        this.LoadLevel(_mapData);
        this.LoadGameObjects(_mapData);

        AddChild(hud);
    }

    //All the tiles get created
    private void LoadLevel(Map _mapData)
    {
        //In case no tile-layer was created within Tiled
        if (_mapData.Layers == null) throw new System.SystemException("There is no tile-layer!");

        Layer mainLayer = _mapData.Layers[0];

        _tileData = mainLayer.GetTileArray();

        for (short row = 0; row < mainLayer.Width; row++)
        {
            for (short column = 0; column < mainLayer.Height; column++)
            {
                _currentTile = _tileData[column, row];

                switch (_currentTile)
                {
                    case 1:
                        Ground ground = new Ground(column * _tileWidth, row * _tileHeight);
                        AddChild(ground);
                        break;
                }
            }
        }
    }

    //All objects get created
    private void LoadGameObjects(Map _mapData)
    {
        //The designer might just want to see tile-layers only, hence the exception gets caught and informs the designer
        try
        {
            ObjectGroup objectLayer = _mapData.ObjectGroups[0];

            foreach (TiledObject currentTiledObject in objectLayer.Objects)
            {
                switch (currentTiledObject.Name)
                {
                    case "FruitStand":
                        VegetableFruitStand fruitStand = new VegetableFruitStand(currentTiledObject.X, currentTiledObject.Y, currentTiledObject.Rotation);
                        AddChild(fruitStand);
                        CollisionObjectList.Add(fruitStand);
                        break;

                    case "FishStand":
                        FishStand fishStand = new FishStand(currentTiledObject.X, currentTiledObject.Y, currentTiledObject.Rotation);
                        AddChild(fishStand);
                        CollisionObjectList.Add(fishStand);
                        break;

                    case "CheeseStand":
                        CheeseStand cheeseStand = new CheeseStand(currentTiledObject.X, currentTiledObject.Y, currentTiledObject.Rotation);
                        AddChild(cheeseStand);
                        CollisionObjectList.Add(cheeseStand);
                        break;

                    case "MeatStand":
                        MeatStand meatStand = new MeatStand(currentTiledObject.X, currentTiledObject.Y, currentTiledObject.Rotation);
                        AddChild(meatStand);
                        CollisionObjectList.Add(meatStand);
                        break;

                    case "NPC":
                        NPC npc = new NPC(currentTiledObject.X, currentTiledObject.Y);
                        AddChild(npc);
                        CollisionObjectList.Add(npc);
                        NPCList.Add(npc);
                        break;

                    case "Character":
                        Character = new Character(currentTiledObject.X, currentTiledObject.Y, _mapData.Width * _mapData.TileWidth, _mapData.Height * _mapData.TileHeight);
                        AddChild(Character);
                        break;

                    case "tableWithBenches":
                        TableWithBenches twb = new TableWithBenches(currentTiledObject.X, currentTiledObject.Y, currentTiledObject.Rotation);
                        AddChild(twb);
                        CollisionObjectList.Add(twb);
                        break;

                    case "Carousel":
                        Carousel carousel = new Carousel(currentTiledObject.X, currentTiledObject.Y, currentTiledObject.Rotation);
                        AddChild(carousel);
                        CollisionObjectList.Add(carousel);
                        break;
                }
            }
        }
        catch (NullReferenceException e) { Console.WriteLine("Keep in mind: No object-layer used!" + e); }
    }

    //Used to reset game - not needed for now!
    //private void Reset()
    //{
    //    foreach (GameObject currentGameObject in game.GetChildren())
    //    {
    //        if (currentGameObject is Shellcreeper) currentGameObject.LateDestroy();
    //    }

    //    foreach (GreenSpawnTube currentGreenSpawnTube in GreenSpawnTubes)
    //    {
    //        currentGreenSpawnTube.ResetSpawnTube();
    //    }

    //    HUD.ResetHUD();
    //    mario.ResetCharacter();

    //}

    //protected void Update()
    //{
    //    if (Input.GetKeyDown(Key.BACKSPACE)) this.Reset();
    //}
}
