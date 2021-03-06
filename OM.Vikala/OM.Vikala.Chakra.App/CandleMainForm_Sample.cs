﻿using MetroFramework.Forms;
using OM.Lib.Base.Enums;
using OM.Lib.Base.Utils;
using OM.PP.Chakra;
using OM.PP.Chakra.Ctx;
using OM.Vikala.Controls.Charts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OM.Vikala.Chakra.App
{
    public partial class CandleMainForm_Sample : Form
    {
        ChartEvents chartEvent = new ChartEvents();

        public CandleMainForm_Sample()
        {
            InitializeComponent();

            chartCS1.InitializeControl();
            chartCS1.InitializeEvent(chartEvent);
            chartCS1.CandleChartType = CandleChartTypeEnum.기본;
            chartCS1.DisplayPointCount = 30;

            chartCS2.InitializeControl();
            chartCS2.InitializeEvent(chartEvent);
            chartCS2.CandleChartType = CandleChartTypeEnum.기본;
            chartCS2.DisplayPointCount = 30;

            tbDT_E.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        public CandleMainForm_Sample(CandleChartTypeEnum type)
        {
            InitializeComponent();

            chartCS1.InitializeControl();
            chartCS1.InitializeEvent(chartEvent);
            chartCS1.CandleChartType = type;
            chartCS1.DisplayPointCount = 30;

            chartCS2.InitializeControl();
            chartCS2.InitializeEvent(chartEvent);
            chartCS2.CandleChartType = type;
            chartCS2.DisplayPointCount = 30;

            tbDT_E.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            //string itemCode = ItemCodeSet.GetItemCode(ItemCode.선물_해외_WTI);
            string itemCode = ItemCodeSet.GetItemCode(ItemCode.지수_국내_코스피200);
            var list = PPContext.Instance.ClientContext.GetSourceData(
                  itemCode
                , Lib.Base.Enums.TimeIntervalEnum.Day
                , null
                , tbDT_E.Text
                , 300);

            int round = ItemCodeUtil.GetItemCodeRoundNum(itemCode);
            List<S_CandleItemData> sourceDatas = new List<S_CandleItemData>();
            foreach (var m in list)
            {
                S_CandleItemData sourceData = new S_CandleItemData(itemCode
                    , (Single)Math.Round(m.OpenVal, round)
                    , (Single)Math.Round(m.HighVal, round)
                    , (Single)Math.Round(m.LowVal, round)
                    , (Single)Math.Round(m.CloseVal, round)
                    , m.Volume
                    , m.DT
                    );
                sourceDatas.Add(sourceData);
            }
            chartCS1.LoadData(itemCode, sourceDatas);

            List<S_CandleItemData> sourceDatas2 = new List<S_CandleItemData>();
            int itemsCnt = 7;
            for (int i = itemsCnt; i < sourceDatas.Count; i++)
            {
                S_CandleItemData transData = new S_CandleItemData(itemCode, sourceDatas.GetRange(i - itemsCnt, itemsCnt));
                sourceDatas2.Add(transData);
            }
            chartCS2.LoadData(itemCode, sourceDatas2);
        }
    }
}
