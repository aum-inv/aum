﻿using OM.Lib.Base.Utils;
using OM.PP.XingApp.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.PP.XingApp.Order
{
    public class WorldFutureOrder
    {
        public string itemCode = string.Empty;
        public WorldFutureOrder(string itemCode)
        {
            this.itemCode = itemCode;
        }
        public bool IsUse = false;
        //public event EventHandler OrderEvent;
        //public event EventHandler OrderCompletedEvent;
        public event EventHandler LogEvent;
        public int RoundNum
        {
            get
            {
                return ItemCodeUtil.GetItemCodeRoundNum(itemCode);
            }
        }

        public OrderItem OrderPrice1 = new OrderItem();
        public OrderItem OrderPrice2 = new OrderItem();
        public OrderItem OrderPrice3 = new OrderItem();

        public string LastPrice = "";

        public void CheckPrice(string itemCode, string price)
        {
            try
            {
                if (itemCode != this.itemCode) return;

                if (LastPrice.Length > 0 && LastPrice == price)
                    return;

                LastPrice = price;

                //신규주문
                if (OrderPrice1.IsRun)
                {
                    var oItem = OrderPrice1;
                    var orderPrice = Math.Round(Convert.ToDouble(oItem.OrderPrice), RoundNum);
                    var contractPrice = Math.Round(Convert.ToDouble(price), RoundNum);

                    if (orderPrice == contractPrice)
                    {
                        OrderPrice1.IsRun = false;
                        RunOrder("신규주문", itemCode, oItem);
                    }
                    
                    //매도
                    //if (oItem.Position == "1" && orderPrice == contractPrice)
                    //{
                    //    OrderPrice1.IsRun = false;
                    //    RunOrder(itemCode, oItem);
                    //}
                    ////매수
                    //else if (oItem.Position == "2" && orderPrice == contractPrice)
                    //{
                    //    OrderPrice1.IsRun = false;
                    //    RunOrder(itemCode, oItem);
                    //}
                }
                //수익주문(잔고가 있는 경우..반대매매)
                if (OrderPrice2.IsRun)
                {
                    var oItem = OrderPrice2;
                    var orderPrice = Math.Round(Convert.ToDouble(oItem.OrderPrice), RoundNum);
                    var contractPrice = Math.Round(Convert.ToDouble(price), RoundNum);

                    if (orderPrice == contractPrice)
                    {
                        OrderPrice2.IsRun = false;
                        RunOrder("수익주문", itemCode, oItem);
                    }
                    //매도
                    //if (oItem.Position == "1" && orderPrice <= contractPrice)
                    //{
                    //    OrderPrice2.IsRun = false;
                    //    RunOrder(itemCode, oItem);
                    //}
                    ////매수
                    //else if (oItem.Position == "2" && orderPrice >= contractPrice)
                    //{
                    //    OrderPrice2.IsRun = false;
                    //    RunOrder(itemCode, oItem);
                    //}
                }
                //손실주문
                if (OrderPrice3.IsRun)
                {
                    var oItem = OrderPrice3;
                    var orderPrice = Math.Round(Convert.ToDouble(oItem.OrderPrice), RoundNum);
                    var contractPrice = Math.Round(Convert.ToDouble(price), RoundNum);

                    if (orderPrice == contractPrice)
                    {
                        OrderPrice3.IsRun = false;
                        RunOrder("손실주문", itemCode, oItem);
                    }
                    
                    //매도
                    //if (oItem.Position == "1" && orderPrice >= contractPrice)
                    //{
                    //    OrderPrice3.IsRun = false;
                    //    RunOrder(itemCode, oItem);
                    //}
                    ////매수
                    //else if (oItem.Position == "2" && orderPrice <= contractPrice)
                    //{
                    //    OrderPrice3.IsRun = false;
                    //    RunOrder(itemCode, oItem);
                    //}
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public void RunOrder(string type, string itemCode, OrderItem oItem)
        {
            try
            {
                if (LogEvent != null)
                {
                    string message = $"{type}:::{itemCode}, {oItem.Position}, {oItem.OrderPrice}";
                    LogEvent(message, new EventArgs());
                }
                //Api.Api_WorldFutureTrade api = new Api.Api_WorldFutureTrade();
                //api.Query(
                //      ""
                //    , DateTime.Now.ToString("yyyyMMdd")
                //    , WorldFutureAccountInfo.계좌번호
                //    , WorldFutureAccountInfo.계좌비밀번호
                //    , itemCode
                //    , "1"
                //    , oItem.Position
                //    , "1"
                //    , oItem.OrderPrice
                //    , oItem.OrderPrice
                //    , oItem.Quantity
                //    );
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}
