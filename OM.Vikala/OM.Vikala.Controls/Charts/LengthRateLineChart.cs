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

namespace OM.Vikala.Controls.Charts
{
    public partial class LengthRateLineChart : BaseChartControl
    {
        protected override string Description => "변동성 체크";
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

        public List<S_CandleItemData> ChartData
        {
            get;
            set;
        }
        public List<S_CandleItemData> ChartDataSub
        {
            get;
            set;
        }
        public void LoadData(string itemCode = ""
            , List<S_CandleItemData> chartData = null
            , Lib.Base.Enums.TimeIntervalEnum timeInterval = Lib.Base.Enums.TimeIntervalEnum.Day)
        {
            LoadData(itemCode, chartData, null, timeInterval);
        }
        public void LoadData(string itemCode = ""
            , List<S_CandleItemData> chartData = null
            , List<S_CandleItemData> chartDataSub = null
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
                //Summary();
            }));
        }

        public LengthRateLineChart()
        {
            InitializeComponent();
            base.SetChartControl(chart, hScrollBar, trackBar);
        }

        public override void View()
        {
            pnlScroll.Visible = IsAutoScrollX;
            if (ChartData == null) return;
            
            foreach (var item in ChartData)
            {
                int idx = chart.Series[0].Points.AddXY(item.DTime, item.HighPrice, item.LowPrice, item.OpenPrice, item.ClosePrice);
                chart.Series[1].Points.AddXY(item.DTime, item.AllVikalaLengthRate); //red
                chart.Series[2].Points.AddXY(item.DTime, item.AllQuantumLengthRate); //low              
                chart.Series[3].Points.AddXY(item.DTime, item.AllLengthRate); //black
            }

            double maxPrice = ChartData.Max(m => m.HighPrice);
            double minPrice = ChartData.Min(m => m.LowPrice);
            maxPrice = maxPrice + SpaceMaxMin;
            minPrice = minPrice - SpaceMaxMin;
            chart.ChartAreas[0].AxisY2.Maximum = maxPrice;
            chart.ChartAreas[0].AxisY2.Minimum = minPrice;

            double maxPriceLine = ChartData.Max(m => m.AllLengthRate);
            double minPriceLine = ChartData.Min(m => m.AllLengthRate);
            double maxPriceLine2 = ChartData.Max(m => m.AllQuantumLengthRate);
            double minPriceLine2 = ChartData.Min(m => m.AllQuantumLengthRate);
            double maxPriceLine3 = ChartData.Max(m => m.AllVikalaLengthRate);
            double minPriceLine3 = ChartData.Min(m => m.AllVikalaLengthRate);
             if (maxPriceLine < maxPriceLine2) maxPriceLine = maxPriceLine2;
            if (minPriceLine > minPriceLine2) minPriceLine = minPriceLine2;
            if (maxPriceLine < maxPriceLine3) maxPriceLine = maxPriceLine3;
            if (minPriceLine > minPriceLine3) minPriceLine = minPriceLine3;
                     
            chart.ChartAreas[0].AxisY.Maximum = maxPriceLine;
            chart.ChartAreas[0].AxisY.Minimum = minPriceLine;
            chart.ChartAreas[0].AxisY.LabelStyle.Format = "{N2}";
  
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
            List <S_CandleItemData> viewLists = null;
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
                chart.ChartAreas[0].AxisX.Maximum = maxDisplayIndex + 1;
                chart.ChartAreas[0].AxisX.Minimum = minDisplayIndex - 1;

                double maxPrice = viewLists.Max(m => m.HighPrice);
                double minPrice = viewLists.Min(m => m.LowPrice);             
                maxPrice = maxPrice + SpaceMaxMin;
                minPrice = minPrice - SpaceMaxMin;
                chart.ChartAreas[0].AxisY2.Maximum = maxPrice;
                chart.ChartAreas[0].AxisY2.Minimum = minPrice;

                double maxPriceLine = viewLists.Max(m => m.AllLengthRate);
                double minPriceLine = viewLists.Min(m => m.AllLengthRate);
                double maxPriceLine2 = viewLists.Max(m => m.AllQuantumLengthRate);
                double minPriceLine2 = viewLists.Min(m => m.AllQuantumLengthRate);
                double maxPriceLine3 = viewLists.Max(m => m.AllVikalaLengthRate);
                double minPriceLine3 = viewLists.Min(m => m.AllVikalaLengthRate);
                if (maxPriceLine < maxPriceLine2) maxPriceLine = maxPriceLine2;
                if (minPriceLine > minPriceLine2) minPriceLine = minPriceLine2;
                if (maxPriceLine < maxPriceLine3) maxPriceLine = maxPriceLine3;
                if (minPriceLine > minPriceLine3) minPriceLine = minPriceLine3;

                chart.ChartAreas[0].AxisY.Maximum = maxPriceLine;
                chart.ChartAreas[0].AxisY.Minimum = minPriceLine;
                chart.ChartAreas[0].AxisY.LabelStyle.Format = "{N2}";
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
    }
}