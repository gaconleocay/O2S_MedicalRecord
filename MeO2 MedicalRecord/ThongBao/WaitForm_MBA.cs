﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraWaitForm;

namespace MeO2_MedicalRecord.ThongBao
{
    public partial class WaitForm_MBA : WaitForm
    {
        public WaitForm_MBA()
        {
            InitializeComponent();
        }

        #region Overrides

        public override void SetCaption(string caption)
        {
            base.SetCaption(caption);
        }
        public override void SetDescription(string description)
        {
            base.SetDescription(description);
        }
        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

        #endregion

        public enum WaitFormCommand
        {
        }
    }
}