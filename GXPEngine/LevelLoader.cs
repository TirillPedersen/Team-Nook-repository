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
    public static List<MarketStands> MarketStandList = new List<MarketStands>();

    public LevelLoader(string mapName)
    {
        _mapData = MapParser.ReadMap(mapName);
        _tileWidth = (short)_mapData.TileWidth;
        _tileHeight = (short)_mapData.TileHeight;

        //Calls both methods to create tiles and objects
        this.LoadLevel(_mapData);
        this.LoadGameObjects(_mapData);

        //MarketStandList = new List<MarketStands>();
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
                        //Block block = new Block();
                        //block.x = column * _tileWidth;
                        //block.y = row * _tileHeight;
                        //AddChild(block);
                        break;

                    case 2:
                        //Ground ground = new Ground();
                        //ground.x = column * _tileWidth;
                        //ground.y = row * _tileHeight;
                        //AddChild(ground);
                        break;
                }
            }
        }
    }

    /*public void BlockRow(byte blockAmount, int blockPositionX, int blockPositionY, bool drawDirection)
    {
        _blockListContainer.Add(new List<Block>());

        for (int i = 0; i < blockAmount; i++)
        {
            if (drawDirection) _blockListContainer.ElementAt(_amountOfLists).Insert(i, new Block(blockPositionX + i * Block.BlockWidth, blockPositionY));
            else _blockListContainer.ElementAt(_amountOfLists).Insert(i, new Block((blockPositionX - Block.BlockWidth) - (i * Block.BlockWidth), blockPositionY));

            AddChild(_blockListContainer.ElementAt(_amountOfLists).ElementAt(i));
        }
        _amountOfLists++;
    }*/

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
                        MarketStands stand = new MarketStands(currentTiledObject.X, currentTiledObject.Y);
                        AddChild(stand);
                        MarketStandList.Add(stand);
                        break;

                        //case "GreenSpawnTube":
                        //    break;

                        //case "GreenPickupTube":
                        //    break;

                        //case "HUD":
                        //    break;
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