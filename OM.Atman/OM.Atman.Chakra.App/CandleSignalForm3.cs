﻿using OM.Lib.Base.Enums;
using OM.Lib.Entity;
using OM.PP.Chakra;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tulpep.NotificationWindow;

namespace OM.Atman.Chakra.App
{
    public partial class CandleSignalForm3 : Form
    {
        string selectedItemCode = "CL";
        Dictionary<string, UC_Signal> uc120List = new Dictionary<string, UC_Signal>();
        Dictionary<string, UC_Signal> ucDayList = new Dictionary<string, UC_Signal>();
  
        Dictionary<TimeIntervalEnum, Dictionary<string, UC_Signal>> ucAll = new Dictionary<TimeIntervalEnum, Dictionary<string, UC_Signal>>();
        public CandleSignalForm3()
        {
            InitializeComponent();

            PPEvents.Instance.CandleOccurHandler += Instance_CandleOccurHandler;
            PPEvents.Instance.CandleChartPatternHandler += Instance_CandleChartPatternHandler;
            PPEvents.Instance.CandleMassPatternHandler += Instance_CandleMassPatternHandler;
         
            this.Load += CandleSignalForm_Load;
            this.FormClosing += CandleSignalForm3_FormClosing;
        }

        private void CandleSignalForm3_FormClosing(object sender, FormClosingEventArgs e)
        {
            PPEvents.Instance.CandleOccurHandler -= Instance_CandleOccurHandler;
            PPEvents.Instance.CandleChartPatternHandler -= Instance_CandleChartPatternHandler;
            PPEvents.Instance.CandleMassPatternHandler -= Instance_CandleMassPatternHandler;
        }

        private void CandleSignalForm_Load(object sender, EventArgs e)
        {
            uc120List.Add("CL", ucSignalCL05);
            uc120List.Add("NG", ucSignalNG05);
            uc120List.Add("GC", ucSignalGC05);
            uc120List.Add("SI", ucSignalSI05);
            uc120List.Add("HMH", ucSignalHSI05);
            uc120List.Add("NQ", ucSignalNQ05);
            uc120List.Add("URO", ucSignalURO05);
            uc120List.Add("ES", ucSignalES05);

            ucDayList.Add("CL", ucSignalCL10);
            ucDayList.Add("NG", ucSignalNG10);
            ucDayList.Add("GC", ucSignalGC10);
            ucDayList.Add("SI", ucSignalSI10);
            ucDayList.Add("HMH", ucSignalHSI10);
            ucDayList.Add("NQ", ucSignalNQ10);
            ucDayList.Add("URO", ucSignalURO10);
            ucDayList.Add("ES", ucSignalES10);

            ucAll.Add(TimeIntervalEnum.Hour_02, uc120List);
            ucAll.Add(TimeIntervalEnum.Day, ucDayList);        
        }

       

