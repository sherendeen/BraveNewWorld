using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using static System.Console;


/// <summary>
/// Written by Seth G. R. Herendeen and Jonah Posey
/// </summary>
namespace BraveNewWorld
{

    public struct TextParts
    {
        public const char LEFT_PARENTHETICAL = '(';
        public const char RIGHT_PARENTHETICAL = ')';
        public const string COMMA_AND_SPACE = ", ";
    }

    public partial class gameForm : Form
    {
        /*Official version number (not the assembly version)*/
        public const string VERSION_NUMBER = "ALPHA 1.0";

        // These are a number of lists creatd to hold the
        // various TextBoxes that are  part of the trading
        // system so as to allow the use of a single for loop
        // when one needs to deal with a number of them,
        // as oppossed to calling each one of them seperately.
        public List<int> tradeOfferMaterials = new List<int>();
        public List<int> tradeDemandMaterials = new List<int>();
        public List<int> tradeOfferQuantity = new List<int>();
        public List<int> tradeDemandQuantity = new List<int>();
        public List<int> tradeCooldownPlayer1 = new List<int>();
        public List<int> tradeCooldownPlayer2 = new List<int>();

        //The establishment of different player objects. In this version
        //we actually cycle between two different player objects
        public Player player1 = new Player("Player 1");
        public Player player2 = new Player("Player 2");

        //event helper object that facilitates in many of the UI updating processes
        EventHelper eh = new EventHelper();

        //tracks whether the game is currently running
        bool isPlaying = true;

        // This is used when adding an item to a trade route so clicking the
        // appropriate button doesn't overwrite anything put before it.
        int offerTradeRow = 0;
        int demandTradeRow = 0;

        // Holds the string values associated with each resource.
        string[] resourceNames = { "Food", "Wood", "Gold", "Iron", "Aluminium", "Salt", "Coal", "Diamond", "Copper", "Oil" };
        
        // Declares and instantiates the SquareTile 'squareTileManagement'.
        SquareTile squareTileManagement = new SquareTile();

        // Keeps track of whose turn it is.
        private bool isPlayerOneTurn = true;


        TileHelper tileHelper = new TileHelper();


        //   private const float HEXAGON_HEIGHT = 12;

        //Selected hexes
        //   private List<PointF> hexagons = new List<PointF>();



        //  HexTile hexTileManagement = new HexTile();
        // HexTileV2 hexTileV2 = new HexTileV2();


        /////////////////////////////////////////////////
        MinsAndMaxes minsAndMaxes;
        //private int result;

        /// <summary>
        /// Initializes the form
        /// </summary>
        public gameForm()
        {
            InitializeComponent();
            // Sets the DomainUpDowns to the bottom index '0'
            // so clicking the 'up' arrow will increment and
            // clicking the 'down' arrow will decrement.
            dmUpDwnOffer.SelectedItem = dmUpDwnOffer.Items[15];
            dmUpDwnDemand.SelectedItem = dmUpDwnOffer.Items[15];
        }

