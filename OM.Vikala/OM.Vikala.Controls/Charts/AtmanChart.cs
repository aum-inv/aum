﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OM.PP.Chakra;
using System.Windows.Forms.DataVisualization.Charting;
using OM.Lib.Base.Utils;
using OM.Vikala.Controls.Properties;
using OM.Lib.Base.Enums;
using OM.Lib.Base;
using OM.Jiva.Chakra.Patterns;

namespace OM.Vikala.Controls.Charts
{
    public partial class AtmanChart : BaseChartControl
    {
        List<double> pmForceList = new List<double>();

        public override CandleChartTypeEnum CandleChartType
        {
            get;
            set;
        } = CandleChartTypeEnum.기본;
                       
        public List<T_QuantumItemData> ChartData
        {
            get;
            set;
        }
        public List<T_QuantumItemData> ChartDataSub
        {
            get;
            set;
        }
        public List<T_QuantumItemData> ChartDataSub2
        {
            get;
            set;
        }

        public override void InitializeControl()
        {            
            //if (IsShowXLine) createXYLineAnnotation();
            //if (IsShowYLine) createYXLineAnnotation();            
        }

        public BaseCandleChartTypeEnum BaseCandleChartType
        {
            get;
            set;
        } = BaseCandleChartTypeEnum.인;

        public void LoadData(string itemCode = ""
           , List<T_QuantumItemData> chartData = null
           , List<T_QuantumItemData> chartDataSub = null
           , List<T_QuantumItemData> chartDataSub2 = null
           , Lib.Base.Enums.TimeIntervalEnum timeInterval = Lib.Base.Enums.TimeIntervalEnum.Day)
        {
            TimeInterval = timeInterval;
            ItemCode = itemCode;
            ChartData = chartData;
            ChartDataSub = chartDataSub;
            ChartDataSub2 = chartDataSub2;

            TotalPointCount = ChartData.Count - 4;

            this.Invoke(new MethodInvoker(() => {
                Reset();
                View();                
            }));
        }

        List<SmartCandleData> smartDataList = new List<SmartCandleData>();
        List<WisdomCandleData> wisdomDataList = new List<WisdomCandleData>();
        List<SmartCandleData> smartBDataList = new List<SmartCandleData>();
        List<WisdomCandleData> wisdomBDataList = new List<WisdomCandleData>();

        List<double> resistanceList = new List<double>();
        List<double> supportList = new List<double>(); 
        List<double> resistanceAvgList = new List<double>();
        List<double> supportAvgList = new List<double>();
        public AtmanChart() 
        {
            InitializeComponent();
            base.SetChartControl(chart, hScrollBar, trackBar);
        }
       
