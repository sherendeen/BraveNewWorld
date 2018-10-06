using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BraveNewWorld
{
    /// <summary>
    /// Used to keep track of the tile that is currently selected
    /// </summary>
    public struct SelectedTile
    {
        public int x;
        public int y;
        public TileType tile;
    }
    
    /// <summary>
    /// This is a class that holds all the relevant methods and variables for
    /// implementing a square-tile based map.
    /// </summary>
    public class SquareTile
    {

        public SelectedTile selectedTileStruct;

        //The height of the 
        private int height = 16;
        public int Height
        {
            get
            {
                return this.height;
            }
            set
            {
                this.height = value;
            }
        }

        /// <summary>
        /// Establishes the settlement
        /// @@@This method is UNUSED@@@
        /// </summary>
        /// <param name="currentPlayer">the player in question</param>
        /// <param name="x">x location on the map</param>
        /// <param name="y">y location on the map</param>
        /// <returns></returns>
        private Settlement EstablishSettlement(Player currentPlayer, int x, int y)
        {
            var settlement = new Settlement();
            settlement.coordinates.x = x;
            settlement.coordinates.y = y;


            return settlement;
        }

        /// <summary>
        /// Changes the tile into a Settlement (TileType.Settlement)
        /// </summary>
        /// <param name="tileHelper"></param>
        public void MakeTileIntoSettlement(TileHelper tileHelper)
        {
            tileHelper.ChangeTile(TileType.Settlement, selectedTileStruct.x, selectedTileStruct.y);   
        }

        /// <summary>
        /// Changes the tile into either a SettlementRed or a
        /// SettlementBlue.
        /// </summary>
        /// <param name="tileHelper"></param>
        /// <param name="isTurnOfPlayerOne"></param>
        public void MakeTileIntoSettlement(TileHelper tileHelper, bool isTurnOfPlayerOne)
        {
            if (isTurnOfPlayerOne)
                tileHelper.ChangeTile(TileType.SettlementBlue, selectedTileStruct.x, selectedTileStruct.y);
            else
                tileHelper.ChangeTile(TileType.SettlementRed, selectedTileStruct.x, selectedTileStruct.y);
        }

        /// <summary>
        /// Default empty constructor
        /// </summary>
        public SquareTile()
        {

        }
        
        /// <summary>
        /// Translates the x and y coordinate on the gamemap into the actual
        /// tile
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void PointToTile(float x, float y, TileHelper tileHelper)
        {
            //float width = height;//weird huh?
            //col = (int)(x / width);
            //if (col % 2 == 0)
            //    row = (int)(y / height);
            //else
            //    row = (int)((y - height / 2) / height);

            ////Find the test area
            //float testx = col * width;
            //float testy = row * height;
            //if (col % 2 == 1) testy += height / 2;

            
            this.selectedTileStruct.x = (int)( x / this.height);
            this.selectedTileStruct.y = (int)( y / this.height);
            this.selectedTileStruct.tile = tileHelper.GetTile(this.selectedTileStruct.x, this.selectedTileStruct.y);
            

        }

        

        /// <summary>
        /// Draws a given tile.
        /// 
        /// </summary>
        /// <param name="graphics">the graphics component attached, often, to the EventArgs e</param>
        /// <param name="x">x coordinate on the map</param>
        /// <param name="y">y coordinate on the map</param>
        /// <param name="tileType">The TileType enum allows us to diferentiate tiles based on terrain.</param>
        public void Draw(Graphics graphics, int x, int y, TileType tileType)
        {
            Image image;
            // Figures out which tile to draw based oof the tile type.
            switch (tileType)
            {
                case TileType.Grass:
                    image = Image.FromFile(@"images/grassplain.png");
                    break;
                case TileType.Water:
                    image = Image.FromFile(@"images/water.png");
                    break;
                case TileType.Forest:
                    image = Image.FromFile(@"images/forest.png");
                    break;
                case TileType.Desert:
                    image = Image.FromFile(@"images/desert.png");
                    break;
                case TileType.Mountain://Added 15 May, 2018
                    image = Image.FromFile(@"images/mountain.png");
                    break;
                case TileType.Hill:
                    image = Image.FromFile(@"images/hill1.png");
                    break;
                case TileType.Tundra:///////////tundra is omitted
                    image = Image.FromFile(@"images/unknown.png");//NOTHING
                    break;
                case TileType.Swamp:
                    image = Image.FromFile(@"images/swamp1.png");
                    break;
                case TileType.Settlement:///currently unused
                    image = Image.FromFile(@"images/hut.png");
                    break;
                case TileType.SettlementRed:
                    image = Image.FromFile(@"images/hut_red.png");
                    break;
                case TileType.SettlementBlue:
                    image = Image.FromFile(@"images/hut_blue.png");
                    break;
                default:
                    image = Image.FromFile(@"images/unknown.png");
                    break;
            }
            
            //Calls the GDI graphics component to draw the image.
            //It has never failed to work.
            graphics.DrawImage(image, x * height, y * height);
        }
        


            
    }
}
