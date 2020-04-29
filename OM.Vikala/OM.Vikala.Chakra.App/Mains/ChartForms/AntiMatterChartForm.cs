﻿using OM.Lib.Base.Enums;
using OM.Lib.Base.Utils;
using OM.PP.Chakra;
using OM.PP.Chakra.Ctx;
using OM.Vikala.Chakra.App.Chakra;
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
    public partial class AntiMatterChartForm : BaseForm
    {
        public PlusMinusTypeEnum PlusMinusTypeEnum
        {
            get;
            set;
        } = PlusMinusTypeEnum.양;

        public AntiMatterChartForm()
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
            userToolStrip.TableViewChangedEvent += UserToolStrip_TableViewChangedEvent;
            userToolStrip.LineChartWidthChangedEvent += UserToolStrip_LineChartWidthChangedEvent;
        }

        private void UserToolStrip_LineChartWidthChangedEvent(object sender, EventArgs e)
        {
            chart.BoldLine(sender.ToString());
            chart2.BoldLine(sender.ToString());
        }

        private void UserToolStrip_TableViewChangedEvent(object sender, EventArgs e)
        {
            if (sender.ToString() == "1")
            {
                tableLayoutPanel1.RowStyles[0].Height = 100.0f;
                tableLayoutPanel1.RowStyles[1].Height = 0f;
            }
            if (sender.ToString() == "2")
            {
                tableLayoutPanel1.RowStyles[1].Height = 100.0f;
                tableLayoutPanel1.RowStyles[0].Height = 0f;
            }
            if (sender.ToString() == "3")
            {
                tableLayoutPanel1.RowStyles[0].Height = 50.0f;
                tableLayoutPanel1.RowStyles[1].Height = 50.0f;
            }
        }
        public override void loadChartControls()
        {
            chart.InitializeControl();
            chart.InitializeEvent(chartEvent);
            chart.DisplayPointCount = itemCnt;

            chart2.InitializeControl();
            chart2.InitializeEvent(chartEvent);
            chart2.DisplayPointCount = itemCnt;

            chart.IsShowLine = true;
            chart2.IsShowLine = true;
        }

        public override void loadData()
        {
            if (base.SelectedItemData == null) return;
            if (string.IsNullOrEmpty(base.SelectedItemData.Code)) return;

            string itemCode = base.SelectedItemData.Code;

            var sourceDatas = PPContext.Instance.ClientContext.GetCandleSourceDataOrderByAsc(                 
                  itemCode
                , base.timeInterval);
            if (sourceDatas == null || sourceDatas.Count == 0) return;

            foreach (var s in sourceDatas)
            {
                if (PlusMinusTypeEnum == PlusMinusTypeEnum.양 && s.PlusMinusType == PlusMinusTypeEnum.음)
                {
                    s.ClosePrice = s.QuantumBasePrice;
                    s.HighPrice = s.QuantumBaseHighPrice;
                    s.LowPrice = s.QuantumLowPrice;
                }
                if (PlusMinusTypeEnum == PlusMinusTypeEnum.음 && s.PlusMinusType == PlusMinusTypeEnum.양)
                {
                    s.ClosePrice = s.QuantumBasePrice;
                    s.HighPrice = s.QuantumBaseHighPrice;
                    s.LowPrice = s.QuantumLowPrice;
                }
            }
            var averageDatas = PPUtils.GetAverageDatas(itemCode, sourceDatas, 7);
            sourceDatas = PPUtils.GetCutDatas(sourceDatas, averageDatas[0].DTime);
            chart.LoadDataAndApply(itemCode, sourceDatas, base.timeInterval, 7);
            chart2.LoadDataAndApply(itemCode, averageDatas, base.timeInterval, 7);
        }
    }
}
