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

namespace OM.Vikala.Controls.Charts
{
    public partial class QuantumLineChartHL : BaseChartControl
    {
        protected override string Description => "주황선:질량평균, 검은색:양종평균";
        public bool IsShowCandle
        {
            get
            {
                return chart.Series[0].Enabled;
            }
            set
            {
                chart.Series[0].Enabled = value;
            }
        }

        public bool IsShowMassVolumn
        {
            get
            {
                return chart.Series[4].Enabled;
            }
            set
            {
                chart.Series[4].Enabled = value;
            }
        }

        public override LineChartTypeEnum LineChartType
        {
            get;
            set;
        } = LineChartTypeEnum.양자라인;

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

        public void LoadData(string itemCode = ""
            , List<T_QuantumItemData> chartData = null
            , Lib.Base.Enums.TimeIntervalEnum timeInterval = Lib.Base.Enums.TimeIntervalEnum.Day)
        {
            LoadData(itemCode, chartData, null, timeInterval);
        }
        public void LoadData(string itemCode = ""
            , List<T_QuantumItemData> chartData = null
            , List<T_QuantumItemData> chartDataSub = null
            , Lib.Base.Enums.TimeIntervalEnum timeInterval = Lib.Base.Enums.TimeIntervalEnum.Day)
        {
            TimeInterval = timeInterval;
            ItemCode = itemCode;
            ChartData = chartData;
            ChartDataSub = chartDataSub;

            TotalPointCount = ChartData.Count;

            this.Invoke(new MethodInvoker(() => {
                Reset();
                View();
                Summary();
            }));
        }

        public QuantumLineChartHL()
        {
            InitializeComponent();
            base.SetChartControl(chart, hScrollBar, trackBar);
        }
                
        public override void View()
        {
            pnlScroll.Visible = IsAutoScrollX;
            if (ChartData == null) return;
            if (ChartDataSub == null) return;

            int bDistance = -1;
            for (int i = 0; i < ChartData.Count; i++)
            {
                var item = ChartData[i];

                int idx = chart.Series[0].Points.AddXY(item.DTime, item.HighPrice, item.LowPrice, item.OpenPrice, item.ClosePrice);

                var itemAvg = ChartDataSub[i];
                chart.Series[1].Points.AddXY(item.DTime, item.T_QuantumHighAvg + itemAvg.HeadLength + itemAvg.LegLength);
                chart.Series[2].Points.AddXY(item.DTime, item.T_QuantumLowAvg - itemAvg.HeadLength - itemAvg.LegLength);
                chart.Series[3].Points.AddXY(item.DTime, 
                    ((item.T_QuantumHighAvg + itemAvg.HeadLength + itemAvg.LegLength)
                    +   (item.T_QuantumLowAvg - itemAvg.HeadLength - itemAvg.LegLength)) / 2.0
                );
              
                int d = PriceTick.GetTickDiff(ItemCode, itemAvg.MassPrice, itemAvg.TotalCenterPrice);
                chart.Series[4].Points.AddXY(item.DTime, d);             
                
                if (bDistance != -1)
                {
                    if (d > bDistance) chart.Series[4].Points[idx].Color = Color.DarkRed;
                    else chart.Series[4].Points[idx].Color = Color.DarkBlue;

                }

                if (d == 0)
                {
                    if(itemAvg.T_HighAvg < itemAvg.LowPrice || itemAvg.T_LowAvg > item.HighPrice)
                        chart.Series[4].Points[idx].Label = "↑";
                }
                bDistance = d;

                {
                    var dataPoint = chart.Series[0].Points[idx];

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
                }
            }
            

            //maxPrice = ChartData.Max(m => m.HighPrice);
            //minPrice = ChartData.Min(m => m.LowPrice);

            //double maxPrice2 = ChartData.Max(m => m.T_QuantumHighAvg);
            //double minPrice2 = ChartData.Min(m => m.T_QuantumLowAvg);
            //if (maxPrice < maxPrice2) maxPrice = maxPrice2;
            //if (minPrice > minPrice2) minPrice = minPrice2;
            //maxPrice2 = ChartData.Max(m => m.T_CloseAvg);
            //minPrice2 = ChartData.Min(m => m.T_CloseAvg);
            //if (maxPrice < maxPrice2) maxPrice = maxPrice2;
            //if (minPrice > minPrice2) minPrice = minPrice2;
            //maxPrice2 = ChartData.Max(m => m.T_QuantumAvg);
            //minPrice2 = ChartData.Min(m => m.T_QuantumAvg);
            //if (maxPrice < maxPrice2) maxPrice = maxPrice2;
            //if (minPrice > minPrice2) minPrice = minPrice2;

            //maxPrice = maxPrice + SpaceMaxMin;
            //minPrice = minPrice - SpaceMaxMin;
            //chart.ChartAreas[0].AxisY2.Maximum = maxPrice;
            //chart.ChartAreas[0].AxisY2.Minimum = minPrice;
            //chart.ChartAreas[0].AxisY.Maximum = 10000;
            //chart.ChartAreas[0].AxisY.Minimum = 0;
            
            SetTrackBar();
            SetScrollBar();
           
            DisplayView();

            IsLoaded = true;

            base.View();
        }