        /// <summary>
        /// Ends the turn and transfers control from one player to the other
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEndTurn_Click(object sender, EventArgs e)
        {
            //PLAYERHELPER contains many helper functions to be used
            //in conjunction with the player class
            PrimativePlayerHelper playerHelper;

            //We do not finish initializing playerHelper until it is
            //established whose turn it is...
            if (isPlayerOneTurn)
            {
                playerHelper = new PrimativePlayerHelper(player1);
            }
            else
            {
                playerHelper = new PrimativePlayerHelper(player2);
            }

            // Stops the game if the player's happiness is less than 0.
            if (playerHelper.CheckResourcesForDefeatCondition())
            {
                isPlaying = false;
            }

            // If the game is still going
            // AND the current player DOES NOT have to establish a settlement this turn
            if (isPlaying && !playerHelper.PlayerObj.HasToEstablishASettlementThisTurn) {

                TextBox[] outputDemandMaterial = {txtBxTradeDealDemandMaterial1, txtBxTradeDealDemandMaterial2,
                txtBxTradeDealDemandMaterial3, txtBxTradeDealDemandMaterial4, txtBxTradeDealDemandMaterial5,
                txtBxTradeDealDemandMaterial6};

                TextBox[] outputOfferMaterial = { txtBxTradeDealOfferMaterial1, txtBxTradeDealOfferMaterial2,
                txtBxTradeDealOfferMaterial3, txtBxTradeDealOfferMaterial4, txtBxTradeDealOfferMaterial5,
                txtBxTradeDealOfferMaterial6};

                TextBox[] outputOfferQuantity = {txtBxTradeDealOfferQuantity1, txtBxTradeDealOfferQuantity2, txtBxTradeDealOfferQuantity3,
                txtBxTradeDealOfferQuantity4, txtBxTradeDealOfferQuantity5, txtBxTradeDealOfferQuantity6};

                TextBox[] outputDemandQuantity = {txtBxTradeDealDemandQuantity1, txtBxTradeDealDemandQuantity2, txtBxTradeDealDemandQuantity3,
                txtBxTradeDealDemandQuantity4, txtBxTradeDealDemandQuantity5, txtBxTradeDealDemandQuantity6};

                //Updates events like news events
                UpdateEvents();

                //Updates resource counts
                UpdateResources();

                //Sets the view back to the view of the world
                pnlDiplomacyScreen.Visible = false;
                pnlMarketScreen.Visible = false;
                pnlNewsScreen.Visible = false;
                pnlProductionScreen.Visible = false;

                // Update conditionally
                if (pctBxPurchase.Image == pctBxFarmer.Image)
                {
                    playerHelper.UpdateFarmer();
                }
                else if (pctBxPurchase.Image == pctBxMiner.Image)
                {
                    playerHelper.UpdateMiner();
                }
                else if (pctBxPurchase.Image == pctBxBlacksmith.Image)
                {
                    playerHelper.UpdateBlacksmith();
                }
                else if (pctBxPurchase.Image == pctBxLumberjack.Image)
                {
                    playerHelper.UpdateLumberjack();
                }
                else if (pctBxPurchase.Image == pctBxMerchant.Image)
                {
                    playerHelper.UpdateMerchant();
                }
                else if (pctBxPurchase.Image == pctBxBanker.Image)
                {
                    playerHelper.UpdateBanker();
                }
                else if (pctBxPurchase.Image == pctBxCitizen.Image)
                {
                    playerHelper.UpdateCitizens();
                }
                else if (pctBxPurchase.Image == pctBxSettlement.Image)
                {
                    playerHelper.EstablishSettlementAndWhatNot(squareTileManagement, tileHelper, isPlayerOneTurn);
                }

                pctBxPurchase.Image = null;

                //I decided I didn't want to move this part to PlayerHelper
                if (isPlayerOneTurn)
                {

                    lblUnproffessionals.Text = "Unproffesional Citizens / Total Citizens   " + (player1.Citizens - player1.Professionals) + " / " + player1.Citizens;
                    lblTradeRoutes.Text = "Trade Routes " + (player1.TradeRoutes - player1.UsedTradeRoutes) + " / " + player1.TradeRoutes;

                }
                else
                {
                    lblUnproffessionals.Text = "Unproffesional Citizens / Total Citizens   " + (player2.Citizens - player2.Professionals) + " / " + player2.Citizens;
                    lblTradeRoutes.Text = "Trade Routes " + (player2.TradeRoutes - player2.UsedTradeRoutes) + " / " + player2.TradeRoutes;
                }
                //
                //FIX@@
                //MessageBox.Show("btnAcceptTradeRoute visibility " + btnAcceptTradeRoute.Visible);

                //if the trade route accept button is pressed
                //then clear text of the UI arrays
                if (btnAcceptTradeRoute.Enabled)
                {
                    for (int i = 0; i < outputDemandMaterial.Length; i++)
                    {
                        outputDemandMaterial[i].Text = "";
                        outputOfferMaterial[i].Text = "";
                        outputOfferQuantity[i].Text = "";
                        outputDemandQuantity[i].Text = "";
                    }

                    btnAcceptTradeRoute.Enabled = false;
                    btnDeclineTradeRoute.Enabled = false;

                } else if (outputOfferMaterial[0].Text != "" || outputDemandMaterial[0].Text != "")
                {

                    btnAcceptTradeRoute.Enabled = true;
                    btnDeclineTradeRoute.Enabled = true;

                }

                btnAcceptTradeRoute.Visible = !btnAcceptTradeRoute.Visible;
                btnDeclineTradeRoute.Visible = !btnDeclineTradeRoute.Visible;

                playerHelper.CheckResourcesForDefeatCondition();

                //Change the turn and thus the player that everything is about
                this.isPlayerOneTurn = !this.isPlayerOneTurn;


                playerHelper.PolymorphicSettlementCheck();

                // Resets the DomainUpDowns to '0' and the RadioButtons to 'none'.
                dmUpDwnOffer.SelectedItem = dmUpDwnOffer.Items[15];
                dmUpDwnDemand.SelectedItem = dmUpDwnOffer.Items[15];

                rdBtnDemandNone.Checked = true;
                rdBtnOfferNone.Checked = true;

                // Appropriately informs the players whose turn it is.
                if (isPlayerOneTurn)
                {

                    lblPlayersTurn.Text = "Player 1's turn";

                } else
                {

                    lblPlayersTurn.Text = "Player 2's turn";

                }

                // Progresses the traderoute cooldown queue.
                for (int i = 0; i < tradeCooldownPlayer1.Count; i++)
                {

                    if (--tradeCooldownPlayer1[i] < 1)
                    {

                        tradeCooldownPlayer1.RemoveAt(0);

                        i--;

                    }

                }

            } else if (playerHelper.PlayerObj.HasToEstablishASettlementThisTurn)
            {

                // Informs the user that they must place a settlement.
                playerHelper.PlayerObj.SettlementCheck();

            }

        }

