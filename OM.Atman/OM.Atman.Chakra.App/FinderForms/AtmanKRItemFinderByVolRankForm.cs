﻿using MetroFramework;
using MetroFramework.Controls;
using OM.Atman.Chakra.App.Config;
using OM.Atman.Chakra.App.Events;
using OM.Atman.Chakra.App.Uc.Plans;
using OM.Atman.Chakra.Ctx;
using OM.Lib.Base.Enums;
using OM.Lib.Framework.Utility;
using OM.PP.Chakra;
using OM.PP.Chakra.Ctx;
using OM.Vikala.Controls.Charts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;

namespace OM.Atman.Chakra.App.FinderForms
{
    public partial class AtmanKRItemFinderByVolRankForm : MetroFramework.Forms.MetroForm
    {
        int displayCnt = 60;

        bool isRunningJongmok = false;

        public AtmanKRItemFinderByVolRankForm()
        {
            InitializeComponent();           
            InitializeControls();
            InitializeEvents();
        }
              
        private void InitializeControls()
        {
            chart.IsShowXLine = chart.IsShowYLine = true;
            chart.InitializeControl();
            chart.InitializeEvent(null);
            chart.DisplayPointCount = displayCnt;
        }
        private void InitializeEvents()
        {
            this.Load += AtmanForm_Load;
        }
                
        private void AtmanForm_Load(object sender, EventArgs e)
        {
            serverInfo();          
        }
       
        private void serverInfo()
        {
            AtmanServerConfigData.IP = ConfigurationManager.AppSettings["AtmanService_IP"];
            AtmanServerConfigData.Port = Convert.ToInt32(ConfigurationManager.AppSettings["AtmanService_Port"]);

            XingServerConfigData.IP = ConfigurationManager.AppSettings["XingService_IP"];
            XingServerConfigData.Port = Convert.ToInt32(ConfigurationManager.AppSettings["XingService_Port"]);
            XingContext.Instance.OnCreateClient();

            PPServerConfigData.IP = ConfigurationManager.AppSettings["PPService_IP"];
            PPServerConfigData.Port = Convert.ToInt32(ConfigurationManager.AppSettings["PPService_Port"]);
            PPContext.Instance.OnCreateClient();
        }

        #region LoadDSiseata
   
       
        #endregion

        #region Control Event
       
        private void dgv_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;

            string code = dgv.Rows[e.RowIndex].Cells[0].Value as string;
            string name = dgv.Rows[e.RowIndex].Cells[1].Value as string;

            tbSelectedCode2.Text = code;
            tbSelectedName2.Text = name;

            Task.Factory.StartNew(() => {
                var sourceDatas = XingContext.Instance.ClientContext.GetJongmokSiseData(code, "2", "0", "300");
                if (sourceDatas == null || sourceDatas.Count == 0) return;
                int totalCnt = sourceDatas.Count;
                if (totalCnt > 300)
                    sourceDatas.RemoveRange(0, totalCnt - 300);
                var averageDatas = PPUtils.GetBalancedAverageDatas(code, sourceDatas, 4);
                sourceDatas = PPUtils.GetCutDatas(sourceDatas, averageDatas[0].DTime);
               
                chart.LoadDataAndApply(code, sourceDatas, averageDatas, TimeIntervalEnum.Day, 9);
                chart.SetYFormat("N0");
            });
        }
        #endregion

