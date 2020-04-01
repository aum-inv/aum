﻿using OM.Lib.Base.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.PP.Chakra
{
    public abstract class A_HLOC
    {
        public string ItemCode
        {
            get;
            set;
        } = "";

        public int RoundLength
        {
            get
            {
                return ItemCodeUtil.GetItemCodeRoundNum(ItemCode);
            }
        }

        private Single _OpenPrice = 0;
        private Single _HighPrice = 0;
        private Single _LowPrice = 0;
        private Single _ClosePrice = 0;

        public Single OpenPrice { get { return (Single)Math.Round(_OpenPrice, RoundLength); } set { _OpenPrice = value; } }
        public Single HighPrice { get { return (Single)Math.Round(_HighPrice, RoundLength); ; } set { _HighPrice = value; } }
        public Single LowPrice { get { return (Single)Math.Round(_LowPrice, RoundLength); ; } set { _LowPrice = value; } }
        public Single ClosePrice { get { return (Single)Math.Round(_ClosePrice, RoundLength); ; } set { _ClosePrice = value; } }
        public Single Volume { get; set; } = 0;
        public Single CenterPrice
        {
            get
            {
                return (Single)Math.Round((HighPrice + LowPrice) / 2.0f, RoundLength);
            }
        }
        public Single HCenterPrice
        {
            get
            {
                return (Single)Math.Round((HighPrice + CenterPrice) / 2.0f, RoundLength);
            }
        }
        public Single LCenterPrice
        {
            get
            {
                return (Single)Math.Round((CenterPrice + LowPrice) / 2.0f, RoundLength);
            }
        }
        public Single MiddlePrice
        {
            get
            {
                return (Single)Math.Round((OpenPrice + ClosePrice) / 2.0f, RoundLength);
            }
        }
        public Single HeadLength
        {
            get
            {
                if (PlusMinusType == PlusMinusTypeEnum.양)
                {
                    return (Single)Math.Round(Math.Abs(HighPrice - ClosePrice), RoundLength);
                }
                else if (PlusMinusType == PlusMinusTypeEnum.음)
                {
                    return (Single)Math.Round(Math.Abs(HighPrice - OpenPrice), RoundLength);
                }
                else
                {
                    return (Single)Math.Round(Math.Abs(HighPrice - ClosePrice), RoundLength);
                }
            }
        }
        public Single LegLength
        {
            get
            {
                if (PlusMinusType == PlusMinusTypeEnum.양)
                {
                    return (Single)Math.Round(Math.Abs(OpenPrice - LowPrice), RoundLength);
                }
                else if (PlusMinusType == PlusMinusTypeEnum.음)
                {
                    return (Single)Math.Round(Math.Abs(ClosePrice - LowPrice), RoundLength);
                }
                else
                {
                    return (Single)Math.Round(Math.Abs(ClosePrice - LowPrice), RoundLength);
                }
            }
        }
        public Single BodyLength
        {
            get
            {
                return (Single)Math.Round(Math.Abs(OpenPrice - ClosePrice), RoundLength);
            }
        }
        public Single TotalLength
        {
            get
            {
                return (Single)Math.Round(Math.Abs(HighPrice - LowPrice), RoundLength);
            }
        }
        public Single QuantumPrice
        {
            get
            {
                Single v = 0f;
                if (PlusMinusType == PlusMinusTypeEnum.양)
                {
                    v = (Single)Math.Round(OpenPrice - Math.Abs(OpenPrice - ClosePrice), RoundLength);
                    v += LegLength;
                    if (v > OpenPrice) v = OpenPrice;
                }
                else if (PlusMinusType == PlusMinusTypeEnum.음)
                {
                    v = (Single)Math.Round(OpenPrice + Math.Abs(OpenPrice - ClosePrice), RoundLength);
                    v -= HeadLength;
                    if (v < OpenPrice) v = OpenPrice;
                }
                else
                    v = (Single)Math.Round(OpenPrice, RoundLength);

                return v;
            }
        }
        public Single QuantumLowPrice
        {
            get
            {
                if (QuantumPrice > LowPrice)
                    return LowPrice;

                return QuantumPrice;
            }
        }
        public Single QuantumHighPrice
        {
            get
            {
                if (QuantumPrice < HighPrice)
                    return HighPrice;

                return QuantumPrice;
            }
        }
        public Single QuantumBasePrice
        {
            get
            {
                if (PlusMinusType == PlusMinusTypeEnum.양)
                {
                    return (Single)Math.Round(OpenPrice - BodyLength, RoundLength);                  
                }
                else if (PlusMinusType == PlusMinusTypeEnum.음)
                {
                    return (Single)Math.Round(OpenPrice + BodyLength, RoundLength);                  
                }
                else
                    return (Single)Math.Round(OpenPrice, RoundLength);
            }
        }        
        public Single QuantumBaseHighPrice
        {
            get
            {
                if (PlusMinusType == PlusMinusTypeEnum.양)
                {
                    return (Single)Math.Round(OpenPrice + LegLength, RoundLength);
                }
                else if (PlusMinusType == PlusMinusTypeEnum.음)
                {
                    return (Single)Math.Round(QuantumBasePrice + LegLength, RoundLength);
                }
                else
                    return (Single)Math.Round(OpenPrice + LegLength, RoundLength);
            }
        }
        public Single QuantumBaseLowPrice
        {
            get
            {
                if (PlusMinusType == PlusMinusTypeEnum.양)
                {
                    return (Single)Math.Round(QuantumBasePrice - HeadLength, RoundLength);
                }
                else if (PlusMinusType == PlusMinusTypeEnum.음)
                {
                    return (Single)Math.Round(OpenPrice - HeadLength, RoundLength);
                }
                else
                    return (Single)Math.Round(OpenPrice - HeadLength, RoundLength);
            }
        }
        public Single QuantumCenterPrice
        {
            get
            {
                return (Single)Math.Round((QuantumHighPrice + QuantumLowPrice) / 2.0f, RoundLength);
            }
        }
        public Single QuantumMiddlePrice
        {
            get
            {
                return (Single)Math.Round((OpenPrice + QuantumPrice) / 2.0f, RoundLength);
            }
        }
        public Single VikalaPrice
        {
            get
            {
                Single v = 0f;
                if (PlusMinusType == PlusMinusTypeEnum.양)
                {
                    v = (Single)Math.Round(ClosePrice + Math.Abs(OpenPrice - ClosePrice), RoundLength);
                    v -= HeadLength;
                    if (v < ClosePrice) v = ClosePrice;
                }
                else if (PlusMinusType == PlusMinusTypeEnum.음)
                {
                    v = (Single)Math.Round(ClosePrice - Math.Abs(OpenPrice - ClosePrice), RoundLength);
                    v += LegLength;
                    if (v > ClosePrice) v = ClosePrice;
                }
                else
                    v = (Single)Math.Round(OpenPrice, RoundLength);

                return v;
            }
        }
        public Single VikalaLowPrice
        {
            get
            {
                if (VikalaPrice > LowPrice)
                    return LowPrice;
                return VikalaPrice;
            }
        }
        public Single VikalaHighPrice
        {
            get
            {
                if (VikalaPrice < HighPrice)
                    return HighPrice;
                return VikalaPrice;
            }
        }
        public Single VikalaCenterPrice
        {
            get
            {
                return (Single)Math.Round((VikalaHighPrice + VikalaLowPrice) / 2.0f, RoundLength);
            }
        }
        public Single VikalaMiddlePrice
        {
            get
            {
                return (Single)Math.Round((ClosePrice + VikalaPrice) / 2.0f, RoundLength);
            }
        }
        public Single TotalCenterPrice
        {
            get
            {
                if (PlusMinusType == PlusMinusTypeEnum.양)
                {
                    return (Single)Math.Round((VikalaHighPrice + QuantumLowPrice) / 2.0f, RoundLength);
                }
                else
                {
                    return (Single)Math.Round((QuantumHighPrice + VikalaLowPrice) / 2.0f, RoundLength);
                }
            }
        }
        public Single MassPrice
        {
            get
            {
                List<double> list = new List<double>();

                list.Add(OpenPrice);
                list.Add(PriceTick.GetUpPriceOfTick(ItemCode, OpenPrice, 1));
                list.Add(PriceTick.GetUpPriceOfTick(ItemCode, OpenPrice, 2));
                list.Add(PriceTick.GetDownPriceOfTick(ItemCode, OpenPrice, 1));
                list.Add(PriceTick.GetDownPriceOfTick(ItemCode, OpenPrice, 2));

                list.Add(HighPrice);
                list.Add(PriceTick.GetUpPriceOfTick(ItemCode, HighPrice, 1));
                list.Add(PriceTick.GetUpPriceOfTick(ItemCode, HighPrice, 2));
                list.Add(PriceTick.GetDownPriceOfTick(ItemCode, HighPrice, 1));
                list.Add(PriceTick.GetDownPriceOfTick(ItemCode, HighPrice, 2));

                list.Add(LowPrice);
                list.Add(PriceTick.GetUpPriceOfTick(ItemCode, LowPrice, 1));
                list.Add(PriceTick.GetUpPriceOfTick(ItemCode, LowPrice, 2));
                list.Add(PriceTick.GetDownPriceOfTick(ItemCode, LowPrice, 1));
                list.Add(PriceTick.GetDownPriceOfTick(ItemCode, LowPrice, 2));

                list.Add(ClosePrice);
                list.Add(PriceTick.GetUpPriceOfTick(ItemCode, ClosePrice, 1));
                list.Add(PriceTick.GetUpPriceOfTick(ItemCode, ClosePrice, 2));
                list.Add(PriceTick.GetDownPriceOfTick(ItemCode, ClosePrice, 1));
                list.Add(PriceTick.GetDownPriceOfTick(ItemCode, ClosePrice, 2));

                list.Add(QuantumHighPrice);
                list.Add(PriceTick.GetUpPriceOfTick(ItemCode, QuantumHighPrice, 1));
                list.Add(PriceTick.GetUpPriceOfTick(ItemCode, QuantumHighPrice, 2));
                list.Add(PriceTick.GetDownPriceOfTick(ItemCode, QuantumHighPrice, 1));
                list.Add(PriceTick.GetDownPriceOfTick(ItemCode, QuantumHighPrice, 2));

                list.Add(QuantumLowPrice);
                list.Add(PriceTick.GetUpPriceOfTick(ItemCode, QuantumLowPrice, 1));
                list.Add(PriceTick.GetUpPriceOfTick(ItemCode, QuantumLowPrice, 2));
                list.Add(PriceTick.GetDownPriceOfTick(ItemCode, QuantumLowPrice, 1));
                list.Add(PriceTick.GetDownPriceOfTick(ItemCode, QuantumLowPrice, 2));

                list.Add(QuantumPrice);
                list.Add(PriceTick.GetUpPriceOfTick(ItemCode, QuantumPrice, 1));
                list.Add(PriceTick.GetUpPriceOfTick(ItemCode, QuantumPrice, 2));
                list.Add(PriceTick.GetDownPriceOfTick(ItemCode, QuantumPrice, 1));
                list.Add(PriceTick.GetDownPriceOfTick(ItemCode, QuantumPrice, 2));

                list.Add(VikalaPrice);
                list.Add(PriceTick.GetUpPriceOfTick(ItemCode, VikalaPrice, 1));
                list.Add(PriceTick.GetUpPriceOfTick(ItemCode, VikalaPrice, 2));
                list.Add(PriceTick.GetDownPriceOfTick(ItemCode, VikalaPrice, 1));
                list.Add(PriceTick.GetDownPriceOfTick(ItemCode, VikalaPrice, 2));

                list.Add(VikalaHighPrice);
                list.Add(PriceTick.GetUpPriceOfTick(ItemCode, VikalaHighPrice, 1));
                list.Add(PriceTick.GetUpPriceOfTick(ItemCode, VikalaHighPrice, 2));
                list.Add(PriceTick.GetDownPriceOfTick(ItemCode, VikalaHighPrice, 1));
                list.Add(PriceTick.GetDownPriceOfTick(ItemCode, VikalaHighPrice, 2));

                list.Add(VikalaLowPrice);
                list.Add(PriceTick.GetUpPriceOfTick(ItemCode, VikalaLowPrice, 1));
                list.Add(PriceTick.GetUpPriceOfTick(ItemCode, VikalaLowPrice, 2));
                list.Add(PriceTick.GetDownPriceOfTick(ItemCode, VikalaLowPrice, 1));
                list.Add(PriceTick.GetDownPriceOfTick(ItemCode, VikalaLowPrice, 2));

                return (Single)Math.Round(list.Average(), RoundLength);

            }
        }
        public Single HMassPrice
        {
            get
            {
                return CenterPrice;
            }
        }
        public Single QMassPrice
        {
            get
            {
                List<double> list = new List<double>();

                list.Add(QuantumHighPrice);
                list.Add(PriceTick.GetUpPriceOfTick(ItemCode, QuantumHighPrice, 1));
                list.Add(PriceTick.GetUpPriceOfTick(ItemCode, QuantumHighPrice, 2));
                list.Add(PriceTick.GetDownPriceOfTick(ItemCode, QuantumHighPrice, 1));
                list.Add(PriceTick.GetDownPriceOfTick(ItemCode, QuantumHighPrice, 2));

                list.Add(QuantumLowPrice);
                list.Add(PriceTick.GetUpPriceOfTick(ItemCode, QuantumLowPrice, 1));
                list.Add(PriceTick.GetUpPriceOfTick(ItemCode, QuantumLowPrice, 2));
                list.Add(PriceTick.GetDownPriceOfTick(ItemCode, QuantumLowPrice, 1));
                list.Add(PriceTick.GetDownPriceOfTick(ItemCode, QuantumLowPrice, 2));

                list.Add(QuantumPrice);
                list.Add(PriceTick.GetUpPriceOfTick(ItemCode, QuantumPrice, 1));
                list.Add(PriceTick.GetUpPriceOfTick(ItemCode, QuantumPrice, 2));
                list.Add(PriceTick.GetDownPriceOfTick(ItemCode, QuantumPrice, 1));
                list.Add(PriceTick.GetDownPriceOfTick(ItemCode, QuantumPrice, 2));

                return (Single)Math.Round(list.Average(), RoundLength);
            }
        }
        public Single VMassPrice
        {
            get
            {
                List<double> list = new List<double>();

                list.Add(VikalaPrice);
                list.Add(PriceTick.GetUpPriceOfTick(ItemCode, VikalaPrice, 1));
                list.Add(PriceTick.GetUpPriceOfTick(ItemCode, VikalaPrice, 2));
                list.Add(PriceTick.GetDownPriceOfTick(ItemCode, VikalaPrice, 1));
                list.Add(PriceTick.GetDownPriceOfTick(ItemCode, VikalaPrice, 2));

                list.Add(VikalaHighPrice);
                list.Add(PriceTick.GetUpPriceOfTick(ItemCode, VikalaHighPrice, 1));
                list.Add(PriceTick.GetUpPriceOfTick(ItemCode, VikalaHighPrice, 2));
                list.Add(PriceTick.GetDownPriceOfTick(ItemCode, VikalaHighPrice, 1));
                list.Add(PriceTick.GetDownPriceOfTick(ItemCode, VikalaHighPrice, 2));

                list.Add(VikalaLowPrice);
                list.Add(PriceTick.GetUpPriceOfTick(ItemCode, VikalaLowPrice, 1));
                list.Add(PriceTick.GetUpPriceOfTick(ItemCode, VikalaLowPrice, 2));
                list.Add(PriceTick.GetDownPriceOfTick(ItemCode, VikalaLowPrice, 1));
                list.Add(PriceTick.GetDownPriceOfTick(ItemCode, VikalaLowPrice, 2));

                return (Single)Math.Round(list.Average(), RoundLength);
            }
        }

        public DateTime DTime { get; set; }

        public virtual PlusMinusTypeEnum PlusMinusType
        {
            get
            {
                if (OpenPrice < ClosePrice) return PlusMinusTypeEnum.양;
                else if (OpenPrice > ClosePrice) return PlusMinusTypeEnum.음;
                else return PlusMinusTypeEnum.무;
            }
        }

        public virtual PlusMinusTypeEnum PlusMinusType2
        {
            get
            {
                if (CenterPrice < MiddlePrice) return PlusMinusTypeEnum.양;
                else if (CenterPrice > MiddlePrice) return PlusMinusTypeEnum.음;
                else return PlusMinusTypeEnum.무;
            }
        }

        public Single 천가격
        {
            get
            {
                if (PlusMinusType == PlusMinusTypeEnum.양)
                    return VikalaPrice;
                else if (PlusMinusType == PlusMinusTypeEnum.음)
                    return QuantumPrice;
                else
                    return 0;
            }
        }
        public Single 천중
        {
            get
            {
                if (PlusMinusType == PlusMinusTypeEnum.양)
                    return VikalaMiddlePrice;
                else if (PlusMinusType == PlusMinusTypeEnum.음)
                    return QuantumMiddlePrice;
                else
                    return 0;
            }
        }
        public Single 천저인고
        {
            get
            {
                if (PlusMinusType == PlusMinusTypeEnum.양)
                    return ClosePrice;
                else if (PlusMinusType == PlusMinusTypeEnum.음)
                    return OpenPrice;
                else
                    return 0;
            }
        }
        public Single 인중
        {
            get
            {
                return MiddlePrice;
            }
        }
        public Single 인저지고
        {
            get
            {
                if (PlusMinusType == PlusMinusTypeEnum.양)
                    return OpenPrice;
                else if (PlusMinusType == PlusMinusTypeEnum.음)
                    return ClosePrice;
                else
                    return 0;
            }
        }
        public Single 지중
        {
            get
            {
                if (PlusMinusType == PlusMinusTypeEnum.양)
                    return QuantumMiddlePrice;
                else if (PlusMinusType == PlusMinusTypeEnum.음)
                    return VikalaMiddlePrice;
                else
                    return 0;
            }
        }
        public Single 지저
        {
            get
            {
                if (PlusMinusType == PlusMinusTypeEnum.양)
                    return QuantumPrice;
                else if (PlusMinusType == PlusMinusTypeEnum.음)
                    return VikalaPrice;
                else
                    return 0;
            }
        }
        public Single 전자가격P
        {
            get
            {
                if (PlusMinusType == PlusMinusTypeEnum.양)
                    return (Single)Math.Round(ClosePrice + BodyLength, RoundLength);
                else
                    return (Single)Math.Round(OpenPrice + BodyLength, RoundLength); ;

            }
        }
        public Single 전자가격M
        {
            get
            {
                if (PlusMinusType == PlusMinusTypeEnum.음)
                    return (Single)Math.Round(ClosePrice - BodyLength, RoundLength);
                else
                    return (Single)Math.Round(OpenPrice - BodyLength, RoundLength); ;

            }
        }





        public int DiceNum
        {
            get
            {
                double[] anArray = {
                        OpenPrice, HighPrice, LowPrice, ClosePrice
                    //,   QuantumPrice, QuantumHighPrice, QuantumLowPrice
                    //,   VikalaPrice, VikalaHighPrice, VikalaLowPrice
                };
                double[] anArray2 = {
                        OpenPrice, ClosePrice
                    //,   QuantumPrice
                    //,   VikalaPrice
                };
                double highPrice = anArray.Max();
                double lowPrice = anArray.Min();
                double openPrice = anArray2.Min();
                double closePrice = anArray2.Max();

                double totalLength = Math.Round(highPrice - lowPrice, RoundLength);
                double headLength = Math.Round(Math.Abs(highPrice - closePrice), RoundLength);
                double legLength = Math.Round(Math.Abs(lowPrice - openPrice), RoundLength);
                double bodyLength = Math.Round(Math.Abs(openPrice - closePrice), RoundLength);

                int headLengthRate = (int)Math.Round((headLength / totalLength * 100.0), 0);
                int bodyLengthRate = (int)Math.Round((bodyLength / totalLength * 100.0), 0);
                int legLengthRate = (int)Math.Round((legLength / totalLength * 100.0), 0);

                int n = 0;
                try
                {
                    if (bodyLengthRate >= 80) n = 1;
                    else if (headLengthRate >= 40 && legLengthRate >= 40) n = 6;

                    else if (headLengthRate >= 40 && legLengthRate == 0) n = 2;
                    else if (headLengthRate == 0 && legLengthRate >= 40) n = 5;

                    else if (headLengthRate >= 20 && legLengthRate >= 20 && headLengthRate > legLengthRate) n = 3;
                    else if (headLengthRate >= 20 && legLengthRate >= 20 && headLengthRate < legLengthRate) n = 4;
                }
                catch (Exception)
                {
                }
                if (n == 0)
                {

                }
                return n;
            }
        }
    }

    //public abstract class A_HLOC
    //{
    //    public string ItemCode
    //    {
    //        get;
    //        set;
    //    } = "";

    //    public int RoundLength
    //    {
    //        get
    //        {
    //            return ItemCodeUtil.GetItemCodeRoundNum(ItemCode);
    //        }
    //    }

    //    private Single _OpenPrice = 0;
    //    private Single _HighPrice = 0;
    //    private Single _LowPrice  = 0;
    //    private Single _ClosePrice = 0;

    //    public Single OpenPrice { get { return (Single)Math.Round(_OpenPrice, RoundLength); } set { _OpenPrice = value; } }
    //    public Single HighPrice { get { return (Single)Math.Round(_HighPrice, RoundLength); ; } set { _HighPrice = value; } }
    //    public Single LowPrice { get { return (Single)Math.Round(_LowPrice, RoundLength); ; } set { _LowPrice = value; } }
    //    public Single ClosePrice { get { return (Single)Math.Round(_ClosePrice, RoundLength); ; } set { _ClosePrice = value; } }

    //    public Single Volume { get; set; } = 0;

    //    public Single CenterPrice
    //    {
    //        get
    //        {
    //            return (Single)Math.Round((HighPrice + LowPrice) / 2.0f, RoundLength);
    //        }
    //    }

    //    public Single HCenterPrice
    //    {
    //        get
    //        {
    //            return (Single)Math.Round((HighPrice + CenterPrice) / 2.0f, RoundLength);
    //        }
    //    }

    //    public Single LCenterPrice
    //    {
    //        get
    //        {
    //            return (Single)Math.Round((CenterPrice + LowPrice) / 2.0f, RoundLength);
    //        }
    //    }

    //    public Single MiddlePrice
    //    {
    //        get
    //        {
    //            return (Single)Math.Round((OpenPrice + ClosePrice) / 2.0f, RoundLength);
    //        }
    //    }

    //    public Single QuantumPrice
    //    {
    //        get
    //        {
    //            if (PlusMinusType == PlusMinusTypeEnum.양)
    //            {
    //                return (Single)Math.Round(OpenPrice - Math.Abs(OpenPrice - ClosePrice), RoundLength);
    //            }
    //            else if (PlusMinusType == PlusMinusTypeEnum.음)
    //            {
    //                return (Single)Math.Round(OpenPrice + Math.Abs(OpenPrice - ClosePrice), RoundLength);
    //            }
    //            else
    //                return (Single)Math.Round(OpenPrice, RoundLength);
    //        }
    //    }        
    //    public Single QuantumLowPrice
    //    {
    //        get
    //        {
    //            return (Single)Math.Round(OpenPrice - Math.Abs(OpenPrice - HighPrice), RoundLength);
    //        }
    //    }
    //    public Single QuantumHighPrice
    //    {
    //        get
    //        {
    //            return (Single)Math.Round(OpenPrice + Math.Abs(OpenPrice - LowPrice), RoundLength);
    //        }
    //    }        
    //    public Single QuantumCenterPrice
    //    {
    //        get
    //        {
    //            return (Single)Math.Round((QuantumHighPrice + QuantumLowPrice) / 2.0f, RoundLength);
    //        }
    //    }            
    //    public Single QuantumMiddlePrice
    //    {
    //        get
    //        {
    //            return (Single)Math.Round((OpenPrice + QuantumPrice) / 2.0f, RoundLength);
    //        }
    //    }

    //    public Single VikalaPrice
    //    {
    //        get
    //        {
    //            if (PlusMinusType == PlusMinusTypeEnum.양)
    //            {
    //                return (Single)Math.Round(ClosePrice + Math.Abs(OpenPrice - ClosePrice), RoundLength);
    //            }
    //            else if (PlusMinusType == PlusMinusTypeEnum.음)
    //            {
    //                return (Single)Math.Round(ClosePrice - Math.Abs(OpenPrice - ClosePrice), RoundLength);
    //            }
    //            else
    //                return (Single)Math.Round(OpenPrice, RoundLength);
    //        }
    //    }
    //    public Single VikalaLowPrice
    //    {
    //        get
    //        {
    //            return (Single)Math.Round(ClosePrice - Math.Abs(ClosePrice - HighPrice), RoundLength);
    //        }
    //    }
    //    public Single VikalaHighPrice
    //    {
    //        get
    //        {
    //            return (Single)Math.Round(ClosePrice + Math.Abs(ClosePrice - LowPrice), RoundLength);
    //        }
    //    }
    //    public Single VikalaCenterPrice
    //    {
    //        get
    //        {
    //            return (Single)Math.Round((VikalaHighPrice + VikalaLowPrice) / 2.0f, RoundLength);
    //        }
    //    }
    //    public Single VikalaMiddlePrice
    //    {
    //        get
    //        {
    //            return (Single)Math.Round((ClosePrice + VikalaPrice) / 2.0f, RoundLength);
    //        }
    //    }

    //    public Single TotalCenterPrice
    //    {
    //        get
    //        {
    //            if (PlusMinusType == PlusMinusTypeEnum.양)
    //            {
    //                return (Single)Math.Round((VikalaHighPrice + QuantumLowPrice) / 2.0f, RoundLength);
    //            }
    //            else
    //            {
    //                return (Single)Math.Round((QuantumHighPrice + VikalaLowPrice) / 2.0f, RoundLength);
    //            }
    //        }
    //    }

    //    public Single MassPrice
    //    {
    //        get
    //        {
    //            List<double> list = new List<double>();

    //            list.Add(OpenPrice);
    //            list.Add(PriceTick.GetUpPriceOfTick(ItemCode, OpenPrice, 1));
    //            list.Add(PriceTick.GetUpPriceOfTick(ItemCode, OpenPrice, 2));
    //            list.Add(PriceTick.GetDownPriceOfTick(ItemCode, OpenPrice, 1));
    //            list.Add(PriceTick.GetDownPriceOfTick(ItemCode, OpenPrice, 2));

    //            list.Add(HighPrice);
    //            list.Add(PriceTick.GetUpPriceOfTick(ItemCode, HighPrice, 1));
    //            list.Add(PriceTick.GetUpPriceOfTick(ItemCode, HighPrice, 2));
    //            list.Add(PriceTick.GetDownPriceOfTick(ItemCode, HighPrice, 1));
    //            list.Add(PriceTick.GetDownPriceOfTick(ItemCode, HighPrice, 2));

    //            list.Add(LowPrice);
    //            list.Add(PriceTick.GetUpPriceOfTick(ItemCode, LowPrice, 1));
    //            list.Add(PriceTick.GetUpPriceOfTick(ItemCode, LowPrice, 2));
    //            list.Add(PriceTick.GetDownPriceOfTick(ItemCode, LowPrice, 1));
    //            list.Add(PriceTick.GetDownPriceOfTick(ItemCode, LowPrice, 2));

    //            list.Add(ClosePrice);
    //            list.Add(PriceTick.GetUpPriceOfTick(ItemCode, ClosePrice, 1));
    //            list.Add(PriceTick.GetUpPriceOfTick(ItemCode, ClosePrice, 2));
    //            list.Add(PriceTick.GetDownPriceOfTick(ItemCode, ClosePrice, 1));
    //            list.Add(PriceTick.GetDownPriceOfTick(ItemCode, ClosePrice, 2));

    //            list.Add(QuantumHighPrice);
    //            list.Add(PriceTick.GetUpPriceOfTick(ItemCode, QuantumHighPrice, 1));
    //            list.Add(PriceTick.GetUpPriceOfTick(ItemCode, QuantumHighPrice, 2));
    //            list.Add(PriceTick.GetDownPriceOfTick(ItemCode, QuantumHighPrice, 1));
    //            list.Add(PriceTick.GetDownPriceOfTick(ItemCode, QuantumHighPrice, 2));

    //            list.Add(QuantumLowPrice);
    //            list.Add(PriceTick.GetUpPriceOfTick(ItemCode, QuantumLowPrice, 1));
    //            list.Add(PriceTick.GetUpPriceOfTick(ItemCode, QuantumLowPrice, 2));
    //            list.Add(PriceTick.GetDownPriceOfTick(ItemCode, QuantumLowPrice, 1));
    //            list.Add(PriceTick.GetDownPriceOfTick(ItemCode, QuantumLowPrice, 2));

    //            list.Add(QuantumPrice);
    //            list.Add(PriceTick.GetUpPriceOfTick(ItemCode, QuantumPrice, 1));
    //            list.Add(PriceTick.GetUpPriceOfTick(ItemCode, QuantumPrice, 2));
    //            list.Add(PriceTick.GetDownPriceOfTick(ItemCode, QuantumPrice, 1));
    //            list.Add(PriceTick.GetDownPriceOfTick(ItemCode, QuantumPrice, 2));

    //            list.Add(VikalaPrice);
    //            list.Add(PriceTick.GetUpPriceOfTick(ItemCode, VikalaPrice, 1));
    //            list.Add(PriceTick.GetUpPriceOfTick(ItemCode, VikalaPrice, 2));
    //            list.Add(PriceTick.GetDownPriceOfTick(ItemCode, VikalaPrice, 1));
    //            list.Add(PriceTick.GetDownPriceOfTick(ItemCode, VikalaPrice, 2));

    //            list.Add(VikalaHighPrice);
    //            list.Add(PriceTick.GetUpPriceOfTick(ItemCode, VikalaHighPrice, 1));
    //            list.Add(PriceTick.GetUpPriceOfTick(ItemCode, VikalaHighPrice, 2));
    //            list.Add(PriceTick.GetDownPriceOfTick(ItemCode, VikalaHighPrice, 1));
    //            list.Add(PriceTick.GetDownPriceOfTick(ItemCode, VikalaHighPrice, 2));

    //            list.Add(VikalaLowPrice);
    //            list.Add(PriceTick.GetUpPriceOfTick(ItemCode, VikalaLowPrice, 1));
    //            list.Add(PriceTick.GetUpPriceOfTick(ItemCode, VikalaLowPrice, 2));
    //            list.Add(PriceTick.GetDownPriceOfTick(ItemCode, VikalaLowPrice, 1));
    //            list.Add(PriceTick.GetDownPriceOfTick(ItemCode, VikalaLowPrice, 2));

    //            return (Single)Math.Round(list.Average(), RoundLength);

    //        }
    //    }

    //    public Single QMassPrice {
    //        get {

    //            return 0;
    //        }
    //    }

    //    public Single VMassPrice {
    //        get
    //        {
    //            return 0;
    //        }
    //    }

    //    public DateTime DTime { get; set; }

    //    public virtual PlusMinusTypeEnum PlusMinusType
    //    {
    //        get
    //        {
    //            if (OpenPrice < ClosePrice) return PlusMinusTypeEnum.양;
    //            else if (OpenPrice > ClosePrice) return PlusMinusTypeEnum.음;
    //            else return PlusMinusTypeEnum.무;
    //        }
    //    }

    //    public virtual PlusMinusTypeEnum PlusMinusType2
    //    {
    //        get
    //        {
    //            if (CenterPrice < MiddlePrice) return PlusMinusTypeEnum.양;
    //            else if (CenterPrice > MiddlePrice) return PlusMinusTypeEnum.음;
    //            else return PlusMinusTypeEnum.무;
    //        }
    //    }

    //    //public virtual Single GetPlusMass(A_HLOC b)
    //    //{
    //    //    //전종 – 현종
    //    //    //전시 – 현시
    //    //    //전시 – 고
    //    //    //현시 - 고
    //    //    //전종 – 고
    //    //    //현종 – 고

    //    //    Single h = b.HighPrice > HighPrice ? b.HighPrice : HighPrice;

    //    //    Single m1 = Math.Abs(b.ClosePrice - this.ClosePrice);
    //    //    Single m2 = Math.Abs(b.OpenPrice - this.OpenPrice);
    //    //    Single m3 = Math.Abs(b.OpenPrice - h);
    //    //    Single m4 = Math.Abs(this.OpenPrice - h);
    //    //    Single m5 = Math.Abs(b.ClosePrice - h);
    //    //    Single m6 = Math.Abs(this.ClosePrice - h);

    //    //    return (m1 + m2 + m3 + m4 + m5 + m6);
    //    //}

    //    //public virtual Single GetMinusMass(A_HLOC b)
    //    //{
    //    //    //전종 – 현종
    //    //    //전시 – 현시
    //    //    //전시 – 고
    //    //    //현시 - 고
    //    //    //전종 – 고
    //    //    //현종 – 고

    //    //    Single l = b.LowPrice < LowPrice ? b.LowPrice : LowPrice;

    //    //    Single m1 = Math.Abs(b.ClosePrice - this.ClosePrice);
    //    //    Single m2 = Math.Abs(b.OpenPrice - this.OpenPrice);
    //    //    Single m3 = Math.Abs(b.OpenPrice - l);
    //    //    Single m4 = Math.Abs(this.OpenPrice - l);
    //    //    Single m5 = Math.Abs(b.ClosePrice - l);
    //    //    Single m6 = Math.Abs(this.ClosePrice - l);

    //    //    return (m1 + m2 + m3 + m4 + m5 + m6);
    //    //}



    //    public Single 천고
    //    {
    //        get
    //        {
    //            if (PlusMinusType == PlusMinusTypeEnum.양)
    //                return VikalaPrice;
    //            else if (PlusMinusType == PlusMinusTypeEnum.음)
    //                return QuantumPrice;
    //            else
    //                return 0;
    //        }
    //    }
    //    public Single 천중
    //    {
    //        get
    //        {
    //            if (PlusMinusType == PlusMinusTypeEnum.양)
    //                return VikalaMiddlePrice;
    //            else if (PlusMinusType == PlusMinusTypeEnum.음)
    //                return QuantumMiddlePrice;
    //            else
    //                return 0;
    //        }
    //    }
    //    public Single 천저인고
    //    {
    //        get
    //        {
    //            if (PlusMinusType == PlusMinusTypeEnum.양)
    //                return ClosePrice;
    //            else if (PlusMinusType == PlusMinusTypeEnum.음)
    //                return OpenPrice;
    //            else
    //                return 0;
    //        }
    //    }
    //    public Single 인중
    //    {
    //        get
    //        {
    //            return MiddlePrice;
    //        }
    //    }
    //    public Single 인저지고
    //    {
    //        get
    //        {
    //            if (PlusMinusType == PlusMinusTypeEnum.양)
    //                return OpenPrice;
    //            else if (PlusMinusType == PlusMinusTypeEnum.음)
    //                return ClosePrice;
    //            else
    //                return 0;
    //        }
    //    }
    //    public Single 지중
    //    {
    //        get
    //        {
    //            if (PlusMinusType == PlusMinusTypeEnum.양)
    //                return QuantumMiddlePrice;
    //            else if (PlusMinusType == PlusMinusTypeEnum.음)
    //                return VikalaMiddlePrice;
    //            else
    //                return 0;
    //        }
    //    }
    //    public Single 지저
    //    {
    //        get
    //        {
    //            if (PlusMinusType == PlusMinusTypeEnum.양)
    //                return QuantumPrice;
    //            else if (PlusMinusType == PlusMinusTypeEnum.음)
    //                return VikalaPrice;
    //            else
    //                return 0;
    //        }
    //    }
    //}
}
