﻿using MaterialSkin;
using MaterialSkin.Controls;
using MetroFramework.Forms;
using OM.Atman.Chakra.Ctx;
using OM.Lib.Base.Enums;
using OM.PP.Chakra.Ctx;
using OM.PP.XingApp.Api;
using OM.PP.XingApp.Config;
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
using XA_SESSIONLib;

namespace OM.PP.XingApp
{
    public partial class XingAppForm : Form
    {
        //xing 인증
        private XASessionClass session = new XASessionClass();

        System.Threading.Timer timer = null;
        System.Threading.Timer timerTick = null;
        bool isRuning = false;
        bool isRuningTick = false;
        bool isLogoned = false;

        bool isAutoFFCL  = false;
        bool isAutoFFNG  = false;
        bool isAutoFFGC  = false;
        bool isAutoFFSI  = false;
        bool isAutoFFHSI = false;
        bool isAutoFFNQ = false;
        bool isAutoFFURO = false;
        bool isAutoFFES = false;

        public XingAppForm()
        {
            InitializeComponent();           
            Control.CheckForIllegalCrossThreadCalls = false;
            this.Load += XingAppForm_Load;
            serverInfo();
            cbLogin.SelectedIndex = 0;
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
            
            try
            {
                XingServerConfigData.IP = ConfigurationManager.AppSettings["XingService_IP"];
                XingServerConfigData.Port = Convert.ToInt32(ConfigurationManager.AppSettings["XingService_Port"]);
                XingContext.Instance.StartServer();
            }
            catch (System.Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void InitializeTimer()
        {
            if(timer == null)
                timer = new System.Threading.Timer(timer_Tick, null, TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(15));

            //if(timerTick == null)
            //    timerTick = new System.Threading.Timer(timer_TickTick, null, TimeSpan.FromSeconds(50), TimeSpan.FromSeconds(15));
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
        private void timer_TickTick(object state)
        {
            if (!isLogoned) return;
            if (isRuningTick) return;
            isRuningTick = true;
            Task.Factory.StartNew(() => {
                if (isAutoFFGC)
                {
                    queryFFRTick(ItemCodeSet.GetItemCode(ItemCode.선물_해외_GOLD));
                    System.Threading.Thread.Sleep(1500);
                }
                if (isAutoFFNG)
                {
                    queryFFRTick(ItemCodeSet.GetItemCode(ItemCode.선물_해외_NG));
                    System.Threading.Thread.Sleep(1500);
                }
                if (isAutoFFSI)
                {
                    queryFFRTick(ItemCodeSet.GetItemCode(ItemCode.선물_해외_SILVER));
                    System.Threading.Thread.Sleep(1500);
                }
                if (isAutoFFCL)
                {
                    queryFFRTick(ItemCodeSet.GetItemCode(ItemCode.선물_해외_WTI));
                    System.Threading.Thread.Sleep(1500);
                }
                if (isAutoFFNQ)
                {
                    queryFFRTick(ItemCodeSet.GetItemCode(ItemCode.선물_해외_나스닥));
                    System.Threading.Thread.Sleep(1500);
                }
                if (isAutoFFHSI)
                {
                    queryFFRTick(ItemCodeSet.GetItemCode(ItemCode.선물_해외_항셍));
                    System.Threading.Thread.Sleep(1500);
                }
                if (isAutoFFURO)
                {
                    queryFFRTick(ItemCodeSet.GetItemCode(ItemCode.선물_해외_유로FX));
                    System.Threading.Thread.Sleep(1500);
                }
                if (isAutoFFES)
                {
                    queryFFRTick(ItemCodeSet.GetItemCode(ItemCode.선물_해외_SNP));
                    System.Threading.Thread.Sleep(1500);
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
                isRuningTick = false;
        }

        #region XingLoginLogout
        private void XingAppForm_Load(object sender, EventArgs e)
        {
            session._IXASessionEvents_Event_Login += Session__IXASessionEvents_Event_Login;
            session.Disconnect += Session_Disconnect;
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
                    //, (int)XA_SESSIONLib.XA_SERVER_TYPE.XA_REAL_SERVER
                    , (int)XA_SESSIONLib.XA_SERVER_TYPE.XA_SIMUL_SERVER
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
        private void btnFFTick_Click(object sender, EventArgs e)
        {
            if (!isLogoned) return;
            Button btn = sender as Button;
            string itemCode = btn.Text;            
            queryFFTick(itemCode);
        }
        private void queryFF(string itemCode)
        {
            int cnt = 100;
            if (rdo200.Checked) cnt = 200;
            else if (rdo500.Checked) cnt = 500;

            Task.Factory.StartNew(() =>
            {
                //{                   
                //    Api_WorldFuture apiFF = new Api.Api_WorldFuture();
                //    apiFF.ApiLogHandler += (log) => { LogWrite(log); };
                //    apiFF.Query(itemCode, "1", "5");
                //    System.Threading.Thread.Sleep(4000);
                //}
                {
                    string ncnt = "10";
                    Api_WorldFuture apiFF = new Api.Api_WorldFuture();
                    apiFF.ApiLogHandler += (log) => { LogWrite(log); };
                    apiFF.Query(itemCode, "1", ncnt);
                    apiFF.manualEvent.WaitOne();
                    System.Threading.Thread.Sleep(1000);

                    if (cnt > 200)
                    {
                        apiFF.manualEvent.Reset();
                        apiFF.Query(itemCode, "1", ncnt, "100", apiFF.lastCts_Date, apiFF.lastCts_Time);
                        apiFF.manualEvent.WaitOne();
                        System.Threading.Thread.Sleep(1000);
                    }
                    if (cnt == 500)
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

                {
                    string ncnt = "30";
                    Api_WorldFuture apiFF = new Api.Api_WorldFuture();
                    apiFF.ApiLogHandler += (log) => { LogWrite(log); };
                    apiFF.Query(itemCode, "1", ncnt);
                    apiFF.manualEvent.WaitOne();
                    System.Threading.Thread.Sleep(1000);

                    if (cnt > 200)
                    {
                        apiFF.manualEvent.Reset();
                        apiFF.Query(itemCode, "1", ncnt, "100", apiFF.lastCts_Date, apiFF.lastCts_Time);
                        apiFF.manualEvent.WaitOne();
                        System.Threading.Thread.Sleep(1000);
                    }
                    if (cnt == 500)
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
                {
                    string ncnt = "60";
                    Api_WorldFuture apiFF = new Api.Api_WorldFuture();
                    apiFF.ApiLogHandler += (log) => { LogWrite(log); };
                    apiFF.Query(itemCode, "1", ncnt);
                    apiFF.manualEvent.WaitOne();
                    System.Threading.Thread.Sleep(1000);

                    if (cnt > 200)
                    {
                        apiFF.manualEvent.Reset();
                        apiFF.Query(itemCode, "1", ncnt, "100", apiFF.lastCts_Date, apiFF.lastCts_Time);
                        apiFF.manualEvent.WaitOne();
                        System.Threading.Thread.Sleep(1000);
                    }
                    if (cnt == 500)
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
                {
                    string ncnt = "120";
                    Api_WorldFuture apiFF = new Api.Api_WorldFuture();
                    apiFF.ApiLogHandler += (log) => { LogWrite(log); };
                    apiFF.Query(itemCode, "1", ncnt);
                    apiFF.manualEvent.WaitOne();
                    System.Threading.Thread.Sleep(1000);

                    if (cnt > 200)
                    {
                        apiFF.manualEvent.Reset();
                        apiFF.Query(itemCode, "1", ncnt, "100", apiFF.lastCts_Date, apiFF.lastCts_Time);
                        apiFF.manualEvent.WaitOne();
                        System.Threading.Thread.Sleep(1000);
                    }
                    if (cnt == 500)
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

                {
                    Api_WorldFutureDWM apiFFDWM = new Api.Api_WorldFutureDWM();
                    apiFFDWM.ApiLogHandler += (log) => { LogWrite(log); };
                    apiFFDWM.Query(itemCode, "2", cnt.ToString());
                    System.Threading.Thread.Sleep(4000);
                }

                isRuning = false;              
            });
        }
        private void queryFFTick(string itemCode)
        {
            //PPContext.Instance.ClientContext.InitSourceTickData(itemCode);
            Task.Factory.StartNew(() =>
            {
                {
                    Api_WorldFutureTick apiFF = new Api.Api_WorldFutureTick(100);
                    apiFF.ApiLogHandler += (log) => { LogWrite(log); };
                    apiFF.Query(itemCode, "180");
                    apiFF.manualEvent.WaitOne();
                    System.Threading.Thread.Sleep(1000);
                }
                {
                    Api_WorldFutureTick apiFF = new Api.Api_WorldFutureTick(100);
                    apiFF.ApiLogHandler += (log) => { LogWrite(log); };
                    apiFF.Query(itemCode, "360");
                    apiFF.manualEvent.WaitOne();
                    System.Threading.Thread.Sleep(1000);
                }
                {
                    Api_WorldFutureTick apiFF = new Api.Api_WorldFutureTick(100);
                    apiFF.ApiLogHandler += (log) => { LogWrite(log); };
                    apiFF.Query(itemCode, "720");
                    apiFF.manualEvent.WaitOne();
                    System.Threading.Thread.Sleep(1000);
                }
                {
                    Api_WorldFutureTick apiFF = new Api.Api_WorldFutureTick(100);
                    apiFF.ApiLogHandler += (log) => { LogWrite(log); };
                    apiFF.Query(itemCode, "1080");
                    apiFF.manualEvent.WaitOne();
                    System.Threading.Thread.Sleep(1000);
                }
                {
                    Api_WorldFutureTick apiFF = new Api.Api_WorldFutureTick(100);
                    apiFF.ApiLogHandler += (log) => { LogWrite(log); };
                    apiFF.Query(itemCode, "1440");
                    //apiFF.manualEvent.WaitOne();
                    //System.Threading.Thread.Sleep(1000);
                }
            });
        }
        private void queryFFR(string itemCode)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    {
                        Api_WorldFuture apiFF = new Api.Api_WorldFuture();
                        apiFF.ApiLogHandler += (log) => { LogWrite(log); };
                        apiFF.Query(itemCode, "1", "5", "50");
                        System.Threading.Thread.Sleep(1500);
                    }
                    {
                        Api_WorldFuture apiFF = new Api.Api_WorldFuture();
                        apiFF.ApiLogHandler += (log) => { LogWrite(log); };
                        apiFF.Query(itemCode, "1", "10", "50");
                        System.Threading.Thread.Sleep(1500);
                    }
                    {
                        Api_WorldFuture apiFF = new Api.Api_WorldFuture();
                        apiFF.ApiLogHandler += (log) => { LogWrite(log); };
                        apiFF.Query(itemCode, "1", "30", "50");
                        System.Threading.Thread.Sleep(1500);
                    }
                    {
                        Api_WorldFuture apiFF = new Api.Api_WorldFuture();
                        apiFF.ApiLogHandler += (log) => { LogWrite(log); };
                        apiFF.Query(itemCode, "1", "60", "50");
                        System.Threading.Thread.Sleep(1500);
                    }
                    {
                        Api_WorldFuture apiFF = new Api.Api_WorldFuture();
                        apiFF.ApiLogHandler += (log) => { LogWrite(log); };
                        apiFF.Query(itemCode, "1", "120", "50");
                        System.Threading.Thread.Sleep(1500);
                    }
                    {
                        Api_WorldFutureDWM apiFFDWM = new Api.Api_WorldFutureDWM();
                        apiFFDWM.ApiLogHandler += (log) => { LogWrite(log); };
                        apiFFDWM.Query(itemCode, "2", "50");
                        System.Threading.Thread.Sleep(1500);
                    }
                }
                catch (Exception ex) { }
                finally
                {
                    isRuning = false;
                }
            });
        }
        private void queryFFRTick(string itemCode)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    PPContext.Instance.ClientContext.InitSourceTickData(itemCode);
                    {
                        Api_WorldFutureTick apiFF = new Api.Api_WorldFutureTick(100);
                        apiFF.ApiLogHandler += (log) => { LogWrite(log); };
                        apiFF.Query(itemCode, "180");
                        apiFF.manualEvent.WaitOne();
                        System.Threading.Thread.Sleep(1000);
                    }
                    {
                        Api_WorldFutureTick apiFF = new Api.Api_WorldFutureTick(100);
                        apiFF.ApiLogHandler += (log) => { LogWrite(log); };
                        apiFF.Query(itemCode, "360");
                        apiFF.manualEvent.WaitOne();
                        System.Threading.Thread.Sleep(1000);
                    }
                    {
                        Api_WorldFutureTick apiFF = new Api.Api_WorldFutureTick(100);
                        apiFF.ApiLogHandler += (log) => { LogWrite(log); };
                        apiFF.Query(itemCode, "720");
                        apiFF.manualEvent.WaitOne();
                        System.Threading.Thread.Sleep(1000);
                    }
                    {
                        Api_WorldFutureTick apiFF = new Api.Api_WorldFutureTick(100);
                        apiFF.ApiLogHandler += (log) => { LogWrite(log); };
                        apiFF.Query(itemCode, "1080");
                        apiFF.manualEvent.WaitOne();
                        System.Threading.Thread.Sleep(1000);
                    }
                    {
                        Api_WorldFutureTick apiFF = new Api.Api_WorldFutureTick(100);
                        apiFF.ApiLogHandler += (log) => { LogWrite(log); };
                        apiFF.Query(itemCode, "1440");
                        //apiFF.manualEvent.WaitOne();
                        //System.Threading.Thread.Sleep(1000);
                    }
                }
                catch (Exception ex) { }
                finally
                {
                    isRuningTick = false;
                }
            });
        }
                     
        private void btnReal_Click(object sender, EventArgs e)
        {
            if (!isLogoned) return;

            btnReal.Enabled = false;

            //bool isRealCL = chkRealCL.Checked;
            //bool isRealNG = chkRealNG.Checked;
            //bool isRealGC = chkRealGC.Checked;
            //bool isRealSI = chkRealSI.Checked;
            //bool isRealHSI = chkRealHSI.Checked;
            //bool isRealNQ = chkRealNQ.Checked;
            //bool isRealURO = chkRealURO.Checked;

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
    }
}
