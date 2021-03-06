﻿using OM.PP.Chakra.Ctx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.Upaya.Chakra.ExApi
{
    public static class XingApi
    {
        public static void Order(string type, string itemCode, string position, string quantity)
        {
            try
            {
                XingContext.Instance.ClientContext.OrderBuySell(itemCode, position, "1", "0", quantity);                
            }
            catch (Exception)
            {
            }
            finally
            {
                string desc = $"종목:{itemCode},포지션:{position},수량:{quantity},현재가:모름";
                var result = TelegramBotApi.SendMessageAsync(type + "=>" + desc);
            }
        }
        public static void Order(string type, string itemCode, string position, string quantity, string cPrice)
        {
            try
            {               
                XingContext.Instance.ClientContext.OrderBuySell(itemCode, position, "1", cPrice, quantity);                
            }
            catch (Exception)
            {
            }
            finally
            {
                string desc = $"종목:{itemCode},포지션:{position},수량:{quantity},현재가:{cPrice}";
                var result = TelegramBotApi.SendMessageAsync(type + "=>" + desc);
            }
        }
        public static void Order(string type, string itemCode, string position, string quantity, string cPrice, string bPrice)
        {
            try
            {
                XingContext.Instance.ClientContext.OrderBuySell(itemCode, position, "1", cPrice, quantity);                
            }
            catch (Exception)
            {
            }
            finally
            {
                string desc = $"종목:{itemCode},포지션:{position},수량:{quantity},현재가:{cPrice},잔고평균:{bPrice}";
                var result = TelegramBotApi.SendMessageAsync(type + "=>" + desc);
            }
        }

        public static void UpayaOrder(string itemCode, string position, string quantity, string tradeType, string cPrice)
        {
            try
            {
                XingContext.Instance.ClientContext.OrderBuySell(itemCode, position, tradeType, cPrice, quantity);
            }
            catch (Exception)
            {
            }
            finally
            {
            }
        }
    }
}
