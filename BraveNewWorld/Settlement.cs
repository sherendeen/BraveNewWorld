using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BraveNewWorld
{
    public class Settlement
    {
        public struct CoordinateSet
        {
            public int x, y;
        }

        // Declares the TileType array 'adjacentTiles' to hold what types of tiles are adjacent to the settlement.
        private TileType[,] adjacentTiles;

        public CoordinateSet coordinates;
        private string settlementName = "";
        public string SettlementName
        {
            get
            {
                return this.settlementName;
            }
            set
            {
                this.settlementName = value;
            }
        }

        public TileType[,] AdjacentTiles
        {
            get
            {
                return this.adjacentTiles;
            }

            set
            {
                this.adjacentTiles = value;
            }
        }

        public Settlement()
        {

        }

        public void SetAdjacentTiles(Player playerObj)
        {
            //Create a two dimensional array of 3 and 3
            this.adjacentTiles = new TileType[3, 3];
            

            //Cycle through the array
            for(int i = this.coordinates.x-1; i < this.coordinates.x+1; i++)
            {
                for(int j = this.coordinates.y+1; j < this.coordinates.y-1; j++)
                {

                    if (i > -1 && i < 29 && j > -1 && j < 39)
                    {
                        //The current adjacent tile is equal to the one from the map
                        //in the same location
                        adjacentTiles[i, j] = new TileHelper().GetTile(i, j);

                        // Increments the appropriate resources by the appropriate amounts based off
                        // what type of tile the current index of the TileHelper 'adjacentTiles' is
                        // equivalent to.
                        switch (adjacentTiles[i,j])
                        {
                            case TileType.Grass:
                                playerObj.Resources[0] += 3;//3 food yield
                                break;
                            case TileType.Forest:
                                playerObj.Resources[0] += 1;//3 food yield
                                playerObj.Resources[1] += 3;
                                break;
                            case TileType.Desert:
                                playerObj.Resources[5] += 1;
                                playerObj.Resources[9] += 2;
                                break;

                            case TileType.Hill:
                                playerObj.Resources[0] += 1;//3 food yield
                                playerObj.Resources[3] += 2;
                                playerObj.Resources[6] += 2;
                                playerObj.Resources[8] += 1;
                                break;

                            case TileType.Mountain:
                                //random luxury resource gained from a mountain

                                Random rnd = new Random();
                                byte result = (byte)rnd.Next(1, 4);
                                switch(result)
                                {
                                    case 1:
                                        playerObj.Resources[7] += 2;
                                        break;
                                    case 2:
                                        playerObj.Resources[2] += 2;
                                        break;
                                    case 3:
                                        playerObj.Resources[4] += 3;
                                        break;
                                    case 4:
                                        playerObj.Resources[6] += 4;
                                        break;
                                }
                                break;

                            case TileType.Swamp:
                                playerObj.Resources[0] += 1;
                                playerObj.Resources[1] += 1;
                                break;

                            case TileType.Water:
                                playerObj.Resources[0] += 2;
                                playerObj.Resources[5] += 3;
                                playerObj.Resources[2] += 1;
                                break;

                        }
                    }

                }

            }

            //Set the yields

        }

        public Settlement(string settlementName, int x, int y)
        {
            this.settlementName = settlementName;
            this.coordinates.x = x;
            this.coordinates.y = y;

        }

        /// <summary>
        /// Facilitates the creation of a settlement.
        /// </summary>
        /// <param name="settlementName">A field that indicates the name of the settlement.</param>
        /// <param name="x">the x location</param>
        /// <param name="y">the y location</param>
        /// <param name="playerObj">the relevant player</param>
        public Settlement(string settlementName, int x, int y, Player playerObj)
        {
            this.settlementName = settlementName;
            this.coordinates.x = x;
            this.coordinates.y = y;
            //lets us know what the adjacent tiles around
            //the settlement are and what they are for
            SetAdjacentTiles(playerObj);
        }


    }
}