        private void Instance_CandleChartPatternHandler(string itemCode, TimeIntervalEnum timeInterval, CandleChartPatternEnum updown)
        {
            if (!ucAll.ContainsKey(timeInterval)) return;
            string signal = "";
            string result = "";
            string soundType = "1";
            switch (updown)
            {
                case CandleChartPatternEnum.SixStrongUp:
                    signal = "▲▲▲▲▲▲";
                    result = "▲";
                    break;
                case CandleChartPatternEnum.SixStrongDown:
                    signal = "▼▼▼▼▼▼";
                    result = "▼";
                    break;
                case CandleChartPatternEnum.SixWeakUp:
                    signal = "△△△△△△";
                    result = "△";
                    break;
                case CandleChartPatternEnum.SixWeakDown:
                    signal = "▽▽▽▽▽▽";
                    result = "▽";
                    break;

                case CandleChartPatternEnum.FiveStrongUp:
                    signal = "▲▲▲▲▲";
                    result = "▲";
                    break;
                case CandleChartPatternEnum.FiveStrongDown:
                    signal = "▼▼▼▼▼";
                    result = "▼";
                    break;
                case CandleChartPatternEnum.FiveWeakUp:
                    signal = "△△△△△";
                    result = "△";
                    break;
                case CandleChartPatternEnum.FiveWeakDown:
                    signal = "▽▽▽▽▽";
                    result = "▽";
                    break;

                case CandleChartPatternEnum.FourStrongUp:
                    signal = "▲▲▲▲";
                    result = "▲";
                    break;
                case CandleChartPatternEnum.FourStrongDown:
                    signal = "▼▼▼▼";
                    result = "▼";
                    break;
                case CandleChartPatternEnum.FourWeakUp:
                    signal = "△△△△";
                    result = "△";
                    break;
                case CandleChartPatternEnum.FourWeakDown:
                    signal = "▽▽▽▽";
                    result = "▽";
                    break;

                case CandleChartPatternEnum.ThreeStrongUp:
                    signal = "▲▲▲";
                    result = "▲";
                    break;
                case CandleChartPatternEnum.ThreeStrongDown:
                    signal = "▼▼▼";
                    result = "▼";
                    break;
                case CandleChartPatternEnum.ThreeWeakUp:
                    signal = "△△△";
                    result = "△";
                    break;
                case CandleChartPatternEnum.ThreeWeakDown:
                    signal = "▽▽▽";
                    result = "▽";
                    break;
            }
            if (result == "▲" || result == "▼")
                soundType = "2";

            this.Invoke(new Action(() =>
            {
                var ucList = ucAll[timeInterval];

                ucList[itemCode].SetCandle6("", "", DateTime.Now);
                ucList[itemCode].SetCandle5("", "", DateTime.Now);
                ucList[itemCode].SetCandle4("", "", DateTime.Now);
                ucList[itemCode].SetCandle3("", "", DateTime.Now);

                if (signal.Length == 6) ucList[itemCode].SetCandle6(signal, result, DateTime.Now);
                else if (signal.Length == 5) ucList[itemCode].SetCandle5(signal, result, DateTime.Now);
                else if (signal.Length == 4) ucList[itemCode].SetCandle4(signal, result, DateTime.Now);
                else if (signal.Length == 3) ucList[itemCode].SetCandle3(signal, result, DateTime.Now);

                ucList[itemCode].SetLastTime(DateTime.Now);

                string title = "캔들패턴 시그널";
                string msg = $"{itemCode}::{EnumUtil.GetTimeIntervalText(timeInterval)}::{updown}";

                if (selectedItemCode == itemCode && updown != CandleChartPatternEnum.None)
                    ShowNotifyIcon(title, msg, soundType);
            }));
        }
        private void Instance_CandleMassPatternHandler(string itemCode, TimeIntervalEnum timeInterval, CandleMassPatternEnum updown)
        {
            if (!ucAll.ContainsKey(timeInterval)) return;
            string signal = "";
            string result = "";
            string soundType = "1";
            switch (updown)
            {
                case CandleMassPatternEnum.MassUpUpUp:
                    signal = "↗↗↗";
                    result = "▲";
                    break;

                case CandleMassPatternEnum.MassDownDownDown:
                    signal = "↘↘↘";
                    result = "▼";
                    break;
                case CandleMassPatternEnum.MassDownUpUp:
                    signal = "↘↗↗";
                    result = "▲";
                    break;

                case CandleMassPatternEnum.MassUpDownDown:
                    signal = "↗↘↘";
                    result = "▼";
                    break;
                case CandleMassPatternEnum.MassDownDownUp:
                    signal = "↘↘↗";
                    result = "△";
                    break;

                case CandleMassPatternEnum.MassUpUpDown:
                    signal = "↗↗↘";
                    result = "▽";
                    break;
                case CandleMassPatternEnum.MassDownUpDown:
                    signal = "↘↗↘";
                    result = "▽";
                    break;

                case CandleMassPatternEnum.MassUpDownUp:
                    signal = "↗↘↗";
                    result = "△";
                    break;
            }
            if (result == "▲" || result == "▼")
                soundType = "2";

            this.Invoke(new Action(() =>
            {
                var ucList = ucAll[timeInterval];
                ucList[itemCode].SetMass("", "", DateTime.Now);
                if (signal.StartsWith("↗") || signal.StartsWith("↘"))
                {
                    ucList[itemCode].SetMass(signal, result, DateTime.Now);
                }

                ucList[itemCode].SetLastTime(DateTime.Now);

                string title = "캔들패턴 시그널";
                string msg = $"{itemCode}::{EnumUtil.GetTimeIntervalText(timeInterval)}::{updown}";

                if (selectedItemCode == itemCode && updown != CandleMassPatternEnum.None)
                    ShowNotifyIcon(title, msg, soundType);
            }));
        }
        private void Instance_CandleOccurHandler(string itemCode, Lib.Base.Enums.TimeIntervalEnum timeInterval, int occurCount, PlusMinusTypeEnum pmType)
        {
            if (!ucAll.ContainsKey(timeInterval)) return;
            string signal = occurCount.ToString();
            string result = "";
            if (pmType == PlusMinusTypeEnum.양) result = "▲";
            else if (pmType == PlusMinusTypeEnum.음) result = "▼";
            string soundType = "1";
            if (occurCount >= 5) soundType = "2";
            if (occurCount == 0) signal = "";
            this.Invoke(new Action(() =>
            {
                var ucList = ucAll[timeInterval];
                ucList[itemCode].SetCCount(signal, result, DateTime.Now);
                ucList[itemCode].SetLastTime(DateTime.Now);

                if (occurCount >= 3)
                {
                    string title = "캔들갯수 시그널";
                    string msg = $"{itemCode}::{EnumUtil.GetTimeIntervalText(timeInterval)}::{occurCount}";

                    if (selectedItemCode == itemCode)
                        ShowNotifyIcon(title, msg, soundType);
                }
            }));
        }

        private void ShowNotifyIcon(string title, string message, string soundType = "1")
        {
            //return;
            try
            {
                PopupNotifier popup = new PopupNotifier();
                popup.ShowGrip = true;
                popup.HeaderColor = Color.DarkBlue;
                popup.BodyColor = Color.DarkRed;
                popup.TitleColor = Color.White;
                popup.ContentColor = Color.WhiteSmoke;
                popup.TitleFont = new Font("고딕", 24.0f);
                popup.ContentFont = new Font("굴림", 12.0f);
                popup.TitleText = title;
                popup.ContentText = message;
                popup.Image = global::OM.Atman.Chakra.App.Properties.Resources.signal;
                popup.ImageSize = new Size(50, 40);
                popup.Delay = 1000 * 5;
                popup.Popup();// show  

                if (soundType == "1")
                {
                    var player = new System.Media.SoundPlayer();
                    player.Stream = Properties.Resources.money;
                    player.Play();
                }
                else if (soundType == "2")
                {
                    var player = new System.Media.SoundPlayer();
                    player.Stream = Properties.Resources.begin_tune;
                    player.Play();
                }
            }
            catch (Exception)
            {
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedItemCode = tabControl1.SelectedTab.Text;
        }
    }
}