        public override void View()
        {
            pnlScroll.Visible = IsAutoScrollX;
            if (ChartData == null) return;

            resistanceList.Clear();
            supportList.Clear();
            resistanceAvgList.Clear();
            supportAvgList.Clear();

            pmForceList.Clear();
                       
            PPUtils.SetRangeHighLowPrice(ChartData, 18);
            PPUtils.SetRangeHighLowPrice(ChartDataSub, 18);

            //resistanceList.Add(PPUtils.GetResistancePrice(pList, 3));
            //resistanceList.Add(PPUtils.GetResistancePrice(mList, 3));
            //supportList.Add(PPUtils.GetSupportPrice(pList, 3));
            //supportList.Add(PPUtils.GetSupportPrice(mList, 3));
            //resistanceAvgList.Add(PPUtils.GetResistancePrice(ChartDataSub, 3));
            //supportAvgList.Add(PPUtils.GetSupportPrice(ChartDataSub, 3));

            smartDataList.Clear(); 
            wisdomDataList.Clear(); 
            smartBDataList.Clear();
            wisdomBDataList.Clear();

            SmartCandleData preSmartData = null; 
            WisdomCandleData preWisdomData = null;           
            for (int i = 0; i < ChartDataSub.Count; i++)
            {
                var cData = ChartDataSub[i];
                SmartCandleData smartData = new SmartCandleData(cData.ItemCode, cData.OpenPrice, cData.HighPrice, cData.LowPrice, cData.ClosePrice, cData.Volume, cData.DTime, preSmartData);
                smartDataList.Add(smartData);
                preSmartData = smartData;
                WisdomCandleData wisdomData = new WisdomCandleData(cData.ItemCode, cData.OpenPrice, cData.HighPrice, cData.LowPrice, cData.ClosePrice, cData.Volume, cData.DTime, preWisdomData);
                wisdomDataList.Add(wisdomData);
                preWisdomData = wisdomData;
            }

            preSmartData = null;
            preWisdomData = null;
            for (int i = 0; i < ChartDataSub2.Count; i++)
            {
                var cData = ChartDataSub2[i];
                SmartCandleData smartData = new SmartCandleData(cData.ItemCode, cData.OpenPrice, cData.HighPrice, cData.LowPrice, cData.ClosePrice, cData.Volume, cData.DTime, preSmartData);
                smartBDataList.Add(smartData);
                preSmartData = smartData;
                WisdomCandleData wisdomData = new WisdomCandleData(cData.ItemCode, cData.OpenPrice, cData.HighPrice, cData.LowPrice, cData.ClosePrice, cData.Volume, cData.DTime, preWisdomData);
                wisdomBDataList.Add(wisdomData);
                preWisdomData = wisdomData;
            }

            double preSPrice1 = 0;
            double preSPrice2 = 0;
            double preSPrice3 = 0;

            double nextSPrice1 = 0;
            double nextSPrice2 = 0;
            double nextSPrice3 = 0;

            double preMassAvg = 0;
            double preQuantumAvg = 0;

            int uItemCount = 0;
            int dItemCount = 0;
            LimitedList<double> dList1 = new LimitedList<double>(5);
            LimitedList<double> dList2 = new LimitedList<double>(5);

            ThreeCandlePattern threePatttern = new ThreeCandlePattern();

            double pmForce = 0;
            double prePMForce = 0;
            pmForceList.Add(0);
            pmForceList.Add(0);
            pmForceList.Add(0);
            pmForceList.Add(0);
            for (int i = 4; i < ChartData.Count; i++)
            {
                var item = ChartData[i];
                var itemAvg = ChartDataSub[i];
                var itemAvg2 = ChartDataSub2[i];
                var smart = smartDataList[i];
                var wisdom = wisdomDataList[i];
                var smartB = smartBDataList[i];
                var wisdomB = wisdomBDataList[i];

                dList1.Insert((smart.Variance_ChartPrice1 + smart.Variance_ChartPrice2 + smart.Variance_ChartPrice3) / 3.0);
                dList2.Insert(smart.Variance_ChartPrice3);
                if ((i + 1) < ChartData.Count)
                {
                    var nsmart = smartDataList[i + 1];
                    nextSPrice1 = nsmart.Variance_ChartPrice1;
                    nextSPrice2 = nsmart.Variance_ChartPrice2;
                    nextSPrice3 = nsmart.Variance_ChartPrice3;
                }
                else 
                {
                    nextSPrice1 = 0;
                    nextSPrice2 = 0;
                    nextSPrice3 = 0;
                }

                int idx = chart.Series["candleBasic"].Points.AddXY(item.DTime, item.HighPrice, item.LowPrice, item.OpenPrice, item.ClosePrice);
                string diceChar = item.GetSpaceType(item);
                //string diceChar = getDiceChar(itemAvg);
                chart.Series["candleBasic"].Points[idx].Label = diceChar;

                //chart.Series["lineElectronForce1"].Points.AddXY(item.DTime, item.T_ElectronForceAvg);
                chart.Series["lineElectronForce1"].Points.AddXY(itemAvg.DTime, itemAvg.T_ElectronForceAvg);
                chart.Series["lineElectronForce2"].Points.AddXY(itemAvg.DTime, 0);

                if (item.VirtualData)
                    SetDataPointColor(chart.Series["candleBasic"].Points[idx], Color.Black, Color.Transparent, Color.Black, 1);

                if (diceChar == "★") 
                    chart.Series["candleBasic"].Points[idx].LabelForeColor = Color.Red;

                chart.Series["lineMess"].Points.AddXY(itemAvg.DTime, itemAvg.T_MassAvg);
                chart.Series["lineQuantum"].Points.AddXY(itemAvg.DTime, itemAvg.T_QuantumAvg);

                chart.Series["lineMessHL"].Points.AddXY(itemAvg.DTime
                    , itemAvg.T_MassAvg + (Math.Abs(itemAvg.T_MassAvg - itemAvg.HighPrice) - Math.Abs(itemAvg.T_MassAvg - itemAvg.LowPrice)));

                chart.Series["lineCandleRangeH"].Points.AddXY(item.DTime, Math.Round(item.RangeHighPrice, RoundLength));
                chart.Series["lineCandleRangeL"].Points.AddXY(item.DTime, Math.Round(item.RangeLowPrice, RoundLength));

                chart.Series["candleAverage"].Points.AddXY(itemAvg.DTime, itemAvg.HighPrice, itemAvg.LowPrice, itemAvg.OpenPrice, itemAvg.ClosePrice);
                diceChar = itemAvg.GetSpaceType(itemAvg);
                chart.Series["candleAverage"].Points[idx].Label = diceChar;
                if (diceChar == "★")
                    chart.Series["candleAverage"].Points[idx].LabelForeColor = Color.Red;

                chart.Series["lineCandleAvgRangeH"].Points.AddXY(itemAvg.DTime, Math.Round(itemAvg.RangeHighPrice, RoundLength));
                chart.Series["lineCandleAvgRangeL"].Points.AddXY(itemAvg.DTime, Math.Round(itemAvg.RangeLowPrice, RoundLength));
              
                chart.Series["candleBAverage"].Points.AddXY(itemAvg2.DTime, itemAvg2.HighPrice, itemAvg2.LowPrice, itemAvg2.OpenPrice, itemAvg2.ClosePrice);

                chart.Series["lineSmartEnergy1"].Points.AddXY(smart.DTime, smart.Variance_ChartPrice1);
                chart.Series["lineSmartEnergy2"].Points.AddXY(smart.DTime, smart.Variance_ChartPrice2);
                chart.Series["lineSmartEnergy3"].Points.AddXY(smart.DTime, smart.Variance_ChartPrice3);

                chart.Series["lineSmartAvg1"].Points.AddXY(smart.DTime, Math.Round(dList1.Average(), RoundLength));
                chart.Series["lineSmartAvg2"].Points.AddXY(smart.DTime, Math.Round(dList2.Average(), RoundLength));

                chart.Series["lineSmartZero"].Points.AddXY(smart.DTime, 0);

                chart.Series["lineSmartBEnergy2"].Points.AddXY(smartB.DTime, smartB.Variance_ChartPrice2);
                chart.Series["lineWisdomBEnergy"].Points.AddXY(wisdomB.DTime, wisdomB.Variance_ChartPrice);
                
                var bitem = ChartData[i - 1];
                chart.Series["stackHL"].Points.AddXY(item.DTime, item.TotalLength);
                chart.Series["stackHL2"].Points.AddXY(item.DTime, (bitem.TotalLength - item.TotalLength) <=0 ? 0 : (bitem.TotalLength - item.TotalLength));

                if (itemAvg.PlusMinusType == PlusMinusTypeEnum.양)
                    pmForce += itemAvg.HeadLength;
                else if (itemAvg.PlusMinusType == PlusMinusTypeEnum.음)
                    pmForce -= itemAvg.LegLength;
                //else
                //{
                //    if (itemAvg.PreCandleItem.PlusMinusType == PlusMinusTypeEnum.양)
                //        pmForce -= itemAvg.PreCandleItem.BodyLength;
                //    else if (itemAvg.PreCandleItem.PlusMinusType == PlusMinusTypeEnum.음)
                //        pmForce += itemAvg.PreCandleItem.BodyLength;
                //}

                chart.Series["linePlusMinusForce"].Points.AddXY(itemAvg.DTime, pmForce);
                chart.Series["linePlusMinusZero"].Points.AddXY(itemAvg.DTime, 0);
                if (pmForce > prePMForce)
                    chart.Series["linePlusMinusForce"].Points[idx].Color = Color.Red;
                else if (pmForce < prePMForce)
                    chart.Series["linePlusMinusForce"].Points[idx].Color = Color.Blue;
                else
                    chart.Series["linePlusMinusForce"].Points[idx].Color = Color.Black;

                pmForceList.Add(pmForce);
                prePMForce = pmForce;
                //if (SelectedPType == "국내지수")
                //{
                //    if (preSPrice1 > preSPrice2 && preSPrice2 > preSPrice3)
                //    {
                //        if (smart.Variance_ChartPrice1 > smart.Variance_ChartPrice2 && smart.Variance_ChartPrice2 > smart.Variance_ChartPrice3)
                //        {
                //            if (preSPrice3 < smart.Variance_ChartPrice3)
                //            {
                //                chart.Series["lineSmartEnergy3"].Points[idx].LabelForeColor = Color.Red;
                //                chart.Series["lineSmartEnergy3"].Points[idx].Label = "▲";    
                //            }
                //        }
                //    }
                //    if (preSPrice1 < preSPrice2 && preSPrice2 < preSPrice3)
                //    {
                //        if (smart.Variance_ChartPrice1 < smart.Variance_ChartPrice2 && smart.Variance_ChartPrice2 < smart.Variance_ChartPrice3)
                //        {
                //            if (preSPrice3 > smart.Variance_ChartPrice3)
                //            {
                //                chart.Series["lineSmartEnergy3"].Points[idx].LabelForeColor = Color.Blue;
                //                chart.Series["lineSmartEnergy3"].Points[idx].Label = "▼";                               
                //            }
                //        }
                //    }
                //}

                //if (SelectedPType == "해외지수")
                //{
                //    if (preSPrice1 > preSPrice2 && preSPrice2 > preSPrice3)
                //    {
                //        if (smart.Variance_ChartPrice1 > smart.Variance_ChartPrice2 && smart.Variance_ChartPrice2 > smart.Variance_ChartPrice3)
                //        {
                //            if (preSPrice3 < smart.Variance_ChartPrice3)
                //            {
                //                chart.Series["lineSmartEnergy3"].Points[idx].LabelForeColor = Color.Red;
                //                chart.Series["lineSmartEnergy3"].Points[idx].Label = "▲";
                //            }
                //        }
                //    }
                //    if (preSPrice1 < preSPrice2 && preSPrice2 < preSPrice3)
                //    {
                //        if (smart.Variance_ChartPrice1 < smart.Variance_ChartPrice2 && smart.Variance_ChartPrice2 < smart.Variance_ChartPrice3)
                //        {
                //            if (preSPrice3 > smart.Variance_ChartPrice3)
                //            {
                //                chart.Series["lineSmartEnergy3"].Points[idx].LabelForeColor = Color.Blue;
                //                chart.Series["lineSmartEnergy3"].Points[idx].Label = "▼";
                //            }
                //        }
                //    }
                //}

                if (SelectedPType == "해외선물" || SelectedPType == "암호화폐")
                {
                    uItemCount++;
                    dItemCount++;
                    if (smart.Variance_ChartPrice3 < 0 && preSPrice3 >= smart.Variance_ChartPrice3 && (nextSPrice3 != 0 && nextSPrice3 >= smart.Variance_ChartPrice3))
                    {
                        bool isRange = true;                       
                        //if (TimeInterval == TimeIntervalEnum.Minute_30 && smart.Variance_ChartPrice3 <= -30) isRange = true;
                        //if (TimeInterval == TimeIntervalEnum.Hour_01 && smart.Variance_ChartPrice3 <= -50) isRange = true;
                        //if (TimeInterval == TimeIntervalEnum.Hour_02 && smart.Variance_ChartPrice3 <= -60) isRange = true;
                        //if (TimeInterval == TimeIntervalEnum.Hour_05 && smart.Variance_ChartPrice3 <= -70) isRange = true;
                        //if (TimeInterval == TimeIntervalEnum.Day && smart.Variance_ChartPrice3 <= -100) isRange = true;
                        //if (TimeInterval == TimeIntervalEnum.Week && smart.Variance_ChartPrice3 <= -300) isRange = true;

                        if (uItemCount >= 5 && isRange)
                        {
                            uItemCount = 0;
                            chart.Series["lineSmartEnergy3"].Points[idx].LabelForeColor = Color.Red;
                            chart.Series["lineSmartEnergy3"].Points[idx].Label = "▲";
                        }

                    }
                    if (smart.Variance_ChartPrice3 > 0 && preSPrice3 <= smart.Variance_ChartPrice3 && (nextSPrice3 != 0 && nextSPrice3 <= smart.Variance_ChartPrice3))
                    {
                        bool isRange = true;                      
                        //if (TimeInterval == TimeIntervalEnum.Minute_30 && smart.Variance_ChartPrice3 >= 30) isRange = true;
                        //if (TimeInterval == TimeIntervalEnum.Hour_01 && smart.Variance_ChartPrice3 >= 50) isRange = true;
                        //if (TimeInterval == TimeIntervalEnum.Hour_02 && smart.Variance_ChartPrice3 >= 60) isRange = true;
                        //if (TimeInterval == TimeIntervalEnum.Hour_05 && smart.Variance_ChartPrice3 >= 70) isRange = true;
                        //if (TimeInterval == TimeIntervalEnum.Day && smart.Variance_ChartPrice3 >= 100) isRange = true;
                        //if (TimeInterval == TimeIntervalEnum.Week && smart.Variance_ChartPrice3 >= 300) isRange = true;

                        if (dItemCount >= 5 && isRange)
                        {
                            dItemCount = 0;
                            chart.Series["lineSmartEnergy3"].Points[idx].LabelForeColor = Color.Blue;
                            chart.Series["lineSmartEnergy3"].Points[idx].Label = "▼";
                        }
                    }
                }              

                //if (SelectedPType == "국내업종")
                //{
                //    if (preSPrice1 > preSPrice2 && preSPrice2 > preSPrice3)
                //    {
                //        if (smart.Variance_ChartPrice1 > smart.Variance_ChartPrice2 && smart.Variance_ChartPrice2 > smart.Variance_ChartPrice3)
                //        {
                //            if (preSPrice3 < smart.Variance_ChartPrice3)
                //            {
                //                chart.Series["lineSmartEnergy3"].Points[idx].LabelForeColor = Color.Red;
                //                chart.Series["lineSmartEnergy3"].Points[idx].Label = "▲";
                //            }
                //        }
                //    }
                //    if (preSPrice1 < preSPrice2 && preSPrice2 < preSPrice3)
                //    {
                //        if (smart.Variance_ChartPrice1 < smart.Variance_ChartPrice2 && smart.Variance_ChartPrice2 < smart.Variance_ChartPrice3)
                //        {
                //            if (preSPrice3 > smart.Variance_ChartPrice3)
                //            {
                //                chart.Series["lineSmartEnergy3"].Points[idx].LabelForeColor = Color.Blue;
                //                chart.Series["lineSmartEnergy3"].Points[idx].Label = "▼";
                //            }
                //        }
                //    }
                //}
               
                preSPrice1 = smart.Variance_ChartPrice1;
                preSPrice2 = smart.Variance_ChartPrice2;
                preSPrice3 = smart.Variance_ChartPrice3;

                preMassAvg = itemAvg.T_MassAvg;
                preQuantumAvg = itemAvg.T_QuantumAvg;

                var dataPoint = chart.Series["candleBasic"].Points[idx];
                var dataPointAvg = chart.Series["candleAverage"].Points[idx];
                dataPoint.Tag = item;

                if (item.PlusMinusType == PlusMinusTypeEnum.양 && item.YinAndYang == PlusMinusTypeEnum.양)
                    SetDataPointColor(dataPoint, Color.Red, Color.Red, Color.Red, 2);
                else if (item.PlusMinusType == PlusMinusTypeEnum.음 && item.YinAndYang == PlusMinusTypeEnum.양)
                    SetDataPointColor(dataPoint, Color.Blue, Color.Blue, Color.Blue, 2);

                else if (item.PlusMinusType == PlusMinusTypeEnum.양)
                    SetDataPointColor(dataPoint, Color.Red, Color.Red, Color.White, 2);
                else if (item.PlusMinusType == PlusMinusTypeEnum.음)
                    SetDataPointColor(dataPoint, Color.Blue, Color.Blue, Color.White, 2);
                
                else
                    SetDataPointColor(dataPoint, Color.Black, Color.Black, Color.White, 2);

                if (itemAvg.PlusMinusType == PlusMinusTypeEnum.양)
                    SetDataPointColor(dataPointAvg, Color.Red, Color.Red, Color.White, 2);
                else if (itemAvg.PlusMinusType == PlusMinusTypeEnum.음)
                    SetDataPointColor(dataPointAvg, Color.Blue, Color.Blue, Color.White, 2);
                else
                    SetDataPointColor(dataPointAvg, Color.Black, Color.Black, Color.White, 2);                                
            }         
            SetTrackBar();
            SetScrollBar();
            DisplayView();
            //ViewPattern();
            IsLoaded = true;

            base.View();
        }

