﻿using OM.Jiva.Chakra.Entities;
using OM.PP.Chakra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OM.Jiva.Chakra.Patterns
{
    public static class PatternUtil
    {
        public static (CandleTimeTypeEnum openType, CandleTimeTypeEnum closeType, CandleTimeTypeEnum highType, CandleTimeTypeEnum lowType) GetCandleTimeType(S_CandleItemData p2, S_CandleItemData p1)
        {
            CandleTimeTypeEnum openType = CandleTimeTypeEnum.무;
            CandleTimeTypeEnum closeType = CandleTimeTypeEnum.무;
            CandleTimeTypeEnum highType = CandleTimeTypeEnum.무;
            CandleTimeTypeEnum lowType = CandleTimeTypeEnum.무;

            if (p2.OpenPrice < p1.OpenPrice) openType = CandleTimeTypeEnum.양;
            else if (p2.OpenPrice > p1.OpenPrice) openType = CandleTimeTypeEnum.음;

            if (p2.ClosePrice < p1.ClosePrice) closeType = CandleTimeTypeEnum.양;
            else if (p2.ClosePrice > p1.ClosePrice) closeType = CandleTimeTypeEnum.음;

            if (p2.HighPrice < p1.HighPrice) highType = CandleTimeTypeEnum.양;
            else if (p2.HighPrice > p1.HighPrice) highType = CandleTimeTypeEnum.음;

            if (p2.ClosePrice < p1.ClosePrice) lowType = CandleTimeTypeEnum.양;
            else if (p2.ClosePrice > p1.ClosePrice) lowType = CandleTimeTypeEnum.음;

            return (openType, closeType, highType, lowType);
        }
        public static CandleSizeTypeEnum GetCandleBodySizeType(S_CandleItemData p2, S_CandleItemData p1)
        {
            CandleSizeTypeEnum type = CandleSizeTypeEnum.NoMatter;

            if (p2.BodyLength < p1.BodyLength) type = CandleSizeTypeEnum.Big;
            else if (p2.BodyLength > p1.BodyLength) type = CandleSizeTypeEnum.Small;

            return type;
        }
        public static CandleSizeTypeEnum GetCandleTotalSizeType(S_CandleItemData p2, S_CandleItemData p1)
        {
            CandleSizeTypeEnum type = CandleSizeTypeEnum.NoMatter;

            if (p2.TotalLength < p1.TotalLength) type = CandleSizeTypeEnum.Big;
            else if (p2.TotalLength > p1.TotalLength) type = CandleSizeTypeEnum.Small;

            return type;
        }

        public static CandlePatternTypeEnum GetCandlePatternType(S_CandleItemData p, List<S_CandleItemData> pList, List<S_CandleItemData> nList)
        {
            //CandlePatternTypeEnum type = CandlePatternTypeEnum.Unknown;

            //double minPPrice = pList.Min((t => t.LowPrice));
            //double maxPPrice = pList.Max((t => t.HighPrice));

            //double minNPrice = nList.Min((t => t.LowPrice));
            //double maxNPrice = nList.Max((t => t.HighPrice));

            //double pDiff = (maxPPrice - minPPrice);
            //double sUPrice = p.ClosePrice + (pDiff * 1.0);
            //double sDPrice = p.ClosePrice - (pDiff * 1.0);
            //double mUPrice = p.ClosePrice + (pDiff * 0.7);
            //double mDPrice = p.ClosePrice - (pDiff * 0.7);
            //double wUPrice = p.ClosePrice + (pDiff * 0.3);
            //double wDPrice = p.ClosePrice - (pDiff * 0.3);

            //if (maxNPrice > sUPrice && minNPrice < sDPrice) type = CandlePatternTypeEnum.StrongSide;
            //else if (maxNPrice > mUPrice && minNPrice < mDPrice) type = CandlePatternTypeEnum.NormalSide;
            //else if (maxNPrice > wUPrice && minNPrice < wDPrice) type = CandlePatternTypeEnum.WeakSide;

            //else if (maxNPrice > sUPrice) type = CandlePatternTypeEnum.StrongUp;
            //else if (minNPrice < sDPrice) type = CandlePatternTypeEnum.StrongDown;

            //else if (maxNPrice > mUPrice) type = CandlePatternTypeEnum.Up;
            //else if (minNPrice < mDPrice) type = CandlePatternTypeEnum.Down;

            //else if (maxNPrice > wUPrice) type = CandlePatternTypeEnum.WeakUp;
            //else if (minNPrice < wDPrice) type = CandlePatternTypeEnum.WeakDown;

            //return type;


            CandlePatternTypeEnum type = CandlePatternTypeEnum.Unknown;

            double minPPrice = p.LowPrice;
            double maxPPrice = p.HighPrice;

            double minNPrice = nList.Min((t => t.LowPrice));
            double maxNPrice = nList.Max((t => t.HighPrice));

            double pDiff = (maxPPrice - minPPrice);
            double sUPrice = p.ClosePrice + (pDiff * 3.0);
            double sDPrice = p.ClosePrice - (pDiff * 3.0);
            double mUPrice = p.ClosePrice + (pDiff * 2.0);
            double mDPrice = p.ClosePrice - (pDiff * 2.0);
            double wUPrice = p.ClosePrice + (pDiff * 1.0);
            double wDPrice = p.ClosePrice - (pDiff * 1.0);

            if (maxNPrice > mUPrice && minNPrice < mDPrice && maxNPrice < sUPrice && minNPrice > sDPrice) 
                type = CandlePatternTypeEnum.StrongSide;         
            else if (maxNPrice > wUPrice && minNPrice < wDPrice && maxNPrice < mUPrice && minNPrice > mDPrice) 
                type = CandlePatternTypeEnum.NormalSide;
            else if (maxNPrice < wUPrice && minNPrice > wDPrice) 
                type = CandlePatternTypeEnum.WeakSide;

            else if (maxNPrice > sUPrice) type = CandlePatternTypeEnum.StrongUp;
            else if (minNPrice < sDPrice) type = CandlePatternTypeEnum.StrongDown;

            else if (maxNPrice > mUPrice) type = CandlePatternTypeEnum.Up;
            else if (minNPrice < mDPrice) type = CandlePatternTypeEnum.Down;

            else if (maxNPrice > wUPrice) type = CandlePatternTypeEnum.WeakUp;
            else if (minNPrice < wDPrice) type = CandlePatternTypeEnum.WeakDown;

            return type;
        }

        public static CandlePatternTypeEnum GetCandlePatternType(S_CandleItemData p, S_CandleItemData p1, S_CandleItemData n1)
        {
            CandlePatternTypeEnum type = CandlePatternTypeEnum.Unknown;

            double minPPrice = p.LowPrice;
            double maxPPrice = p.HighPrice;
            double minNPrice = n1.LowPrice;
            double maxNPrice = n1.HighPrice;

            double pDiff = (maxPPrice - minPPrice);
            double sUPrice = p.ClosePrice + (pDiff * 3.0);
            double sDPrice = p.ClosePrice - (pDiff * 3.0);
            double mUPrice = p.ClosePrice + (pDiff * 2.0);
            double mDPrice = p.ClosePrice - (pDiff * 2.0);
            double wUPrice = p.ClosePrice + (pDiff * 1.0);
            double wDPrice = p.ClosePrice - (pDiff * 1.0);

            if (maxNPrice > sUPrice && minNPrice < sDPrice) type = CandlePatternTypeEnum.StrongSide;
            else if (maxNPrice > mUPrice && minNPrice < mDPrice) type = CandlePatternTypeEnum.NormalSide;
            else if (maxNPrice > wUPrice && minNPrice < wDPrice) type = CandlePatternTypeEnum.WeakSide;

            else if (maxNPrice > sUPrice) type = CandlePatternTypeEnum.StrongUp;
            else if (minNPrice < sDPrice) type = CandlePatternTypeEnum.StrongDown;

            else if (maxNPrice > mUPrice) type = CandlePatternTypeEnum.Up;
            else if (minNPrice < mDPrice) type = CandlePatternTypeEnum.Down;

            else if (maxNPrice > wUPrice) type = CandlePatternTypeEnum.WeakUp;
            else if (minNPrice < wDPrice) type = CandlePatternTypeEnum.WeakDown;

            return type;
        }
        public static CandleForcePatternData GetForcePatternInfo(S_CandleItemData source)
        {
            var data = new Entities.CandleForcePatternData();

            if (source.PreCandleItem == null) return data;
            if (source.PreCandleItem.PreCandleItem == null) return data;
            if (source.PreCandleItem.PreCandleItem.PreCandleItem == null) return data;
            if (source.PreCandleItem.PreCandleItem.PreCandleItem.PreCandleItem == null) return data;

            var p3 = source.PreCandleItem.PreCandleItem.PreCandleItem;
            var p2 = source.PreCandleItem.PreCandleItem;
            var p1 = source.PreCandleItem;
            var p0 = source;

            data.Item = source.ItemCode;
            data.GForceType_P3 = Convert.ToInt32(p3.GForceType).ToString();
            data.GForceType_P2 = Convert.ToInt32(p2.GForceType).ToString();
            data.GForceType_P1 = Convert.ToInt32(p1.GForceType).ToString();
            data.GForceType_P0 = Convert.ToInt32(p0.GForceType).ToString();

            data.EForceType_OC_P3 = Convert.ToInt32(p3.EForceType_OC).ToString();
            data.EForceType_OC_P2 = Convert.ToInt32(p2.EForceType_OC).ToString();
            data.EForceType_OC_P1 = Convert.ToInt32(p1.EForceType_OC).ToString();
            data.EForceType_OC_P0 = Convert.ToInt32(p0.EForceType_OC).ToString();

            data.EForceType_CC_P3 = Convert.ToInt32(p3.EForceType_CC).ToString();
            data.EForceType_CC_P2 = Convert.ToInt32(p2.EForceType_CC).ToString();
            data.EForceType_CC_P1 = Convert.ToInt32(p1.EForceType_CC).ToString();
            data.EForceType_CC_P0 = Convert.ToInt32(p0.EForceType_CC).ToString();

            data.SForceType_O_P3 = Convert.ToInt32(p3.SForceType_O).ToString();
            data.SForceType_O_P2 = Convert.ToInt32(p2.SForceType_O).ToString();
            data.SForceType_O_P1 = Convert.ToInt32(p1.SForceType_O).ToString();
            data.SForceType_O_P0 = Convert.ToInt32(p0.SForceType_O).ToString();

            data.SForceType_H_P3 = Convert.ToInt32(p3.SForceType_H).ToString();
            data.SForceType_H_P2 = Convert.ToInt32(p2.SForceType_H).ToString();
            data.SForceType_H_P1 = Convert.ToInt32(p1.SForceType_H).ToString();
            data.SForceType_H_P0 = Convert.ToInt32(p0.SForceType_H).ToString();

            data.SForceType_L_P3 = Convert.ToInt32(p3.SForceType_L).ToString();
            data.SForceType_L_P2 = Convert.ToInt32(p2.SForceType_L).ToString();
            data.SForceType_L_P1 = Convert.ToInt32(p1.SForceType_L).ToString();
            data.SForceType_L_P0 = Convert.ToInt32(p0.SForceType_L).ToString();

            data.SForceType_C_P3 = Convert.ToInt32(p3.SForceType_C).ToString();
            data.SForceType_C_P2 = Convert.ToInt32(p2.SForceType_C).ToString();
            data.SForceType_C_P1 = Convert.ToInt32(p1.SForceType_C).ToString();
            data.SForceType_C_P0 = Convert.ToInt32(p0.SForceType_C).ToString();

            data.WForceType_T_P3 = Convert.ToInt32(p3.WForceType_T).ToString();
            data.WForceType_T_P2 = Convert.ToInt32(p2.WForceType_T).ToString();
            data.WForceType_T_P1 = Convert.ToInt32(p1.WForceType_T).ToString();
            data.WForceType_T_P0 = Convert.ToInt32(p0.WForceType_T).ToString();

            data.WForceType_H_P3 = Convert.ToInt32(p3.WForceType_H).ToString();
            data.WForceType_H_P2 = Convert.ToInt32(p2.WForceType_H).ToString();
            data.WForceType_H_P1 = Convert.ToInt32(p1.WForceType_H).ToString();
            data.WForceType_H_P0 = Convert.ToInt32(p0.WForceType_H).ToString();

            data.WForceType_B_P3 = Convert.ToInt32(p3.WForceType_B).ToString();
            data.WForceType_B_P2 = Convert.ToInt32(p2.WForceType_B).ToString();
            data.WForceType_B_P1 = Convert.ToInt32(p1.WForceType_B).ToString();
            data.WForceType_B_P0 = Convert.ToInt32(p0.WForceType_B).ToString();

            data.WForceType_L_P3 = Convert.ToInt32(p3.WForceType_L).ToString();
            data.WForceType_L_P2 = Convert.ToInt32(p2.WForceType_L).ToString();
            data.WForceType_L_P1 = Convert.ToInt32(p1.WForceType_L).ToString();
            data.WForceType_L_P0 = Convert.ToInt32(p0.WForceType_L).ToString();

            data.StartDT = p0.DTime;
            data.EndDT = p0.DTime;

            return data;
        }
        public static CandlePatternData GetCandlePatternInfo(S_CandleItemData source)
        {
            var data = new Entities.CandlePatternData();

            if (source.PreCandleItem == null) return data;
            if (source.PreCandleItem.PreCandleItem == null) return data;
            if (source.PreCandleItem.PreCandleItem.PreCandleItem == null) return data;

            var p3 = source.PreCandleItem.PreCandleItem.PreCandleItem;
            var p2 = source.PreCandleItem.PreCandleItem;
            var p1 = source.PreCandleItem;
            var p0 = source;

            data.Item = source.ItemCode;
            data.PlusMinusType_P3 = Convert.ToInt32(p3.PlusMinusType).ToString();
            data.PlusMinusType_P2 = Convert.ToInt32(p2.PlusMinusType).ToString();
            data.PlusMinusType_P1 = Convert.ToInt32(p1.PlusMinusType).ToString();
            data.PlusMinusType_P0 = Convert.ToInt32(p0.PlusMinusType).ToString();

            data.CandleSpaceType_P3 = Convert.ToInt32(p3.SpaceType_C).ToString();
            data.CandleSpaceType_P2 = Convert.ToInt32(p2.SpaceType_C).ToString();
            data.CandleSpaceType_P1 = Convert.ToInt32(p1.SpaceType_C).ToString();
            data.CandleSpaceType_P0 = Convert.ToInt32(p0.SpaceType_C).ToString();

            var timeType = PatternUtil.GetCandleTimeType(p2, p1);
            data.CandleTimeType_O_P21 = Convert.ToInt32(timeType.openType).ToString();
            data.CandleTimeType_C_P21 = Convert.ToInt32(timeType.closeType).ToString();
            data.CandleTimeType_H_P21 = Convert.ToInt32(timeType.highType).ToString();
            data.CandleTimeType_L_P21 = Convert.ToInt32(timeType.lowType).ToString();

            timeType = PatternUtil.GetCandleTimeType(p1, p0);
            data.CandleTimeType_O_P10 = Convert.ToInt32(timeType.openType).ToString();
            data.CandleTimeType_C_P10 = Convert.ToInt32(timeType.closeType).ToString();
            data.CandleTimeType_H_P10 = Convert.ToInt32(timeType.highType).ToString();
            data.CandleTimeType_L_P10 = Convert.ToInt32(timeType.lowType).ToString();

            timeType = PatternUtil.GetCandleTimeType(p2, p0);
            data.CandleTimeType_O_P20 = Convert.ToInt32(timeType.openType).ToString();
            data.CandleTimeType_C_P20 = Convert.ToInt32(timeType.closeType).ToString();
            data.CandleTimeType_H_P20 = Convert.ToInt32(timeType.highType).ToString();
            data.CandleTimeType_L_P20 = Convert.ToInt32(timeType.lowType).ToString();

            timeType = PatternUtil.GetCandleTimeType(p3, p2);
            data.CandleTimeType_O_P32 = Convert.ToInt32(timeType.openType).ToString();
            data.CandleTimeType_C_P32 = Convert.ToInt32(timeType.closeType).ToString();
            data.CandleTimeType_H_P32 = Convert.ToInt32(timeType.highType).ToString();
            data.CandleTimeType_L_P32 = Convert.ToInt32(timeType.lowType).ToString();

            timeType = PatternUtil.GetCandleTimeType(p3, p1);
            data.CandleTimeType_O_P31 = Convert.ToInt32(timeType.openType).ToString();
            data.CandleTimeType_C_P31 = Convert.ToInt32(timeType.closeType).ToString();
            data.CandleTimeType_H_P31 = Convert.ToInt32(timeType.highType).ToString();
            data.CandleTimeType_L_P31 = Convert.ToInt32(timeType.lowType).ToString();

            timeType = PatternUtil.GetCandleTimeType(p3, p0);
            data.CandleTimeType_O_P30 = Convert.ToInt32(timeType.openType).ToString();
            data.CandleTimeType_C_P30 = Convert.ToInt32(timeType.closeType).ToString();
            data.CandleTimeType_H_P30 = Convert.ToInt32(timeType.highType).ToString();
            data.CandleTimeType_L_P30 = Convert.ToInt32(timeType.lowType).ToString();

            var sizeType = PatternUtil.GetCandleBodySizeType(p2, p1);
            data.CandleSizeType_B_P21 = Convert.ToInt32(sizeType).ToString();
            sizeType = PatternUtil.GetCandleBodySizeType(p1, p0);
            data.CandleSizeType_B_P10 = Convert.ToInt32(sizeType).ToString();
            sizeType = PatternUtil.GetCandleBodySizeType(p2, p0);
            data.CandleSizeType_B_P20 = Convert.ToInt32(sizeType).ToString();

            sizeType = PatternUtil.GetCandleBodySizeType(p3, p2);
            data.CandleSizeType_B_P32 = Convert.ToInt32(sizeType).ToString();
            sizeType = PatternUtil.GetCandleBodySizeType(p3, p1);
            data.CandleSizeType_B_P31 = Convert.ToInt32(sizeType).ToString();
            sizeType = PatternUtil.GetCandleBodySizeType(p3, p0);
            data.CandleSizeType_B_P30 = Convert.ToInt32(sizeType).ToString();

            sizeType = PatternUtil.GetCandleTotalSizeType(p2, p1);
            data.CandleSizeType_T_P21 = Convert.ToInt32(sizeType).ToString();
            sizeType = PatternUtil.GetCandleTotalSizeType(p1, p0);
            data.CandleSizeType_T_P10 = Convert.ToInt32(sizeType).ToString();
            sizeType = PatternUtil.GetCandleTotalSizeType(p2, p0);
            data.CandleSizeType_T_P20 = Convert.ToInt32(sizeType).ToString();

            sizeType = PatternUtil.GetCandleTotalSizeType(p3, p2);
            data.CandleSizeType_T_P32 = Convert.ToInt32(sizeType).ToString();
            sizeType = PatternUtil.GetCandleTotalSizeType(p3, p1);
            data.CandleSizeType_T_P31 = Convert.ToInt32(sizeType).ToString();
            sizeType = PatternUtil.GetCandleTotalSizeType(p3, p0);
            data.CandleSizeType_T_P30 = Convert.ToInt32(sizeType).ToString();

            data.StartDT = p0.DTime;
            data.EndDT = p0.DTime;

            return data;
        }

        public static CandlePattern_Four GetFourPatternInfo(S_CandleItemData source)
        {
            var data = new Entities.CandlePattern_Four();

            if (source.PreCandleItem == null) return data;
            if (source.PreCandleItem.PreCandleItem == null) return data;
            if (source.PreCandleItem.PreCandleItem.PreCandleItem == null) return data;

            var p3 = source.PreCandleItem.PreCandleItem.PreCandleItem;
            var p2 = source.PreCandleItem.PreCandleItem;
            var p1 = source.PreCandleItem;
            var p0 = source;

            data.Item = source.ItemCode;
            data.PlusMinusType_P3 = Convert.ToInt32(p3.PlusMinusType).ToString();
            data.PlusMinusType_P2 = Convert.ToInt32(p2.PlusMinusType).ToString();
            data.PlusMinusType_P1 = Convert.ToInt32(p1.PlusMinusType).ToString();
            data.PlusMinusType_P0 = Convert.ToInt32(p0.PlusMinusType).ToString();

            data.CandleSpaceType_P3 = Convert.ToInt32(p3.SpaceType_C).ToString();
            data.CandleSpaceType_P2 = Convert.ToInt32(p2.SpaceType_C).ToString();
            data.CandleSpaceType_P1 = Convert.ToInt32(p1.SpaceType_C).ToString();
            data.CandleSpaceType_P0 = Convert.ToInt32(p0.SpaceType_C).ToString();

            var timeType = PatternUtil.GetCandleTimeType(p2, p1);
            data.CandleTimeType_O_P21 = Convert.ToInt32(timeType.openType).ToString();
            data.CandleTimeType_C_P21 = Convert.ToInt32(timeType.closeType).ToString();
            data.CandleTimeType_H_P21 = Convert.ToInt32(timeType.highType).ToString();
            data.CandleTimeType_L_P21 = Convert.ToInt32(timeType.lowType).ToString();

            timeType = PatternUtil.GetCandleTimeType(p1, p0);
            data.CandleTimeType_O_P10 = Convert.ToInt32(timeType.openType).ToString();
            data.CandleTimeType_C_P10 = Convert.ToInt32(timeType.closeType).ToString();
            data.CandleTimeType_H_P10 = Convert.ToInt32(timeType.highType).ToString();
            data.CandleTimeType_L_P10 = Convert.ToInt32(timeType.lowType).ToString();

            timeType = PatternUtil.GetCandleTimeType(p2, p0);
            data.CandleTimeType_O_P20 = Convert.ToInt32(timeType.openType).ToString();
            data.CandleTimeType_C_P20 = Convert.ToInt32(timeType.closeType).ToString();
            data.CandleTimeType_H_P20 = Convert.ToInt32(timeType.highType).ToString();
            data.CandleTimeType_L_P20 = Convert.ToInt32(timeType.lowType).ToString();

            timeType = PatternUtil.GetCandleTimeType(p3, p2);
            data.CandleTimeType_O_P32 = Convert.ToInt32(timeType.openType).ToString();
            data.CandleTimeType_C_P32 = Convert.ToInt32(timeType.closeType).ToString();
            data.CandleTimeType_H_P32 = Convert.ToInt32(timeType.highType).ToString();
            data.CandleTimeType_L_P32 = Convert.ToInt32(timeType.lowType).ToString();
            
            timeType = PatternUtil.GetCandleTimeType(p3, p1);
            data.CandleTimeType_O_P31 = Convert.ToInt32(timeType.openType).ToString();
            data.CandleTimeType_C_P31 = Convert.ToInt32(timeType.closeType).ToString();
            data.CandleTimeType_H_P31 = Convert.ToInt32(timeType.highType).ToString();
            data.CandleTimeType_L_P31 = Convert.ToInt32(timeType.lowType).ToString();
            
            timeType = PatternUtil.GetCandleTimeType(p3, p0);
            data.CandleTimeType_O_P30 = Convert.ToInt32(timeType.openType).ToString();
            data.CandleTimeType_C_P30 = Convert.ToInt32(timeType.closeType).ToString();
            data.CandleTimeType_H_P30 = Convert.ToInt32(timeType.highType).ToString();
            data.CandleTimeType_L_P30 = Convert.ToInt32(timeType.lowType).ToString();

            var sizeType = PatternUtil.GetCandleBodySizeType(p2, p1);
            data.CandleSizeType_B_P21 = Convert.ToInt32(sizeType).ToString();
            sizeType = PatternUtil.GetCandleBodySizeType(p1, p0);
            data.CandleSizeType_B_P10 = Convert.ToInt32(sizeType).ToString();
            sizeType = PatternUtil.GetCandleBodySizeType(p2, p0);
            data.CandleSizeType_B_P20 = Convert.ToInt32(sizeType).ToString();

            sizeType = PatternUtil.GetCandleBodySizeType(p3, p2);
            data.CandleSizeType_B_P32 = Convert.ToInt32(sizeType).ToString();
            sizeType = PatternUtil.GetCandleBodySizeType(p3, p1);
            data.CandleSizeType_B_P31 = Convert.ToInt32(sizeType).ToString();
            sizeType = PatternUtil.GetCandleBodySizeType(p3, p0);
            data.CandleSizeType_B_P30 = Convert.ToInt32(sizeType).ToString();

            sizeType = PatternUtil.GetCandleTotalSizeType(p2, p1);
            data.CandleSizeType_T_P21 = Convert.ToInt32(sizeType).ToString();
            sizeType = PatternUtil.GetCandleTotalSizeType(p1, p0);
            data.CandleSizeType_T_P10 = Convert.ToInt32(sizeType).ToString();
            sizeType = PatternUtil.GetCandleTotalSizeType(p2, p0);
            data.CandleSizeType_T_P20 = Convert.ToInt32(sizeType).ToString();

            sizeType = PatternUtil.GetCandleTotalSizeType(p3, p2);
            data.CandleSizeType_T_P32 = Convert.ToInt32(sizeType).ToString();
            sizeType = PatternUtil.GetCandleTotalSizeType(p3, p1);
            data.CandleSizeType_T_P31 = Convert.ToInt32(sizeType).ToString();
            sizeType = PatternUtil.GetCandleTotalSizeType(p3, p0);
            data.CandleSizeType_T_P30 = Convert.ToInt32(sizeType).ToString();

            data.StartDT = p3.DTime;
            data.EndDT = p0.DTime;

            return data;
        }

        public static CandlePattern_Three GetThreePatternInfo(S_CandleItemData source)
        {
            var data = new Entities.CandlePattern_Three();

            if (source.PreCandleItem == null) return data;
            if (source.PreCandleItem.PreCandleItem == null) return data;
         
            var p2 = source.PreCandleItem.PreCandleItem;
            var p1 = source.PreCandleItem;
            var p0 = source;
                       
            data.Item = source.ItemCode;
            data.PlusMinusType_P2 = Convert.ToInt32(p2.PlusMinusType).ToString();
            data.PlusMinusType_P1 = Convert.ToInt32(p1.PlusMinusType).ToString();
            data.PlusMinusType_P0 = Convert.ToInt32(p0.PlusMinusType).ToString();
            data.CandleSpaceType_P2 = Convert.ToInt32(p2.SpaceType_C).ToString();
            data.CandleSpaceType_P1 = Convert.ToInt32(p1.SpaceType_C).ToString();
            data.CandleSpaceType_P0 = Convert.ToInt32(p0.SpaceType_C).ToString();

            var timeType = PatternUtil.GetCandleTimeType(p2, p1);
            data.CandleTimeType_O_P21 = Convert.ToInt32(timeType.openType).ToString();
            data.CandleTimeType_C_P21 = Convert.ToInt32(timeType.closeType).ToString();
            data.CandleTimeType_H_P21 = Convert.ToInt32(timeType.highType).ToString();
            data.CandleTimeType_L_P21 = Convert.ToInt32(timeType.lowType).ToString();

            timeType = PatternUtil.GetCandleTimeType(p1, p0);
            data.CandleTimeType_O_P10 = Convert.ToInt32(timeType.openType).ToString();
            data.CandleTimeType_C_P10 = Convert.ToInt32(timeType.closeType).ToString();
            data.CandleTimeType_H_P10 = Convert.ToInt32(timeType.highType).ToString();
            data.CandleTimeType_L_P10 = Convert.ToInt32(timeType.lowType).ToString();

            timeType = PatternUtil.GetCandleTimeType(p2, p0);
            data.CandleTimeType_O_P20 = Convert.ToInt32(timeType.openType).ToString();
            data.CandleTimeType_C_P20 = Convert.ToInt32(timeType.closeType).ToString();
            data.CandleTimeType_H_P20 = Convert.ToInt32(timeType.highType).ToString();
            data.CandleTimeType_L_P20 = Convert.ToInt32(timeType.lowType).ToString();

            var sizeType = PatternUtil.GetCandleBodySizeType(p2, p1);
            data.CandleSizeType_B_P21 = Convert.ToInt32(sizeType).ToString();
            sizeType = PatternUtil.GetCandleBodySizeType(p1, p0);
            data.CandleSizeType_B_P10 = Convert.ToInt32(sizeType).ToString();
            sizeType = PatternUtil.GetCandleBodySizeType(p2, p0);
            data.CandleSizeType_B_P20 = Convert.ToInt32(sizeType).ToString();

            sizeType = PatternUtil.GetCandleTotalSizeType(p2, p1);
            data.CandleSizeType_T_P21 = Convert.ToInt32(sizeType).ToString();
            sizeType = PatternUtil.GetCandleTotalSizeType(p1, p0);
            data.CandleSizeType_T_P10 = Convert.ToInt32(sizeType).ToString();
            sizeType = PatternUtil.GetCandleTotalSizeType(p2, p0);
            data.CandleSizeType_T_P20 = Convert.ToInt32(sizeType).ToString();

            data.StartDT = p2.DTime;
            data.EndDT = p0.DTime;

            return data;
        }

        public static CandlePattern_Two GetTwoPatternInfo(S_CandleItemData source)
        {
            var data = new Entities.CandlePattern_Two();

            if (source.PreCandleItem == null) return data;
         
            var p1 = source.PreCandleItem;
            var p0 = source;

            data.Item = source.ItemCode;
            data.PlusMinusType_P1 = Convert.ToInt32(p1.PlusMinusType).ToString();
            data.PlusMinusType_P0 = Convert.ToInt32(p0.PlusMinusType).ToString();
            data.CandleSpaceType_P1 = Convert.ToInt32(p1.SpaceType_C).ToString();
            data.CandleSpaceType_P0 = Convert.ToInt32(p0.SpaceType_C).ToString();
                     
            var timeType = PatternUtil.GetCandleTimeType(p1, p0);
            data.CandleTimeType_O_P10 = Convert.ToInt32(timeType.openType).ToString();
            data.CandleTimeType_C_P10 = Convert.ToInt32(timeType.closeType).ToString();
            data.CandleTimeType_H_P10 = Convert.ToInt32(timeType.highType).ToString();
            data.CandleTimeType_L_P10 = Convert.ToInt32(timeType.lowType).ToString();

            var sizeType = PatternUtil.GetCandleBodySizeType(p1, p0);
            data.CandleSizeType_B_P10 = Convert.ToInt32(sizeType).ToString();
           
            sizeType = PatternUtil.GetCandleTotalSizeType(p1, p0);
            data.CandleSizeType_T_P10 = Convert.ToInt32(sizeType).ToString();
         
            data.StartDT = p1.DTime;
            data.EndDT = p0.DTime;

            return data;
        }

    }
}
