﻿using MaterialSkin;
using MaterialSkin.Controls;
using MetroFramework.Forms;
using OM.Atman.Chakra.Ctx;
using OM.Lib.Base.Enums;
using OM.PP.Chakra;
using OM.PP.Chakra.Ctx;
using OM.PP.XingApp.Api;
using OM.PP.XingApp.Config;
using OM.Upaya.Chakra.Ctx;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XA_SESSIONLib;

namespace OM.PP.XingApp
{
    public partial class XingAppForm : Form
    {
        //xing 인증
        private XASessionClass session = new XASessionClass();

        System.Threading.Timer timer = null; 
        System.Threading.Timer timerKr = null;

        bool isRuning = false;
      
        bool isLogoned = false;

        bool isAutoFFCL  = false;
        bool isAutoFFNG  = false;
        bool isAutoFFGC  = false;
        bool isAutoFFSI  = false;
        bool isAutoFFHSI = false;
        bool isAutoFFNQ = false;
        bool isAutoFFURO = false;
        bool isAutoFFES = false;

        bool isAutoKospi = false;
        bool isAutoKosdaq = false;
        public XingAppForm()
        {
            InitializeComponent();           
            Control.CheckForIllegalCrossThreadCalls = false;
            this.Load += XingAppForm_Load;
            serverInfo();
            cbLogin.SelectedIndex = 1;
        }

        public XingAppForm(string loginIndex)
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            this.Load += XingAppForm_Load;
            serverInfo();
            cbLogin.SelectedIndex = Convert.ToInt32(loginIndex);

            Task.Factory.StartNew(() => {
                System.Threading.Thread.Sleep(1000 * 10);
                this.Invoke(new MethodInvoker(() =>
                {
                    btnLogin.PerformClick();
                }));
            });

            Task.Factory.StartNew(() => {
                System.Threading.Thread.Sleep(1000 * 30);
                this.Invoke(new MethodInvoker(() =>
                {
                    btnReal.PerformClick();
                }));
            });
        }
        private void serverInfo()
        {
            PPServerConfigData.IP = ConfigurationManager.AppSettings["PPService_IP"];
            PPServerConfigData.Port = Convert.ToInt32(ConfigurationManager.AppSettings["PPService_Port"]);
            PPContext.Instance.OnCreateClient();

            AtmanServerConfigData.IP = ConfigurationManager.AppSettings["AtmanService_IP"];
            AtmanServerConfigData.Port = Convert.ToInt32(ConfigurationManager.AppSettings["AtmanService_Port"]);
            AtmanContext.Instance.OnCreateClient();

            UpayaServerConfigData.IP = ConfigurationManager.AppSettings["UpayaService_IP"];
            UpayaServerConfigData.Port = Convert.ToInt32(ConfigurationManager.AppSettings["UpayaService_Port"]);
            UpayaContext.Instance.OnCreateClient();

            try
            {
                XingServerConfigData.IP = ConfigurationManager.AppSettings["XingService_IP"];
                XingServerConfigData.Port = Convert.ToInt32(ConfigurationManager.AppSettings["XingService_Port"]);
                XingContext.Instance.StartServer();
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                //MessageBox.Show(ex.Message);
            }
        }