        public void ViewPattern(bool isLast = false)
        {
            ThreeCandlePattern threePatttern = new ThreeCandlePattern();

            int idx = isLast ? ChartData.Count - 10 : 10;

            for (int i = idx; i < ChartData.Count; i++)
            {              

                var item = ChartData[i];

                var patternRet = threePatttern.CheckAllPattern(item);

                if (patternRet.patternType == CandlePatternTypeEnum.Down)
                {
                    DisplayPattern(item, Color.Blue, "ⓟ", patternRet.patternName);
                }
                if (patternRet.patternType == CandlePatternTypeEnum.Up)
                {
                    DisplayPattern(item, Color.Red, "ⓟ", patternRet.patternName);
                }
            }
        }
 
        public void DisplayView()
        {
            chart.Update();

            int scrollVal = hScrollBar.Value;
            if (scrollVal < hScrollBar.Minimum) scrollVal = hScrollBar.Minimum;
            if (scrollVal > hScrollBar.Maximum) scrollVal = hScrollBar.Maximum;

            int trackView = trackBar.Value;
            int displayItemCount = DisplayPointCount * trackView;

            List<T_QuantumItemData> viewLists = null;
            List<T_QuantumItemData> viewAvgLists = null;
            List<T_QuantumItemData> viewAvgBLists = null;
            List<SmartCandleData> viewSmartLists = null;
            List<SmartCandleData> viewSmartBLists = null;
            List<double> viewPMForceLists = null;
            int maxDisplayIndex = 0;
            int minDisplayIndex = 0;
            if (scrollVal == hScrollBar.Minimum)
            {
                int maxIndex = ChartData.Count > displayItemCount ? displayItemCount - 1 : ChartData.Count;
                if (displayItemCount > ChartData.Count) displayItemCount = ChartData.Count;
                viewLists = ChartData.GetRange(0, maxIndex);
                viewAvgLists = ChartDataSub.GetRange(0, maxIndex);
                viewAvgBLists = ChartDataSub2.GetRange(0, maxIndex);
                viewSmartLists = smartDataList.GetRange(0, maxIndex);
                viewSmartBLists = smartBDataList.GetRange(0, maxIndex);
                viewPMForceLists = pmForceList.GetRange(0, maxIndex);
                maxDisplayIndex = displayItemCount;
                minDisplayIndex = 0;
            }
            else if (scrollVal == hScrollBar.Maximum)
            {
                int minIndex = ChartData.Count < displayItemCount ? 0 : ChartData.Count - displayItemCount;
                if (displayItemCount > ChartData.Count) displayItemCount = ChartData.Count;
                viewLists = ChartData.GetRange(minIndex, ChartData.Count < displayItemCount ? ChartData.Count : displayItemCount);
                viewAvgLists = ChartDataSub.GetRange(minIndex, ChartData.Count < displayItemCount ? ChartData.Count : displayItemCount);
                viewAvgBLists = ChartDataSub2.GetRange(minIndex, ChartData.Count < displayItemCount ? ChartData.Count : displayItemCount);
                viewSmartLists = smartDataList.GetRange(minIndex, ChartData.Count < displayItemCount ? ChartData.Count : displayItemCount);
                viewSmartBLists = smartBDataList.GetRange(minIndex, ChartData.Count < displayItemCount ? ChartData.Count : displayItemCount);
                viewPMForceLists = pmForceList.GetRange(minIndex, ChartData.Count < displayItemCount ? ChartData.Count : displayItemCount);

                maxDisplayIndex = ChartData.Count;
                minDisplayIndex = minIndex;
            }
            else
            {
                //int currentIndex = (scrollVal - 1) * displayItemCount;
                int currentIndex = (scrollVal - 1);
                if (ChartData.Count < currentIndex + displayItemCount)
                    displayItemCount = ChartData.Count - currentIndex;

                viewLists = ChartData.GetRange(currentIndex, displayItemCount);
                viewAvgLists = ChartDataSub.GetRange(currentIndex, displayItemCount);
                viewAvgBLists = ChartDataSub2.GetRange(currentIndex, displayItemCount);
                viewSmartLists = smartDataList.GetRange(currentIndex, displayItemCount);
                viewSmartBLists = smartBDataList.GetRange(currentIndex, displayItemCount);
                viewPMForceLists = pmForceList.GetRange(currentIndex, displayItemCount);

                maxDisplayIndex = currentIndex + displayItemCount;
                minDisplayIndex = currentIndex;
            }

            if (viewLists != null)
            {
                foreach (var ca in chart.ChartAreas)
                {
                    ca.AxisX.Maximum = maxDisplayIndex - 3;
                    ca.AxisX.Minimum = minDisplayIndex - 1;
                }
                double maxPrice = viewLists.Max(m => m.HighPrice);
                double minPrice = viewLists.Min(m => m.LowPrice);
                double devation = (maxPrice - minPrice) / 10.0;
                maxPrice = maxPrice + SpaceMaxMin;
                minPrice = minPrice - SpaceMaxMin;
                chart.ChartAreas["ca1"].AxisY2.Maximum = maxPrice;
                chart.ChartAreas["ca1"].AxisY2.Minimum = minPrice;
                chart.ChartAreas["ca1"].AxisY.Maximum = viewAvgLists.Max(m => m.T_ElectronForceAvg) + 0.5;
                chart.ChartAreas["ca1"].AxisY.Minimum = viewAvgLists.Min(m => m.T_ElectronForceAvg) - 0.5;


                maxPrice = viewAvgLists.Max(m => m.HighPrice);
                minPrice = viewAvgLists.Min(m => m.LowPrice);
                double devationAvg = (maxPrice - minPrice) / 10.0;
                maxPrice = maxPrice + SpaceMaxMin;
                minPrice = minPrice - SpaceMaxMin;
                chart.ChartAreas["ca2"].AxisY2.Maximum = maxPrice;
                chart.ChartAreas["ca2"].AxisY2.Minimum = minPrice;
               
                maxPrice = viewAvgBLists.Max(m => m.HighPrice);
                minPrice = viewAvgBLists.Min(m => m.LowPrice);
                maxPrice = maxPrice + SpaceMaxMin + 1;
                minPrice = minPrice - SpaceMaxMin - 1;
                chart.ChartAreas["ca3"].AxisY2.Maximum = maxPrice;
                chart.ChartAreas["ca3"].AxisY2.Minimum = minPrice;

                maxPrice = viewSmartLists.Max(m => m.Variance_ChartPrice3);
                minPrice = viewSmartLists.Min(m => m.Variance_ChartPrice3);
                maxPrice = maxPrice + SpaceMaxMin + 1;
                minPrice = minPrice - SpaceMaxMin - 1;
                //maxPrice = smartDataList.GetRange(2, smartDataList.Count - 3).Max(m => m.Variance_ChartPrice2);
                //maxPrice = smartDataList.GetRange(2, smartDataList.Count - 3).Min(m => m.Variance_ChartPrice2);              
                chart.ChartAreas["ca4"].AxisY2.Maximum = maxPrice;
                chart.ChartAreas["ca4"].AxisY2.Minimum = minPrice;

                maxPrice = viewSmartBLists.Max(m => m.Variance_ChartPrice3);
                minPrice = viewSmartBLists.Min(m => m.Variance_ChartPrice3);
                maxPrice = maxPrice + SpaceMaxMin;
                minPrice = minPrice - SpaceMaxMin;
                //maxPrice = smartBDataList.GetRange(2, smartBDataList.Count - 3).Max(m => m.Variance_ChartPrice2);
                //maxPrice = smartBDataList.GetRange(2, smartBDataList.Count - 3).Min(m => m.Variance_ChartPrice2);
                chart.ChartAreas["ca5"].AxisY2.Maximum = maxPrice;
                chart.ChartAreas["ca5"].AxisY2.Minimum = minPrice;

                chart.ChartAreas["ca6"].AxisY2.Maximum = viewPMForceLists.Max();
                chart.ChartAreas["ca6"].AxisY2.Minimum = viewPMForceLists.Min();

                var pList = PPUtils.GetExceptPlusMinusList(ChartData, PlusMinusTypeEnum.양);
                var mList = PPUtils.GetExceptPlusMinusList(ChartData, PlusMinusTypeEnum.음);
                //double devation = ItemCodeSet.GetDeviation(ItemCode);
                               
                resistanceList = PPUtils.GetResistancePrices(pList, devation, 3, 3);
                supportList = PPUtils.GetSupportPrices(mList, devation, 3, 3);
                resistanceAvgList = PPUtils.GetResistancePrices(ChartDataSub, devationAvg, 3, 3);
                supportAvgList = PPUtils.GetSupportPrices(ChartDataSub, devationAvg, 3, 3);
              
                RemoveAnnotationsByTag("RS");
                DisplayResistanceAndSupportLine();
                DisplayAvgResistanceAndSupportLine();
            }
        }

