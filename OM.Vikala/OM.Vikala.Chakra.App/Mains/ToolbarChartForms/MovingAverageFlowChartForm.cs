﻿using OM.Lib.Base.Enums;
using OM.Lib.Base.Utils;
using OM.PP.Chakra;
using OM.PP.Chakra.Ctx;
using OM.Vikala.Chakra.App.Config;
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

namespace OM.Vikala.Chakra.App.Mains.ToolbarChartForms
{
    public partial class MovingAverageFlowChartForm : BaseForm
    {
        BaseChartControl qMin1 = new QuantumLineChart();
        BaseChartControl qMin2 = new QuantumLineChart();
 
        public MovingAverageFlowChartForm(TimeIntervalEnum  timeInterval = TimeIntervalEnum.Day)
        {
            InitializeComponent();
            base.setToolStrip(userToolStrip1);
            InitializeControls();

            base.timeInterval = timeInterval;
        }

        private void InitializeControls()
        {
            loadChartControls();
            setTimer();
            userToolStrip.IsVisibleAlignmentButton = false;
            userToolStrip.TableViewChangedEvent += UserToolStrip_TableViewChangedEvent;
            userToolStrip.LineChartWidthChangedEvent += UserToolStrip_LineChartWidthChangedEvent;

            userToolStrip.IsVisibleExpand = false;
            userToolStrip.IsVisibleMdiButton = false;
            userToolStrip.IsVisibleTimeIntervalButton = false;
        }
        private void UserToolStrip_LineChartWidthChangedEvent(object sender, EventArgs e)
        {
            chart1.BoldLine(sender.ToString());
            chart2.BoldLine(sender.ToString());
        }
        private void UserToolStrip_TableViewChangedEvent(object sender, EventArgs e)
        {
        }
        public override void loadChartControls()
        {
            chart1.InitializeControl();
            chart1.InitializeEvent(chartEvent);
            chart1.DisplayPointCount = itemCnt;

            chart2.InitializeControl();
            chart2.InitializeEvent(chartEvent);
            chart2.DisplayPointCount = itemCnt;
        }

        public override void loadData()
        {
            if (base.SelectedItemData == null) return;
            if (string.IsNullOrEmpty(base.SelectedItemData.Code)) return;

            string itemCode = base.SelectedItemData.Code;

            var sourceDatas = PPContext.Instance.ClientContext.GetCandleSourceDataOrderByAsc(
                  itemCode
                , timeInterval);        

            if (sourceDatas == null || sourceDatas.Count == 0) return;

            var averageDatas1 = PPUtils.GetBalancedAverageDatas(itemCode, sourceDatas, 4);

            PPUtils.RemoveGapPrice(sourceDatas);    

            var averageDatas2 = PPUtils.GetBalancedAverageDatas(itemCode, sourceDatas, 4);
            
            chart1.LoadDataAndApply(itemCode, averageDatas1, averageDatas1, timeInterval, 5);

            chart2.LoadDataAndApply(itemCode, averageDatas2, averageDatas2, timeInterval, 5);
        }
    }
}
