﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.PP.Chakra.Indicators
{
    public static class Cleaners
    {

        public static List<Quote> PrepareHistory(IEnumerable<Quote> history)
        {
            // we cannot rely on date consistency when looking back, so we add an index and sort

            if (history == null || !history.Any())
            {
            }

            // return if already processed (no missing indexes)
            if (!history.Any(x => x.Index == null))
            {
                return history.OrderBy(x => x.Index).ToList();
            }

            // add index and check for errors
            int i = 1;
            DateTime lastDate = DateTime.MinValue;
            foreach (Quote h in history.OrderBy(x => x.Date))
            {
                h.Index = i++;

                if (lastDate == h.Date)
                {
                }

                lastDate = h.Date;

                // TODO: more error evaluation (impossible values, missing values, etc.)
            }

            return history.OrderBy(x => x.Index).ToList();
        }


        internal static List<BasicData> PrepareBasicData(IEnumerable<BasicData> basicData)
        {
            // we cannot rely on date consistency when looking back, so we add an index and sort

            if (basicData == null || !basicData.Any())
            {               
            }

            // return if already processed (no missing indexes)
            if (!basicData.Any(x => x.Index == null))
            {
                return basicData.OrderBy(x => x.Index).ToList();
            }

            // add index and check for errors
            int i = 1;
            DateTime lastDate = DateTime.MinValue;
            foreach (BasicData h in basicData.OrderBy(x => x.Date))
            {
                h.Index = i++;

                if (lastDate == h.Date)
                {
                }

                lastDate = h.Date;

                // TODO: more error evaluation (impossible values, missing values, etc.)
            }

            return basicData.OrderBy(x => x.Index).ToList();
        }


        internal static IEnumerable<BasicData> ConvertHistoryToBasic(IEnumerable<Quote> history, string element = "C")
        {
            // elements represents the targeted OHLCV parts, so use "O" to return <Open> as base data, etc.

            if (element == "O")
            {
                IEnumerable<BasicData> basicData =
                    history.Select(x => new BasicData { Index = x.Index, Date = x.Date, Value = x.Open });                  
                
                return PrepareBasicData(basicData);
            }
            else if (element == "H")
            {
                IEnumerable<BasicData> basicData =
                    history.Select(x => new BasicData { Index = x.Index, Date = x.Date, Value = x.High });

                return PrepareBasicData(basicData);
            }
            else if (element == "L")
            {
                IEnumerable<BasicData> basicData =
                    history.Select(x => new BasicData { Index = x.Index, Date = x.Date, Value = x.Low });

                return PrepareBasicData(basicData);
            }
            else
            {
                IEnumerable<BasicData> basicData =
                    history.Select(x => new BasicData { Index = x.Index, Date = x.Date, Value = x.Close });

                return PrepareBasicData(basicData);
            }           
        }
    }
}
