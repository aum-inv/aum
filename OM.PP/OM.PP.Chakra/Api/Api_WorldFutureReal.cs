﻿using OM.Atman.Chakra.Ctx;
using OM.Lib.Base.Enums;
using OM.Lib.Entity;
using OM.PP.Chakra.Ctx;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XA_DATASETLib;

namespace OM.PP.XingApp.Api
{
    class Api_WorldFutureReal : BaseRealApi
    {
        public string ItemCode
        {
            get;
            set;
        }
        public string ShCode
        {
            get;
            set;
        }
        public TimeIntervalEnum TimeInterval
        {
            get;
            set;
        }

        public Api_WorldFutureReal() : base("OVC")
        {
        }

        #region Query
        /// <summary>
        /// 분 쿼리일때는 qrycnt
        /// 일 이상 쿼리일때는 날짜로 가져온다.
        /// </summary>
        /// <param name="shcode">101</param>
        /// <param name="ncnt"></param>
        /// <param name="qrycnt"></param>
        /// <param name="cts_date"></param>
        /// <param name="cts_time"></param>
        /// <param name="cts_daygb">연속당일구분(0. 연속전체, 1.연속당)</param>
        public void Query(string shcode, string itemCode)
        {
            ShCode = shcode;
            ItemCode = itemCode;
            real.SetFieldData(inBlock, "symbol", shcode);
            real.AdviseRealData();
        }

        public override void Real_ReceiveRealData(string szTrCode)
        {
            string ovsdate = real.GetFieldData(outBlock, "ovsdate");
            string trdtm = real.GetFieldData(outBlock, "trdtm");
            string kordate = real.GetFieldData(outBlock, "kordate");
            string kortm = real.GetFieldData(outBlock, "kortm");
            string open = real.GetFieldData(outBlock, "open");
            string high = real.GetFieldData(outBlock, "high");
            string low = real.GetFieldData(outBlock, "low");
            string curpr = real.GetFieldData(outBlock, "curpr");
            string ydiffSign = real.GetFieldData(outBlock, "ydiffSign");
            string ydiffpr = real.GetFieldData(outBlock, "ydiffpr");
            string chgrate = real.GetFieldData(outBlock, "chgrate");

            Task.Factory.StartNew(() => {
                if (Order.OrderInstances.WorldFutureOrderInstance[ItemCode].IsUse)
                Order.OrderInstances.WorldFutureOrderInstance[ItemCode].CheckPrice(ShCode, curpr);
            });

            CurrentPrice item = new CurrentPrice();
            item.chetime = ovsdate + trdtm;
            item.chetimeKr = kordate + kortm;
            item.open = open;
            item.high = high;
            item.low = low;
            item.price = curpr;
            item.sign = ydiffSign;
            item.change = ydiffpr;
            item.drate = chgrate;

            Task.Factory.StartNew(() => {
                AtmanContext.Instance.ClientContext.SetCurrentPrice(ItemCode, item);
            });

            Task.Factory.StartNew(() => {
                PPContext.Instance.ClientContext.SetCurrentPrice(ItemCode, item);
            });
        }
        #endregion
    }
}