        private void hScrollBar_ValueChanged(object sender, EventArgs e)
        {
            if (!IsLoaded) return;
            DisplayView();
            if (ChartEventInstance != null)
                ChartEventInstance.OnChangeChartScrollBarHandler(this.chart, hScrollBar.Value);
        }

        private void trackBar_ValueChanged(object sender, EventArgs e)
        {
            if (!IsLoaded) return;
          
            SetScrollBar();
            DisplayView();
            if (ChartEventInstance != null)
                ChartEventInstance.OnChangeChartTrackBarHandler(this.chart, trackBar.Value);

            SelectedTrackBarValue = (int)trackBar.Value;
        }
        
        private void chart_PostPaint(object sender, ChartPaintEventArgs e)
        {
            DrawChartTitle(e);
        }

        #region CandleChartType

        #endregion

        #region Chart Util

        #endregion

        private void chart_MouseClick(object sender, MouseEventArgs e)
        {
        }

        #region Chart Util
        public void ClearChartLabel(string type)
        {
            for (int i = chart.Annotations.Count - 1; i >= 0; i--)
            {
                var a = chart.Annotations[i];

                if (a.Tag != null && a.Tag.ToString() == type)
                    chart.Annotations.Remove(a);
            }

            chart.Update();
        }
        public void DisplayPattern(T_QuantumItemData item, Color color, string type, string title = "")
        {
            int idx = 0;
            foreach (var p in chart.Series[0].Points)
            {
                var pItem = p.Tag as T_QuantumItemData;

                if (pItem == item)
                {                    
                    //if (item.PlusMinusType == PlusMinusTypeEnum.양)
                    //    SetDataPointColor(p, color, color, color, 2);
                    //else if (item.PlusMinusType == PlusMinusTypeEnum.음)
                    //    SetDataPointColor(p, color, color, color, 2);

                    p.Label += Environment.NewLine + title;
                    p.LabelForeColor = color;
                    break;
                }
                idx++;
            }
        }
       