        //public void SetScrollBar()
        //{
        //    int trackView = trackBar.Value;
        //    int displayItemCount = DisplayPointCount * trackView;

        //    int maxScrollValue = (int)Math.Ceiling((Convert.ToDouble(ChartData.Count) / Convert.ToDouble(displayItemCount)));
        //    int minScrollValue = 1;

        //    hScrollBar.Minimum = minScrollValue;
        //    hScrollBar.Maximum = maxScrollValue;
        //    hScrollBar.Value = maxScrollValue;
        //    hScrollBar.LargeChange = 1;
        //    hScrollBar.SmallChange = 1;
        //}

        //public void SetTrackBar()
        //{
        //    pnlScroll.Visible = IsAutoScrollX;
        //    int maxScrollValue = (int)Math.Ceiling((Convert.ToDouble(ChartData.Count) / Convert.ToDouble(DisplayPointCount)));
        //    int minScrollValue = 1;

        //    trackBar.Minimum = minScrollValue;
        //    trackBar.Maximum = maxScrollValue;
        //    trackBar.Value = minScrollValue;
        //    trackBar.LargeChange = 1;
        //    trackBar.SmallChange = 1;
        //}

        public void DisplayView()
        {
            chart.Update();

            int scrollVal = hScrollBar.Value;
            if (scrollVal < hScrollBar.Minimum) scrollVal = hScrollBar.Minimum;
            if (scrollVal > hScrollBar.Maximum) scrollVal = hScrollBar.Maximum;

            int trackView = trackBar.Value;
            int displayItemCount = DisplayPointCount * trackView;   
            
            List <T_QuantumItemData> viewLists = null;
            int maxDisplayIndex = 0;
            int minDisplayIndex = 0;
            if (scrollVal == hScrollBar.Minimum)
            {
                int maxIndex = ChartData.Count > displayItemCount ? displayItemCount - 1 : ChartData.Count;
                if (displayItemCount > ChartData.Count) displayItemCount = ChartData.Count; 
                viewLists = ChartData.GetRange(0, maxIndex);
                maxDisplayIndex = displayItemCount;
                minDisplayIndex = 0;
            }
            else if (scrollVal == hScrollBar.Maximum)
            {
                int minIndex = ChartData.Count < displayItemCount ? 0 : ChartData.Count - displayItemCount;
                if (displayItemCount > ChartData.Count) displayItemCount = ChartData.Count;
                viewLists = ChartData.GetRange(minIndex, ChartData.Count < displayItemCount ? ChartData.Count : displayItemCount);
                maxDisplayIndex = ChartData.Count;
                minDisplayIndex = minIndex;
            }
            else
            {
                int currentIndex = (scrollVal - 1);
                if (ChartData.Count < currentIndex + displayItemCount)
                    displayItemCount = ChartData.Count - currentIndex;
                viewLists = ChartData.GetRange(currentIndex, displayItemCount);

                maxDisplayIndex = currentIndex + displayItemCount;
                minDisplayIndex = currentIndex;
            }
            if (viewLists != null)
            {
                double maxPrice = 0.0;
                double minPrice = 0.0;              
                maxPrice = viewLists.Max(m => m.HighPrice);
                minPrice = viewLists.Min(m => m.LowPrice);

                double maxPrice2 = viewLists.Max(m => m.T_QuantumHighAvg);
                double minPrice2 = viewLists.Min(m => m.T_QuantumLowAvg);
                if (maxPrice < maxPrice2) maxPrice = maxPrice2;
                if (minPrice > minPrice2) minPrice = minPrice2;
                maxPrice2 = viewLists.Max(m => m.T_CloseAvg);
                minPrice2 = viewLists.Min(m => m.T_CloseAvg);
                if (maxPrice < maxPrice2) maxPrice = maxPrice2;
                if (minPrice > minPrice2) minPrice = minPrice2;
                maxPrice2 = viewLists.Max(m => m.T_QuantumAvg);
                minPrice2 = viewLists.Min(m => m.T_QuantumAvg);
                if (maxPrice < maxPrice2) maxPrice = maxPrice2;
                if (minPrice > minPrice2) minPrice = minPrice2;

                maxPrice = maxPrice + SpaceMaxMin;
                minPrice = minPrice - SpaceMaxMin;
                chart.ChartAreas[0].AxisY2.Maximum = maxPrice;
                chart.ChartAreas[0].AxisY2.Minimum = minPrice;
                chart.ChartAreas[0].AxisX.Maximum = maxDisplayIndex + 1;
                chart.ChartAreas[0].AxisX.Minimum = minDisplayIndex - 1;
                chart.ChartAreas[0].AxisY.Maximum = 10000;
                chart.ChartAreas[0].AxisY.Minimum = 0;
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
            SelectedTrackBarValue = (int)trackBar.Value;
            SetScrollBar();
            DisplayView();
            if (ChartEventInstance != null)
                ChartEventInstance.OnChangeChartTrackBarHandler(this.chart, trackBar.Value);
        }

        private void chart_PostPaint(object sender, ChartPaintEventArgs e)
        {
            DrawChartTitle(e);
        }

        private void chart_MouseClick(object sender, MouseEventArgs e)
        {
        }
    }
}