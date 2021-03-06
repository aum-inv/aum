﻿using OM.Lib.Base.Enums;
using OM.Lib.Base.Utils;
using OM.PP.Chakra;
using OM.PP.Chakra.Ctx;
using OM.Vikala.Controls.Charts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OM.Vikala.Chakra.App.Mains.ChartForm
{
    public partial class LinePointChartForm : BaseForm
    {
        public LinePointChartForm()
        {
            InitializeComponent();
            base.setToolStrip(userToolStrip1);
            InitializeControls();
        }

        private void InitializeControls()
        {           
            loadChartControls();
            setTimer();
            userToolStrip.IsVisibleAlignmentButton = false;
            userToolStrip.LineChartWidthChangedEvent += UserToolStrip_LineChartWidthChangedEvent;
        }

        private void UserToolStrip_LineChartWidthChangedEvent(object sender, EventArgs e)
        {
            chart.BoldLine(sender.ToString());
            chart2.BoldLine(sender.ToString());
        }
        public override void loadChartControls()
        {
            chart.InitializeControl();
            chart.InitializeEvent(chartEvent);
            chart.DisplayPointCount = itemCnt;

            chart2.InitializeControl();
            chart2.InitializeEvent(chartEvent);
            chart2.DisplayPointCount = itemCnt;
        }

        public override void loadData()
        {
            if (base.SelectedItemData == null) return;
            if (string.IsNullOrEmpty(base.SelectedItemData.Code)) return;

            string itemCode = base.SelectedItemData.Code;

            var candles = PPContext.Instance.ClientContext.GetCandleSourceDataOrderByAsc(                 
                  itemCode
                , base.timeInterval);
            if (candles == null || candles.Count == 0) return;
            List<S_LineItemData> sourceDatas = new List<S_LineItemData>();
           var list = PPUtils.GetSixPointsByCandles(candles);
            foreach (var m in list)
            {                
                //S_LineItemData sourceData = new S_LineItemData(
                //    itemCode
                //    , m.OpenPrice
                //    , m.HighPrice
                //    , m.LowPrice
                //    , m.ClosePrice
                //    , m.DTime
                //    );
                //sourceDatas.Add(sourceData);
            }            

            chart.loadDataAndApply(itemCode, sourceDatas, base.timeInterval, 7);

            var averageDatas = PPUtils.GetAverageDatas(itemCode, sourceDatas, 7);
            chart2.loadDataAndApply(itemCode, averageDatas, base.timeInterval, 7);
        }
    }
}
