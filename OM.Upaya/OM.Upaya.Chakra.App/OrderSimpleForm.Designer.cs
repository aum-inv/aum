﻿namespace OM.Upaya.Chakra.App
{
    partial class OrderSimpleForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrderSimpleForm));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnIII = new System.Windows.Forms.Button();
            this.tbItem = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBuy1 = new System.Windows.Forms.Button();
            this.btnBuy2 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbQty = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnSell2 = new System.Windows.Forms.Button();
            this.btnSell1 = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.tbCurPrice = new System.Windows.Forms.TextBox();
            this.chkCurPrice = new System.Windows.Forms.CheckBox();
            this.tbOrderPrice = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.rdoTradeType2 = new System.Windows.Forms.RadioButton();
            this.rdoTradeType1 = new System.Windows.Forms.RadioButton();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.chkIsUseLoss = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbAvgPrice1 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbLossPrice2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbLossPrice1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbAvgPrice2 = new System.Windows.Forms.TextBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.chkIsUseRevenue = new System.Windows.Forms.CheckBox();
            this.chkMinimumRevenue1 = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tbMinimumRevenue1 = new System.Windows.Forms.TextBox();
            this.chkMinimumRevenue2 = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbMinimumRevenue2 = new System.Windows.Forms.TextBox();
            this.chkIsUseRevenue2 = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkIsUseRevenue1 = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbRevenueRate = new System.Windows.Forms.TextBox();
            this.tbRevenuePrice = new System.Windows.Forms.TextBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.rdoPosition2 = new System.Windows.Forms.RadioButton();
            this.rdoPosition1 = new System.Windows.Forms.RadioButton();
            this.chkOrderConfirm = new System.Windows.Forms.CheckBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnIII);
            this.groupBox2.Controls.Add(this.tbItem);
            this.groupBox2.Location = new System.Drawing.Point(16, 15);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.groupBox2.Size = new System.Drawing.Size(120, 62);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "상품코드";
            // 
            // btnIII
            // 
            this.btnIII.Location = new System.Drawing.Point(77, 25);
            this.btnIII.Name = "btnIII";
            this.btnIII.Size = new System.Drawing.Size(35, 23);
            this.btnIII.TabIndex = 2;
            this.btnIII.Text = "III";
            this.btnIII.UseVisualStyleBackColor = true;
            this.btnIII.Click += new System.EventHandler(this.btnIII_Click);
            // 
            // tbItem
            // 
            this.tbItem.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbItem.Location = new System.Drawing.Point(7, 24);
            this.tbItem.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.tbItem.Name = "tbItem";
            this.tbItem.Size = new System.Drawing.Size(63, 25);
            this.tbItem.TabIndex = 0;
            this.tbItem.Text = "CL";
            this.tbItem.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbItem.TextChanged += new System.EventHandler(this.tbItem_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnBuy1);
            this.groupBox1.Controls.Add(this.btnBuy2);
            this.groupBox1.Location = new System.Drawing.Point(16, 248);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.groupBox1.Size = new System.Drawing.Size(228, 90);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "진입주문";
            // 
            // btnBuy1
            // 
            this.btnBuy1.BackColor = System.Drawing.Color.Blue;
            this.btnBuy1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuy1.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnBuy1.ForeColor = System.Drawing.Color.White;
            this.btnBuy1.Location = new System.Drawing.Point(124, 28);
            this.btnBuy1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnBuy1.Name = "btnBuy1";
            this.btnBuy1.Size = new System.Drawing.Size(92, 50);
            this.btnBuy1.TabIndex = 1;
            this.btnBuy1.Text = "매도진입";
            this.btnBuy1.UseVisualStyleBackColor = false;
            this.btnBuy1.Click += new System.EventHandler(this.btnBuy1_Click);
            // 
            // btnBuy2
            // 
            this.btnBuy2.BackColor = System.Drawing.Color.Red;
            this.btnBuy2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuy2.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnBuy2.ForeColor = System.Drawing.Color.White;
            this.btnBuy2.Location = new System.Drawing.Point(12, 28);
            this.btnBuy2.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnBuy2.Name = "btnBuy2";
            this.btnBuy2.Size = new System.Drawing.Size(92, 50);
            this.btnBuy2.TabIndex = 0;
            this.btnBuy2.Text = "매수진입";
            this.btnBuy2.UseVisualStyleBackColor = false;
            this.btnBuy2.Click += new System.EventHandler(this.btnBuy2_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbQty);
            this.groupBox3.Location = new System.Drawing.Point(140, 15);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.groupBox3.Size = new System.Drawing.Size(104, 62);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "수량";
            // 
            // tbQty
            // 
            this.tbQty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbQty.Location = new System.Drawing.Point(7, 24);
            this.tbQty.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.tbQty.Name = "tbQty";
            this.tbQty.Size = new System.Drawing.Size(90, 25);
            this.tbQty.TabIndex = 0;
            this.tbQty.Text = "0";
            this.tbQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbQty.TextChanged += new System.EventHandler(this.tbQty_TextChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnSell2);
            this.groupBox4.Controls.Add(this.btnSell1);
            this.groupBox4.Location = new System.Drawing.Point(16, 345);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.groupBox4.Size = new System.Drawing.Size(228, 90);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "청산주문";
            // 
            // btnSell2
            // 
            this.btnSell2.BackColor = System.Drawing.Color.Red;
            this.btnSell2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSell2.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnSell2.ForeColor = System.Drawing.Color.White;
            this.btnSell2.Location = new System.Drawing.Point(124, 28);
            this.btnSell2.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnSell2.Name = "btnSell2";
            this.btnSell2.Size = new System.Drawing.Size(92, 50);
            this.btnSell2.TabIndex = 1;
            this.btnSell2.Text = "매도청산";
            this.btnSell2.UseVisualStyleBackColor = false;
            this.btnSell2.Click += new System.EventHandler(this.btnSell2_Click);
            // 
            // btnSell1
            // 
            this.btnSell1.BackColor = System.Drawing.Color.Blue;
            this.btnSell1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSell1.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnSell1.ForeColor = System.Drawing.Color.White;
            this.btnSell1.Location = new System.Drawing.Point(12, 28);
            this.btnSell1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnSell1.Name = "btnSell1";
            this.btnSell1.Size = new System.Drawing.Size(92, 50);
            this.btnSell1.TabIndex = 0;
            this.btnSell1.Text = "매수청산";
            this.btnSell1.UseVisualStyleBackColor = false;
            this.btnSell1.Click += new System.EventHandler(this.btnSell1_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.tbCurPrice);
            this.groupBox5.Controls.Add(this.chkCurPrice);
            this.groupBox5.Controls.Add(this.tbOrderPrice);
            this.groupBox5.Location = new System.Drawing.Point(16, 151);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.groupBox5.Size = new System.Drawing.Size(228, 60);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "지정가격";
            // 
            // tbCurPrice
            // 
            this.tbCurPrice.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbCurPrice.Location = new System.Drawing.Point(127, 28);
            this.tbCurPrice.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.tbCurPrice.Name = "tbCurPrice";
            this.tbCurPrice.ReadOnly = true;
            this.tbCurPrice.Size = new System.Drawing.Size(92, 18);
            this.tbCurPrice.TabIndex = 22;
            this.tbCurPrice.Text = "000.00";
            this.tbCurPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chkCurPrice
            // 
            this.chkCurPrice.AutoSize = true;
            this.chkCurPrice.Location = new System.Drawing.Point(105, 29);
            this.chkCurPrice.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.chkCurPrice.Name = "chkCurPrice";
            this.chkCurPrice.Size = new System.Drawing.Size(18, 17);
            this.chkCurPrice.TabIndex = 21;
            this.chkCurPrice.UseVisualStyleBackColor = true;
            // 
            // tbOrderPrice
            // 
            this.tbOrderPrice.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbOrderPrice.Location = new System.Drawing.Point(7, 24);
            this.tbOrderPrice.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.tbOrderPrice.Name = "tbOrderPrice";
            this.tbOrderPrice.Size = new System.Drawing.Size(88, 25);
            this.tbOrderPrice.TabIndex = 0;
            this.tbOrderPrice.Text = "000.00";
            this.tbOrderPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbOrderPrice.TextChanged += new System.EventHandler(this.tbOrderPrice_TextChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.rdoTradeType2);
            this.groupBox6.Controls.Add(this.rdoTradeType1);
            this.groupBox6.Location = new System.Drawing.Point(16, 89);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.groupBox6.Size = new System.Drawing.Size(228, 58);
            this.groupBox6.TabIndex = 7;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "주문타입";
            // 
            // rdoTradeType2
            // 
            this.rdoTradeType2.AutoSize = true;
            this.rdoTradeType2.Location = new System.Drawing.Point(129, 25);
            this.rdoTradeType2.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.rdoTradeType2.Name = "rdoTradeType2";
            this.rdoTradeType2.Size = new System.Drawing.Size(73, 19);
            this.rdoTradeType2.TabIndex = 1;
            this.rdoTradeType2.Text = "지정가";
            this.rdoTradeType2.UseVisualStyleBackColor = true;
            this.rdoTradeType2.CheckedChanged += new System.EventHandler(this.rdoTradeType2_CheckedChanged);
            // 
            // rdoTradeType1
            // 
            this.rdoTradeType1.AutoSize = true;
            this.rdoTradeType1.Checked = true;
            this.rdoTradeType1.Location = new System.Drawing.Point(12, 25);
            this.rdoTradeType1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.rdoTradeType1.Name = "rdoTradeType1";
            this.rdoTradeType1.Size = new System.Drawing.Size(73, 19);
            this.rdoTradeType1.TabIndex = 0;
            this.rdoTradeType1.TabStop = true;
            this.rdoTradeType1.Text = "시장가";
            this.rdoTradeType1.UseVisualStyleBackColor = true;
            this.rdoTradeType1.CheckedChanged += new System.EventHandler(this.rdoTradeType1_CheckedChanged);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.chkIsUseLoss);
            this.groupBox7.Controls.Add(this.label8);
            this.groupBox7.Controls.Add(this.tbLossPrice2);
            this.groupBox7.Controls.Add(this.label5);
            this.groupBox7.Controls.Add(this.tbLossPrice1);
            this.groupBox7.Location = new System.Drawing.Point(268, 145);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.groupBox7.Size = new System.Drawing.Size(236, 103);
            this.groupBox7.TabIndex = 8;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "손실청산 설정";
            // 
            // chkIsUseLoss
            // 
            this.chkIsUseLoss.AutoSize = true;
            this.chkIsUseLoss.Location = new System.Drawing.Point(12, 80);
            this.chkIsUseLoss.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.chkIsUseLoss.Name = "chkIsUseLoss";
            this.chkIsUseLoss.Size = new System.Drawing.Size(154, 19);
            this.chkIsUseLoss.TabIndex = 19;
            this.chkIsUseLoss.Text = "손실청산규칙 사용";
            this.chkIsUseLoss.UseVisualStyleBackColor = true;
            this.chkIsUseLoss.CheckedChanged += new System.EventHandler(this.chkIsUseLoss_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(280, 111);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 15);
            this.label2.TabIndex = 14;
            this.label2.Text = "매도평균가격 : ";
            // 
            // tbAvgPrice1
            // 
            this.tbAvgPrice1.Location = new System.Drawing.Point(405, 104);
            this.tbAvgPrice1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.tbAvgPrice1.Name = "tbAvgPrice1";
            this.tbAvgPrice1.Size = new System.Drawing.Size(84, 25);
            this.tbAvgPrice1.TabIndex = 13;
            this.tbAvgPrice1.Text = "0";
            this.tbAvgPrice1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbAvgPrice1.TextChanged += new System.EventHandler(this.tbAvgPrice1_TextChanged);
            this.tbAvgPrice1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbAvgPrice1_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(28, 51);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(95, 15);
            this.label8.TabIndex = 12;
            this.label8.Text = "2차 로스컷 : ";
            // 
            // tbLossPrice2
            // 
            this.tbLossPrice2.Location = new System.Drawing.Point(137, 48);
            this.tbLossPrice2.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.tbLossPrice2.Name = "tbLossPrice2";
            this.tbLossPrice2.Size = new System.Drawing.Size(84, 25);
            this.tbLossPrice2.TabIndex = 11;
            this.tbLossPrice2.Text = "0";
            this.tbLossPrice2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbLossPrice2.TextChanged += new System.EventHandler(this.tbLossPrice2_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 22);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "1차 로스컷 : ";
            // 
            // tbLossPrice1
            // 
            this.tbLossPrice1.Location = new System.Drawing.Point(137, 18);
            this.tbLossPrice1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.tbLossPrice1.Name = "tbLossPrice1";
            this.tbLossPrice1.Size = new System.Drawing.Size(84, 25);
            this.tbLossPrice1.TabIndex = 8;
            this.tbLossPrice1.Text = "0";
            this.tbLossPrice1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbLossPrice1.TextChanged += new System.EventHandler(this.tbLossPrice1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(280, 81);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "매수평균가격 : ";
            // 
            // tbAvgPrice2
            // 
            this.tbAvgPrice2.Location = new System.Drawing.Point(405, 74);
            this.tbAvgPrice2.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.tbAvgPrice2.Name = "tbAvgPrice2";
            this.tbAvgPrice2.Size = new System.Drawing.Size(84, 25);
            this.tbAvgPrice2.TabIndex = 0;
            this.tbAvgPrice2.Text = "0";
            this.tbAvgPrice2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbAvgPrice2.TextChanged += new System.EventHandler(this.tbAvgPrice2_TextChanged);
            this.tbAvgPrice2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbAvgPrice2_KeyDown);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.chkIsUseRevenue);
            this.groupBox8.Controls.Add(this.chkMinimumRevenue1);
            this.groupBox8.Controls.Add(this.label13);
            this.groupBox8.Controls.Add(this.tbMinimumRevenue1);
            this.groupBox8.Controls.Add(this.chkMinimumRevenue2);
            this.groupBox8.Controls.Add(this.label12);
            this.groupBox8.Controls.Add(this.tbMinimumRevenue2);
            this.groupBox8.Controls.Add(this.chkIsUseRevenue2);
            this.groupBox8.Controls.Add(this.label4);
            this.groupBox8.Controls.Add(this.chkIsUseRevenue1);
            this.groupBox8.Controls.Add(this.label6);
            this.groupBox8.Controls.Add(this.tbRevenueRate);
            this.groupBox8.Controls.Add(this.tbRevenuePrice);
            this.groupBox8.Location = new System.Drawing.Point(265, 261);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.groupBox8.Size = new System.Drawing.Size(236, 175);
            this.groupBox8.TabIndex = 9;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "수익설정(고저점대비)";
            // 
            // chkIsUseRevenue
            // 
            this.chkIsUseRevenue.AutoSize = true;
            this.chkIsUseRevenue.Location = new System.Drawing.Point(12, 148);
            this.chkIsUseRevenue.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.chkIsUseRevenue.Name = "chkIsUseRevenue";
            this.chkIsUseRevenue.Size = new System.Drawing.Size(154, 19);
            this.chkIsUseRevenue.TabIndex = 28;
            this.chkIsUseRevenue.Text = "수익청산규칙 사용";
            this.chkIsUseRevenue.UseVisualStyleBackColor = true;
            this.chkIsUseRevenue.CheckedChanged += new System.EventHandler(this.chkIsUseRevenue_CheckedChanged);
            // 
            // chkMinimumRevenue1
            // 
            this.chkMinimumRevenue1.AutoSize = true;
            this.chkMinimumRevenue1.Location = new System.Drawing.Point(112, 118);
            this.chkMinimumRevenue1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.chkMinimumRevenue1.Name = "chkMinimumRevenue1";
            this.chkMinimumRevenue1.Size = new System.Drawing.Size(18, 17);
            this.chkMinimumRevenue1.TabIndex = 27;
            this.chkMinimumRevenue1.UseVisualStyleBackColor = true;
            this.chkMinimumRevenue1.CheckedChanged += new System.EventHandler(this.chkMinimumRevenue1_CheckedChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(20, 118);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(77, 15);
            this.label13.TabIndex = 26;
            this.label13.Text = "매도최소 :";
            // 
            // tbMinimumRevenue1
            // 
            this.tbMinimumRevenue1.Location = new System.Drawing.Point(140, 113);
            this.tbMinimumRevenue1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.tbMinimumRevenue1.Name = "tbMinimumRevenue1";
            this.tbMinimumRevenue1.Size = new System.Drawing.Size(84, 25);
            this.tbMinimumRevenue1.TabIndex = 25;
            this.tbMinimumRevenue1.Text = "0";
            this.tbMinimumRevenue1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbMinimumRevenue1.TextChanged += new System.EventHandler(this.tbMinimumRevenue1_TextChanged);
            // 
            // chkMinimumRevenue2
            // 
            this.chkMinimumRevenue2.AutoSize = true;
            this.chkMinimumRevenue2.Location = new System.Drawing.Point(112, 85);
            this.chkMinimumRevenue2.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.chkMinimumRevenue2.Name = "chkMinimumRevenue2";
            this.chkMinimumRevenue2.Size = new System.Drawing.Size(18, 17);
            this.chkMinimumRevenue2.TabIndex = 24;
            this.chkMinimumRevenue2.UseVisualStyleBackColor = true;
            this.chkMinimumRevenue2.CheckedChanged += new System.EventHandler(this.chkMinimumRevenue2_CheckedChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(20, 85);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 15);
            this.label12.TabIndex = 23;
            this.label12.Text = "매수최소 :";
            // 
            // tbMinimumRevenue2
            // 
            this.tbMinimumRevenue2.Location = new System.Drawing.Point(140, 81);
            this.tbMinimumRevenue2.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.tbMinimumRevenue2.Name = "tbMinimumRevenue2";
            this.tbMinimumRevenue2.Size = new System.Drawing.Size(84, 25);
            this.tbMinimumRevenue2.TabIndex = 22;
            this.tbMinimumRevenue2.Text = "0";
            this.tbMinimumRevenue2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbMinimumRevenue2.TextChanged += new System.EventHandler(this.tbMinimumRevenue2_TextChanged);
            // 
            // chkIsUseRevenue2
            // 
            this.chkIsUseRevenue2.AutoSize = true;
            this.chkIsUseRevenue2.Location = new System.Drawing.Point(112, 54);
            this.chkIsUseRevenue2.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.chkIsUseRevenue2.Name = "chkIsUseRevenue2";
            this.chkIsUseRevenue2.Size = new System.Drawing.Size(18, 17);
            this.chkIsUseRevenue2.TabIndex = 21;
            this.chkIsUseRevenue2.UseVisualStyleBackColor = true;
            this.chkIsUseRevenue2.CheckedChanged += new System.EventHandler(this.chkIsUseRevenue2_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 54);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "등락율 :";
            // 
            // chkIsUseRevenue1
            // 
            this.chkIsUseRevenue1.AutoSize = true;
            this.chkIsUseRevenue1.Location = new System.Drawing.Point(112, 25);
            this.chkIsUseRevenue1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.chkIsUseRevenue1.Name = "chkIsUseRevenue1";
            this.chkIsUseRevenue1.Size = new System.Drawing.Size(18, 17);
            this.chkIsUseRevenue1.TabIndex = 20;
            this.chkIsUseRevenue1.UseVisualStyleBackColor = true;
            this.chkIsUseRevenue1.CheckedChanged += new System.EventHandler(this.chkIsUseRevenue1_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(52, 28);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 15);
            this.label6.TabIndex = 3;
            this.label6.Text = "가격 :";
            // 
            // tbRevenueRate
            // 
            this.tbRevenueRate.Location = new System.Drawing.Point(140, 51);
            this.tbRevenueRate.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.tbRevenueRate.Name = "tbRevenueRate";
            this.tbRevenueRate.Size = new System.Drawing.Size(84, 25);
            this.tbRevenueRate.TabIndex = 2;
            this.tbRevenueRate.Text = "0.3";
            this.tbRevenueRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbRevenueRate.TextChanged += new System.EventHandler(this.tbRevenueRate_TextChanged);
            // 
            // tbRevenuePrice
            // 
            this.tbRevenuePrice.Location = new System.Drawing.Point(140, 20);
            this.tbRevenuePrice.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.tbRevenuePrice.Name = "tbRevenuePrice";
            this.tbRevenuePrice.Size = new System.Drawing.Size(84, 25);
            this.tbRevenuePrice.TabIndex = 0;
            this.tbRevenuePrice.Text = "0";
            this.tbRevenuePrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbRevenuePrice.TextChanged += new System.EventHandler(this.tbRevenuePrice_TextChanged);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.rdoPosition2);
            this.groupBox9.Controls.Add(this.rdoPosition1);
            this.groupBox9.Location = new System.Drawing.Point(268, 15);
            this.groupBox9.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.groupBox9.Size = new System.Drawing.Size(236, 47);
            this.groupBox9.TabIndex = 10;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "포지션";
            // 
            // rdoPosition2
            // 
            this.rdoPosition2.AutoSize = true;
            this.rdoPosition2.Location = new System.Drawing.Point(135, 20);
            this.rdoPosition2.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.rdoPosition2.Name = "rdoPosition2";
            this.rdoPosition2.Size = new System.Drawing.Size(58, 19);
            this.rdoPosition2.TabIndex = 1;
            this.rdoPosition2.Text = "매수";
            this.rdoPosition2.UseVisualStyleBackColor = true;
            this.rdoPosition2.CheckedChanged += new System.EventHandler(this.rdoPosition2_CheckedChanged);
            // 
            // rdoPosition1
            // 
            this.rdoPosition1.AutoSize = true;
            this.rdoPosition1.Checked = true;
            this.rdoPosition1.Location = new System.Drawing.Point(36, 20);
            this.rdoPosition1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.rdoPosition1.Name = "rdoPosition1";
            this.rdoPosition1.Size = new System.Drawing.Size(58, 19);
            this.rdoPosition1.TabIndex = 0;
            this.rdoPosition1.TabStop = true;
            this.rdoPosition1.Text = "매도";
            this.rdoPosition1.UseVisualStyleBackColor = true;
            this.rdoPosition1.CheckedChanged += new System.EventHandler(this.rdoPosition1_CheckedChanged);
            // 
            // chkOrderConfirm
            // 
            this.chkOrderConfirm.AutoSize = true;
            this.chkOrderConfirm.Checked = true;
            this.chkOrderConfirm.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOrderConfirm.Location = new System.Drawing.Point(133, 231);
            this.chkOrderConfirm.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.chkOrderConfirm.Name = "chkOrderConfirm";
            this.chkOrderConfirm.Size = new System.Drawing.Size(109, 19);
            this.chkOrderConfirm.TabIndex = 22;
            this.chkOrderConfirm.Text = "주문 확인창";
            this.chkOrderConfirm.UseVisualStyleBackColor = true;
            // 
            // OrderSimpleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 445);
            this.Controls.Add(this.chkOrderConfirm);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.tbAvgPrice1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.tbAvgPrice2);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.MaximizeBox = false;
            this.Name = "OrderSimpleForm";
            this.Text = "간단 주문창 및 손실수익자동청산";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox tbQty;
        private System.Windows.Forms.Button btnBuy1;
        private System.Windows.Forms.Button btnBuy2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnSell2;
        private System.Windows.Forms.Button btnSell1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox tbOrderPrice;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton rdoTradeType1;
        private System.Windows.Forms.RadioButton rdoTradeType2;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbLossPrice2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbLossPrice1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbAvgPrice2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbAvgPrice1;
        private System.Windows.Forms.CheckBox chkIsUseLoss;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.CheckBox chkMinimumRevenue1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbMinimumRevenue1;
        private System.Windows.Forms.CheckBox chkMinimumRevenue2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbMinimumRevenue2;
        private System.Windows.Forms.CheckBox chkIsUseRevenue2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkIsUseRevenue1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbRevenueRate;
        private System.Windows.Forms.TextBox tbRevenuePrice;
        private System.Windows.Forms.CheckBox chkIsUseRevenue;
        private System.Windows.Forms.CheckBox chkCurPrice;
        private System.Windows.Forms.TextBox tbCurPrice;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.RadioButton rdoPosition2;
        private System.Windows.Forms.RadioButton rdoPosition1;
        private System.Windows.Forms.CheckBox chkOrderConfirm;
        private System.Windows.Forms.Button btnIII;
    }
}