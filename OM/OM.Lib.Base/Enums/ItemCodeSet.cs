﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.Lib.Base.Enums
{
    public static class ItemCodeSet
    {
        public static ItemData[] Items = new ItemData[]
        {
              new ItemData(){ Name = "선택하세요" , Code = ""}
            //, new ItemData(){ Name = "지수-국내-코스피" , Code = "001", Length = 2}
            //, new ItemData(){ Name = "지수-국내-코스피200" , Code = "101", Length = 2}
            //, new ItemData(){ Name = "선물-국내-코스피200" , Code = "90199999", Length = 2, SHCode = "101P3000", Tick=0.05}
            //, new ItemData(){ Name = "지수-해외-다우" , Code = "DJI", Length = 2}
            //, new ItemData(){ Name = "지수-해외-나스닥" , Code = "NAS", Length = 2}
            //, new ItemData(){ Name = "지수-해외-홍콩" , Code = "HMH", Length = 2}
            //, new ItemData(){ Name = "지수-해외-상해종합" , Code = "SHS", Length = 2}
            //, new ItemData(){ Name = "지수-해외-니케이225" , Code = "NII", Length = 2}
            //, new ItemData(){ Name = "-" , Code = ""}
            //, new ItemData(){ Name = "선물-국내-코스피200" , Code = "KS", Length = 2}
            , new ItemData(){ Name = "해선-WTI" , Code = "CL", Length = 2, SHCode = "CLJ20", Tick=0.01} //6-20  
            , new ItemData(){ Name = "해선-NG" , Code = "NG", Length = 3, SHCode = "NGU20", Tick=0.001}//6-29
            , new ItemData(){ Name = "해선-GOLD" , Code = "GC", Length = 1, SHCode = "GCZ20", Tick=0.1}//5-29
            , new ItemData(){ Name = "해선-SILVER" , Code = "SI", Length = 3, SHCode = "SIN20", Tick=0.1}//4-29
            , new ItemData(){ Name = "해선-항셍" , Code = "HMH", Length = 0, SHCode = "HMHM20", Tick=1.0} //4-29
            , new ItemData(){ Name = "해선-나스닥" , Code = "NQ", Length = 0, SHCode = "NQM20", Tick=0.25}  //6-21
            , new ItemData(){ Name = "해선-유로FX" , Code = "URO", Length = 5, SHCode = "UROM20", Tick=0.00005} //6-17
             , new ItemData(){ Name = "해선-SNP" , Code = "ES", Length = 2, SHCode = "ESM20", Tick=0.25} //6-21
            //, new ItemData(){ Name = "-" , Code = ""}
            //, new ItemData(){ Name = "환율-EUR/USD" , Code = "EURUSD", Length = 2}
            //, new ItemData(){ Name = "환율-USD/JPY" , Code = "USDJPY", Length = 2}
            //, new ItemData(){ Name = "-" , Code = ""}
            //, new ItemData(){ Name = "CRYPTO-BTC/USD" , Code = "BTCUSD", Length = 2}
            //, new ItemData(){ Name = "CRYPTO-ETH/USD" , Code = "ETHUSD", Length = 2}
            //, new ItemData(){ Name = "-" , Code = ""}
            //, new ItemData(){ Name = "주식-국내-삼성전자" , Code = ""}
            //, new ItemData(){ Name = "주식-국내-SK하이닉스" , Code = ""}
            //, new ItemData(){ Name = "주식-국내-현대차" , Code = ""}
            //, new ItemData(){ Name = "주식-국내-삼성바이오로직스" , Code = ""}
            //, new ItemData(){ Name = "주식-국내-LG화학" , Code = ""}
            //, new ItemData(){ Name = "주식-국내-셀트리온" , Code = ""}
            //, new ItemData(){ Name = "주식-국내-POSCO" , Code = ""}
            //, new ItemData(){ Name = "주식-국내-삼성물산" , Code = ""}
            //, new ItemData(){ Name = "주식-국내-한국전력" , Code = ""}
            //, new ItemData(){ Name = "주식-국내-현대모비스" , Code = ""}
            //, new ItemData(){ Name = "주식-국내-KODEX레버리지" , Code = "122630"}
            //, new ItemData(){ Name = "주식-국내-KODEX200선물인버스2X" , Code = "252670"}
            //, new ItemData(){ Name = "-" , Code = ""}
            //, new ItemData(){ Name = "주식-해외-Apple" , Code = "", Length = 2}
            //, new ItemData(){ Name = "주식-해외-Amazon" , Code = "", Length = 2}
            //, new ItemData(){ Name = "주식-해외-Netflix" , Code = "", Length = 2}
            //, new ItemData(){ Name = "주식-해외-Facebook" , Code = "", Length = 2}
        };
        public static List<ItemData> GetItems(string group)
        {
            List<ItemData> list = new List<ItemData>();
            foreach (var m in Items)
            {
                if (m.Name.StartsWith(group)) list.Add(m);
            }
            return list;
        }

        public static string GetItemCode(string name)
        {
            foreach (var m in Items)
            {
                if (m.Name == name) return m.Code;
            }

            return "";
        }
        public static string GetItemSHCodeByName(string name)
        {
            foreach (var m in Items)
            {
                if (m.Name == name) return m.SHCode;
            }

            return "";
        }
        public static string GetItemSHCodeByCode(string code)
        {
            foreach (var m in Items)
            {
                if (m.Code == code) return m.SHCode;
            }

            return "";
        }

        public static string GetItemName(string code)
        {
            foreach (var m in Items)
            {
                if (m.Code == code) return m.Name;
            }

            return "";
        }
        public static int GetItemRoundNum(string code)
        {
            foreach (var m in Items)
            {
                if (m.Code == code) return m.Length;
            }

            return 2;
        }
        public static double GetTick(string code)
        {
            foreach (var m in Items)
            {
                if (m.Code == code) return m.Tick;
            }

            return 0.01;
        }
    }
    public class ItemData
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public string SHCode { get; set; }

        public int Length { get; set; } = 0;

        public double Tick { get; set; } = 0.00;
    }
}
