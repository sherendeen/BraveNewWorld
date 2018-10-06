using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BraveNewWorld
{
    public enum TileType
    {
       Unknown,//Mostly serves as a place-holder
       Grass,
       Water,
       Forest,
       Mountain,
       Hill,
       Desert,
       Tundra,//unused
       Swamp,
       //We were using this for settlements but then
       //made SettlementRed and SettlementBlue to
       //provide a visual cue
        Settlement,
       SettlementRed,//exist for presentation purposes
       SettlementBlue//exist for presentation purposes
    }
    public class TileHelper
    {
        private bool hasMapAlreadyBeenGenerated = false;
        //The possible terrains that a newly generated map may possess.
        private TileType[] tiles = { TileType.Grass, TileType.Water, TileType.Forest,
            TileType.Mountain, TileType.Hill, TileType.Desert,/* TileType.Tundra,*/ TileType.Swamp};
        private TileType[,] exampleTileMap;
        
        /// <summary>
        /// This so-called example tilemap was supposed to be for testing
        /// but is now holding the actual generated map.
        /// </summary>
        public TileType[,] ExampleTileMap
        {
            get
            {
                return this.exampleTileMap;
            }
            set
            {
                this.exampleTileMap = value;
            }
        }

        /// <summary>
        /// Changes a given tile at a specified coordinate.
        /// You MUST redraw the map after doing this
        /// </summary>
        /// <param name="newTile"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public void ChangeTile(TileType newTile, int row, int col)
        {
            this.exampleTileMap[row, col] = newTile;
        }

        /// <summary>
        /// Gets a tile
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public TileType GetTile(int row, int col)
        {
            TileType tile = TileType.Unknown;
            try
            {
                tile = exampleTileMap[row, col];
            }
            catch(IndexOutOfRangeException ioore)
            {
                Console.WriteLine(Environment.NewLine + "Index Out Of Range Exception caught in GetTile.\n Likely cause was that the map was not yet generated. " + Environment.NewLine);
                Console.WriteLine(ioore.StackTrace.ToString());
            }
            return tile;
        }
        
        /// <summary>
        /// Empty default constructor, per the
        /// Microsoft C# Style Guide
        /// </summary>
        public TileHelper()
        {

        }

        /// <summary>
        /// Generates a tile map
        /// </summary>
        /// <param name="width">the max width of the map</param>
        /// <param name="height">the max height of the map</param>
        public void GenerateTileMap(int width, int height)
        {

            //I do not remember what this was for
            string test = "Hello World";
            test = test.Replace(" ","");
            //end of that weird stuff
            
            if (!hasMapAlreadyBeenGenerated)
            {
                try
                {
                    Random rnd = new Random();
                    exampleTileMap = new TileType[width, height];

                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < height; j++)
                        {
                            //Thread.Sleep(100);
                            exampleTileMap[i, j] = tiles[rnd.Next(tiles.Length)];

                        }

                    }
                }
                catch (IndexOutOfRangeException ioore)
                {
                    Console.WriteLine();
                    Console.WriteLine(ioore.StackTrace);
                    Console.WriteLine(ioore.Source);
                }
            }
            else
            {
                //to my knowledge, this error only ever appears when you have 
                //the cursor over the map just as it 
                //is generating for the first time. You would have to have it
                //over the map just before the program starts. It is actually
                //a frequent occurence
                Console.WriteLine("Map did NOT generate because it had already happened before!!!");
            }
            this.hasMapAlreadyBeenGenerated = true;//Do not regenerate map again!
        }
        
    }

}
