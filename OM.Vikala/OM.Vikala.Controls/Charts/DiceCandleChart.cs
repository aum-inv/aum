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
    public partial class DiceCandleChart : BaseChartControl
    {
        protected override string Description => "";       
      
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


        public DiceCandleChart()
        {
            InitializeComponent();
            base.SetChartControl(chart, hScrollBar, trackBar);
        }
                
        public override void View()
        {
            pnlScroll.Visible = IsAutoScrollX;
            if (ChartData == null) return;
          
            T_QuantumItemData bItem = null;
            for (int i = 0; i < ChartData.Count; i++)
            {
                var item = ChartData[i];
                int idx = chart.Series[0].Points.AddXY(item.DTime, item.HighPrice, item.LowPrice, item.OpenPrice, item.ClosePrice);


                item = ChartDataSub[i]; 
                string diceChar = getDiceChar(item);
                chart.Series[0].Points[idx].Label = diceChar;
              
                chart.Series[1].Points.AddXY(item.DTime, item.T_MassAvg);
                chart.Series[2].Points.AddXY(item.DTime, item.T_QuantumAvg);                     
                chart.Series[3].Points.AddXY(item.DTime, 0);
               
                if (bItem != null)
                {
                    bool isSignal = false;
                    if (item.T_MassAvg < item.T_QuantumAvg)
                    {
                        if (item.T_QuantumAvg < item.LowPrice)
                        {
                            isSignal = true;
                        }
                        if (item.T_MassAvg > item.HighPrice)
                        {
                            isSignal = true;
                        }
                    }
                    if (item.T_MassAvg > item.T_QuantumAvg)
                    {
                        if (item.T_QuantumAvg > item.HighPrice)
                        {
                            isSignal = true;
                        }
                        if (item.T_MassAvg < item.LowPrice)
                        {
                            isSignal = true;
                        }
                    }

                    if (bItem.DiceNum + item.DiceNum == 7 && isSignal)
                    {
                        chart.Series[3].Points[idx].Label = "↑";                     
                    }                   
                }

                bItem = item;                
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

                maxPrice = maxPrice + SpaceMaxMin + SpaceMaxMin;
                minPrice = minPrice - SpaceMaxMin - SpaceMaxMin;
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

            if (item.DiceNum == 1 && item.PlusMinusType == PlusMinusTypeEnum.양) return "➀";
            if (item.DiceNum == 2 && item.PlusMinusType == PlusMinusTypeEnum.양) return "➁";
            if (item.DiceNum == 3 && item.PlusMinusType == PlusMinusTypeEnum.양) return "➂";
            if (item.DiceNum == 4 && item.PlusMinusType == PlusMinusTypeEnum.양) return "➃";
            if (item.DiceNum == 5 && item.PlusMinusType == PlusMinusTypeEnum.양) return "➄";
            if (item.DiceNum == 6 && item.PlusMinusType == PlusMinusTypeEnum.양) return "➅";

            if (item.DiceNum == 1 && item.PlusMinusType == PlusMinusTypeEnum.음) return "❶";
            if (item.DiceNum == 2 && item.PlusMinusType == PlusMinusTypeEnum.음) return "❷";
            if (item.DiceNum == 3 && item.PlusMinusType == PlusMinusTypeEnum.음) return "❸";
            if (item.DiceNum == 4 && item.PlusMinusType == PlusMinusTypeEnum.음) return "❹";
            if (item.DiceNum == 5 && item.PlusMinusType == PlusMinusTypeEnum.음) return "❺";
            if (item.DiceNum == 6 && item.PlusMinusType == PlusMinusTypeEnum.음) return "❻";

            if (item.DiceNum == 1 && item.PlusMinusType == PlusMinusTypeEnum.무) return "◆";
            if (item.DiceNum == 2 && item.PlusMinusType == PlusMinusTypeEnum.무) return "◆";
            if (item.DiceNum == 3 && item.PlusMinusType == PlusMinusTypeEnum.무) return "◆";
            if (item.DiceNum == 4 && item.PlusMinusType == PlusMinusTypeEnum.무) return "◆";
            if (item.DiceNum == 5 && item.PlusMinusType == PlusMinusTypeEnum.무) return "◆";
            if (item.DiceNum == 6 && item.PlusMinusType == PlusMinusTypeEnum.무) return "◆";

            return "";
        }
    }
}