using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BraveNewWorld
{
    public class Player
    {

        private int citizens = 1;
        private int professionals = 0;
        private int tradeRoutes = 1;
        private int usedTradeRoutes = 0;
        private int[] professions = new int[6];
        private int[] resources = new int[11];
        private int[] yields = new int[10];
        private List<Settlement> settlements = new List<Settlement>();
        private string name = "";//the friendly name we give the player

        private bool hasToEstablishASettlementThisTurn = true;

        public bool HasToEstablishASettlementThisTurn
        {
            get
            {
                return this.hasToEstablishASettlementThisTurn;
            }
            set
            {
                this.hasToEstablishASettlementThisTurn = value;
            }
        }

        public Player()
        {

            resources[10] = 10;

            for (int i = 0; i < yields.Length; i++)
            {

                yields[i] = 2;

            }

        }

        public Player(string name)
        {
            resources[10] = 10;

            for (int i = 0; i < yields.Length; i++)
            {

                yields[i] = 2;

            }
            this.name = name;
        }

        public int UsedTradeRoutes
        {
            get
            {
                return this.usedTradeRoutes;
            }

            set
            {
                this.usedTradeRoutes = value;
            }
        }

        public int TradeRoutes
        {
            get
            {
                return this.tradeRoutes;
            }

            set
            {
                this.tradeRoutes = value;
            }
        }

        public int Citizens
        {
            get
            {
                return this.citizens;
            }

            set
            {
                this.citizens = value;
            }
        }

        public int Professionals
        {
            get
            {
                return this.professionals;
            }

            set
            {
                this.professionals = value;
            }
        }

        public int[] Professions
        {
            get
            {
                return this.professions;
            }

            set
            {
                this.professions = value;
            }
        }

        public int[] Resources
        {
            get
            {
                return this.resources;
            }

            set
            {
                this.resources = value;
            }
        }

        public int[] Yields
        {
            get
            {
                return this.yields;
            }

            set
            {
                this.yields = value;
            }
        }

        public List<Settlement> Settlements
        {
            get
            {
                return this.settlements;
            }

            set
            {
                this.settlements = value;
            }
        }

        /// <summary>
        /// The friendly, human-readable name
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
            }
        }

        public void AddSettlementToSettlementListOfPlayer(Settlement settlement)
        {
            this.settlements.Add(settlement);
        }

        public void SettlementCheck()
        {
            if(this.settlements.Count < 1)
                MessageBox.Show("You have to establish your first settlement this turn! Please click a location on the map");
        }

    }
}