        private void btnSearchRank_Click(object sender, EventArgs e)
        {
            dgv.Rows.Clear();
            string gubun = "0";
            if (chkKospi.Checked && chkKosdaq.Checked) gubun = "0";
            else if (chkKospi.Checked) gubun = "1";
            else if (chkKosdaq.Checked) gubun = "2";

            string sdiff = nudSdiff.Value.ToString();
            string ediff = nudEdiff.Value.ToString();

            Task.Factory.StartNew(() => {
                var sourceDatas = XingContext.Instance.ClientContext.GetJongMokRankData(gubun, sdiff, ediff);
                if (sourceDatas == null || sourceDatas.Count == 0) return;
                foreach (var item in sourceDatas)
                {
                    this.Invoke(new Action(() => {

                        int rIdx = dgv.Rows.Add(item.종목코드
                                                , item.종목명                                              
                                                , item.현재가
                                                , item.전일대비구분
                                                , item.등락율 + "%"
                                                , item.전일대비                                                   
                                                );

                        // 1:상한 2:상승 3:보합 4:하한 5:하락
                        if (item.전일대비구분 == "1")
                        {
                            dgv.Rows[rIdx].Cells[3].Value = "▲";
                            dgv.Rows[rIdx].Cells[3].Style.ForeColor =
                            dgv.Rows[rIdx].Cells[4].Style.ForeColor =
                            dgv.Rows[rIdx].Cells[5].Style.ForeColor = Color.Red;

                        }
                        else if (item.전일대비구분 == "2")
                        {
                            dgv.Rows[rIdx].Cells[3].Value = "△";
                            dgv.Rows[rIdx].Cells[3].Style.ForeColor =
                            dgv.Rows[rIdx].Cells[4].Style.ForeColor =
                            dgv.Rows[rIdx].Cells[5].Style.ForeColor = Color.Red;
                        }
                        else if (item.전일대비구분 == "3")
                        {
                            dgv.Rows[rIdx].Cells[3].Value = "◇";
                        }
                        else if (item.전일대비구분 == "5")
                        {
                            dgv.Rows[rIdx].Cells[3].Value = "▽";
                            dgv.Rows[rIdx].Cells[3].Style.ForeColor =
                            dgv.Rows[rIdx].Cells[4].Style.ForeColor =
                            dgv.Rows[rIdx].Cells[5].Style.ForeColor = Color.Blue;
                        }
                        else if (item.전일대비구분 == "4")
                        {
                            dgv.Rows[rIdx].Cells[3].Value = "▼";
                            dgv.Rows[rIdx].Cells[3].Style.ForeColor =
                            dgv.Rows[rIdx].Cells[4].Style.ForeColor =
                            dgv.Rows[rIdx].Cells[5].Style.ForeColor = Color.Blue;
                        }
                    }));
                }
            });
        }
              
