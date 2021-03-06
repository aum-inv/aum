﻿using OM.Lib.Base.Enums;
using System;

namespace OM.Vikala.Chakra.App.Mains.Strategy
{
    partial class CandleSummaryForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CandleSummaryForm));
            this.dgv = new System.Windows.Forms.DataGridView();
            this.상품명 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.구분 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.방향 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.매수가격 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.매도가격 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.수익매수가격 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.수익매도가격 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.손실청산가격 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tbItem = new System.Windows.Forms.TextBox();
            this.lblMonitorState = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToOrderColumns = true;
            this.dgv.AllowUserToResizeColumns = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.상품명,
            this.구분,
            this.방향,
            this.매수가격,
            this.매도가격,
            this.수익매수가격,
            this.수익매도가격,
            this.손실청산가격});
            this.dgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgv.Location = new System.Drawing.Point(2, 58);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgv.Size = new System.Drawing.Size(750, 581);
            this.dgv.TabIndex = 2;
            this.dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellContentClick);
            // 
            // 상품명
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.상품명.DefaultCellStyle = dataGridViewCellStyle11;
            this.상품명.Frozen = true;
            this.상품명.HeaderText = "상품코드";
            this.상품명.Name = "상품명";
            this.상품명.Width = 60;
            // 
            // 구분
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.구분.DefaultCellStyle = dataGridViewCellStyle12;
            this.구분.Frozen = true;
            this.구분.HeaderText = "시간";
            this.구분.Name = "구분";
            this.구분.Width = 80;
            // 
            // 방향
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.방향.DefaultCellStyle = dataGridViewCellStyle13;
            this.방향.Frozen = true;
            this.방향.HeaderText = "방향";
            this.방향.Name = "방향";
            this.방향.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.방향.Width = 80;
            // 
            // 매수가격
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.매수가격.DefaultCellStyle = dataGridViewCellStyle14;
            this.매수가격.Frozen = true;
            this.매수가격.HeaderText = "Trend";
            this.매수가격.Name = "매수가격";
            this.매수가격.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // 매도가격
            // 
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.매도가격.DefaultCellStyle = dataGridViewCellStyle15;
            this.매도가격.Frozen = true;
            this.매도가격.HeaderText = "Volatility";
            this.매도가격.Name = "매도가격";
            // 
            // 수익매수가격
            // 
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.수익매수가격.DefaultCellStyle = dataGridViewCellStyle16;
            this.수익매수가격.Frozen = true;
            this.수익매수가격.HeaderText = "BaseLine";
            this.수익매수가격.Name = "수익매수가격";
            this.수익매수가격.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // 수익매도가격
            // 
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.수익매도가격.DefaultCellStyle = dataGridViewCellStyle17;
            this.수익매도가격.Frozen = true;
            this.수익매도가격.HeaderText = "TrendOfStrength";
            this.수익매도가격.Name = "수익매도가격";
            this.수익매도가격.Width = 120;
            // 
            // 손실청산가격
            // 
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.손실청산가격.DefaultCellStyle = dataGridViewCellStyle18;
            this.손실청산가격.Frozen = true;
            this.손실청산가격.HeaderText = "ForcastOfStrength";
            this.손실청산가격.Name = "손실청산가격";
            this.손실청산가격.Width = 120;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.tbItem);
            this.panel4.Controls.Add(this.lblMonitorState);
            this.panel4.Controls.Add(this.btnStop);
            this.panel4.Controls.Add(this.btnStart);
            this.panel4.Location = new System.Drawing.Point(2, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(750, 51);
            this.panel4.TabIndex = 3;
            // 
            // tbItem
            // 
            this.tbItem.Font = new System.Drawing.Font("굴림", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tbItem.Location = new System.Drawing.Point(262, 8);
            this.tbItem.Name = "tbItem";
            this.tbItem.Size = new System.Drawing.Size(157, 38);
            this.tbItem.TabIndex = 3;
            this.tbItem.Text = "CL";
            this.tbItem.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblMonitorState
            // 
            this.lblMonitorState.AutoSize = true;
            this.lblMonitorState.Font = new System.Drawing.Font("맑은 고딕", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonitorState.Location = new System.Drawing.Point(3, 6);
            this.lblMonitorState.Name = "lblMonitorState";
            this.lblMonitorState.Size = new System.Drawing.Size(191, 40);
            this.lblMonitorState.TabIndex = 2;
            this.lblMonitorState.Text = "모니터링중지";
            // 
            // btnStop
            // 
            this.btnStop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnStop.BackgroundImage")));
            this.btnStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnStop.Location = new System.Drawing.Point(654, 2);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(58, 46);
            this.btnStop.TabIndex = 1;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnStart.BackgroundImage")));
            this.btnStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnStart.Location = new System.Drawing.Point(591, 2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(58, 46);
            this.btnStart.TabIndex = 0;
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // CandleSummaryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(757, 641);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.dgv);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CandleSummaryForm";
            this.Text = "아트만_챠트요약";
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblMonitorState;
        private System.Windows.Forms.TextBox tbItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn 상품명;
        private System.Windows.Forms.DataGridViewTextBoxColumn 구분;
        private System.Windows.Forms.DataGridViewTextBoxColumn 방향;
        private System.Windows.Forms.DataGridViewTextBoxColumn 매수가격;
        private System.Windows.Forms.DataGridViewTextBoxColumn 매도가격;
        private System.Windows.Forms.DataGridViewTextBoxColumn 수익매수가격;
        private System.Windows.Forms.DataGridViewTextBoxColumn 수익매도가격;
        private System.Windows.Forms.DataGridViewTextBoxColumn 손실청산가격;
    }
}