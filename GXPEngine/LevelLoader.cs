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
    public static List<MarketStand> MarketStandList = new List<MarketStand>();
    public static List<NPC> NPCList = new List<NPC>();
    public static Character character;

    public LevelLoader(string mapName)
    {
        _mapData = MapParser.ReadMap(mapName);
        _tileWidth = (short)_mapData.TileWidth;
        _tileHeight = (short)_mapData.TileHeight;

        //Calls both methods to create tiles and objects
        this.LoadLevel(_mapData);
        this.LoadGameObjects(_mapData);
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
                    case "MarketStand":
                        VegetableFruitStand fruitStand = new VegetableFruitStand(currentTiledObject.X, currentTiledObject.Y);
                        AddChild(fruitStand);
                        MarketStandList.Add(fruitStand);
                        break;

                    case "FishStand":
                        FishStand fishStand = new FishStand(currentTiledObject.X, currentTiledObject.Y);
                        AddChild(fishStand);
                        MarketStandList.Add(fishStand);
                        break;

                    case "CheeseStand":
                        CheeseStand cheeseStand = new CheeseStand(currentTiledObject.X, currentTiledObject.Y);
                        AddChild(cheeseStand);
                        MarketStandList.Add(cheeseStand);
                        break;

                    case "MeatStand":
                        MeatStand meatStand = new MeatStand(currentTiledObject.X, currentTiledObject.Y);
                        AddChild(meatStand);
                        MarketStandList.Add(meatStand);
                        break;

                    case "NPC":
                        NPC npc = new NPC(currentTiledObject.X, currentTiledObject.Y);
                        AddChild(npc);
                        NPCList.Add(npc);
                        break;

                    case "Character":
                        character = new Character(currentTiledObject.X, currentTiledObject.Y);
                        AddChild(character);
                        Console.WriteLine("Executed");
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
