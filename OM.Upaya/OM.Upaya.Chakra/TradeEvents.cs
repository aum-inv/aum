﻿using OM.Lib.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.Upaya.Chakra
{
    public delegate void TradeRuleDelegate(string atmanName, string itemCode, string message);
    public delegate void TradeRuleDelegate2(string atmanName, string ruleID, string itemCode, string message);
    public delegate void StrategyTradeRuleDelegate(string type, string desc);

    public class TradeEvents
    {
        private static TradeEvents instance = new TradeEvents();

        public static TradeEvents Instance
        {
            get
            {
                return instance;
            }
        }

        public event TradeRuleDelegate TradeRuleHandler;
        public event TradeRuleDelegate2 TradeRuleHandler2;

        public void OnTradeRuleHandler(string atmanName, string item, string message)
        {
            if (TradeRuleHandler != null)
                TradeRuleHandler(atmanName, item, message);
        }
        public void OnTradeRuleHandler(string atmanName, string ruleID, string item, string message)
        {
            if (TradeRuleHandler2 != null)
                TradeRuleHandler2(atmanName, ruleID, item, message);
        }
    }
}
