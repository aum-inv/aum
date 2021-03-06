﻿using OM.Lib.Base.Enums;
using OM.Lib.Base.Utils;
using OM.Lib.Framework.Utility;
using OM.PP.Chakra;
using OM.PP.Chakra.Ctx;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XA_DATASETLib;

namespace OM.PP.XingApp.Ex.Api
{
    class Api_WorldIndex : BaseApi
    {
        public ManualResetEvent manualEvent = new ManualResetEvent(false);
        public string ItemCode
        {
            get;
            set;
        }

        public TimeIntervalEnum TimeInterval
        {
            get;
            set;
        }

        public Api_WorldIndex() : base("t3518")
        {
        }

        List<S_CandleItemData> returnList = new List<S_CandleItemData>();

        #region Query Http
        public List<S_CandleItemData> Query(
             string itemCode
           , string gubun = "D" //M : 15Min, H : Hour, D : Day, W : Week, M : Month        
           )
        {
            this.ItemCode = itemCode;
            int round = ItemCodeUtil.GetItemCodeRoundNum(ItemCode);

            Task.Factory.StartNew(() => {
                try
                {
                    string symbol = "169";
                    if (itemCode == "DJI@DJI") symbol = "169";
                    else if (itemCode == "NAS@IXIC") symbol = "14958";
                    else if (itemCode == "SPI@SPX") symbol = "166";

                    else if (itemCode == "HSI@HSI") symbol = "179";
                    else if (itemCode == "SHS@000002") symbol = "40820";
                    else if (itemCode == "NII@NI225") symbol = "178";
                    

                    string resolution = gubun;
                    if (gubun == "H" || gubun == "1H") resolution = "60";
                    else if (gubun == "2H") resolution = "120";
                    else if (gubun == "4H") resolution = "240";
                    else if (gubun == "5H") resolution = "300";
                    else if (gubun == "M" || gubun == "1M") resolution = "1";
                    else if (gubun == "5M") resolution = "5";
                    else if (gubun == "15M") resolution = "15";
                    else if (gubun == "30M") resolution = "30";

                    Int32 from = (Int32)(DateTime.UtcNow.AddYears(-2).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                    Int32 to = (Int32)(DateTime.UtcNow.AddDays(10).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

                    if (gubun == "M" || gubun == "1M")
                    {
                        from = (Int32)(DateTime.UtcNow.AddDays(-1).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                        to = (Int32)(DateTime.UtcNow.AddDays(10).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                    }
                    else if (gubun == "5M")
                    {
                        from = (Int32)(DateTime.UtcNow.AddDays(-5).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                        to = (Int32)(DateTime.UtcNow.AddDays(10).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                    }
                    else if (gubun == "15M")
                    {
                        from = (Int32)(DateTime.UtcNow.AddDays(-15).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                        to = (Int32)(DateTime.UtcNow.AddDays(10).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                    }
                    else if (gubun == "30M")
                    {
                        from = (Int32)(DateTime.UtcNow.AddMonths(-1).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                        to = (Int32)(DateTime.UtcNow.AddDays(1).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                    }
                    else if (gubun == "H" || gubun == "1H")
                    {
                        from = (Int32)(DateTime.UtcNow.AddMonths(-2).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                        to = (Int32)(DateTime.UtcNow.AddDays(10).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                    }
                    else if (gubun == "2H")
                    {
                        from = (Int32)(DateTime.UtcNow.AddMonths(-4).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                        to = (Int32)(DateTime.UtcNow.AddDays(10).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                    }
                    else if (gubun == "4H")
                    {
                        from = (Int32)(DateTime.UtcNow.AddMonths(-8).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                        to = (Int32)(DateTime.UtcNow.AddDays(10).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                    }
                    else if (gubun == "5H")
                    {
                        from = (Int32)(DateTime.UtcNow.AddMonths(-10).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                        to = (Int32)(DateTime.UtcNow.AddDays(10).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                    }
                    else if (gubun == "W")
                    {
                        from = (Int32)(DateTime.UtcNow.AddYears(-5).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                        to = (Int32)(DateTime.UtcNow.AddDays(10).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                    }
                    else if (gubun == "M")
                    {
                        from = (Int32)(DateTime.UtcNow.AddYears(-10).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                        to = (Int32)(DateTime.UtcNow.AddDays(10).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                    }

                    string urlPath = $"https://tvc4.forexpros.com/1cc1f0b6f392b9fad2b50b7aebef1f7c/1601866558/18/18/88/history?symbol={symbol}&resolution={resolution}&from={from}&to={to}";
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlPath);
                    request.MaximumAutomaticRedirections = 4;
                    request.MaximumResponseHeadersLength = 4;
                    request.Credentials = CredentialCache.DefaultCredentials;
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);

                    string content = readStream.ReadToEnd();

                    var dyObj = JsonConverter.JsonToDynamicObject(content);
                    int cnt = dyObj.t.Count;
                    for (int i = 0; i < cnt; i++)
                    {
                        Int64 t = dyObj.t[i];
                        double o = dyObj.o[i];
                        double c = dyObj.c[i];
                        double h = dyObj.h[i];
                        double l = dyObj.l[i];
                        DateTime cTime = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(t);
                        //Console.WriteLine($"DT : {cTime.ToLongDateString()} O : {Math.Round(o, 2)}, H : {Math.Round(h, 2)}, L : {Math.Round(l, 2)}, C : {Math.Round(c, 2)}");

                        S_CandleItemData data = new S_CandleItemData();
                        data.DTime = cTime;
                        data.ItemCode = ItemCode;
                        data.OpenPrice = (Single)Math.Round(o, round);
                        data.HighPrice = (Single)Math.Round(h, round);
                        data.LowPrice = (Single)Math.Round(l, round);
                        data.ClosePrice = (Single)Math.Round(c, round);
                        data.Volume = 0;

                        returnList.Add(data);
                    }
                }
                catch (Exception ex)
                {
                    string err = ex.Message;
                }
                finally
                {
                    manualEvent.Set();
                }
            });
            manualEvent.WaitOne();

            return returnList;
        }

        public List<S_CandleItemData> Query(
                string itemCode
            , string gubun//M : 15Min, H : Hour, D : Day, W : Week, M : Month
            , DateTime sDT
            , DateTime dDT
           )
        {
            this.ItemCode = itemCode;
            int round = ItemCodeUtil.GetItemCodeRoundNum(ItemCode);

            Int32 from = (Int32)(sDT.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            Int32 to = (Int32)(dDT.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            Task.Factory.StartNew(() => {
                try
                {
                    string symbol = "169";
                    if (itemCode == "DJI@DJI") symbol = "169";
                    else if (itemCode == "NAS@IXIC") symbol = "14958";
                    else if (itemCode == "SPI@SPX") symbol = "166";

                    else if (itemCode == "HSI@HSI") symbol = "179";
                    else if (itemCode == "SHS@000002") symbol = "40820";
                    else if (itemCode == "NII@NI225") symbol = "178";

                    string resolution = gubun;
                    if (gubun == "H" || gubun == "1H") resolution = "60";
                    else if (gubun == "2H") resolution = "120";
                    else if (gubun == "4H") resolution = "240";
                    else if (gubun == "5H") resolution = "300";
                    else if (gubun == "M" || gubun == "1M") resolution = "1";
                    else if (gubun == "5M") resolution = "5";
                    else if (gubun == "15M") resolution = "15";
                    else if (gubun == "30M") resolution = "30";

                    string urlPath = $"https://tvc4.forexpros.com/1cc1f0b6f392b9fad2b50b7aebef1f7c/1601866558/18/18/88/history?symbol={symbol}&resolution={resolution}&from={from}&to={to}";
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlPath);
                    request.MaximumAutomaticRedirections = 4;
                    request.MaximumResponseHeadersLength = 4;
                    request.Credentials = CredentialCache.DefaultCredentials;
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);

                    string content = readStream.ReadToEnd();

                    var dyObj = JsonConverter.JsonToDynamicObject(content);
                    int cnt = dyObj.t.Count;
                    for (int i = 0; i < cnt; i++)
                    {
                        Int64 t = dyObj.t[i];
                        double o = dyObj.o[i];
                        double c = dyObj.c[i];
                        double h = dyObj.h[i];
                        double l = dyObj.l[i];
                        DateTime cTime = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(t);
                        //Console.WriteLine($"DT : {cTime.ToLongDateString()} O : {Math.Round(o, 2)}, H : {Math.Round(h, 2)}, L : {Math.Round(l, 2)}, C : {Math.Round(c, 2)}");

                        S_CandleItemData data = new S_CandleItemData();
                        data.DTime = cTime;
                        data.ItemCode = ItemCode;
                        data.OpenPrice = (Single)Math.Round(o, round);
                        data.HighPrice = (Single)Math.Round(h, round);
                        data.LowPrice = (Single)Math.Round(l, round);
                        data.ClosePrice = (Single)Math.Round(c, round);
                        data.Volume = 0;

                        returnList.Add(data);
                    }
                }
                catch (Exception ex)
                {
                    string err = ex.Message;
                }
                finally
                {
                    manualEvent.Set();
                }
            });
            manualEvent.WaitOne();

            return returnList;
        }
        #endregion

        #region Query
        /// <summary>
        /// 분 쿼리일때는 qrycnt
        /// 일 이상 쿼리일때는 날짜로 가져온다.
        /// </summary>

        //public List<S_CandleItemData> Query(
        //      string symbol = ""
        //    , string gubun = "0" //0:일 1:주 2:월           
        //    )
        //{
        //    ItemCode = symbol;

        //    if (gubun == "0")
        //        TimeInterval = TimeIntervalEnum.Day;
        //    else if (gubun == "1")
        //        TimeInterval = TimeIntervalEnum.Week;
        //    else if (gubun == "2")
        //        TimeInterval = TimeIntervalEnum.Month;
        //    string kind = "S";
        //    string qrycnt = "500"; //건수
        //    string cts_date = "";
        //    string cts_time = "";

        //    query.SetFieldData(inBlock, "kind", 0, kind);
        //    query.SetFieldData(inBlock, "symbol", 0, symbol);
        //    query.SetFieldData(inBlock, "cnt", 0, qrycnt);
        //    query.SetFieldData(inBlock, "jgbn", 0, gubun);
        //    query.SetFieldData(inBlock, "nmin", 0, "");
        //    query.SetFieldData(inBlock, "cts_date", 0, cts_date);
        //    query.SetFieldData(inBlock, "cts_time", 0, cts_time);
        //    query.Request(false);
        //    manualEvent.WaitOne();

        //    return returnList.OrderBy(t => t.DTime).ToList();
        //}

        //protected override void query_ReceiveData(string szTrCode)
        //{
        //    try
        //    {
        //        int blockCnt = Convert.ToInt32(query.GetBlockCount(outBlock1));
        //        int round = ItemCodeUtil.GetItemCodeRoundNum(ItemCode);

        //        for (int idx = 0; idx < blockCnt; idx++)
        //        {
        //            string date = query.GetFieldData(outBlock1, "date", idx);
        //            string time = query.GetFieldData(outBlock1, "time", idx);
        //            string open = query.GetFieldData(outBlock1, "open", idx);
        //            string high = query.GetFieldData(outBlock1, "high", idx);
        //            string low = query.GetFieldData(outBlock1, "low", idx);
        //            string close = query.GetFieldData(outBlock1, "price", idx);
        //            string volume = query.GetFieldData(outBlock1, "volume", idx);

        //            if (date.Length == 0) continue;

        //            //string format = "yyyyMMdd" + (time.Length > 0 ? "HHmmss" : "");
        //            string format = "yyyyMMdd";
        //            var dt = DateTime.ParseExact(date, format, CultureInfo.InvariantCulture);

        //            S_CandleItemData data = new S_CandleItemData();
        //            data.DTime = dt;
        //            data.ItemCode = ItemCode;
        //            data.OpenPrice = (Single)Math.Round(Convert.ToDouble(open) * 100, round);
        //            data.HighPrice = (Single)Math.Round(Convert.ToDouble(high) * 100, round);
        //            data.LowPrice = (Single)Math.Round(Convert.ToDouble(low) * 100, round);
        //            data.ClosePrice = (Single)Math.Round(Convert.ToDouble(close) * 100, round);
        //            data.Volume = Convert.ToSingle(volume);

        //            returnList.Add(data);
        //        }
        //        OnApiLog("Api_WorldIndex ::: query_ReceiveData");
        //    }
        //    catch (Exception ex)
        //    {
        //        OnApiLog("Error ::: " + ex.Message);
        //    }
        //    finally
        //    {
        //        manualEvent.Set();
        //    }
        //}       
    }

    #endregion
}
