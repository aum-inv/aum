﻿namespace OM.Atman.Chakra.App.Uc
{
    partial class Uc_ListItem
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

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.itemType = new MetroFramework.Controls.MetroTile();
            this.lbTitle = new MetroFramework.Controls.MetroLabel();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.lbDuration = new MetroFramework.Controls.MetroLabel();
            this.SuspendLayout();
            // 
            // itemType
            // 
            this.itemType.ActiveControl = null;
            this.itemType.Dock = System.Windows.Forms.DockStyle.Left;
            this.itemType.Location = new System.Drawing.Point(2, 2);
            this.itemType.Name = "itemType";
            this.itemType.Size = new System.Drawing.Size(38, 39);
            this.itemType.TabIndex = 1;
            this.itemType.Text = "◎";
            this.itemType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.itemType.TileTextFontSize = MetroFramework.MetroTileTextSize.Small;
            this.itemType.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            this.itemType.UseSelectable = true;
            this.itemType.Click += new System.EventHandler(this.itemType_Click);
            this.itemType.MouseClick += new System.Windows.Forms.MouseEventHandler(this.item_MouseClick);
            this.itemType.MouseEnter += new System.EventHandler(this.item_MouseEnter);
            this.itemType.MouseLeave += new System.EventHandler(this.item_MouseLeave);
            // 
            // lbTitle
            // 
            this.lbTitle.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lbTitle.Location = new System.Drawing.Point(45, 4);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(200, 15);
            this.lbTitle.TabIndex = 2;
            this.lbTitle.Text = "코스피200 투자계획 (투자완료)";
            this.lbTitle.MouseClick += new System.Windows.Forms.MouseEventHandler(this.item_MouseClick);
            this.lbTitle.MouseEnter += new System.EventHandler(this.item_MouseEnter);
            this.lbTitle.MouseLeave += new System.EventHandler(this.item_MouseLeave);
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.OldLace;
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(40, 38);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(208, 3);
            this.pnlBottom.TabIndex = 5;
            // 
            // lbDuration
            // 
            this.lbDuration.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lbDuration.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lbDuration.Location = new System.Drawing.Point(45, 22);
            this.lbDuration.Name = "lbDuration";
            this.lbDuration.Size = new System.Drawing.Size(200, 15);
            this.lbDuration.TabIndex = 3;
            this.lbDuration.Text = "기간 : 20.05.12 ~ 20.06.30";
            this.lbDuration.UseCustomBackColor = true;
            this.lbDuration.UseCustomForeColor = true;
            this.lbDuration.MouseClick += new System.Windows.Forms.MouseEventHandler(this.item_MouseClick);
            this.lbDuration.MouseEnter += new System.EventHandler(this.item_MouseEnter);
            this.lbDuration.MouseLeave += new System.EventHandler(this.item_MouseLeave);
            // 
            // Uc_ListItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.lbDuration);
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.itemType);
            this.Name = "Uc_ListItem";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.Size = new System.Drawing.Size(250, 43);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.item_MouseClick);
            this.MouseEnter += new System.EventHandler(this.item_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.item_MouseLeave);
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroTile itemType;
        private MetroFramework.Controls.MetroLabel lbTitle;
        private System.Windows.Forms.Panel pnlBottom;
        private MetroFramework.Controls.MetroLabel lbDuration;
    }
}