        private void UpdateResources()
        {

            //for (int i = 0; i < player1.Yields.Length; i++)
            //{

            //    player1.Resources[i] += player1.Yields[i];

            //}
            PrimativePlayerHelper playerHelper;
            if (isPlayerOneTurn)
                playerHelper = new PrimativePlayerHelper(player1);
            else
                playerHelper = new PrimativePlayerHelper(player2);

            playerHelper.AddYieldsToResouces();

            //I did not want to move this to the PlayerHelper
            //class due to the fact that these are GUI and moving
            //such things make me feel a little nervous

            if (isPlayerOneTurn) {

                txtBxFood.Text = "" + player1.Resources[0] + " + (" + player1.Yields[0] + ")";
                txtBxWood.Text = "" + player1.Resources[1] + " + (" + player1.Yields[1] + ")";
                txtBxGold.Text = "" + player1.Resources[2] + " + (" + player1.Yields[2] + ")";
                txtBxIron.Text = "" + player1.Resources[3] + " + (" + player1.Yields[3] + ")";
                txtBxAluminium.Text = "" + player1.Resources[4] + " + (" + player1.Yields[4] + ")";
                txtBxSalt.Text = "" + player1.Resources[5] + " + (" + player1.Yields[5] + ")";
                txtBxCoal.Text = "" + player1.Resources[6] + " + (" + player1.Yields[6] + ")";
                txtBxDiamond.Text = "" + player1.Resources[7] + " + (" + player1.Yields[7] + ")";
                txtBxCopper.Text = "" + player1.Resources[8] + " + (" + player1.Yields[8] + ")";
                txtBxOil.Text = "" + player1.Resources[9] + " + (" + player1.Yields[9] + ")";
                txtBxHappiness.Text = "" + (player1.Resources[10] + (player1.Resources[2] * .4) + (player1.Resources[3] * .2) +
                    (player1.Resources[4] * .1) + (player1.Resources[5] * .3) + (player1.Resources[6] * .2) + (player1.Resources[7] * .4)
                     + (player1.Resources[8] * .2) + (player1.Resources[9] * .3) - (player1.Citizens * 5) - (player1.Professionals * 10));
            }
            else
            {
                txtBxFood.Text = "" + player2.Resources[0] + " + (" + player2.Yields[0] + ")";
                txtBxWood.Text = "" + player2.Resources[1] + " + (" + player2.Yields[1] + ")";
                txtBxGold.Text = "" + player2.Resources[2] + " + (" + player2.Yields[2] + ")";
                txtBxIron.Text = "" + player2.Resources[3] + " + (" + player2.Yields[3] + ")";
                txtBxAluminium.Text = "" + player2.Resources[4] + " + (" + player2.Yields[4] + ")";
                txtBxSalt.Text = "" + player2.Resources[5] + " + (" + player2.Yields[5] + ")";
                txtBxCoal.Text = "" + player2.Resources[6] + " + (" + player2.Yields[6] + ")";
                txtBxDiamond.Text = "" + player2.Resources[7] + " + (" + player2.Yields[7] + ")";
                txtBxCopper.Text = "" + player2.Resources[8] + " + (" + player2.Yields[8] + ")";
                txtBxOil.Text = "" + player2.Resources[9] + " + (" + player2.Yields[9] + ")";
                txtBxHappiness.Text = "" + (player2.Resources[10] + (player2.Resources[2] * .4) + (player2.Resources[3] * .2) +
                    (player2.Resources[4] * .1) + (player2.Resources[5] * .3) + (player2.Resources[6] * .2) + (player2.Resources[7] * .4)
                     + (player2.Resources[8] * .2) + (player2.Resources[9] * .3) - (player2.Citizens * 5) - (player2.Professionals * 10));
            }
        }

        // The following four methods are responsible for opening and closing the menu screens.
        // They do this by setting all other screens to be invisible and then toggling the
        // visibility of the screen related to the PictureBox you clicked on. This way there
        // is only ever one screen open, and the toggle allows you to open and close the screen
        // using the same control.
        private void BtnNews_Click(object sender, EventArgs e)
        {

            pnlDiplomacyScreen.Visible = false;
            pnlMarketScreen.Visible = false;
            pnlNewsScreen.Visible = !pnlNewsScreen.Visible;
            pnlProductionScreen.Visible = false;

        }

        private void BtnMarket_Click(object sender, EventArgs e)
        {

            pnlDiplomacyScreen.Visible = false;
            pnlMarketScreen.Visible = !pnlMarketScreen.Visible;
            pnlNewsScreen.Visible = false;
            pnlProductionScreen.Visible = false;

        }

        private void BtnUnits_Click(object sender, EventArgs e)
        {

            pnlDiplomacyScreen.Visible = false;
            pnlMarketScreen.Visible = false;
            pnlNewsScreen.Visible = false;
            pnlProductionScreen.Visible = !pnlProductionScreen.Visible;

        }

        private void BtnDiplomacy_Click(object sender, EventArgs e)
        {

            pnlDiplomacyScreen.Visible = !pnlDiplomacyScreen.Visible;
            pnlMarketScreen.Visible = false;
            pnlNewsScreen.Visible = false;
            pnlProductionScreen.Visible = false;

        }

        /// <summary>
        /// Draws the tiles to the pctBxGameScreen (i.e the world screen)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pctBxGameScreen_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            minsAndMaxes.xMin = 0;
            minsAndMaxes.xMax = pctBxGameScreen.ClientSize.Width;
            minsAndMaxes.yMin = 0;
            minsAndMaxes.yMax = pctBxGameScreen.ClientSize.Height;

            tileHelper.GenerateTileMap(60, 30);
            TileType[,] tiles = tileHelper.ExampleTileMap;
            int rowLength = tiles.GetLength(0);
            int colLength = tiles.GetLength(1);


            //draws an example tile map
            for (int i = 0; i < rowLength; i = i + 1)
            {
                for (int j = 0; j < colLength; j = j + 1)
                {
                    squareTileManagement.Draw(e.Graphics, i, j, tiles[i, j]);
                }
            }

        }

        /// <summary>
        /// Allows for the user to read information about a given tile when hovering over it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pctBxGameScreen_MouseMove(object sender, MouseEventArgs e)
        {
            int row, col;
            //  hexTileManagement.PointToHex(e.X, e.Y, HEXAGON_HEIGHT, out row,
            //      out col);
            //    hexTileV2.PointToHex(e.X, e.Y, HEXAGON_HEIGHT, out row, out col);
            try
            {
                squareTileManagement.PointToTile(e.X, e.Y, tileHelper);
            }
            catch (NullReferenceException ex)
            {

                Console.Write("Map was not yet generated.");

            }

            row = squareTileManagement.selectedTileStruct.x;
            col = squareTileManagement.selectedTileStruct.y;
            //if it is the first player's turn, indicate as much when showing
            //tile into in the title bar
            if (isPlayerOneTurn)
            {
                this.Text = "It is player one's turn. | " + TextParts.LEFT_PARENTHETICAL.ToString() + row +
                   TextParts.COMMA_AND_SPACE + col +
                   TextParts.RIGHT_PARENTHETICAL + " " + squareTileManagement.selectedTileStruct.tile;
            }
            else//otherwise do not
            {
                this.Text = "It is player two's turn. | " + TextParts.LEFT_PARENTHETICAL.ToString() + row +
                   TextParts.COMMA_AND_SPACE + col +
                   TextParts.RIGHT_PARENTHETICAL + " " + squareTileManagement.selectedTileStruct.tile;
            }
        }

