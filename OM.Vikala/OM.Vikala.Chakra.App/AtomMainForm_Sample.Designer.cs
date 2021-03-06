﻿namespace OM.Vikala.Chakra.App
{
    partial class AtomMainForm_Sample
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AtomMainForm_Sample));
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.tbDT_E = new System.Windows.Forms.TextBox();
            this.btnLoadData = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.chartAT2 = new OM.Vikala.Controls.Charts.AtomChart();
            this.chartAT1 = new OM.Vikala.Controls.Charts.AtomChart();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButton4);
            this.panel1.Controls.Add(this.radioButton3);
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Controls.Add(this.numericUpDown1);
            this.panel1.Controls.Add(this.tbDT_E);
            this.panel1.Controls.Add(this.btnLoadData);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 4);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(930, 20);
            this.panel1.TabIndex = 0;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Checked = true;
            this.radioButton4.Dock = System.Windows.Forms.DockStyle.Left;
            this.radioButton4.Location = new System.Drawing.Point(475, 0);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(35, 20);
            this.radioButton4.TabIndex = 8;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "일";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Dock = System.Windows.Forms.DockStyle.Left;
            this.radioButton3.Location = new System.Drawing.Point(428, 0);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(47, 20);
            this.radioButton3.TabIndex = 7;
            this.radioButton3.Text = "60분";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Dock = System.Windows.Forms.DockStyle.Left;
            this.radioButton2.Location = new System.Drawing.Point(381, 0);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(47, 20);
            this.radioButton2.TabIndex = 6;
            this.radioButton2.Text = "30분";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Dock = System.Windows.Forms.DockStyle.Left;
            this.radioButton1.Location = new System.Drawing.Point(334, 0);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(47, 20);
            this.radioButton1.TabIndex = 5;
            this.radioButton1.Text = "10분";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Dock = System.Windows.Forms.DockStyle.Left;
            this.numericUpDown1.Location = new System.Drawing.Point(214, 0);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 21);
            this.numericUpDown1.TabIndex = 4;
            // 
            // tbDT_E
            // 
            this.tbDT_E.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbDT_E.Location = new System.Drawing.Point(93, 0);
            this.tbDT_E.Margin = new System.Windows.Forms.Padding(2);
            this.tbDT_E.Name = "tbDT_E";
            this.tbDT_E.Size = new System.Drawing.Size(121, 21);
            this.tbDT_E.TabIndex = 3;
            this.tbDT_E.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnLoadData
            // 
            this.btnLoadData.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnLoadData.Location = new System.Drawing.Point(0, 0);
            this.btnLoadData.Margin = new System.Windows.Forms.Padding(2);
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(93, 20);
            this.btnLoadData.TabIndex = 0;
            this.btnLoadData.Text = "LoadData";
            this.btnLoadData.UseVisualStyleBackColor = true;
            this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tableLayoutPanel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 24);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(930, 468);
            this.panel2.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.chartAT2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.chartAT1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(930, 468);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // chartAT2
            // 
            this.chartAT2.CandleChartType = OM.Vikala.Controls.Charts.CandleChartTypeEnum.기본;
            this.chartAT2.ChartData = null;
            this.chartAT2.ChartEventInstance = null;
            this.chartAT2.DisplayPointCount = 30;
            this.chartAT2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartAT2.IsAutoScrollX = true;
            this.chartAT2.IsLoaded = false;
            this.chartAT2.IsShowCandle = false;
            this.chartAT2.IsShowLine = false;
            this.chartAT2.IsShowXLine = false;
            this.chartAT2.ItemCode = "";
            this.chartAT2.LineChartType = OM.Vikala.Controls.Charts.LineChartTypeEnum.기본;
            this.chartAT2.Location = new System.Drawing.Point(6, 238);
            this.chartAT2.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.chartAT2.Name = "chartAT2";
            this.chartAT2.SelectedTrackBarValue = 1;
            this.chartAT2.Size = new System.Drawing.Size(918, 226);
            this.chartAT2.TabIndex = 3;
            this.chartAT2.TimeInterval = OM.Lib.Base.Enums.TimeIntervalEnum.Day;
            this.chartAT2.Title = null;
            // 
            // chartAT1
            // 
            this.chartAT1.CandleChartType = OM.Vikala.Controls.Charts.CandleChartTypeEnum.기본;
            this.chartAT1.ChartData = null;
            this.chartAT1.ChartEventInstance = null;
            this.chartAT1.DisplayPointCount = 30;
            this.chartAT1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartAT1.IsAutoScrollX = true;
            this.chartAT1.IsLoaded = false;
            this.chartAT1.IsShowCandle = false;
            this.chartAT1.IsShowLine = false;
            this.chartAT1.IsShowXLine = false;
            this.chartAT1.ItemCode = "";
            this.chartAT1.LineChartType = OM.Vikala.Controls.Charts.LineChartTypeEnum.기본;
            this.chartAT1.Location = new System.Drawing.Point(6, 4);
            this.chartAT1.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.chartAT1.Name = "chartAT1";
            this.chartAT1.SelectedTrackBarValue = 1;
            this.chartAT1.Size = new System.Drawing.Size(918, 226);
            this.chartAT1.TabIndex = 2;
            this.chartAT1.TimeInterval = OM.Lib.Base.Enums.TimeIntervalEnum.Day;
            this.chartAT1.Title = null;
            // 
            // AtomMainForm_Sample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 496);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AtomMainForm_Sample";
            this.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Text = "아톰(원자)챠트 테스트 폼";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnLoadData;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Controls.Charts.AtomChart chartAT1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.TextBox tbDT_E;
        private Controls.Charts.AtomChart chartAT2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton4;
    }
}

