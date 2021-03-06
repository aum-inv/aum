﻿using OM.Lib.Base.Enums;
using OM.Lib.Base.Utils;
using OM.Lib.Entity;
using OM.Lib.Framework.Db;
using OM.PP.Chakra;
using OM.PP.Chakra.Ctx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.Atman.Chakra
{
    public class TickLineTradeRule : BaseTradeRule
    {
        protected override string AtmanName => "TICK PATTERN";

        string priceType;


        UpDownPatternEnum lastUpDownH = UpDownPatternEnum.None;
        UpDownPatternEnum lastUpDownL = UpDownPatternEnum.None;
        UpDownPatternEnum lastUpDownM = UpDownPatternEnum.None;

        UpDownPatternEnum lastUpDownHL = UpDownPatternEnum.None;
        UpDownPatternEnum lastUpDownLM = UpDownPatternEnum.None;
        UpDownPatternEnum lastUpDownHM = UpDownPatternEnum.None;

        public TickLineTradeRule(string itemCode, string priceType)
        {
            ItemCode = itemCode;
            this.priceType = priceType;
        }

        public override void InitRule()
        {
        }
        public override void Close()
        {
            
        }
        public override void AnalysisAsync(CurrentPrice price)
        {
            Task.Factory.StartNew(() =>
            {
                Analysis(price);
            });
        }
        public override void Analysis(CurrentPrice price)
        {
            try
            {
                switch (priceType)
                {
                    case "0":
                        CPrice = Math.Round(Convert.ToDouble(price.price), RoundNum);
                        break;
                    case "1":
                        CPrice = Math.Round(price.price3, RoundNum);
                        break;
                    case "2":
                        CPrice = Math.Round(price.price5, RoundNum);
                        break;
                    case "3":
                        CPrice = Math.Round(price.price7, RoundNum);
                        break;
                    default:
                        CPrice = Math.Round(Convert.ToDouble(price.price), RoundNum);
                        break;
                }
                if (BPrice == CPrice) return;

                PriceList.Insert(CPrice);
                
              

            }
            catch (Exception)
            {
            }
            finally
            {
                BPrice = CPrice;
            }
        }
    }
}