        /// <summary>
        /// When the user selects a tile on the world, make it register as much
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pctBxGameScreen_MouseClick(object sender, MouseEventArgs e)
        {
          //  int row, col;

            PrimativePlayerHelper playerHelper;
            if (isPlayerOneTurn)
                playerHelper = new PrimativePlayerHelper(player1);
            else
                playerHelper = new PrimativePlayerHelper(player2);

            squareTileManagement.PointToTile(e.X, e.Y, tileHelper);

            //does everything what had been done by what was commented out below
            //THis is the polymorphic way of achieving what was commented out underneath
            //It is one of the many optimizations done throughout the program
            playerHelper.EstablishSettlementAndWhatNot(squareTileManagement, tileHelper,isPlayerOneTurn);

            //if (isPlayerOneTurn)
            //{
            //    if (player1.HasToEstablishASettlementThisTurn)
            //    {
            //        if (squareTileManagement.selectedTileStruct.tile != TileType.Settlement
            //            && squareTileManagement.selectedTileStruct.tile != TileType.Water
            //            && squareTileManagement.selectedTileStruct.tile != TileType.Mountain)
            //        {
            //            squareTileManagement.MakeTileIntoSettlement(tileHelper);
            //            Console.WriteLine("making tile into a settlement");
            //            player1.HasToEstablishASettlementThisTurn = false;
            //            player1.AddSettlementToSettlementListOfPlayer(new Settlement("",
            //                this.squareTileManagement.selectedTileStruct.x,
            //                this.squareTileManagement.selectedTileStruct.y));
            //        }
            //    }
            //}
            //else
            //{
            //    if (player2.HasToEstablishASettlementThisTurn)
            //    {
            //        if (squareTileManagement.selectedTileStruct.tile != TileType.Settlement
            //            && squareTileManagement.selectedTileStruct.tile != TileType.Water
            //            && squareTileManagement.selectedTileStruct.tile != TileType.Mountain)
            //        {
            //            squareTileManagement.MakeTileIntoSettlement(tileHelper);
            //            Console.WriteLine("making tile into a settlement");
            //            player2.HasToEstablishASettlementThisTurn = false;
            //            player2.AddSettlementToSettlementListOfPlayer(new Settlement("",
            //                this.squareTileManagement.selectedTileStruct.x,
            //                this.squareTileManagement.selectedTileStruct.y));
            //        }
            //    }
            //}


            //If the map has changed, refresh the bitmap
            pctBxGameScreen.Refresh();
        }

        /// <summary>
        /// Refresh when the screen is resized***
        /// This is not actually needed because we don't actually do anything when the form is resized
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pctBxGameScreen_Resize(object sender, EventArgs e)
        {
            pctBxGameScreen.Refresh();

        }

        /// <summary>
        /// Updates the events. Probably not a candidate to be moved 
        /// </summary>
        private void UpdateEvents()
        {
            // Adds a number of items to the ListBox that displays news based off
            // the values returned by the various update methods.
            lstBxNews.Items.Add(UpdateCrime());
            lstBxNews.Items.Add(UpdateDisaster());
            lstBxNews.Items.Add(UpdateWeather());
        }

        // The following 8 methods either change the image of the PictureBox 'pctBxPurchase',
        // which is used later to determine what, if anything, the player bought, or notifies
        // the user that they cannot buy what they were attempting to purchase.

        private void pctBxFarmer_Click(object sender, EventArgs e)
        {

            if (isPlayerOneTurn)
            {

                if (player1.Resources[1] > 39 && player1.Professionals < player1.Citizens)
                {

                    pctBxPurchase.Image = pctBxFarmer.Image;

                }
                else
                {

                    MessageBox.Show("INVALID SELECTION: you either have too many professionals or not enough resources.");

                }

            }
            else
            {

                if (player1.Resources[2] > 39 && player2.Professionals < player2.Citizens)
                {

                    pctBxPurchase.Image = pctBxFarmer.Image;

                }
                else
                {

                    MessageBox.Show("INVALID SELECTION: you either have too many professionals or not enough resources.");

                }

            }

        }

        private void pctBxMiner_Click(object sender, EventArgs e)
        {

            if (isPlayerOneTurn)
            {

                if (player1.Resources[1] > 39 && player1.Professionals < player1.Citizens)
                {

                    pctBxPurchase.Image = pctBxMiner.Image;

                }
                else
                {

                    MessageBox.Show("INVALID SELECTION: you either have too many professionals or not enough resources.");

                }

            }
            else
            {

                if (player1.Resources[2] > 39 && player2.Professionals < player2.Citizens)
                {

                    pctBxPurchase.Image = pctBxMiner.Image;

                }
                else
                {

                    MessageBox.Show("INVALID SELECTION: you either have too many professionals or not enough resources.");

                }

            }

        }