        private void InitializeTimer()
        {
            if (timerKr == null)
                timerKr = new System.Threading.Timer(timer_TickKr, null, TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(15));

            if (timer == null)
                timer = new System.Threading.Timer(timer_Tick, null, TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(15));

            //if(timerTick == null)
            //    timerTick = new System.Threading.Timer(timer_TickTick, null, TimeSpan.FromSeconds(50), TimeSpan.FromSeconds(15));
        }
        private void timer_TickKr(object state)
        {
            if (!isLogoned) return;
            if (isRuning) return;
            isRuning = true;
            Task.Factory.StartNew(() => {
                if (isAutoKospi)
                {
                    queryUpjong(ItemCodeSet.GetItemCode(ItemCode.지수_국내_코스피200));                   
                    System.Threading.Thread.Sleep(3000 * 6);
                }
                if (isAutoKosdaq)
                {
                    queryUpjong(ItemCodeSet.GetItemCode(ItemCode.지수_국내_코스닥));
                    System.Threading.Thread.Sleep(3000 * 6);
                }                
            });
            if (!isAutoKospi && !isAutoKosdaq)
                isRuning = false;
        }
        private void timer_Tick(object state)
        {
            if (!isLogoned) return;
            if (isRuning) return;
            isRuning = true;
            Task.Factory.StartNew(() => { 
                if (isAutoFFGC)
                {
                    queryFF(ItemCodeSet.GetItemCode(ItemCode.선물_해외_GOLD));
                    System.Threading.Thread.Sleep(3000 * 6);
                }
                if (isAutoFFNG)
                {
                    queryFF(ItemCodeSet.GetItemCode(ItemCode.선물_해외_NG));
                    System.Threading.Thread.Sleep(3000 * 6);
                }
                if (isAutoFFSI)
                {
                    queryFF(ItemCodeSet.GetItemCode(ItemCode.선물_해외_SILVER));
                    System.Threading.Thread.Sleep(3000 * 6);
                }
                if (isAutoFFCL)
                {
                    queryFF(ItemCodeSet.GetItemCode(ItemCode.선물_해외_WTI));
                    System.Threading.Thread.Sleep(3000 * 6);
                }
                if (isAutoFFNQ)
                {
                    queryFF(ItemCodeSet.GetItemCode(ItemCode.선물_해외_나스닥));
                    System.Threading.Thread.Sleep(3000 * 6);
                }
                if (isAutoFFHSI)
                {
                    queryFF(ItemCodeSet.GetItemCode(ItemCode.선물_해외_항셍));
                    System.Threading.Thread.Sleep(3000 * 6);
                }
                if (isAutoFFURO)
                {
                    queryFF(ItemCodeSet.GetItemCode(ItemCode.선물_해외_유로FX));
                    System.Threading.Thread.Sleep(3000 * 6);
                }
                if (isAutoFFES)
                {
                    queryFF(ItemCodeSet.GetItemCode(ItemCode.선물_해외_SNP));
                    System.Threading.Thread.Sleep(3000 * 6);
                }
            });
            if (!isAutoFFGC
                && !isAutoFFNG
                && !isAutoFFSI
                && !isAutoFFCL
                && !isAutoFFNQ
                && !isAutoFFHSI
                && !isAutoFFURO
                && !isAutoFFES)
                isRuning = false;
        }
       
        #region XingLoginLogout
        private void XingAppForm_Load(object sender, EventArgs e)
        {
            cbLogin.SelectedIndex = 1;

            session._IXASessionEvents_Event_Login += Session__IXASessionEvents_Event_Login;
            session.Disconnect += Session_Disconnect;

            Task.Factory.StartNew(() => {
                System.Threading.Thread.Sleep(1000 * 2);
                this.Invoke(new MethodInvoker(() =>
                {
                    btnLogin.PerformClick();
                }));
            });
        }

        private void Session_Disconnect()
        {
            lblLogin.BackColor = Color.Black;
            isLogoned = false;
        }