        private void btnSearchJongmok_Click(object sender, EventArgs e)
        {
            if (!isRunningJongmok) isRunningJongmok = true;
            else
            {
                isRunningJongmok = false;
                return;
            }

            Task.Factory.StartNew(() =>
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (!isRunningJongmok) return;
                    string code = row.Cells[0].Value as string;

                    Task.Factory.StartNew(() =>
                    {
                        var sourceDatas = XingContext.Instance.ClientContext.GetJongmokSiseData(code, "2", "0", "20");
                        if (sourceDatas == null || sourceDatas.Count == 0) return;
                        var averageDatas = PPUtils.GetBalancedAverageDatas(code, sourceDatas, 4);

                        List<SmartCandleData> smartDataList = new List<SmartCandleData>();
                        List<WisdomCandleData> wisdomDataList = new List<WisdomCandleData>();
                        SmartCandleData preSmartData = null;
                        WisdomCandleData preWisdomData = null;
                        for (int i = 0; i < averageDatas.Count; i++)
                        {
                            var cData = averageDatas[i];
                            SmartCandleData smartData = new SmartCandleData(cData.ItemCode, cData.OpenPrice, cData.HighPrice, cData.LowPrice, cData.ClosePrice, cData.Volume, cData.DTime, preSmartData);
                            smartDataList.Add(smartData);
                            preSmartData = smartData;
                            WisdomCandleData wisdomData = new WisdomCandleData(cData.ItemCode, cData.OpenPrice, cData.HighPrice, cData.LowPrice, cData.ClosePrice, cData.Volume, cData.DTime, preWisdomData);
                            wisdomDataList.Add(wisdomData);
                            preWisdomData = wisdomData;
                        }

                        var sR0 = getSmartLine(0, smartDataList);
                        var sR1 = getSmartLine(1, smartDataList);
                        var sR2 = getSmartLine(2, smartDataList);
                        var sR3 = getSmartLine(3, smartDataList);
                     
                        var swR4 = getSmartWisdomLine(4, smartDataList, wisdomDataList);
                        var swR3 = getSmartWisdomLine(3, smartDataList, wisdomDataList);
                        var swR2 = getSmartWisdomLine(2, smartDataList, wisdomDataList);
                        var swR1 = getSmartWisdomLine(1, smartDataList, wisdomDataList);
                        var swR0 = getSmartWisdomLine(0, smartDataList, wisdomDataList);

                        //this.Invoke(new Action(() => {
                        row.Cells["S3"].Value = sR3.Item1;
                        row.Cells["S3"].Style.ForeColor = sR3.Item2;
                        row.Cells["S2"].Value = sR2.Item1;
                        row.Cells["S2"].Style.ForeColor = sR2.Item2;
                        row.Cells["S1"].Value = sR1.Item1;
                        row.Cells["S1"].Style.ForeColor = sR1.Item2;
                        row.Cells["S0"].Value = sR0.Item1;
                        row.Cells["S0"].Style.ForeColor = sR0.Item2;

                        row.Cells["SW4"].Value = swR4.Item1;
                        row.Cells["SW4"].Style.ForeColor = swR4.Item2;
                        row.Cells["SW3"].Value = swR3.Item1;
                        row.Cells["SW3"].Style.ForeColor = swR3.Item2;
                        row.Cells["SW2"].Value = swR2.Item1;
                        row.Cells["SW2"].Style.ForeColor = swR2.Item2;
                        row.Cells["SW1"].Value = swR1.Item1;
                        row.Cells["SW1"].Style.ForeColor = swR1.Item2;
                        row.Cells["SW0"].Value = swR0.Item1;
                        row.Cells["SW0"].Style.ForeColor = swR0.Item2;

                        //}));                                                            
                    });

                    System.Threading.Thread.Sleep(2000);
                }
            });
        }

        private (string, Color) getSmartLine(int preCount, List<SmartCandleData> list)
        {
            var m1 = list[list.Count - 2 - preCount];
            var m0 = list[list.Count - 1 - preCount];

            if (m1.Variance_ChartPrice2 < m0.Variance_ChartPrice2) return ("▲", Color.Red);
            else if (m1.Variance_ChartPrice2 > m0.Variance_ChartPrice2) return ("▼", Color.Blue);
            else return ("◇", Color.Black);
        }
        private (string, Color) getWisdomLine(int preCount, List<WisdomCandleData> list)
        {
            var m1 = list[list.Count - 2 - preCount];
            var m0 = list[list.Count - 1 - preCount];

            if (m1.Variance_ChartPrice < m0.Variance_ChartPrice) return ("▲", Color.Red);
            else if (m1.Variance_ChartPrice > m0.Variance_ChartPrice) return ("▼", Color.Blue);
            else return ("◇", Color.Black);
        }
        private (string, Color) getSmartWisdomLine(int preCount, List<SmartCandleData> smartDataList, List<WisdomCandleData> wisdomDataList)
        {
            var s = smartDataList[smartDataList.Count - 1 - preCount];
            var w = wisdomDataList[wisdomDataList.Count - 1 - preCount];

            if (s.Variance_ChartPrice2 > w.Variance_ChartPrice) return ("▲", Color.Red);
            else if (s.Variance_ChartPrice2 < w.Variance_ChartPrice) return ("▼", Color.Blue);
            else return ("◇", Color.Black);
        }

        private void btnGoNaver_Click(object sender, EventArgs e)
        {
            if (tbSelectedCode2.Text.Length == 0) return;
            string url = "https://finance.naver.com/item/main.nhn?code=" + tbSelectedCode2.Text;
            System.Diagnostics.Process.Start("chrome", url);
        }

        private void btnGoAlpha_Click(object sender, EventArgs e)
        {
            if (tbSelectedCode2.Text.Length == 0) return;
            string url = "https://www.alphasquare.co.kr/home/stock/stock-summary?code=" + tbSelectedCode2.Text;
            System.Diagnostics.Process.Start("chrome", url);
        }
    }
}