        private void pctBxBlacksmith_Click(object sender, EventArgs e)
        {

            if (isPlayerOneTurn)
            {

                if (player1.Resources[1] > 39 && player1.Professionals < player1.Citizens)
                {

                    pctBxPurchase.Image = pctBxBlacksmith.Image;

                }
                else
                {

                    MessageBox.Show("INVALID SELECTION: you either have too many professionals or not enough resources.");

                }

            }
            else
            {

                if (player1.Resources[2] > 39 && player2.Professionals < player2.Citizens)
                {

                    pctBxPurchase.Image = pctBxBlacksmith.Image;

                }
                else
                {

                    MessageBox.Show("INVALID SELECTION: you either have too many professionals or not enough resources.");

                }

            }

        }

        private void pctBxLumberjack_Click(object sender, EventArgs e)
        {

            if (isPlayerOneTurn) {

                if (player1.Resources[1] > 39 && player1.Professionals < player1.Citizens)
                {

                    pctBxPurchase.Image = pctBxLumberjack.Image;

                }
                else
                {

                    MessageBox.Show("INVALID SELECTION: you either have too many professionals or not enough resources.");

                }

            } else
            {

                if (player1.Resources[2] > 39 && player2.Professionals < player2.Citizens)
                {

                    pctBxPurchase.Image = pctBxLumberjack.Image;

                }
                else
                {

                    MessageBox.Show("INVALID SELECTION: you either have too many professionals or not enough resources.");

                }

            }

        }

        private void pctBxSettlement_Click(object sender, EventArgs e)
        {

            if (isPlayerOneTurn)
            {

                if (player1.Resources[1] > 39 && player1.Professionals < player1.Citizens)
                {

                    pctBxPurchase.Image = pctBxSettlement.Image;

                }
                else
                {

                    MessageBox.Show("INVALID SELECTION: you either have too many professionals or not enough resources.");

                }

            }
            else
            {

                if (player1.Resources[2] > 39 && player2.Professionals < player2.Citizens)
                {

                    pctBxPurchase.Image = pctBxSettlement.Image;

                }
                else
                {

                    MessageBox.Show("INVALID SELECTION: you either have too many professionals or not enough resources.");

                }

            }

        }

        private void pctBxMerchant_Click(object sender, EventArgs e)
        {

            if (isPlayerOneTurn)
            {

                if (player1.Resources[1] > 39 && player1.Professionals < player1.Citizens)
                {

                    pctBxPurchase.Image = pctBxMerchant.Image;

                }
                else
                {

                    MessageBox.Show("INVALID SELECTION: you either have too many professionals or not enough resources.");

                }

            }
            else
            {

                if (player1.Resources[2] > 39 && player2.Professionals < player2.Citizens)
                {

                    pctBxPurchase.Image = pctBxMerchant.Image;

                }
                else
                {

                    MessageBox.Show("INVALID SELECTION: you either have too many professionals or not enough resources.");

                }

            }

        }

        private void pctBxBanker_Click(object sender, EventArgs e)
        {

            if (isPlayerOneTurn)
            {

                if (player1.Resources[1] > 39 && player1.Professionals < player1.Citizens)
                {

                    pctBxPurchase.Image = pctBxBanker.Image;

                }
                else
                {

                    MessageBox.Show("INVALID SELECTION: you either have too many professionals or not enough resources.");

                }

            }
            else
            {

                if (player1.Resources[2] > 39 && player2.Professionals < player2.Citizens)
                {

                    pctBxPurchase.Image = pctBxBanker.Image;

                }
                else
                {

                    MessageBox.Show("INVALID SELECTION: you either have too many professionals or not enough resources.");

                }

            }

        }

        private void pctCitizen_Click(object sender, EventArgs e)
        {

            if (isPlayerOneTurn)
            {

                if (player1.Resources[1] > 39 && player1.Professionals < player1.Citizens)
                {

                    pctBxPurchase.Image = pctBxCitizen.Image;

                }
                else
                {

                    MessageBox.Show("INVALID SELECTION: you either have too many professionals or not enough resources.");

                }

            }
            else
            {

                if (player1.Resources[2] > 39 && player2.Professionals < player2.Citizens)
                {

                    pctBxPurchase.Image = pctBxCitizen.Image;

                }
                else
                {

                    MessageBox.Show("INVALID SELECTION: you either have too many professionals or not enough resources.");

                }

            }

        }

        // Resets the image of the PictureBox 'pctBxPurchase'.
        private void pctBxPurchase_Click(object sender, EventArgs e)
        {

            pctBxPurchase.Image = null;

        }

        // Uses a random object to determine what, if any, crime happens,
        // returns that as a string, and changes various aspects of the
        // player's resources or yields based off what is returned.
        public string UpdateCrime()
        {

            int eventNum;
            Random rnd = new Random();

            eventNum = rnd.Next(100);

            if (eventNum > 80)
            {
                eventNum = rnd.Next(100);

                if (eventNum > 90)
                {

                    player1.Resources[10] -= 15 * player1.Citizens;

                    return "Crime - Assassination -> major drop in happiness";
                }
                else if (eventNum > 70)
                {
                    player1.TradeRoutes--;
                    return "Crime - trade route was robbed -> lose trade route";
                }
                else if (eventNum > 40)
                {

                    player1.Resources[2] -= 4 * player1.Citizens;
                    return "Crime - bank robbery - > lose gold";

                }
                else
                {

                    player1.Resources[10] -= 3 * player1.Resources[2];
                    player1.Resources[2] -= 3 * player1.Citizens;

                    return "Crime - embezzlement -> lower happiness and lose " +
                        "gold";
                    

                }

            }

            return "No crimes";

        }