        public void DisplayResistanceAndSupportLine()
        {
            int idx = 1;
            int max = 1;
            foreach (var p in resistanceList)
            {
                HorizontalLineAnnotation yLine = new HorizontalLineAnnotation();
                yLine.AxisX = chart.ChartAreas[0].AxisX;
                yLine.AxisY = chart.ChartAreas[0].AxisY2;
                yLine.AnchorY = p;
                yLine.ToolTip = p.ToString();
                yLine.IsSizeAlwaysRelative = false;              
                yLine.IsInfinitive = true;
                yLine.ClipToChartArea = chart.ChartAreas[0].Name;
                yLine.LineColor = Color.Red;
                yLine.LineWidth = 1;
                yLine.Tag = "RS";
                chart.Annotations.Add(yLine);

                if (idx >= max) break;
                idx++;
            }

            idx = 1;
            foreach (var p in supportList)
            {
                HorizontalLineAnnotation yLine = new HorizontalLineAnnotation();
                yLine.AxisX = chart.ChartAreas[0].AxisX;
                yLine.AxisY = chart.ChartAreas[0].AxisY2;
                yLine.AnchorY = p;
                yLine.IsSizeAlwaysRelative = false;
                yLine.ToolTip = p.ToString();
                yLine.IsInfinitive = true;
                yLine.ClipToChartArea = chart.ChartAreas[0].Name;
                yLine.LineColor = Color.Blue ;
                yLine.LineWidth = 1;
                yLine.Tag = "RS";
                chart.Annotations.Add(yLine);

                if (idx >= max) break;
                idx++;
            }
        }
        public void DisplayAvgResistanceAndSupportLine()
        {
            int idx = 1;
            int max = 1;
            foreach (var p in resistanceAvgList)
            {
                HorizontalLineAnnotation yLine = new HorizontalLineAnnotation();
                yLine.AxisX = chart.ChartAreas[1].AxisX;
                yLine.AxisY = chart.ChartAreas[1].AxisY2;
                yLine.AnchorY = p;
                yLine.IsSizeAlwaysRelative = false;
                yLine.ToolTip = p.ToString();
                yLine.IsInfinitive = true;
                yLine.ClipToChartArea = chart.ChartAreas[1].Name;
                yLine.LineColor = Color.Red;
                yLine.LineWidth = 1;
                yLine.Tag = "RS";
                chart.Annotations.Add(yLine);

                if (idx >= max) break;
                idx++;
            }

            idx = 1;
            foreach (var p in supportAvgList)
            {
                HorizontalLineAnnotation yLine = new HorizontalLineAnnotation();
                yLine.AxisX = chart.ChartAreas[1].AxisX;
                yLine.AxisY = chart.ChartAreas[1].AxisY2;
                yLine.AnchorY = p;
                yLine.IsSizeAlwaysRelative = false;
                yLine.ToolTip = p.ToString();
                yLine.IsInfinitive = true;
                yLine.ClipToChartArea = chart.ChartAreas[1].Name;
                yLine.LineColor = Color.Blue;
                yLine.LineWidth = 1;
                yLine.Tag = "RS";
                chart.Annotations.Add(yLine);

                if (idx >= max) break;
                idx++;
            }
        }
        public void DisplayChartLabel(DateTime dt, Color color, string type, string title = "★")
        {
            foreach (var p in chart.Series[0].Points)
            {
                var d = p.Tag as T_QuantumItemData;

                if (d.DTime.Year == dt.Year 
                    && d.DTime.Month == dt.Month 
                    && d.DTime.Day == dt.Day 
                    && d.DTime.Hour == dt.Hour 
                    && d.DTime.Minute == dt.Minute)
                {
                    TextAnnotation a = new TextAnnotation();
                    a.Font = new Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
                    a.Text = title;
                    a.ForeColor = color;
                    a.IsSizeAlwaysRelative = true;
                    a.AnchorAlignment = ContentAlignment.TopCenter;
                    a.AnchorDataPoint = p;
                    a.Tag = type;
                    a.AnchorOffsetY = -5;                  
                    chart.Annotations.Add(a);
                    break;
                }
            }
        }
        #endregion

