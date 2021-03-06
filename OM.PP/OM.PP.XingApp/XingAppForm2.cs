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
    public partial class XingAppForm2 : MetroForm
    {
        //xing 인증
        private XASessionClass session = new XASessionClass();

        System.Threading.Timer timer = null;

        Api_Index apiIndex = new Api.Api_Index();
        Api_IndexFuture apiIndexF = new Api.Api_IndexFuture();
        Api_IndexFutureDWM apiIndexFDWM = new Api.Api_IndexFutureDWM();
   

        bool isRuning = false;
        
        bool isLogoned = false;

        bool isAutoKoreaIndex = false;
        bool isAutoKoreaFuture = false;
        bool isAutoFFCL  = false;
        bool isAutoFFNG  = false;
        bool isAutoFFGC  = false;
        bool isAutoFFSI  = false;
        bool isAutoFFHSI = false;
        bool isAutoFFNQ = false;
        bool isAutoFFURO = false;

        bool isSepFFCL = false;
        bool isSepFFNG = false;
        bool isSepFFGC = false;
        bool isSepFFSI = false;
        bool isSepFFHSI = false;
        bool isSepFFNQ = false;
        bool isSepFFURO = false;
        public XingAppForm2()
        {
            InitializeComponent();
           
            Control.CheckForIllegalCrossThreadCalls = false;
            this.Load += XingAppForm_Load;

            apiIndex.ApiLogHandler += (log) => { LogWrite(log); };
            apiIndexF.ApiLogHandler += (log) => { LogWrite(log); };
            apiIndexFDWM.ApiLogHandler += (log) => { LogWrite(log); };           
      
            serverInfo();

            cbLogin.SelectedIndex = 0;
        }
        private void serverInfo()
        {
            PPServerConfigData.IP = ConfigurationManager.AppSettings["PPService_IP"];
            PPServerConfigData.Port = Convert.ToInt32(ConfigurationManager.AppSettings["PPService_Port"]);
            PPContext.Instance.OnCreateClient();

            AtmanServerConfigData.IP = ConfigurationManager.AppSettings["AtmanService_IP"];
            AtmanServerConfigData.Port = Convert.ToInt32(ConfigurationManager.AppSettings["AtmanService_Port"]);
            AtmanContext.Instance.OnCreateClient();
        }

        private void InitializeTimer()
        {
            timer = new System.Threading.Timer(timer_Tick, null, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(10));           
        }

        private void timer_Tick(object state)
        {
            if (isRuning) return;

            isRuning = true;
            if (isAutoKoreaIndex)
            {
                queryKoreaIndexR(ItemCodeSet.GetItemCode(ItemCode.지수_국내_코스피200));
                System.Threading.Thread.Sleep(3000);                
            }
            if (isAutoKoreaFuture)
            {
                queryKoreaIndexFR(ItemCodeSet.GetItemCode(ItemCode.선물_국내_코스피200));
                System.Threading.Thread.Sleep(3000);
            }

            if (isAutoFFGC)
            {
                queryFFR(ItemCodeSet.GetItemCode(ItemCode.선물_해외_GOLD));
                System.Threading.Thread.Sleep(3000);
            }
            if (isAutoFFNG)
            {
                queryFFR(ItemCodeSet.GetItemCode(ItemCode.선물_해외_NG));
                System.Threading.Thread.Sleep(3000);
            }
            if (isAutoFFSI)
            {
                queryFFR(ItemCodeSet.GetItemCode(ItemCode.선물_해외_SILVER));
                System.Threading.Thread.Sleep(3000);
            }
            if (isAutoFFCL)
            {
                queryFFR(ItemCodeSet.GetItemCode(ItemCode.선물_해외_WTI));
                System.Threading.Thread.Sleep(3000);
            }
            if (isAutoFFNQ)
            {
                queryFFR(ItemCodeSet.GetItemCode(ItemCode.선물_해외_나스닥));
                System.Threading.Thread.Sleep(3000);
            }
            if (isAutoFFHSI)
            {
                queryFFR(ItemCodeSet.GetItemCode(ItemCode.선물_해외_항셍));
                System.Threading.Thread.Sleep(3000);
            }
            if (isAutoFFURO)
            {
                queryFFR(ItemCodeSet.GetItemCode(ItemCode.선물_해외_유로FX));
                System.Threading.Thread.Sleep(3000);
            }
            isRuning = false;
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
            //MessageBox.Show(szMsg);

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
                    //, (int)XA_SESSIONLib.XA_SERVER_TYPE.XA_REAL_SERVER
                    , (int)XA_SESSIONLib.XA_SERVER_TYPE.XA_SIMUL_SERVER
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

                try
                {
                    XingServerConfigData.IP = ConfigurationManager.AppSettings["XingService_IP"];
                    XingServerConfigData.Port = Convert.ToInt32(ConfigurationManager.AppSettings["XingService_Port"]);
                    XingContext.Instance.StartServer();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

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

        #region QueryButtonEvent
        private void btnKospi200_Click(object sender, EventArgs e)
        {
            if (!isLogoned) return;
            queryKoreaIndex(ItemCodeSet.GetItemCode(ItemCode.지수_국내_코스피200));
        }
        private void btnKospi200F_Click(object sender, EventArgs e)
        {
            if (!isLogoned) return;
            queryKoreaIndexF(ItemCodeSet.GetItemCode(ItemCode.선물_국내_코스피200));
        }
        private void btnFF_Click(object sender, EventArgs e)
        {
            if (!isLogoned) return;
            Button btn = sender as Button;
            queryFF(btn.Text);
        }
        #endregion

        private void queryKoreaIndex(string itemCode)
        {
            //apiIndex.Query(itemCode, "2", "1");
            //System.Threading.Thread.Sleep(3000);

            Task.Factory.StartNew(() =>
            {
                apiIndex.Query(itemCode, "1", "1");
                System.Threading.Thread.Sleep(3000);

                apiIndex.Query(itemCode, "1", "5"); 
                System.Threading.Thread.Sleep(3000);

                apiIndex.Query(itemCode, "1", "10");
                System.Threading.Thread.Sleep(3000);

                apiIndex.Query(itemCode, "1", "30"); 
                System.Threading.Thread.Sleep(3000);

                apiIndex.Query(itemCode, "1", "60");
                System.Threading.Thread.Sleep(3000);

                apiIndex.Query(itemCode, "1", "120"); 
                System.Threading.Thread.Sleep(3000);

                apiIndex.Query(itemCode, "2");
                System.Threading.Thread.Sleep(3000);

                apiIndex.Query(itemCode, "3");
                System.Threading.Thread.Sleep(3000);

                apiIndex.Query(itemCode, "4");
                System.Threading.Thread.Sleep(3000);
            });
        }
        private void queryKoreaIndexR(string itemCode)
        {
            apiIndex.Query(itemCode, "1", "1", "10"); 
            System.Threading.Thread.Sleep(3000);

            apiIndex.Query(itemCode, "1", "5", "5"); 
            System.Threading.Thread.Sleep(3000);

            apiIndex.Query(itemCode, "1", "10", "3");
            System.Threading.Thread.Sleep(3000);

            apiIndex.Query(itemCode, "1", "30", "1");
            System.Threading.Thread.Sleep(3000);

            apiIndex.Query(itemCode, "1", "60", "1");
            System.Threading.Thread.Sleep(3000);

            apiIndex.Query(itemCode, "1", "120", "1");
            System.Threading.Thread.Sleep(3000);

            apiIndex.Query(itemCode, "2", "1", "1");
            System.Threading.Thread.Sleep(3000);
        }

        private void queryKoreaIndexF(string itemCode)
        {   
            Task.Factory.StartNew(() =>
            {
                apiIndexF.Query(itemCode, "1", "1"); 
                System.Threading.Thread.Sleep(3000);

                apiIndexF.Query(itemCode, "1", "5");
                System.Threading.Thread.Sleep(3000);

                apiIndexF.Query(itemCode, "1", "10");
                System.Threading.Thread.Sleep(3000);

                apiIndexF.Query(itemCode, "1", "30");
                System.Threading.Thread.Sleep(3000);

                apiIndexF.Query(itemCode, "1", "60");
                System.Threading.Thread.Sleep(3000);

                apiIndexF.Query(itemCode, "1", "120");
                System.Threading.Thread.Sleep(3000);

                apiIndexFDWM.Query(itemCode, "2");
                System.Threading.Thread.Sleep(3000);

                apiIndexFDWM.Query(itemCode, "3");
                System.Threading.Thread.Sleep(3000);

                apiIndexFDWM.Query(itemCode, "4");
                System.Threading.Thread.Sleep(3000);
            });
        }
        private void queryKoreaIndexFR(string itemCode)
        {
            apiIndexF.Query(itemCode, "1", "1", "100");
            System.Threading.Thread.Sleep(3000);

            apiIndexF.Query(itemCode, "1", "5", "50");
            System.Threading.Thread.Sleep(3000);

            apiIndexF.Query(itemCode, "1", "10", "30");
            System.Threading.Thread.Sleep(3000);

            apiIndexF.Query(itemCode, "1", "30", "10");
            System.Threading.Thread.Sleep(3000);

            apiIndexF.Query(itemCode, "1", "60", "10");
            System.Threading.Thread.Sleep(3000);

            apiIndexF.Query(itemCode, "1", "120", "10");
            System.Threading.Thread.Sleep(3000);

            apiIndexFDWM.Query(itemCode, "2", "1");
            System.Threading.Thread.Sleep(3000);
        }
        
        private void queryFF(string itemCode)
        {
            //apiFF.Query(itemCode, "1", "1");
            //apiFFDWM.Query(itemCode, "2");

            Task.Factory.StartNew(() =>
            {
                Api_WorldFuture apiFF = new Api.Api_WorldFuture();
                Api_WorldFutureDWM apiFFDWM = new Api.Api_WorldFutureDWM();
                apiFF.ApiLogHandler += (log) => { LogWrite(log); };
                apiFFDWM.ApiLogHandler += (log) => { LogWrite(log); };

                apiFF.Query(itemCode, "1", "1");
                System.Threading.Thread.Sleep(3000);

                apiFF.Query(itemCode, "1", "5");
                System.Threading.Thread.Sleep(3000);

                apiFF.Query(itemCode, "1", "10");
                System.Threading.Thread.Sleep(3000);

                apiFF.Query(itemCode, "1", "30");
                System.Threading.Thread.Sleep(3000);

                apiFF.Query(itemCode, "1", "60");
                System.Threading.Thread.Sleep(3000);

                apiFF.Query(itemCode, "1", "120"); 
                System.Threading.Thread.Sleep(3000);

                apiFFDWM.Query(itemCode, "2"); 
                System.Threading.Thread.Sleep(3000);

                apiFFDWM.Query(itemCode, "3");
                System.Threading.Thread.Sleep(3000);

                apiFFDWM.Query(itemCode, "4"); 
                System.Threading.Thread.Sleep(3000);
            });
        }
        private void queryFFR(string itemCode)
        {
            Api_WorldFuture apiFF = new Api.Api_WorldFuture();
            Api_WorldFutureDWM apiFFDWM = new Api.Api_WorldFutureDWM();
            apiFF.ApiLogHandler += (log) => { LogWrite(log); };
            apiFFDWM.ApiLogHandler += (log) => { LogWrite(log); };

            apiFF.Query(itemCode, "1", "1", "50");
            System.Threading.Thread.Sleep(3000);

            apiFF.Query(itemCode, "1", "5", "50");
            System.Threading.Thread.Sleep(3000);

            apiFF.Query(itemCode, "1", "10", "50"); 
            System.Threading.Thread.Sleep(3000);

            apiFF.Query(itemCode, "1", "30", "50");
            System.Threading.Thread.Sleep(3000);

            apiFF.Query(itemCode, "1", "60", "50");
            System.Threading.Thread.Sleep(3000);

            apiFF.Query(itemCode, "1", "120", "50");
            System.Threading.Thread.Sleep(3000);

            apiFFDWM.Query(itemCode, "2", "50");
            System.Threading.Thread.Sleep(3000);
        }

        private void btnKospiSeparate_Click(object sender, EventArgs e)
        {
            if (!isLogoned) return;
            //string itemCode = ItemCodeSet.GetItemCode(ItemCode.선물_국내_코스피200);
            string itemCode = ItemCodeSet.GetItemCode(ItemCode.선물_국내_코스피200);
            Button btn = sender as Button;
            Task.Factory.StartNew(() =>
            {
                switch (btn.Text)
                {
                    case "1분":
                        apiIndex.Query(itemCode, "1", "1", "100");
                        System.Threading.Thread.Sleep(3000);
                        apiIndexF.Query(itemCode, "1", "1", "100");
                        break;
                    case "5분":
                        apiIndex.Query(itemCode, "1", "5", "50");
                        System.Threading.Thread.Sleep(3000);
                        apiIndexF.Query(itemCode, "1", "5", "50");
                        break;
                    case "10분":
                        apiIndex.Query(itemCode, "1", "10", "40");
                        System.Threading.Thread.Sleep(3000);
                        apiIndexF.Query(itemCode, "1", "10", "40");
                        break;
                    case "30분":
                        apiIndex.Query(itemCode, "1", "30", "30");
                        System.Threading.Thread.Sleep(3000);
                        apiIndexF.Query(itemCode, "1", "30", "30");
                        break;
                    case "60분":
                        apiIndex.Query(itemCode, "1", "60", "20");
                        System.Threading.Thread.Sleep(3000);
                        apiIndexF.Query(itemCode, "1", "60", "20");
                        break;
                    case "120분":
                        apiIndex.Query(itemCode, "1", "120", "10");
                        System.Threading.Thread.Sleep(3000);
                        apiIndexF.Query(itemCode, "1", "120", "10");
                        break;
                    case "일":
                        apiIndex.Query(itemCode, "2", "1", "2");
                        System.Threading.Thread.Sleep(3000);
                        apiIndexFDWM.Query(itemCode, "2", "2");
                        break;
                    case "주":
                        apiIndex.Query(itemCode, "3", "1", "1");
                        System.Threading.Thread.Sleep(3000);
                        apiIndexFDWM.Query(itemCode, "3", "1");
                        break;
                    case "월":
                        apiIndex.Query(itemCode, "4", "1", "1");
                        System.Threading.Thread.Sleep(3000);
                        apiIndexFDWM.Query(itemCode, "4", "1");
                        break;
                }
            });
        }
        private void btnFFSeparate_Click(object sender, EventArgs e)
        {
            if (!isLogoned) return;
            Button btn = sender as Button;

            Api_WorldFuture apiFF = new Api.Api_WorldFuture();
            Api_WorldFutureDWM apiFFDWM = new Api.Api_WorldFutureDWM();
            apiFF.ApiLogHandler += (log) => { LogWrite(log); };
            apiFFDWM.ApiLogHandler += (log) => { LogWrite(log); };

            switch (btn.Text)
            {
                case "1분":
                    Task.Factory.StartNew(() => {
                        if (isSepFFCL)
                        {
                            apiFF.Query("CL", "1", "1", "100");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFNG)
                        {
                            apiFF.Query("NG", "1", "1", "100");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFGC)
                        {
                            apiFF.Query("GC", "1", "1", "100");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFSI)
                        {
                            apiFF.Query("SI", "1", "1", "100");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFHSI)
                        {
                            apiFF.Query("HMH", "1", "1", "100");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFNQ)
                        {
                            apiFF.Query("NQ", "1", "1", "100");
                            System.Threading.Thread.Sleep(3000);
                        }
                    });
                    break;
                case "5분":
                    Task.Factory.StartNew(() => {
                        if (isSepFFCL)
                        {
                            apiFF.Query("CL", "1", "5", "50");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFNG)
                        {
                            apiFF.Query("NG", "1", "5", "50");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFGC)
                        {
                            apiFF.Query("GC", "1", "5", "50");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFSI)
                        {
                            apiFF.Query("SI", "1", "5", "50");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFHSI)
                        {
                            apiFF.Query("HMH", "1", "5", "50");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFNQ)
                        {
                            apiFF.Query("NQ", "1", "5", "50");
                            System.Threading.Thread.Sleep(3000);
                        }
                    });
                    break;
                case "10분":
                    Task.Factory.StartNew(() => {
                        if (isSepFFCL)
                        {
                            apiFF.Query("CL", "1", "10", "40");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFNG)
                        {
                            apiFF.Query("NG", "1", "10", "40");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFGC)
                        {
                            apiFF.Query("GC", "1", "10", "40");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFSI)
                        {
                            apiFF.Query("SI", "1", "10", "40");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFHSI)
                        {
                            apiFF.Query("HMH", "1", "10", "40");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFNQ)
                        {
                            apiFF.Query("NQ", "1", "10", "40");
                            System.Threading.Thread.Sleep(3000);
                        }
                    });
                    break;
                case "30분":
                    Task.Factory.StartNew(() => {
                        if (isSepFFCL)
                        {
                            apiFF.Query("CL", "1", "30", "30");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFNG)
                        {
                            apiFF.Query("NG", "1", "30", "30");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFGC)
                        {
                            apiFF.Query("GC", "1", "30", "30");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFSI)
                        {
                            apiFF.Query("SI", "1", "30", "30");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFHSI)
                        {
                            apiFF.Query("HMH", "1", "30", "30");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFNQ)
                        {
                            apiFF.Query("NQ", "1", "30", "30");
                            System.Threading.Thread.Sleep(3000);
                        }
                    });
                    break;
                case "60분":
                    Task.Factory.StartNew(() => {
                        if (isSepFFCL)
                        {
                            apiFF.Query("CL", "1", "60", "30");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFNG)
                        {
                            apiFF.Query("NG", "1", "60", "30");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFGC)
                        {
                            apiFF.Query("GC", "1", "60", "30");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFSI)
                        {
                            apiFF.Query("SI", "1", "60", "30");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFHSI)
                        {
                            apiFF.Query("HMH", "1", "60", "30");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFNQ)
                        {
                            apiFF.Query("NQ", "1", "60", "30");
                            System.Threading.Thread.Sleep(3000);
                        }
                    });
                    break;
                case "120분":
                    Task.Factory.StartNew(() => {
                        if (isSepFFCL)
                        {
                            apiFF.Query("CL", "1", "120", "30");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFNG)
                        {
                            apiFF.Query("NG", "1", "120", "30");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFGC)
                        {
                            apiFF.Query("GC", "1", "120", "30");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFSI)
                        {
                            apiFF.Query("SI", "1", "120", "30");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFHSI)
                        {
                            apiFF.Query("HMH", "1", "120", "30");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFNQ)
                        {
                            apiFF.Query("NQ", "1", "120", "30");
                            System.Threading.Thread.Sleep(3000);
                        }
                    });
                    break;
                case "일":
                    Task.Factory.StartNew(() => {
                        if (isSepFFCL)
                        {
                            apiFFDWM.Query("CL", "2", "5");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFNG)
                        {
                            apiFFDWM.Query("NG", "2", "5");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFGC)
                        {
                            apiFFDWM.Query("GC", "2", "5");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFSI)
                        {
                            apiFFDWM.Query("SI", "2", "5");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFHSI)
                        {
                            apiFFDWM.Query("HMH", "2", "5");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFNQ)
                        {
                            apiFFDWM.Query("NQ", "2", "5");
                            System.Threading.Thread.Sleep(3000);
                        }
                    });
                    break;
                case "주":
                    Task.Factory.StartNew(() => {
                        if (isSepFFCL)
                        {
                            apiFFDWM.Query("CL", "3", "5");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFNG)
                        {
                            apiFFDWM.Query("NG", "3", "5");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFGC)
                        {
                            apiFFDWM.Query("GC", "3", "5");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFSI)
                        {
                            apiFFDWM.Query("SI", "3", "5");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFHSI)
                        {
                            apiFFDWM.Query("HMH", "3", "5");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFNQ)
                        {
                            apiFFDWM.Query("NQ", "3", "5");
                            System.Threading.Thread.Sleep(3000);
                        }
                    });
                    break;
                case "월":
                    Task.Factory.StartNew(() => {
                        if (isSepFFCL)
                        {
                            apiFFDWM.Query("CL", "4", "5");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFNG)
                        {
                            apiFFDWM.Query("NG", "4", "5");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFGC)
                        {
                            apiFFDWM.Query("GC", "4", "5");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFSI)
                        {
                            apiFFDWM.Query("SI", "4", "5");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFHSI)
                        {
                            apiFFDWM.Query("HMH", "4", "5");
                            System.Threading.Thread.Sleep(3000);
                        }
                        if (isSepFFNQ)
                        {
                            apiFFDWM.Query("NQ", "4", "5");
                            System.Threading.Thread.Sleep(3000);
                        }
                    });
                    break;
            }
        }
        
        private void btnReal_Click(object sender, EventArgs e)
        {
            if (!isLogoned) return;

            btnReal.Enabled = false;

            bool isRealCL = chkRealCL.Checked;
            bool isRealNG = chkRealNG.Checked;
            bool isRealGC = chkRealGC.Checked;
            bool isRealSI = chkRealSI.Checked;
            bool isRealHSI = chkRealHSI.Checked;
            bool isRealNQ = chkRealNQ.Checked;
            bool isRealURO = chkRealURO.Checked;

            Task.Factory.StartNew(() => {
                Api.Api_IndexFutureReal realKF = new Api_IndexFutureReal();
                realKF.Query(ItemCodeSet.GetItemSHCodeByName(ItemCode.선물_국내_코스피200), "90199999");
                System.Threading.Thread.Sleep(5000);

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
                }
                if (isRealURO)
                {
                    Api.Api_WorldFutureReal realFFNQ = new Api_WorldFutureReal();
                    realFFNQ.Query(ItemCodeSet.GetItemSHCodeByName(ItemCode.선물_해외_유로FX), "URO");
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
            if (c.Text == "KP200")
                isAutoKoreaIndex = c.Checked;
            if (c.Text == "KP200 선물")
                isAutoKoreaFuture = c.Checked;
            if (c.Text == "CL") isAutoFFCL  = c.Checked;
            if (c.Text == "NG") isAutoFFNG  = c.Checked;
            if (c.Text == "GC") isAutoFFGC  = c.Checked;
            if (c.Text == "SI") isAutoFFSI  = c.Checked;
            if (c.Text == "HMH") isAutoFFHSI = c.Checked;
            if (c.Text == "NQ") isAutoFFNQ = c.Checked;
            if (c.Text == "URO") isAutoFFURO = c.Checked;
        }

        private void chkSep_CheckedChanged(object sender, EventArgs e)
        {
            var c = sender as CheckBox;         
            if (c.Text == "CL") isSepFFCL = c.Checked;
            if (c.Text == "NG") isSepFFNG = c.Checked;
            if (c.Text == "GC") isSepFFGC = c.Checked;
            if (c.Text == "SI") isSepFFSI = c.Checked;
            if (c.Text == "HMH")isSepFFHSI = c.Checked;
            if (c.Text == "NQ") isSepFFNQ = c.Checked;
            if (c.Text == "URO") isSepFFURO = c.Checked;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (! session.IsConnected()) {
                timer1.Stop();

                noti.BalloonTipIcon = ToolTipIcon.Warning;
                noti.BalloonTipText = "Disconnect Xing";
                noti.BalloonTipTitle = "Disconnect Xing";
                noti.ShowBalloonTip(5000);

                var player = new System.Media.SoundPlayer();
                player.Stream = Properties.Resources.통신단절;
                player.Play();
            }
        }
    }
}