        private void Session__IXASessionEvents_Event_Login(string szCode, string szMsg)
        {
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            bool isAliveServer = session.ConnectServer(AccountInfo.서버정보, AccountInfo.서버포트);
            LogWrite("IsAliveServer => " + isAliveServer.ToString());
            bool isSuccess = false;

            
            if (cbLogin.SelectedIndex == 0)
            {
                isSuccess = session.Login(
                      AccountInfoLEE.접속아이디
                    , AccountInfoLEE.접속비밀번호
                    , AccountInfoLEE.인증비밀번호
                    , (int)XA_SESSIONLib.XA_SERVER_TYPE.XA_REAL_SERVER
                    //, (int)XA_SESSIONLib.XA_SERVER_TYPE.XA_SIMUL_SERVER
                    , false);
                WorldFutureAccountInfo.LoginUser = "LEE";
            }
            else if (cbLogin.SelectedIndex == 1)
            {
                isSuccess = session.Login(
                      AccountInfoSON.접속아이디
                    , AccountInfoSON.접속비밀번호
                    , AccountInfoSON.인증비밀번호
                    , (int)XA_SESSIONLib.XA_SERVER_TYPE.XA_REAL_SERVER
                    //, (int)XA_SESSIONLib.XA_SERVER_TYPE.XA_SIMUL_SERVER
                    , false);
                WorldFutureAccountInfo.LoginUser = "SON";
            }
            else if (cbLogin.SelectedIndex == 2)
            {
                isSuccess = session.Login(
                      AccountInfoJIN.접속아이디
                    , AccountInfoJIN.접속비밀번호
                    , AccountInfoJIN.인증비밀번호
                    //, (int)XA_SESSIONLib.XA_SERVER_TYPE.XA_REAL_SERVER
                    , (int)XA_SESSIONLib.XA_SERVER_TYPE.XA_SIMUL_SERVER
                    , false);
                WorldFutureAccountInfo.LoginUser = "JIN";
            }
            else if (cbLogin.SelectedIndex == 3)
            {
                isSuccess = session.Login(
                      AccountInfoJUNG.접속아이디
                    , AccountInfoJUNG.접속비밀번호
                    , AccountInfoJUNG.인증비밀번호
                    //, (int)XA_SESSIONLib.XA_SERVER_TYPE.XA_REAL_SERVER
                    , (int)XA_SESSIONLib.XA_SERVER_TYPE.XA_SIMUL_SERVER
                    , false);
                WorldFutureAccountInfo.LoginUser = "JUNG";
            }

            if (isSuccess)
            {
                btnLogin.Enabled = false;
                lblLogin.BackColor = isSuccess ? Color.Blue : Color.Black;
                LogWrite("IsConnectedServer => " + isSuccess.ToString());
                isLogoned = true;
                btnClose.Enabled = true;

                InitializeTimer();
                timer1.Enabled = true;
                timer1.Start();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        void LogWrite(string log)
        {
            if (tbLog.InvokeRequired)
            {
                tbLog.Invoke(new MethodInvoker(() =>
                {
                    tbLog.AppendText(log);
                    tbLog.AppendText(Environment.NewLine);
                }));
            }
            else
            {
                tbLog.AppendText(log);
                tbLog.AppendText(Environment.NewLine);
            }
        }
        private void btnFF_Click(object sender, EventArgs e)
        {
            if (!isLogoned) return;
            Button btn = sender as Button;
            string itemCode = btn.Text;           
            queryFF(itemCode);
        }

        private void btnUpjong_Click(object sender, EventArgs e)
        {
            if (!isLogoned) return;
            Button btn = sender as Button;
            string itemCode = btn.Tag.ToString();
            queryUpjong(itemCode);
        }
        private void btnWorldIndex_Click(object sender, EventArgs e)
        {
            if (!isLogoned) return;
            Button btn = sender as Button;
            string itemCode = btn.Tag.ToString();
            queryWorldIndex(itemCode);
        }
        private void queryFF(string itemCode)
        {
            int cnt = 100;
            if (rdo200.Checked) cnt = 200;
            else if (rdo500.Checked) cnt = 500;

            Task.Factory.StartNew(() =>
            {
                {
                    string ncnt = "01";
                    Api_WorldFuture apiFF = new Api.Api_WorldFuture();
                    apiFF.ApiLogHandler += (log) => { LogWrite(log); };
                    apiFF.Query(itemCode, "1", ncnt);
                    apiFF.manualEvent.WaitOne();
                    System.Threading.Thread.Sleep(1000);
                    runApi(itemCode, cnt, ncnt, apiFF);
                }

                {
                    string ncnt = "05";
                    Api_WorldFuture apiFF = new Api.Api_WorldFuture();
                    apiFF.ApiLogHandler += (log) => { LogWrite(log); };
                    apiFF.Query(itemCode, "1", ncnt);
                    apiFF.manualEvent.WaitOne();
                    System.Threading.Thread.Sleep(1000);
                    runApi(itemCode, cnt, ncnt, apiFF);
                }

                {
                    string ncnt = "10";
                    Api_WorldFuture apiFF = new Api.Api_WorldFuture();
                    apiFF.ApiLogHandler += (log) => { LogWrite(log); };
                    apiFF.Query(itemCode, "1", ncnt);
                    apiFF.manualEvent.WaitOne();
                    System.Threading.Thread.Sleep(1000);
                    runApi(itemCode, cnt, ncnt, apiFF);
                }

                {
                    string ncnt = "30";
                    Api_WorldFuture apiFF = new Api.Api_WorldFuture();
                    apiFF.ApiLogHandler += (log) => { LogWrite(log); };
                    apiFF.Query(itemCode, "1", ncnt);
                    apiFF.manualEvent.WaitOne();
                    System.Threading.Thread.Sleep(1000);
                    runApi(itemCode, cnt, ncnt, apiFF);
                }
                {
                    string ncnt = "60";
                    Api_WorldFuture apiFF = new Api.Api_WorldFuture();
                    apiFF.ApiLogHandler += (log) => { LogWrite(log); };
                    apiFF.Query(itemCode, "1", ncnt);
                    apiFF.manualEvent.WaitOne();
                    System.Threading.Thread.Sleep(1000);
                    runApi(itemCode, cnt, ncnt, apiFF);
                }

                {
                    string ncnt = "120";
                    Api_WorldFuture apiFF = new Api.Api_WorldFuture();
                    apiFF.ApiLogHandler += (log) => { LogWrite(log); };
                    apiFF.Query(itemCode, "1", ncnt);
                    apiFF.manualEvent.WaitOne();
                    System.Threading.Thread.Sleep(1000);
                    runApi(itemCode, cnt, ncnt, apiFF);
                }

                {
                    string ncnt = "180";
                    Api_WorldFuture apiFF = new Api.Api_WorldFuture();
                    apiFF.ApiLogHandler += (log) => { LogWrite(log); };
                    apiFF.Query(itemCode, "1", ncnt);
                    apiFF.manualEvent.WaitOne();
                    System.Threading.Thread.Sleep(1000);
                    runApi(itemCode, cnt, ncnt, apiFF);
                }

                {
                    string ncnt = "240";
                    Api_WorldFuture apiFF = new Api.Api_WorldFuture();
                    apiFF.ApiLogHandler += (log) => { LogWrite(log); };
                    apiFF.Query(itemCode, "1", ncnt);
                    apiFF.manualEvent.WaitOne();
                    System.Threading.Thread.Sleep(1000);
                    runApi(itemCode, cnt, ncnt, apiFF);
                }
                
                {
                    string ncnt = "300";
                    Api_WorldFuture apiFF = new Api.Api_WorldFuture();
                    apiFF.ApiLogHandler += (log) => { LogWrite(log); };
                    apiFF.Query(itemCode, "1", ncnt);
                    apiFF.manualEvent.WaitOne();
                    System.Threading.Thread.Sleep(1000);
                    runApi(itemCode, cnt, ncnt, apiFF);
                }

                {
                    string ncnt = "360";
                    Api_WorldFuture apiFF = new Api.Api_WorldFuture();
                    apiFF.ApiLogHandler += (log) => { LogWrite(log); };
                    apiFF.Query(itemCode, "1", ncnt);
                    apiFF.manualEvent.WaitOne();
                    System.Threading.Thread.Sleep(1000);
                    runApi(itemCode, cnt, ncnt, apiFF);
                }

                {
                    string ncnt = "420";
                    Api_WorldFuture apiFF = new Api.Api_WorldFuture();
                    apiFF.ApiLogHandler += (log) => { LogWrite(log); };
                    apiFF.Query(itemCode, "1", ncnt);
                    apiFF.manualEvent.WaitOne();
                    System.Threading.Thread.Sleep(1000);
                    runApi(itemCode, cnt, ncnt, apiFF);
                }

                {
                    string ncnt = "720";
                    Api_WorldFuture apiFF = new Api.Api_WorldFuture();
                    apiFF.ApiLogHandler += (log) => { LogWrite(log); };
                    apiFF.Query(itemCode, "1", ncnt);
                    apiFF.manualEvent.WaitOne();
                    System.Threading.Thread.Sleep(1000);
                    runApi(itemCode, cnt, ncnt, apiFF);
                }

                {
                    Api_WorldFutureDWM apiFFDWM = new Api.Api_WorldFutureDWM();
                    apiFFDWM.ApiLogHandler += (log) => { LogWrite(log); };
                    apiFFDWM.Query(itemCode, "2", cnt.ToString());
                    System.Threading.Thread.Sleep(4000);
                }

                isRuning = false;              
            });
        }      
        private static void runApi(string itemCode, int cnt, string ncnt, Api_WorldFuture apiFF)
        {
            if (cnt >= 200)
            {
                apiFF.manualEvent.Reset();
                apiFF.Query(itemCode, "1", ncnt, "100", apiFF.lastCts_Date, apiFF.lastCts_Time);
                apiFF.manualEvent.WaitOne();
                System.Threading.Thread.Sleep(1000);
            }
            if (cnt >= 500)
            {
                apiFF.manualEvent.Reset();
                apiFF.Query(itemCode, "1", ncnt, "100", apiFF.lastCts_Date, apiFF.lastCts_Time);
                apiFF.manualEvent.WaitOne();
                System.Threading.Thread.Sleep(1000);

                apiFF.manualEvent.Reset();
                apiFF.Query(itemCode, "1", ncnt, "100", apiFF.lastCts_Date, apiFF.lastCts_Time);
                apiFF.manualEvent.WaitOne();
                System.Threading.Thread.Sleep(1000);

                apiFF.manualEvent.Reset();
                apiFF.Query(itemCode, "1", ncnt, "100", apiFF.lastCts_Date, apiFF.lastCts_Time);
                apiFF.manualEvent.WaitOne();
                System.Threading.Thread.Sleep(1000);
            }
        }


        private void queryUpjong(string itemCode)
        {
            Task.Factory.StartNew(() =>
            {
                {
                    string ncnt = "01";
                    Api_Upjong api = new Api.Api_Upjong();
                    api.ApiLogHandler += (log) => { LogWrite(log); };
                    api.Query(itemCode, "1", ncnt);
                    api.manualEvent.WaitOne();
                    System.Threading.Thread.Sleep(1000);
                }
                {
                    string ncnt = "05";
                    Api_Upjong api = new Api.Api_Upjong();
                    api.ApiLogHandler += (log) => { LogWrite(log); };
                    api.Query(itemCode, "1", ncnt);
                    api.manualEvent.WaitOne();
                    System.Threading.Thread.Sleep(1000);
                }
                {
                    string ncnt = "10";
                    Api_Upjong api = new Api.Api_Upjong();
                    api.ApiLogHandler += (log) => { LogWrite(log); };
                    api.Query(itemCode, "1", ncnt);
                    api.manualEvent.WaitOne();
                    System.Threading.Thread.Sleep(1000);
                }
                {
                    string ncnt = "30";
                    Api_Upjong api = new Api.Api_Upjong();
                    api.ApiLogHandler += (log) => { LogWrite(log); };
                    api.Query(itemCode, "1", ncnt);
                    api.manualEvent.WaitOne();
                    System.Threading.Thread.Sleep(1000);
                }
                {
                    string ncnt = "60";
                    Api_Upjong api = new Api.Api_Upjong();
                    api.ApiLogHandler += (log) => { LogWrite(log); };
                    api.Query(itemCode, "1", ncnt);
                    api.manualEvent.WaitOne();
                    System.Threading.Thread.Sleep(1000);                    
                }

                {
                    string ncnt = "120";
                    Api_Upjong api = new Api.Api_Upjong();
                    api.ApiLogHandler += (log) => { LogWrite(log); };
                    api.Query(itemCode, "1", ncnt);
                    api.manualEvent.WaitOne();
                    System.Threading.Thread.Sleep(1000);
                }

                {
                    string ncnt = "180";
                    Api_Upjong api = new Api.Api_Upjong();
                    api.ApiLogHandler += (log) => { LogWrite(log); };
                    api.Query(itemCode, "1", ncnt);
                    api.manualEvent.WaitOne();
                    System.Threading.Thread.Sleep(1000);
                }

                {
                    Api_Upjong api = new Api.Api_Upjong();
                    api.ApiLogHandler += (log) => { LogWrite(log); };
                    api.Query(itemCode, "2", "");
                    api.manualEvent.WaitOne();
                    System.Threading.Thread.Sleep(1000);
                }

                {
                    Api_Upjong api = new Api.Api_Upjong();
                    api.ApiLogHandler += (log) => { LogWrite(log); };
                    api.Query(itemCode, "3", "");
                    System.Threading.Thread.Sleep(1000);
                }

                isRuning = false;
            });
        }

        private void queryWorldIndex(string itemCode)
        {
            Task.Factory.StartNew(() =>
            {
                {
                    Api_WorldIndex api = new Api.Api_WorldIndex();
                    api.ApiLogHandler += (log) => { LogWrite(log); };
                    api.Query("S", itemCode, "0");
                    api.manualEvent.WaitOne();
                    System.Threading.Thread.Sleep(1000);
                }
                {
                    Api_WorldIndex api = new Api.Api_WorldIndex();
                    api.ApiLogHandler += (log) => { LogWrite(log); };
                    api.Query("S", itemCode, "1");
                    System.Threading.Thread.Sleep(1000);
                }                

                isRuning = false;
            });
        }
        private void btnReal_Click(object sender, EventArgs e)
        {
            if (!isLogoned) return;

            btnReal.Enabled = false;

            bool isRealCL = true;
            bool isRealNG = true;
            bool isRealGC = true;
            bool isRealSI = true;
            bool isRealHSI = true;
            bool isRealNQ = true;
            bool isRealURO = true;
            bool isRealES = true;

            Task.Factory.StartNew(() => {    
                if (isRealCL)
                {
                    Api.Api_WorldFutureReal realFFCL = new Api_WorldFutureReal();
                    realFFCL.Query(ItemCodeSet.GetItemSHCodeByName(ItemCode.선물_해외_WTI), "CL");
                    System.Threading.Thread.Sleep(5000);
                }
                if (isRealNG)
                {
                    Api.Api_WorldFutureReal realFFNG = new Api_WorldFutureReal();
                    realFFNG.Query(ItemCodeSet.GetItemSHCodeByName(ItemCode.선물_해외_NG), "NG");
                    System.Threading.Thread.Sleep(5000);
                }
                if (isRealGC)
                {
                    Api.Api_WorldFutureReal realFFGC = new Api_WorldFutureReal();
                    realFFGC.Query(ItemCodeSet.GetItemSHCodeByName(ItemCode.선물_해외_GOLD), "GC");
                    System.Threading.Thread.Sleep(5000);
                }
                if (isRealSI)
                {
                    Api.Api_WorldFutureReal realFFSI = new Api_WorldFutureReal();
                    realFFSI.Query(ItemCodeSet.GetItemSHCodeByName(ItemCode.선물_해외_SILVER), "SI");
                    System.Threading.Thread.Sleep(5000);
                }
                if (isRealHSI)
                {
                    Api.Api_WorldFutureReal realFFHSI = new Api_WorldFutureReal();
                    realFFHSI.Query(ItemCodeSet.GetItemSHCodeByName(ItemCode.선물_해외_항셍), "HMH");
                    System.Threading.Thread.Sleep(5000);
                }
                if (isRealNQ)
                {
                    Api.Api_WorldFutureReal realFFNQ = new Api_WorldFutureReal();
                    realFFNQ.Query(ItemCodeSet.GetItemSHCodeByName(ItemCode.선물_해외_나스닥), "NQ");
                    System.Threading.Thread.Sleep(5000);
                }
                if (isRealURO)
                {
                    Api.Api_WorldFutureReal realFFURO = new Api_WorldFutureReal();
                    realFFURO.Query(ItemCodeSet.GetItemSHCodeByName(ItemCode.선물_해외_유로FX), "URO");
                }
                if (isRealES)
                {
                    Api.Api_WorldFutureReal realFFES = new Api_WorldFutureReal();
                    realFFES.Query(ItemCodeSet.GetItemSHCodeByName(ItemCode.선물_해외_SNP), "ES");
                }
            });
        }
        private void btnRealKr_Click(object sender, EventArgs e)
        {
            if (!isLogoned) return;

            btnRealKr.Enabled = false;

            bool isRealKospi = true;
            bool isRealKospi200 = true;
            bool isRealKosdaq = true;

            Task.Factory.StartNew(() => {
                if (isRealKospi)
                {
                    Api.Api_IndexReal real = new Api_IndexReal();
                    real.Query(ItemCodeSet.GetItemSHCodeByName(ItemCode.지수_국내_코스피));
                    System.Threading.Thread.Sleep(5000);
                }
                
                if (isRealKospi200)
                {
                    Api.Api_IndexReal real = new Api_IndexReal();
                    real.Query(ItemCodeSet.GetItemSHCodeByName(ItemCode.지수_국내_코스피200));
                    System.Threading.Thread.Sleep(5000);
                }
                
                if (isRealKosdaq)
                {
                    Api.Api_IndexReal real = new Api_IndexReal();
                    real.Query(ItemCodeSet.GetItemSHCodeByName(ItemCode.지수_국내_코스닥));
                }
            });
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            new OrderAppForm().Show();
        }
        private void chkAuto_Changed(object sender, EventArgs e)
        {
            var c = sender as CheckBox;
            
            if (c.Text == "CL") isAutoFFCL  = c.Checked;
            if (c.Text == "NG") isAutoFFNG  = c.Checked;
            if (c.Text == "GC") isAutoFFGC  = c.Checked;
            if (c.Text == "SI") isAutoFFSI  = c.Checked;
            if (c.Text == "HMH") isAutoFFHSI = c.Checked;
            if (c.Text == "NQ") isAutoFFNQ = c.Checked;
            if (c.Text == "URO") isAutoFFURO = c.Checked;
            if (c.Text == "ES") isAutoFFES = c.Checked;
        }
        private void chkAutoKr_Changed(object sender, EventArgs e)
        {
            var c = sender as CheckBox;

            if (c.Tag.ToString() == "KOSPI") isAutoKospi = c.Checked;
            if (c.Tag.ToString() == "KOSDAQ") isAutoKosdaq = c.Checked;
           
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (! session.IsConnected()) {
                timer1.Stop();
                timer1.Enabled = false;
                isLogoned = false;
                btnReal.Enabled = true;
                btnLogin.Enabled = true;
                noti.BalloonTipIcon = ToolTipIcon.Warning;
                noti.BalloonTipText = "Disconnect Xing";
                noti.BalloonTipTitle = "Disconnect Xing";
                noti.ShowBalloonTip(5000);

                var player = new System.Media.SoundPlayer();
                player.Stream = Properties.Resources.통신단절;
                player.Play();

                var task = Atman.Chakra.ExApi.TelegramBotApi.SendMessageAsync("시세 연결이 끊어짐");

                //Task.Factory.StartNew(() => {
                //    System.Threading.Thread.Sleep(1000 * 30);
                //    this.Invoke(new MethodInvoker(() =>
                //    {
                //        btnLogin.PerformClick();
                //    }));
                //});
               
                //Task.Factory.StartNew(() => {
                //    System.Threading.Thread.Sleep(1000 * 30);
                //    this.Invoke(new MethodInvoker(() =>
                //    {
                //        btnReal.PerformClick();
                //    }));
                //});
            }
        }
        private void chkSendRealtime_CheckedChanged(object sender, EventArgs e)
        {
            Api_WorldFutureReal.IsSend = chkSendRealtime.Checked;
        }
        private void chkSendRealtimeKr_CheckedChanged(object sender, EventArgs e)
        {
            Api_IndexReal.IsSend = chkSendRealtimeKr.Checked;
        }
        private void btnFileOpen_Click(object sender, EventArgs e)
        {
            string path = Environment.CurrentDirectory;

            string folder = (sender as Button).Text.ToString() ;

            string fullPath = System.IO.Path.Combine(path, "data", folder);

            if (!System.IO.Directory.Exists(fullPath)) return;

            var fileList = Directory.GetFiles(fullPath);

            foreach (var f in fileList)
            {
                TimeIntervalEnum timeIntervalEnum =  TimeIntervalEnum.None;

                if (f.IndexOf("(720분)") > -1) timeIntervalEnum = TimeIntervalEnum.Hour_12;
                else if (f.IndexOf("(480분)") > -1) timeIntervalEnum = TimeIntervalEnum.Hour_08;
                else if (f.IndexOf("(360분)") > -1) timeIntervalEnum = TimeIntervalEnum.Hour_06;
                else if (f.IndexOf("(300분)") > -1) timeIntervalEnum = TimeIntervalEnum.Hour_05;
                else if (f.IndexOf("(240분)") > -1) timeIntervalEnum = TimeIntervalEnum.Hour_04;
                else if (f.IndexOf("(180분)") > -1) timeIntervalEnum = TimeIntervalEnum.Hour_03;
                else if (f.IndexOf("(120분)") > -1) timeIntervalEnum = TimeIntervalEnum.Hour_02;
                else if (f.IndexOf("(60분)") > -1) timeIntervalEnum = TimeIntervalEnum.Hour_01;
                else if (f.IndexOf("(일)") > -1) timeIntervalEnum = TimeIntervalEnum.Day;
                else if (f.IndexOf("(주)") > -1) timeIntervalEnum = TimeIntervalEnum.Day;
                else if (f.IndexOf("(01분)") > -1) timeIntervalEnum = TimeIntervalEnum.Minute_01;
                else if (f.IndexOf("(05분)") > -1) timeIntervalEnum = TimeIntervalEnum.Minute_05;
                else if (f.IndexOf("(10분)") > -1) timeIntervalEnum = TimeIntervalEnum.Minute_10;
                else if (f.IndexOf("(30분)") > -1) timeIntervalEnum = TimeIntervalEnum.Minute_30;

                if (timeIntervalEnum == TimeIntervalEnum.None) continue;

                List<S_CandleItemData> dummyList = new List<S_CandleItemData>();

                using (var reader = new StreamReader(f))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();

                        if (line.StartsWith("[")) continue;
                        
                        string[] values = null;

                        if (f.ToUpper().EndsWith(".TXT"))
                            values = line.Split("\t".ToCharArray());
                        else if (f.ToUpper().EndsWith(".CSV"))
                            values = line.Split(',');
                        else 
                            values = line.Split(',');

                        S_CandleItemData data = new S_CandleItemData();
                        if (timeIntervalEnum == TimeIntervalEnum.Day || timeIntervalEnum == TimeIntervalEnum.Week)
                        {
                            data.DTime = Convert.ToDateTime(values[0].Trim());
                            data.ItemCode = folder.ToUpper();
                            data.OpenPrice = Convert.ToSingle(values[1].Trim());
                            data.HighPrice = Convert.ToSingle(values[2].Trim());
                            data.LowPrice = Convert.ToSingle(values[3].Trim());
                            data.ClosePrice = Convert.ToSingle(values[4].Trim());
                            data.Volume = 0;
                        }
                        else
                        {
                            data.DTime = Convert.ToDateTime(values[0].Trim() + " " + values[1].Trim());
                            data.ItemCode = folder.ToUpper();
                            data.OpenPrice = Convert.ToSingle(values[2].Trim());
                            data.HighPrice = Convert.ToSingle(values[3].Trim());
                            data.LowPrice = Convert.ToSingle(values[4].Trim());
                            data.ClosePrice = Convert.ToSingle(values[5].Trim());
                            data.Volume = 0;
                        }
                        dummyList.Add(data);
                    }
                }

                dummyList.Reverse();
                foreach (var data in dummyList)
                {
                    try
                    {
                        PPContext.Instance.ClientContext.SetCandleSourceData(data.ItemCode, timeIntervalEnum, data);

                        LogWrite($"date : {data.DTime} opne : {data.OpenPrice} high : {data.HighPrice} low : {data.LowPrice} close : {data.ClosePrice} ");
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                }
            }
        }

        private void XingAppForm_Load_1(object sender, EventArgs e)
        {
            btnLogin.PerformClick();
        }

       
    }
}