        /// <summary>
        /// Updates the wheather my dude
        /// </summary>
        /// <returns>Returns a weather string</returns>
        public string UpdateWeather()
        {

            int eventNum;
            Random rnd = new Random();

            eventNum = rnd.Next(100);

            if (eventNum > 50)
            {

                eventNum = rnd.Next(100);

                if (eventNum > 90)
                {

                    for (int i = 0; i < player1.Yields.Length; i++)
                    {

                        player1.Yields[i]--;

                    }

                    return "Weather - hot -> slightly lower yield on all " +
                        "resources";

                }
                else if (eventNum > 70)
                {

                    player1.Yields[0] -= 2 * player1.Citizens;

                    return "Weather - drought -> lower food yields";

                }
                else if (eventNum > 40)
                {

                    player1.Yields[0] -= 2 * player1.Citizens;
                    player1.Yields[1] -= 2 * player1.Citizens;

                    return "Weather - cold -> lower yield on organic resources";

                }
                else
                {
                    
                    player1.Yields[0] += player1.Citizens;

                    return "Weather - rain -> higher food yields";

                }

            }

            return "Weather - normal -> no effect";

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>a string in the form of a natural disaster or some other disaster</returns>
        public string UpdateDisaster()
        {
            int eventNum;
            Random rnd = new Random();

            eventNum = rnd.Next(100);

            if (eventNum > 95)
            {

                eventNum = rnd.Next(100);

                if (eventNum > 90)
                {

                    for (int i = 0; i < player1.Yields.Length; i++)
                    {

                        player1.Yields[i] -= player1.Citizens;

                    }

                    return "Natural disaster - plague -> significantly lower yields on all resources";

                }
                else if (eventNum > 70)
                {

                    if ((player1.Resources[1] -= 100) < 0)
                    {

                        player1.Resources[1] = 0;

                    }

                    return "Natural disaster - earthquake -> lose wood";

                }
                else if (eventNum > 40)
                {

                    player1.Resources[0] -= player1.Yields[0];

                    return "Natural disaster - flood -> lose food equivalent to yield";

                }
                else
                {

                    player1.Resources[1] -= player1.Yields[1];

                    return "Natural disaster - fire -> lose wood equivalent to yield";

                }

            }

            return "No natural disasters";

        }

        private void ToolTipInformatica_Popup(object sender, PopupEventArgs e)
        {

        }

        // Allows the player to add the selected amounts of the selected resources to a trade route proposal.
        private void btnMakeTradeRoute_Click(object sender, EventArgs e)
        {

            int demandResource = 0;
            int offerResource = 0;
            bool invalid = false;

            TextBox[] outputDemandMaterial = {txtBxTradeDealDemandMaterial1, txtBxTradeDealDemandMaterial2,
                txtBxTradeDealDemandMaterial3, txtBxTradeDealDemandMaterial4, txtBxTradeDealDemandMaterial5,
                txtBxTradeDealDemandMaterial6};

            TextBox[] outputOfferMaterial = { txtBxTradeDealOfferMaterial1, txtBxTradeDealOfferMaterial2,
                txtBxTradeDealOfferMaterial3, txtBxTradeDealOfferMaterial4, txtBxTradeDealOfferMaterial5,
                txtBxTradeDealOfferMaterial6};

            TextBox[] outputOfferQuantity = {txtBxTradeDealOfferQuantity1, txtBxTradeDealOfferQuantity2, txtBxTradeDealOfferQuantity3,
                txtBxTradeDealOfferQuantity4, txtBxTradeDealOfferQuantity5, txtBxTradeDealOfferQuantity6};

            TextBox[] outputDemandQuantity = {txtBxTradeDealDemandQuantity1, txtBxTradeDealDemandQuantity2, txtBxTradeDealDemandQuantity3,
                txtBxTradeDealDemandQuantity4, txtBxTradeDealDemandQuantity5, txtBxTradeDealDemandQuantity6};

            // Checks if the player has an open traderoute
            if (player1.UsedTradeRoutes < player1.TradeRoutes) {

                // Adds the appropriate resource.
                if (rdBtnOfferFood.Checked && player1.Resources[0] > 0)
                {

                    outputDemandMaterial[offerTradeRow].Text = resourceNames[0];

                    offerResource = 0;

                    tradeOfferMaterials.Add(0);

                }
                else if (rdBtnOfferWood.Checked && player1.Resources[1] > 0)
                {

                    outputDemandMaterial[offerTradeRow].Text = resourceNames[1];

                    offerResource = 1;

                    tradeOfferMaterials.Add(1);

                }
                else if (rdBtnOfferGold.Checked && player1.Resources[2] > 0)
                {

                    outputDemandMaterial[offerTradeRow].Text = resourceNames[2];

                    offerResource = 2;

                    tradeOfferMaterials.Add(2);

                }
                else if (rdBtnOfferIron.Checked && player1.Resources[3] > 0)
                {

                    outputDemandMaterial[offerTradeRow].Text = resourceNames[3];

                    offerResource = 3;

                    tradeOfferMaterials.Add(3);

                }
                else if (rdBtnOfferAluminium.Checked && player1.Resources[4] > 0)
                {

                    outputDemandMaterial[offerTradeRow].Text = resourceNames[4];

                    offerResource = 4;

                    tradeOfferMaterials.Add(4);

                }
                else if (rdBtnOfferSalt.Checked && player1.Resources[5] > 0)
                {

                    outputDemandMaterial[offerTradeRow].Text = resourceNames[5];

                    offerResource = 5;

                    tradeOfferMaterials.Add(5);

                }
                else if (rdBtnOfferCoal.Checked && player1.Resources[6] > 0)
                {

                    outputDemandMaterial[offerTradeRow].Text = resourceNames[6];

                    offerResource = 6;

                    tradeOfferMaterials.Add(6);

                }
                else if (rdBtnOfferDiamonds.Checked && player1.Resources[7] > 0)
                {

                    outputDemandMaterial[offerTradeRow].Text = resourceNames[7];

                    offerResource = 7;

                    tradeOfferMaterials.Add(7);

                }
                else if (rdBtnOfferCopper.Checked && player1.Resources[8] > 0)
                {

                    outputDemandMaterial[offerTradeRow].Text = resourceNames[8];

                    offerResource = 8;

                    tradeOfferMaterials.Add(8);

                }
                else if (rdBtnOfferOil.Checked && player1.Resources[9] > 0)
                {

                    outputDemandMaterial[offerTradeRow].Text = resourceNames[9];

                    offerResource = 9;

                    tradeOfferMaterials.Add(9);

                }
                else if (rdBtnOfferNone.Checked)
                {

                    outputDemandMaterial[offerTradeRow].Text = "";

                }

                // Adds the appropriate resource.
                if (rdBtnDemandFood.Checked && player1.Resources[0] > 0)
                {

                    outputOfferMaterial[offerTradeRow].Text = resourceNames[0];

                    demandResource = 0;

                    tradeDemandMaterials.Add(0);

                }
                else if (rdBtnDemandWood.Checked && player1.Resources[1] > 0)
                {

                    outputOfferMaterial[offerTradeRow].Text = resourceNames[1];

                    demandResource = 1;

                    tradeDemandMaterials.Add(1);

                }
                else if (rdBtnDemandGold.Checked && player1.Resources[2] > 0)
                {

                    outputOfferMaterial[offerTradeRow].Text = resourceNames[2];

                    demandResource = 2;

                    tradeDemandMaterials.Add(2);

                }
                else if (rdBtnDemandIron.Checked && player1.Resources[3] > 0)
                {

                    outputOfferMaterial[offerTradeRow].Text = resourceNames[3];

                    demandResource = 3;

                    tradeDemandMaterials.Add(3);

                }
                else if (rdBtnDemandAluminium.Checked && player1.Resources[4] > 0)
                {

                    outputOfferMaterial[offerTradeRow].Text = resourceNames[4];

                    demandResource = 4;

                    tradeDemandMaterials.Add(4);

                }
                else if (rdBtnDemandSalt.Checked && player1.Resources[5] > 0)
                {

                    outputOfferMaterial[offerTradeRow].Text = resourceNames[5];

                    demandResource = 5;

                    tradeDemandMaterials.Add(5);

                }
                else if (rdBtnDemandCoal.Checked && player1.Resources[6] > 0)
                {

                    outputOfferMaterial[offerTradeRow].Text = resourceNames[6];

                    demandResource = 6;

                    tradeDemandMaterials.Add(6);

                }
                else if (rdBtnDemandDiamonds.Checked && player1.Resources[7] > 0)
                {

                    outputOfferMaterial[offerTradeRow].Text = resourceNames[7];

                    demandResource = 7;

                    tradeDemandMaterials.Add(7);

                }
                else if (rdBtnDemandCopper.Checked && player1.Resources[8] > 0)
                {

                    outputOfferMaterial[offerTradeRow].Text = resourceNames[8];

                    demandResource = 8;

                    tradeDemandMaterials.Add(8);

                }
                else if (rdBtnDemandOil.Checked && player1.Resources[9] > 0)
                {

                    outputOfferMaterial[offerTradeRow].Text = resourceNames[9];

                    demandResource = 9;

                    tradeDemandMaterials.Add(9);

                }
                else if (rdBtnDemandNone.Checked)
                {

                    outputOfferMaterial[offerTradeRow].Text = "";

                }

                // Checks if the player is infact trading: a resource, a quantity of that resource that is not 0,
                // and a quantity that is not higher than the current amount of that resource.
                if (!rdBtnOfferNone.Checked && dmUpDwnOffer.Text != "0" && player1.Resources[offerResource] > 0)
                {

                    // Updates the trade route proposal.
                    if (int.Parse(dmUpDwnOffer.Text) > player1.Resources[offerResource])
                    {

                        outputDemandQuantity[offerTradeRow].Text = "" + player1.Resources[offerResource];

                    } else
                    {

                        outputDemandQuantity[offerTradeRow].Text = dmUpDwnOffer.Text;

                    }

                    tradeOfferQuantity.Add(int.Parse(outputDemandQuantity[offerTradeRow].Text));

                    offerTradeRow++;

                }
                else
                {

                    outputDemandMaterial[offerTradeRow].Text = "";

                    invalid = true;

                }

                if (!rdBtnDemandNone.Checked && dmUpDwnDemand.Text != "0" && player1.Resources[demandResource] > 0)
                {

                    if (int.Parse(dmUpDwnDemand.Text) > player1.Resources[demandResource])
                    {

                        outputOfferQuantity[demandTradeRow].Text = "" + player1.Resources[demandResource];

                    }
                    else
                    {

                        outputOfferQuantity[demandTradeRow].Text = dmUpDwnDemand.Text;

                    }

                    tradeDemandQuantity.Add(int.Parse(outputOfferQuantity[demandTradeRow].Text));

                    demandTradeRow++;

                }
                else
                {

                    outputOfferMaterial[demandTradeRow].Text = "";

                    invalid = true;

                }

                // Informs the user that they cannot trade what they were trying to trade.
                if (invalid)
                {

                    MessageBox.Show("INVALID TRADE ROUTE PARAMETERS: You have tried to trade with a resource you do not have or you have tried to trade 0 of an item.");

                }

                btnDeclineTradeRoute.Enabled = false;
                btnAcceptTradeRoute.Enabled = false;

            }

        }

        // Adds and/or subtracts the appropriate qunatities of the appropriate resources from/to the resources of the appropriate player.
        private void btnAcceptTradeRoute_Click(object sender, EventArgs e)
        {

            TextBox[] outputDemandMaterial = {txtBxTradeDealDemandMaterial1, txtBxTradeDealDemandMaterial2,
                txtBxTradeDealDemandMaterial3, txtBxTradeDealDemandMaterial4, txtBxTradeDealDemandMaterial5,
                txtBxTradeDealDemandMaterial6};

            TextBox[] outputOfferMaterial = { txtBxTradeDealOfferMaterial1, txtBxTradeDealOfferMaterial2,
                txtBxTradeDealOfferMaterial3, txtBxTradeDealOfferMaterial4, txtBxTradeDealOfferMaterial5,
                txtBxTradeDealOfferMaterial6};

            TextBox[] outputOfferQuantity = {txtBxTradeDealOfferQuantity1, txtBxTradeDealOfferQuantity2, txtBxTradeDealOfferQuantity3,
                txtBxTradeDealOfferQuantity4, txtBxTradeDealOfferQuantity5, txtBxTradeDealOfferQuantity6};

            TextBox[] outputDemandQuantity = {txtBxTradeDealDemandQuantity1, txtBxTradeDealDemandQuantity2, txtBxTradeDealDemandQuantity3,
                txtBxTradeDealDemandQuantity4, txtBxTradeDealDemandQuantity5, txtBxTradeDealDemandQuantity6};

            if (isPlayerOneTurn) {

                for (int i = 0; i < tradeDemandMaterials.Count; i++)
                {

                    player1.Resources[tradeDemandMaterials[i]] += tradeDemandQuantity[i];
                    player2.Resources[tradeDemandMaterials[i]] -= tradeDemandQuantity[i];

                }

                for (int i = 0; i < tradeOfferMaterials.Count; i++)
                {

                    player1.Resources[tradeOfferMaterials[i]] += tradeOfferQuantity[i];
                    player2.Resources[tradeOfferMaterials[i]] -= tradeOfferQuantity[i];

                }

                player1.UsedTradeRoutes++;

                tradeCooldownPlayer1.Add(5);

            }
            else
            {

                for (int i = 0; i < tradeDemandMaterials.Count; i++)
                {

                    player2.Resources[tradeDemandMaterials[i]] += tradeDemandQuantity[i];
                    player1.Resources[tradeDemandMaterials[i]] -= tradeDemandQuantity[i];

                }

                for (int i = 0; i < tradeOfferMaterials.Count; i++)
                {

                    player2.Resources[tradeOfferMaterials[i]] += tradeOfferQuantity[i];
                    player1.Resources[tradeOfferMaterials[i]] -= tradeOfferQuantity[i];

                }

                player2.UsedTradeRoutes++;

                tradeCooldownPlayer2.Add(5);

            }

            // Clears the trade route proposal.
            for (int i = 0; i < outputDemandMaterial.Length; i++)
            {

                outputDemandMaterial[i].Text = "";
                outputOfferMaterial[i].Text = "";
                outputOfferQuantity[i].Text = "";
                outputDemandQuantity[i].Text = "";

            }

            btnDeclineTradeRoute.Enabled = false;
            btnAcceptTradeRoute.Enabled = false;

        }

        // Resets the trade route proposal.
        private void btnDeclineTradeRoute_Click(object sender, EventArgs e)
        {


            TextBox[] outputDemandMaterial = {txtBxTradeDealDemandMaterial1, txtBxTradeDealDemandMaterial2,
                txtBxTradeDealDemandMaterial3, txtBxTradeDealDemandMaterial4, txtBxTradeDealDemandMaterial5,
                txtBxTradeDealDemandMaterial6};

            TextBox[] outputOfferMaterial = { txtBxTradeDealOfferMaterial1, txtBxTradeDealOfferMaterial2,
                txtBxTradeDealOfferMaterial3, txtBxTradeDealOfferMaterial4, txtBxTradeDealOfferMaterial5,
                txtBxTradeDealOfferMaterial6};

            TextBox[] outputOfferQuantity = {txtBxTradeDealOfferQuantity1, txtBxTradeDealOfferQuantity2, txtBxTradeDealOfferQuantity3,
                txtBxTradeDealOfferQuantity4, txtBxTradeDealOfferQuantity5, txtBxTradeDealOfferQuantity6};

            TextBox[] outputDemandQuantity = {txtBxTradeDealDemandQuantity1, txtBxTradeDealDemandQuantity2, txtBxTradeDealDemandQuantity3,
                txtBxTradeDealDemandQuantity4, txtBxTradeDealDemandQuantity5, txtBxTradeDealDemandQuantity6};

            for (int i = 0; i < outputDemandMaterial.Length; i++)
            {

                outputDemandMaterial[i].Text = "";
                outputOfferMaterial[i].Text = "";
                outputOfferQuantity[i].Text = "";
                outputDemandQuantity[i].Text = "";

            }

            btnDeclineTradeRoute.Enabled = false;
            btnAcceptTradeRoute.Enabled = false;

        }

        /// <summary>
        /// When the credits button is pressed, LAUNCH the credits form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMenu_Click(object sender, EventArgs e)
        {
            var fCredits = new Credits();
            fCredits.Show();
        }
    }

}