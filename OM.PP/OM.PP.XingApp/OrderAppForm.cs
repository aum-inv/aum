﻿using OM.Lib.Base.Enums;
using OM.PP.XingApp.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OM.PP.XingApp
{
    public partial class OrderAppForm : Form
    {
        public OrderAppForm()
        {
            InitializeComponent();
            this.Load += OrderAppForm_Load;
        }

        private void OrderAppForm_Load(object sender, EventArgs e)
        {
            tbItemK.Text = ItemCodeSet.GetItemSHCodeByName(ItemCode.선물_국내_코스피200);

            tbItemGC.Text = ItemCodeSet.GetItemSHCodeByName(ItemCode.선물_해외_GOLD);
            tbItemNG .Text = ItemCodeSet.GetItemSHCodeByName(ItemCode.선물_해외_NG);
            tbItemSI.Text = ItemCodeSet.GetItemSHCodeByName(ItemCode.선물_해외_SILVER);
            tbItemCL.Text = ItemCodeSet.GetItemSHCodeByName(ItemCode.선물_해외_WTI);
            tbItemNQ.Text = ItemCodeSet.GetItemSHCodeByName(ItemCode.선물_해외_나스닥);
            tbItemHSI.Text = ItemCodeSet.GetItemSHCodeByName(ItemCode.선물_해외_항셍);

            OrderInstances.IndexFutureOrderInstance.LogEvent += OrderInstance_LogEvent;

            OrderInstances.WorldFutureOrderInstanceCL.LogEvent += OrderInstance_LogEvent;
            OrderInstances.WorldFutureOrderInstanceNG.LogEvent += OrderInstance_LogEvent;
            OrderInstances.WorldFutureOrderInstanceHSI.LogEvent += OrderInstance_LogEvent;
            OrderInstances.WorldFutureOrderInstanceNQ.LogEvent += OrderInstance_LogEvent;
            OrderInstances.WorldFutureOrderInstanceGC.LogEvent += OrderInstance_LogEvent;
            OrderInstances.WorldFutureOrderInstanceSI.LogEvent += OrderInstance_LogEvent;
        }

        private void OrderInstance_LogEvent(object sender, EventArgs e)
        {
            tbLog.Invoke(new MethodInvoker(() =>
            {
                tbLog.AppendText(sender.ToString());
                tbLog.AppendText(Environment.NewLine);
            }));
        }

        #region tbItem_TextChanged
        private void tbItemK_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.IndexFutureOrderInstance.itemCode = (sender as TextBox).Text;
        }

        private void tbItemCL_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceCL.itemCode = (sender as TextBox).Text;
        }

        private void tbItemNG_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceNG.itemCode = (sender as TextBox).Text;
        }

        private void tbItemHSI_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceHSI.itemCode = (sender as TextBox).Text;
        }

        private void tbItemNQ_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceNQ.itemCode = (sender as TextBox).Text;
        }

        private void tbItemGC_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceGC.itemCode = (sender as TextBox).Text;
        }

        private void tbItemSI_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceSI.itemCode = (sender as TextBox).Text;
        }
        #endregion

        #region tbQuantity_TextChanged
        private void tbQuantityK_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.IndexFutureOrderInstance.OrderPrice1.Quantity = (sender as TextBox).Text;
            OrderInstances.IndexFutureOrderInstance.OrderPrice2.Quantity = (sender as TextBox).Text;
            OrderInstances.IndexFutureOrderInstance.OrderPrice3.Quantity = (sender as TextBox).Text;
        }

        private void tbQuantityCL_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceCL.OrderPrice1.Quantity = (sender as TextBox).Text;
            OrderInstances.WorldFutureOrderInstanceCL.OrderPrice2.Quantity = (sender as TextBox).Text;
            OrderInstances.WorldFutureOrderInstanceCL.OrderPrice3.Quantity = (sender as TextBox).Text;
        }

        private void tbQuantityNG_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceNG.OrderPrice1.Quantity = (sender as TextBox).Text;
            OrderInstances.WorldFutureOrderInstanceNG.OrderPrice2.Quantity = (sender as TextBox).Text;
            OrderInstances.WorldFutureOrderInstanceNG.OrderPrice3.Quantity = (sender as TextBox).Text;
        }

        private void tbQuantityHSI_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceHSI.OrderPrice1.Quantity = (sender as TextBox).Text;
            OrderInstances.WorldFutureOrderInstanceHSI.OrderPrice2.Quantity = (sender as TextBox).Text;
            OrderInstances.WorldFutureOrderInstanceHSI.OrderPrice3.Quantity = (sender as TextBox).Text;
        }

        private void tbQuantityNQ_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceNQ.OrderPrice1.Quantity = (sender as TextBox).Text;
            OrderInstances.WorldFutureOrderInstanceNQ.OrderPrice2.Quantity = (sender as TextBox).Text;
            OrderInstances.WorldFutureOrderInstanceNQ.OrderPrice3.Quantity = (sender as TextBox).Text;
        }

        private void tbQuantityGC_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceGC.OrderPrice1.Quantity = (sender as TextBox).Text;
            OrderInstances.WorldFutureOrderInstanceGC.OrderPrice2.Quantity = (sender as TextBox).Text;
            OrderInstances.WorldFutureOrderInstanceGC.OrderPrice3.Quantity = (sender as TextBox).Text;
        }

        private void tbQuantitySI_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceSI.OrderPrice1.Quantity = (sender as TextBox).Text;
            OrderInstances.WorldFutureOrderInstanceSI.OrderPrice2.Quantity = (sender as TextBox).Text;
            OrderInstances.WorldFutureOrderInstanceSI.OrderPrice3.Quantity = (sender as TextBox).Text;
        }
        #endregion

        #region chkIsUse_CheckedChanged
        private void chkIsUseK_CheckedChanged(object sender, EventArgs e)
        {
            OrderInstances.IndexFutureOrderInstance.IsUse = (sender as CheckBox).Checked;
        }

        private void chkIsUseCL_CheckedChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceCL.IsUse = (sender as CheckBox).Checked;
        }

        private void chkIsUseNG_CheckedChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceNG.IsUse = (sender as CheckBox).Checked;
        }

        private void chkIsUseHSI_CheckedChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceHSI.IsUse = (sender as CheckBox).Checked;
        }

        private void chkIsUseNQ_CheckedChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceNQ.IsUse = (sender as CheckBox).Checked;
        }

        private void chkIsUseGC_CheckedChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceGC.IsUse = (sender as CheckBox).Checked;
        }

        private void chkIsUseSI_CheckedChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceSI.IsUse = (sender as CheckBox).Checked;
        }
        #endregion

        #region tbPrice_TextChanged
        private void tbPrice1K_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.IndexFutureOrderInstance.OrderPrice1.OrderPrice = (sender as TextBox).Text;
        }

        private void tbPrice1CL_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceCL.OrderPrice1.OrderPrice = (sender as TextBox).Text;
        }

        private void tbPrice1NG_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceNG.OrderPrice1.OrderPrice = (sender as TextBox).Text;
        }

        private void tbPrice1HSI_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceHSI.OrderPrice1.OrderPrice = (sender as TextBox).Text;
        }

        private void tbPrice1NQ_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceNQ.OrderPrice1.OrderPrice = (sender as TextBox).Text;
        }

        private void tbPrice1GC_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceGC.OrderPrice1.OrderPrice = (sender as TextBox).Text;
        }

        private void tbPrice1SI_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceSI.OrderPrice1.OrderPrice = (sender as TextBox).Text;
        }

        private void tbPrice2K_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.IndexFutureOrderInstance.OrderPrice2.OrderPrice = (sender as TextBox).Text;
        }

        private void tbPrice2CL_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceCL.OrderPrice2.OrderPrice = (sender as TextBox).Text;
        }

        private void tbPrice2NG_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceNG.OrderPrice2.OrderPrice = (sender as TextBox).Text;
        }

        private void tbPrice2HSI_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceHSI.OrderPrice2.OrderPrice = (sender as TextBox).Text;
        }

        private void tbPrice2NQ_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceNQ.OrderPrice2.OrderPrice = (sender as TextBox).Text;
        }

        private void tbPrice2GC_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceGC.OrderPrice2.OrderPrice = (sender as TextBox).Text;
        }

        private void tbPrice2SI_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceSI.OrderPrice2.OrderPrice = (sender as TextBox).Text;
        }

        private void tbPrice3K_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.IndexFutureOrderInstance.OrderPrice3.OrderPrice = (sender as TextBox).Text;
        }

        private void tbPrice3CL_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceCL.OrderPrice3.OrderPrice = (sender as TextBox).Text;
        }

        private void tbPrice3NG_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceNG.OrderPrice3.OrderPrice = (sender as TextBox).Text;
        }

        private void tbPrice3HSI_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceHSI.OrderPrice3.OrderPrice = (sender as TextBox).Text;
        }

        private void tbPrice3NQ_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceNQ.OrderPrice3.OrderPrice = (sender as TextBox).Text;
        }

        private void tbPrice3GC_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceGC.OrderPrice3.OrderPrice = (sender as TextBox).Text;
        }

        private void tbPrice3SI_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceSI.OrderPrice3.OrderPrice = (sender as TextBox).Text;
        }
        #endregion

        #region tbUpDown_TextChanged
        private void tbUpDown1K_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.IndexFutureOrderInstance.OrderPrice1.Position = (sender as TextBox).Text;
        }

        private void tbUpDown1CL_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceCL.OrderPrice1.Position = (sender as TextBox).Text;
        }

        private void tbUpDown1NG_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceNG.OrderPrice1.Position = (sender as TextBox).Text;
        }

        private void tbUpDown1HSI_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceHSI.OrderPrice1.Position = (sender as TextBox).Text;
        }

        private void tbUpDown1NQ_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceNQ.OrderPrice1.Position = (sender as TextBox).Text;
        }

        private void tbUpDown1GC_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceGC.OrderPrice1.Position = (sender as TextBox).Text;
        }

        private void tbUpDown1SI_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceSI.OrderPrice1.Position = (sender as TextBox).Text;
        }

        private void tbUpDown2K_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.IndexFutureOrderInstance.OrderPrice2.Position = (sender as TextBox).Text;
        }

        private void tbUpDown2CL_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceCL.OrderPrice2.Position = (sender as TextBox).Text;
        }

        private void tbUpDown2NG_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceNG.OrderPrice2.Position = (sender as TextBox).Text;
        }

        private void tbUpDown2HSI_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceHSI.OrderPrice2.Position = (sender as TextBox).Text;
        }

        private void tbUpDown2NQ_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceNQ.OrderPrice2.Position = (sender as TextBox).Text;
        }

        private void tbUpDown2GC_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceGC.OrderPrice2.Position = (sender as TextBox).Text;
        }

        private void tbUpDown2SI_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceSI.OrderPrice2.Position = (sender as TextBox).Text;
        }

        private void tbUpDown3K_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.IndexFutureOrderInstance.OrderPrice3.Position = (sender as TextBox).Text;
        }

        private void tbUpDown3CL_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceCL.OrderPrice3.Position = (sender as TextBox).Text;
        }

        private void tbUpDown3NG_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceNG.OrderPrice3.Position = (sender as TextBox).Text;
        }

        private void tbUpDown3HSI_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceHSI.OrderPrice3.Position = (sender as TextBox).Text;
        }

        private void tbUpDown3NQ_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceNQ.OrderPrice3.Position = (sender as TextBox).Text;
        }

        private void tbUpDown3GC_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceGC.OrderPrice3.Position = (sender as TextBox).Text;
        }

        private void tbUpDown3SI_TextChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceSI.OrderPrice3.Position = (sender as TextBox).Text;
        }
        #endregion

        #region chkIsUse_CheckedChanged
        private void chkIsUse1K_CheckedChanged(object sender, EventArgs e)
        {
            OrderInstances.IndexFutureOrderInstance.OrderPrice1.IsRun = (sender as CheckBox).Checked;
        }

        private void chkIsUse1CL_CheckedChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceCL.OrderPrice1.IsRun = (sender as CheckBox).Checked;
        }

        private void chkIsUse1NG_CheckedChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceNG.OrderPrice1.IsRun = (sender as CheckBox).Checked;
        }

        private void chkIsUse1SHI_CheckedChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceHSI.OrderPrice1.IsRun = (sender as CheckBox).Checked;
        }

        private void chkIsUse1NQ_CheckedChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceNQ.OrderPrice1.IsRun = (sender as CheckBox).Checked;
        }

        private void chkIsUse1GC_CheckedChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceGC.OrderPrice1.IsRun = (sender as CheckBox).Checked;
        }

        private void chkIsUse1SI_CheckedChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceSI.OrderPrice1.IsRun = (sender as CheckBox).Checked;
        }

        private void chkIsUse2K_CheckedChanged(object sender, EventArgs e)
        {
            OrderInstances.IndexFutureOrderInstance.OrderPrice2.IsRun = (sender as CheckBox).Checked;
        }

        private void chkIsUse2CL_CheckedChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceCL.OrderPrice2.IsRun = (sender as CheckBox).Checked;
        }

        private void chkIsUse2NG_CheckedChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceNG.OrderPrice2.IsRun = (sender as CheckBox).Checked;
        }

        private void chkIsUse2SHI_CheckedChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceHSI.OrderPrice2.IsRun = (sender as CheckBox).Checked;
        }

        private void chkIsUse2NQ_CheckedChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceNQ.OrderPrice2.IsRun = (sender as CheckBox).Checked;
        }

        private void chkIsUse2GC_CheckedChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceGC.OrderPrice2.IsRun = (sender as CheckBox).Checked;
        }

        private void chkIsUse2SI_CheckedChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceSI.OrderPrice2.IsRun = (sender as CheckBox).Checked;
        }

        private void chkIsUse3K_CheckedChanged(object sender, EventArgs e)
        {
            OrderInstances.IndexFutureOrderInstance.OrderPrice3.IsRun = (sender as CheckBox).Checked;
        }

        private void chkIsUse3CL_CheckedChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceCL.OrderPrice3.IsRun = (sender as CheckBox).Checked;
        }

        private void chkIsUse3NG_CheckedChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceNG.OrderPrice3.IsRun = (sender as CheckBox).Checked;
        }

        private void chkIsUse3SHI_CheckedChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceHSI.OrderPrice3.IsRun = (sender as CheckBox).Checked;
        }

        private void chkIsUse3NQ_CheckedChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceNQ.OrderPrice3.IsRun = (sender as CheckBox).Checked;
        }

        private void chkIsUse3GC_CheckedChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceGC.OrderPrice3.IsRun = (sender as CheckBox).Checked;
        }

        private void chkIsUse3SI_CheckedChanged(object sender, EventArgs e)
        {
            OrderInstances.WorldFutureOrderInstanceSI.OrderPrice3.IsRun = (sender as CheckBox).Checked;
        }
        #endregion
    }
}