        private string getDiceChar(A_HLOC item)
        {
            //if (n == 1) return "⚀";
            //if (n == 2) return "⚁";
            //if (n == 3) return "⚂";
            //if (n == 4) return "⚃";
            //if (n == 5) return "⚄";
            //if (n == 6) return "⚅";
            //❶❷❸❹❺❻❼❽❾❿
            //➀➁➂➃➄➅➆➇➈➉

            //if (item.DiceNum == 1 && item.PlusMinusType == PlusMinusTypeEnum.양) return "➀";
            //if (item.DiceNum == 2 && item.PlusMinusType == PlusMinusTypeEnum.양) return "➁";
            //if (item.DiceNum == 3 && item.PlusMinusType == PlusMinusTypeEnum.양) return "➂";
            //if (item.DiceNum == 4 && item.PlusMinusType == PlusMinusTypeEnum.양) return "➃";
            //if (item.DiceNum == 5 && item.PlusMinusType == PlusMinusTypeEnum.양) return "➄";
            //if (item.DiceNum == 6 && item.PlusMinusType == PlusMinusTypeEnum.양) return "➅";

            //if (item.DiceNum == 1 && item.PlusMinusType == PlusMinusTypeEnum.음) return "❶";
            //if (item.DiceNum == 2 && item.PlusMinusType == PlusMinusTypeEnum.음) return "❷";
            //if (item.DiceNum == 3 && item.PlusMinusType == PlusMinusTypeEnum.음) return "❸";
            //if (item.DiceNum == 4 && item.PlusMinusType == PlusMinusTypeEnum.음) return "❹";
            //if (item.DiceNum == 5 && item.PlusMinusType == PlusMinusTypeEnum.음) return "❺";
            //if (item.DiceNum == 6 && item.PlusMinusType == PlusMinusTypeEnum.음) return "❻";

            //if (item.DiceNum == 1 && item.PlusMinusType == PlusMinusTypeEnum.무) return "◆";
            //if (item.DiceNum == 2 && item.PlusMinusType == PlusMinusTypeEnum.무) return "◆";
            //if (item.DiceNum == 3 && item.PlusMinusType == PlusMinusTypeEnum.무) return "◆";
            //if (item.DiceNum == 4 && item.PlusMinusType == PlusMinusTypeEnum.무) return "◆";
            //if (item.DiceNum == 5 && item.PlusMinusType == PlusMinusTypeEnum.무) return "◆";
            //if (item.DiceNum == 6 && item.PlusMinusType == PlusMinusTypeEnum.무) return "◆";

            if (item.DiceNum == 1) return "➀";
            if (item.DiceNum == 2) return "➁";
            if (item.DiceNum == 3) return "➂";
            if (item.DiceNum == 4) return "➃";
            if (item.DiceNum == 5) return "➄";
            if (item.DiceNum == 6) return "➅";
            if (item.DiceNum == 7) return "⑦";
            return "★";
        }
    }
}
