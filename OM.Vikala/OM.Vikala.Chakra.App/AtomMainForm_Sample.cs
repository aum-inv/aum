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
    public partial class AtomMainForm_Sample : Form
    {
        ChartEvents chartEvent = new ChartEvents();

        public AtomMainForm_Sample()
        {
            InitializeComponent();

            chartAT1.InitializeControl();
            chartAT1.InitializeEvent(chartEvent);
            chartAT1.DisplayPointCount = 30;

            chartAT2.InitializeControl();
            chartAT2.InitializeEvent(chartEvent);
            chartAT2.DisplayPointCount = 30;
            tbDT_E.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        private void btnLoadData_Click(object sender, EventArgs e)
        {
            TimeIntervalEnum timeInterval = TimeIntervalEnum.Day;

            if (radioButton1.Checked) timeInterval = TimeIntervalEnum.Hour_01;
            if (radioButton2.Checked) timeInterval = TimeIntervalEnum.Hour_02;
            if (radioButton3.Checked) timeInterval = TimeIntervalEnum.Hour_03;
            if (radioButton4.Checked) timeInterval = TimeIntervalEnum.Day;

            string itemCode = ItemCodeSet.GetItemCode(ItemCode.지수_국내_코스피200);
            var list = PPContext.Instance.ClientContext.GetSourceData(
                  itemCode
                , timeInterval
                , null
                , tbDT_E.Text
                , 300);
            
            int round = ItemCodeUtil.GetItemCodeRoundNum(itemCode);

            List<S_CandleItemData> sourceDatas = new List<S_CandleItemData>();
            foreach (var m in list)
            {
                S_CandleItemData sourceData = new S_CandleItemData(
                    itemCode
                    , (Single)Math.Round(m.OpenVal, round)
                    , (Single)Math.Round(m.HighVal, round)
                    , (Single)Math.Round(m.LowVal, round)
                    , (Single)Math.Round(m.CloseVal, round)
                    , m.Volume
                    , m.DT
                    );

                sourceDatas.Add(sourceData);
            }

            int itemsCnt = 7;
            List<T_AtomItemData> transformedDatas = new List<T_AtomItemData>();
            for (int i = itemsCnt; i <= sourceDatas.Count; i++)
            {
                T_AtomItemData transData = new T_AtomItemData(sourceDatas[i-1], sourceDatas.GetRange(i - itemsCnt, itemsCnt));
                transData.Transform();
                transformedDatas.Add(transData);
            }
            chartAT1.LoadData(itemCode, transformedDatas, timeInterval);


            List<S_CandleItemData> sourceDatas2 = new List<S_CandleItemData>();
            itemsCnt = 7;
            for (int i = itemsCnt; i < sourceDatas.Count; i++)
            {
                S_CandleItemData transData = new S_CandleItemData(itemCode, sourceDatas.GetRange(i - itemsCnt, itemsCnt));
                sourceDatas2.Add(transData);
            }
            itemsCnt = 7;
            List<T_AtomItemData> transformedDatas2 = new List<T_AtomItemData>();
            for (int i = itemsCnt; i <= sourceDatas2.Count; i++)
            {
                T_AtomItemData transData = new T_AtomItemData(sourceDatas2[i - 1], sourceDatas2.GetRange(i - itemsCnt, itemsCnt));
                transData.Transform();
                transformedDatas2.Add(transData);
            }
            chartAT2.LoadData(itemCode, transformedDatas2, timeInterval);
        }
    }
}
