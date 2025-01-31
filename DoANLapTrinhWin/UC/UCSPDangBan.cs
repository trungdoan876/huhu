﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DoANLapTrinhWin
{
    public partial class UCSPDangBan : UserControl
    {
        SanPham sp;
        SanPhamDAO spDAO = new SanPhamDAO();
        public UCSPDangBan()
        {
            InitializeComponent();
        }
        public UCSPDangBan(SanPham sp)
        {
            InitializeComponent();
            this.sp = sp;
            this.lblMaSP.Text = sp.MaSP;
            this.lblTenSP.Text = sp.TenSP;
            this.lblGiaTien.Text = sp.GiaBan + "VND";
            this.picHinh.Image = Global.ByteArrayToImage(sp.Hinh);
        }

        private void GoDangBan_Click(object sender, EventArgs e)
        {
            SanPham sp = new SanPham(lblMaSP.Text);
            spDAO.GoSanPham(sp);
        }
    }
}
