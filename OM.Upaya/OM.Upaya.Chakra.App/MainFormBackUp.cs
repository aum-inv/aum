﻿using OM.Atman.Chakra.Ctx;
using OM.Lib.Base;
using OM.Lib.Base.Enums;
using OM.Lib.Entity;
using OM.Lib.Framework.Db;
using OM.PP.Chakra;
using OM.PP.Chakra.Ctx;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OM.Atman.Chakra.App
{
    public partial class MainFormBackUp : Form
    {
        Dictionary<string, Label> lblPrice = new Dictionary<string, Label>();
        Dictionary<string, Label> lblUpDown = new Dictionary<string, Label>();
        Dictionary<string, Label> lblDiff = new Dictionary<string, Label>();
        Dictionary<string, Label> lblRate = new Dictionary<string, Label>();

        public MainFormBackUp()
        {
            InitializeComponent();
            InitializeControls();
        }
        private void InitializeControls()
        {
            lblPrice.Add("90199999", lblPrice999);
            lblPrice.Add("CL", lblPriceCL);
            lblPrice.Add("NG", lblPriceNG);
            lblPrice.Add("GC", lblPriceGC);
            lblPrice.Add("SI", lblPriceSI);
            lblPrice.Add("HSI", lblPriceHSI);
            lblPrice.Add("NQ", lblPriceNQ);
            lblPrice.Add("URO", lblPriceURO);

            lblUpDown.Add("90199999", lblUpDown999);
            lblUpDown.Add("CL", lblUpDownCL);
            lblUpDown.Add("NG", lblUpDownNG);
            lblUpDown.Add("GC", lblUpDownGC);
            lblUpDown.Add("SI", lblUpDownSI);
            lblUpDown.Add("HSI", lblUpDownHSI);
            lblUpDown.Add("NQ", lblUpDownNQ);
            lblUpDown.Add("URO", lblUpDownURO);

            lblDiff.Add("90199999", lblDiff999);
            lblDiff.Add("CL", lblDiffCL);
            lblDiff.Add("NG", lblDiffNG);
            lblDiff.Add("GC", lblDiffGC);
            lblDiff.Add("SI", lblDiffSI);
            lblDiff.Add("HSI", lblDiffHSI);
            lblDiff.Add("NQ", lblDiffNQ);
            lblDiff.Add("URO", lblDiffURO);

            lblRate.Add("90199999", lblRate999);
            lblRate.Add("CL", lblRateCL);
            lblRate.Add("NG", lblRateNG);
            lblRate.Add("GC", lblRateGC);
            lblRate.Add("SI", lblRateSI);
            lblRate.Add("HSI", lblRateHSI);
            lblRate.Add("NQ", lblRateNQ);
            lblRate.Add("URO", lblRateURO);
        }
        private void serverInfo()
        {
            AtmanServerConfigData.IP = ConfigurationManager.AppSettings["AtmanService_IP"];
            AtmanServerConfigData.Port = Convert.ToInt32(ConfigurationManager.AppSettings["AtmanService_Port"]);

            XingServerConfigData.IP = ConfigurationManager.AppSettings["XingService_IP"];
            XingServerConfigData.Port = Convert.ToInt32(ConfigurationManager.AppSettings["XingService_Port"]);
            XingContext.Instance.OnCreateClient();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Process currentProcess = Process.GetCurrentProcess();
            currentProcess.Kill();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                serverInfo();

                AtmanContext.Instance.StartServer();

                btnStart.Enabled = false;

                PPStorage.Instance.Init("CL");
                PPStorage.Instance.Init("NG");
                PPStorage.Instance.Init("GC");
                PPStorage.Instance.Init("SI");
                PPStorage.Instance.Init("HSI");
                PPStorage.Instance.Init("NQ");
                PPStorage.Instance.Init("URO");

                LoadCandle("CL");
                LoadCandle("NG");
                LoadCandle("GC");
                LoadCandle("SI");
                LoadCandle("HSI");
                LoadCandle("NQ");
                LoadCandle("URO");                

                InitThread();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Process currentProcess = Process.GetCurrentProcess();
            currentProcess.Kill();
        }
        private void BindSise(string itemCode, CurrentPrice current)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                try
                {
                    lblPrice[itemCode].Text = current.price;
                    lblDiff[itemCode].Text = current.change;
                    lblRate[itemCode].Text = current.drate + "%";

                    if (current.sign == "1" || current.sign == "2")
                    {
                        lblUpDown[itemCode].Text = "▲";
                        lblPrice[itemCode].ForeColor = Color.Red;
                        lblUpDown[itemCode].ForeColor = Color.Red;
                        lblDiff[itemCode].ForeColor = Color.Red;
                        lblRate[itemCode].ForeColor = Color.Red;
                    }
                    else if (current.sign == "4" || current.sign == "5")
                    {
                        lblUpDown[itemCode].Text = "▼";
                        lblPrice[itemCode].ForeColor = Color.Blue;
                        lblUpDown[itemCode].ForeColor = Color.Blue;
                        lblDiff[itemCode].ForeColor = Color.Blue;
                        lblRate[itemCode].ForeColor = Color.Blue;
                    }
                    else
                    {
                        lblUpDown[itemCode].Text = "-";
                        lblPrice[itemCode].ForeColor = Color.White;
                        lblUpDown[itemCode].ForeColor = Color.White;
                        lblDiff[itemCode].ForeColor = Color.White;
                        lblRate[itemCode].ForeColor = Color.White;
                    }
                }
                catch (Exception ex) { }
            }));
        }

        private void LoadCandle(string itemCode)
        {
            int displayCnt = 10;
            PurushaPrakriti pp = new PurushaPrakriti();
            pp.Item = itemCode;
            pp.Interval = (int)EnumUtil.GetTimeIntervalValue("1분");
            pp.DisplayCount = 30;
            try
            {
                Entities entities = (Entities)pp.Collect(new Query("USP_PP_SIMPLE_LST"));
                List<PurushaPrakriti> list = entities.Cast<PurushaPrakriti>().ToList();
                List<S_CandleItemData> resultList = new List<S_CandleItemData>();
                foreach (var m in list)
                {
                    var item = new S_CandleItemData(itemCode, m.OpenVal, m.HighVal, m.LowVal, m.CloseVal, m.Volume, m.DT);

                    resultList.Add(item);
                }
                PPStorage.Instance.Init01(itemCode, resultList);
            }
            catch (Exception) { }

            pp = new PurushaPrakriti();
            pp.Item = itemCode;
            pp.Interval = (int)EnumUtil.GetTimeIntervalValue("5분");
            pp.DisplayCount = 30;
            try
            {
                Entities entities = (Entities)pp.Collect(new Query("USP_PP_SIMPLE_LST"));
                List<PurushaPrakriti> list = entities.Cast<PurushaPrakriti>().ToList();
                List<S_CandleItemData> resultList = new List<S_CandleItemData>();
                foreach (var m in list)
                {
                    var item = new S_CandleItemData(itemCode, m.OpenVal, m.HighVal, m.LowVal, m.CloseVal, m.Volume, m.DT);

                    resultList.Add(item);
                }
                PPStorage.Instance.Init05(itemCode, resultList);
            }
            catch (Exception) { }

            pp = new PurushaPrakriti();
            pp.Item = itemCode;
            pp.Interval = (int)EnumUtil.GetTimeIntervalValue("10분");
            pp.DisplayCount = displayCnt;
            try
            {
                Entities entities = (Entities)pp.Collect(new Query("USP_PP_SIMPLE_LST"));
                List<PurushaPrakriti> list = entities.Cast<PurushaPrakriti>().ToList();
                List<S_CandleItemData> resultList = new List<S_CandleItemData>();
                foreach (var m in list)
                {
                    var item = new S_CandleItemData(itemCode, m.OpenVal, m.HighVal, m.LowVal, m.CloseVal, m.Volume, m.DT);

                    resultList.Add(item);
                }
                PPStorage.Instance.Init10(itemCode, resultList);
            }
            catch (Exception) { }

            pp = new PurushaPrakriti();
            pp.Item = itemCode;
            pp.Interval = (int)EnumUtil.GetTimeIntervalValue("30분");
            pp.DisplayCount = displayCnt;
            try
            {
                Entities entities = (Entities)pp.Collect(new Query("USP_PP_SIMPLE_LST"));
                List<PurushaPrakriti> list = entities.Cast<PurushaPrakriti>().ToList();
                List<S_CandleItemData> resultList = new List<S_CandleItemData>();
                foreach (var m in list)
                {
                    var item = new S_CandleItemData(itemCode, m.OpenVal, m.HighVal, m.LowVal, m.CloseVal, m.Volume, m.DT);

                    resultList.Add(item);
                }
                PPStorage.Instance.Init30(itemCode, resultList);
            }
            catch (Exception) { }

            pp = new PurushaPrakriti();
            pp.Item = itemCode;
            pp.Interval = (int)EnumUtil.GetTimeIntervalValue("60분");
            pp.DisplayCount = displayCnt;
            try
            {
                Entities entities = (Entities)pp.Collect(new Query("USP_PP_SIMPLE_LST"));
                List<PurushaPrakriti> list = entities.Cast<PurushaPrakriti>().ToList();
                List<S_CandleItemData> resultList = new List<S_CandleItemData>();
                foreach (var m in list)
                {
                    var item = new S_CandleItemData(itemCode, m.OpenVal, m.HighVal, m.LowVal, m.CloseVal, m.Volume, m.DT);

                    resultList.Add(item);
                }
                PPStorage.Instance.Init60(itemCode, resultList);
            }
            catch (Exception) { }

            pp = new PurushaPrakriti();
            pp.Item = itemCode;
            pp.Interval = (int)EnumUtil.GetTimeIntervalValue("120분");
            pp.DisplayCount = displayCnt;
            try
            {
                Entities entities = (Entities)pp.Collect(new Query("USP_PP_SIMPLE_LST"));
                List<PurushaPrakriti> list = entities.Cast<PurushaPrakriti>().ToList();
                List<S_CandleItemData> resultList = new List<S_CandleItemData>();
                foreach (var m in list)
                {
                    var item = new S_CandleItemData(itemCode, m.OpenVal, m.HighVal, m.LowVal, m.CloseVal, m.Volume, m.DT);

                    resultList.Add(item);
                }
                PPStorage.Instance.Init120(itemCode, resultList);
            }
            catch (Exception) { }

            pp = new PurushaPrakriti();
            pp.Item = itemCode;
            pp.Interval = (int)EnumUtil.GetTimeIntervalValue("일");
            pp.DisplayCount = displayCnt;
            try
            {
                Entities entities = (Entities)pp.Collect(new Query("USP_PP_SIMPLE_LST"));
                List<PurushaPrakriti> list = entities.Cast<PurushaPrakriti>().ToList();
                List<S_CandleItemData> resultList = new List<S_CandleItemData>();
                foreach (var m in list)
                {
                    var item = new S_CandleItemData(itemCode, m.OpenVal, m.HighVal, m.LowVal, m.CloseVal, m.Volume, m.DT);

                    resultList.Add(item);
                }
                PPStorage.Instance.InitDay(itemCode, resultList);
            }
            catch (Exception) { }
        }
            
        private void InitThread()
        {
            System.Threading.Thread tCL =
                new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(Run));
            tCL.Start("CL");
            
            System.Threading.Thread tNG =
                new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(Run));
            tNG.Start("NG");

            System.Threading.Thread tGC =
                new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(Run));
            tGC.Start("GC");

            System.Threading.Thread tSI =
                new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(Run));
            tSI.Start("SI");

            System.Threading.Thread tHSI =
                new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(Run));
            tHSI.Start("HSI");
            
            System.Threading.Thread tNQ =
                new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(Run));
            tNQ.Start("NQ");

            System.Threading.Thread tURO =
                new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(Run));
            tURO.Start("URO");
        }

        bool isSiseBinding = false;
        private void Run(object o)
        {
            string itemCode = (string)o;

            string lastPrice = "";

            LimitedList<double> ll3 = new LimitedList<double>(3);
            LimitedList<double> ll5 = new LimitedList<double>(5);
            LimitedList<double> ll7 = new LimitedList<double>(7);

            int rnd = ItemCodeSet.GetItemRoundNum(itemCode);

            while (true)
            {
                if (!SiseStorage.Instance.Prices.ContainsKey(itemCode))
                {
                    System.Threading.Thread.Sleep(1000);
                    continue;
                }

                var priceQueue = SiseStorage.Instance.Prices[itemCode];

                if (priceQueue.Count == 0)
                {
                    System.Threading.Thread.Sleep(100);
                    continue;
                }
                CurrentPrice price;
                var isDequeue = priceQueue.TryDequeue(out price);
                if (isDequeue)
                {
                    if (lastPrice == price.price) continue;
                    lastPrice = price.price;

                    double d = Math.Round(Convert.ToDouble(lastPrice), rnd);
                    ll3.Insert(d);
                    ll5.Insert(d);
                    ll7.Insert(d);
                    price.price3 = ll3.Count == 3 ? Math.Round(ll3.Average(), rnd) : 0;
                    price.price5 = ll3.Count == 5 ? Math.Round(ll5.Average(), rnd) : 0;
                    price.price7 = ll3.Count == 7 ? Math.Round(ll7.Average(), rnd) : 0;

                    SiseEvents.Instance.OnSiseHandler(itemCode, price);

                    PPStorage.Instance.Add(itemCode, price.DTime, (Single)Math.Round(Convert.ToSingle(price.price), rnd));
                    
                    if (isSiseBinding)
                    {
                        BindSise(itemCode, price);
                    }
                }
            }
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void chkSise_CheckedChanged(object sender, EventArgs e)
        {
            isSiseBinding = chkSise.Checked;
        }
        private void btnAtomRule_Click(object sender, EventArgs e)
        {
            new AtomRuleForm().Show();
        }
        private void btnMessRule_Click(object sender, EventArgs e)
        {
            new MessRuleForm().Show();
        }

        private void btnTwoLineRule_Click(object sender, EventArgs e)
        {
            new TwoLineRuleForm().Show();
        }

        private void btnTradeRule_Click(object sender, EventArgs e)
        {
            new TradeRuleForm().Show();
        }

        private void btnPatternTradeRule_Click(object sender, EventArgs e)
        {
            new PatternTradeRuleForm().Show();
        }

        private void btnPatternTradeRule2_Click(object sender, EventArgs e)
        {
            new PatternTradeRuleForm2().Show();
        }
        private void btnPatternTradeRule3_Click(object sender, EventArgs e)
        {
            new PatternTradeRuleForm3().Show();
        }

        private void btnPatternTradeRule4_Click(object sender, EventArgs e)
        {
            new PatternTradeRuleForm4().Show();
        }
        private void btnCandleSignal_Click(object sender, EventArgs e)
        {
            new CandleSignalForm2().Show();          
        }
        private void btnCandleSignal2_Click(object sender, EventArgs e)
        {
            new CandleSignalForm3().Show();
        }
        private void btnSimpleOrder_Click(object sender, EventArgs e)
        {
            new OrderSimpleForm().Show();
        }
        private void btnMITOrder_Click(object sender, EventArgs e)
        {
            new OrderMITForm().Show();
        }

        private void btnLoadCandle_Click(object sender, EventArgs e)
        {
            LoadCandle("CL");
            LoadCandle("NG");
            LoadCandle("GC");
            LoadCandle("SI");
            LoadCandle("HSI");
            LoadCandle("NQ");
            LoadCandle("URO");
        }        
    }
}
