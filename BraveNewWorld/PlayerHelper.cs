using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BraveNewWorld
{
    /// <summary>
    /// This class is intended to faciliate the common functionalities of a given player
    /// by providing polymorphic methods of doing things
    /// </summary>
    public class PrimativePlayerHelper
    {
        private Player playerObj;//the playerObj is of type Player. It is what 
        //allows us to manage the players from this player helper class
        public Player PlayerObj
        {
            get
            {
                return this.playerObj;
            }

            set
            {
                this.playerObj = value;
            }
        }

        /// <summary>
        /// Empty default constructor
        /// </summary>
        public PrimativePlayerHelper()
        {
        }        

        /// <summary>
        /// Accepts the playerObj and makes it equal to the private
        /// field also called playerObj
        /// </summary>
        /// <param name="playerObj"></param>
        public PrimativePlayerHelper(Player playerObj)
        {
            this.playerObj = playerObj;
        }

        public void UpdateFarmer()
        {
            playerObj.Professionals++;
            playerObj.Professions[0]++;
            playerObj.Yields[0] += 2;
        }

        public void UpdateMiner()
        {
            playerObj.Professionals++;
            playerObj.Professions[1]++;
            playerObj.Yields[5]++;
            playerObj.Yields[6]++;
            playerObj.Yields[7]++;
        }

        public void UpdateBlacksmith()
        {
            playerObj.Professionals++;
            playerObj.Professions[2]++;
            playerObj.Yields[3]++;
            playerObj.Yields[4]++;
            playerObj.Yields[8]++;
        }

        public void UpdateLumberjack()
        {

            playerObj.Professionals++;
            playerObj.Professions[3]++;
            playerObj.Yields[1] += 2;
        }

        public void UpdateMerchant()
        {
            playerObj.Professionals++;
            playerObj.Professions[4]++;
            playerObj.TradeRoutes++;
        }

        public void UpdateBanker()
        {
            playerObj.Professionals++;
            playerObj.Professions[5]++;
            playerObj.Yields[2]++;
        }

        /// <summary>
        /// Increments citizen count
        /// </summary>
        public void UpdateCitizens()
        {
            playerObj.Citizens++;
            for (int i = 0; i < playerObj.Yields.Length; i++)
            {

                playerObj.Yields[i] += 2;

            }
        }

        /// <summary>
        /// CHecks to seee if the player is dead or not from a lack of resources
        /// </summary>
        /// <returns></returns>
        public bool CheckResourcesForDefeatCondition()
        {
            if (playerObj.Resources[10] < 0)
            {

                System.Windows.Forms.MessageBox.Show("Your economy has fallen and your people have revolted, " +playerObj.Name, playerObj.Name+" has been defeated!");

                return true;

            }

            return false;

        }

        /// <summary>
        /// The yields are added to the resources
        /// </summary>
        public void AddYieldsToResouces()
        {
            for (int i = 0; i < playerObj.Yields.Length; i++)
            {

                playerObj.Resources[i] += playerObj.Yields[i];

            }
        }

        /// <summary>
        /// Checks to see if there is a settlement
        /// If there are none, perform a settlement check
        /// </summary>
        public void PolymorphicSettlementCheck()
        {
            playerObj.SettlementCheck();
        }

        /// <summary>
        /// This method refers to the Player class method called 
        /// 'AddSettlementToSettlementListOfPlayer(Settlement,int,int,Player)'
        /// This AddSettlementToSettlementListOfPlayer method does what it
        /// suggests: it adds a settlement to the player's list of settlements.
        /// 
        /// It also refers to the square tile management method called 'MakeTileIntoSettlement'
        /// which aesthetically transforms a given tile into a settlement tile
        /// 
        /// @@@@This version of EstablishSettlementAndWhatNot is not actually used.@@@@
        /// The overloaded one is (which is down below)
        /// 
        /// </summary>
        /// <param name="squareTileManagement"></param>
        /// <param name="tileHelper"></param>
        public void EstablishSettlementAndWhatNot(SquareTile squareTileManagement, TileHelper tileHelper)
        {
            // Checks if the player has a settlement they can place.
            if (playerObj.HasToEstablishASettlementThisTurn)
            {
                // Checks if the selected tile is a valid placement spot.
                if (    squareTileManagement.selectedTileStruct.tile != TileType.Settlement
                    &&  squareTileManagement.selectedTileStruct.tile != TileType.Water
                    &&  squareTileManagement.selectedTileStruct.tile != TileType.Mountain)
                {
                    squareTileManagement.MakeTileIntoSettlement(tileHelper);
                    Console.WriteLine("making tile into a settlement");
                    playerObj.HasToEstablishASettlementThisTurn = false;
                    playerObj.AddSettlementToSettlementListOfPlayer(new Settlement("",
                        squareTileManagement.selectedTileStruct.x,
                        squareTileManagement.selectedTileStruct.y,playerObj));
                }
            }
        }

        /// <summary>
        /// This method refers to the Player class method called 
        /// 'AddSettlementToSettlementListOfPlayer(Settlement,int,int,Player)'
        /// This AddSettlementToSettlementListOfPlayer method does what it
        /// suggests: it adds a settlement to the player's list of settlements.
        /// 
        /// </summary>
        /// <param name="squareTileManagement">the object pertaining to the </param>
        /// <param name="tileHelper"></param>
        /// <param name="isTurnOfPlayerOne"></param>
        public void EstablishSettlementAndWhatNot(SquareTile squareTileManagement, TileHelper tileHelper, bool isTurnOfPlayerOne)
        {
            if (playerObj.HasToEstablishASettlementThisTurn)
            {
                //this if-statement prevents the player from placing a settlement
                //on another settlement, water or a mountain.
                if (squareTileManagement.selectedTileStruct.tile != TileType.Settlement
                    && squareTileManagement.selectedTileStruct.tile != TileType.SettlementBlue
                    && squareTileManagement.selectedTileStruct.tile != TileType.SettlementRed
                    && squareTileManagement.selectedTileStruct.tile != TileType.Water
                    && squareTileManagement.selectedTileStruct.tile != TileType.Mountain)
                {
                    //aesthetically makes the tile into a settlement
                    squareTileManagement.MakeTileIntoSettlement(tileHelper,isTurnOfPlayerOne);
                    //debug statement
                    Console.WriteLine("making tile into a settlement");

                    //Indicates that the player no longer has to set 
                    //up a settlement
                    playerObj.HasToEstablishASettlementThisTurn = false;
                    //Adds the settlement to the list
                    playerObj.AddSettlementToSettlementListOfPlayer(new Settlement("",
                        squareTileManagement.selectedTileStruct.x,
                        squareTileManagement.selectedTileStruct.y,playerObj));
                }
            }
        }

        /// <summary>
        /// not sure why I bothered making a playerHelper deconstructor
        /// </summary>
        ~PrimativePlayerHelper()
        {
            
        }

        
    }
}
