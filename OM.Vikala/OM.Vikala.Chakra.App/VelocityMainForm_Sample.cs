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
    public partial class VelocityMainForm_Sample : Form
    {
        ChartEvents chartEvent1 = new ChartEvents();
        ChartEvents chartEvent2 = new ChartEvents();
        ChartEvents chartEvent3 = new ChartEvents();
        public VelocityMainForm_Sample()
        {
            InitializeComponent();

            chartVL1.InitializeControl();
            chartVL1.InitializeEvent(chartEvent1);
            chartVL1.DisplayPointCount = 30;

            chartVL2.InitializeControl();
            chartVL2.InitializeEvent(chartEvent2);
            chartVL2.DisplayPointCount = 30;

            chartVL3.InitializeControl();
            chartVL3.InitializeEvent(chartEvent3);
            chartVL3.DisplayPointCount = 30;

            tbDT_E.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        private void btnLoadData_Click(object sender, EventArgs e)
        {
            string itemCode = ItemCodeSet.GetItemCode(ItemCode.지수_국내_코스피200);

            var list = PPContext.Instance.ClientContext.GetSourceData(
                  itemCode
                , Lib.Base.Enums.TimeIntervalEnum.Day
                , null
                , tbDT_E.Text
                , 120);

            int round = ItemCodeUtil.GetItemCodeRoundNum(itemCode);

            List<S_CandleItemData> sourceDatas = new List<S_CandleItemData>();
            List<T_VelocityItemData> transformedDatas1 = new List<T_VelocityItemData>();
            List<T_VelocityItemData> transformedDatas2 = new List<T_VelocityItemData>();
            List<T_VelocityItemData> transformedDatas3 = new List<T_VelocityItemData>();

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

            int itemsCnt = 20;
            for (int i = itemsCnt; i < sourceDatas.Count; i++)
            {
                T_VelocityItemData transData = 
                    new T_VelocityItemData(sourceDatas[i], sourceDatas.GetRange(i - itemsCnt, itemsCnt));
                transData.Transform();
                transformedDatas1.Add(transData);
            }
            

            itemsCnt = 10;
            for (int i = itemsCnt; i < sourceDatas.Count; i++)
            {
                T_VelocityItemData transData =
                    new T_VelocityItemData(sourceDatas[i], sourceDatas.GetRange(i - itemsCnt, itemsCnt));
                transData.Transform();
                transformedDatas2.Add(transData);
            }
            

            itemsCnt = 5;
            for (int i = itemsCnt; i < sourceDatas.Count; i++)
            {
                T_VelocityItemData transData =
                    new T_VelocityItemData(sourceDatas[i], sourceDatas.GetRange(i - itemsCnt, itemsCnt));
                transData.Transform();
                transformedDatas3.Add(transData);
            }

            for (int i = 0; i < numericUpDown1.Value; i++)
            {
                transformedDatas1.Remove(transformedDatas1.Last());
                transformedDatas2.Remove(transformedDatas2.Last());
                transformedDatas3.Remove(transformedDatas3.Last());
            }

            chartVL1.LoadData(itemCode, transformedDatas1);
            chartVL2.LoadData(itemCode, transformedDatas2);
            chartVL3.LoadData(itemCode, transformedDatas3);
        }
    }
}
