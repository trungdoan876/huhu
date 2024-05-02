﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DoANLapTrinhWin
{
    public partial class UCSP : UserControl
    {
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.connStr);
        SanPham sp;
        SanPhamDAO spDAO = new SanPhamDAO();
        YeuThichDAO ytdao = new YeuThichDAO();
        GioHangDAO ghdao = new GioHangDAO();
        Global gl = new Global();
        string tenTK;
        private void UCSP_Load(object sender, EventArgs e)
        {

        }
        public UCSP()
        {
               InitializeComponent();
        }
        public UCSP(SanPham sp,string tenTK)
        {
            this.sp = sp;
            this.tenTK= tenTK;
            InitializeComponent();
            this.lblMaSP.Text = sp.MaSP;
            this.lblTenSP.Text = sp.TenSP;
            this.lblGiaBan.Text =  sp.GiaBan ;
            this.lblGiaGoc.Text =  sp.GiaGoc ;
            this.lblDiaChi.Text = sp.DiaChi;
            this.picHinh.Image = Global.ByteArrayToImage(sp.Hinh);
        }
        private void UCSP_Click(object sender, EventArgs e)
        {
            this.Hide(); //an form 1
            FCTSP formCTSP = new FCTSP(sp,picClick,tenTK);
            formCTSP.ShowDialog();
            formCTSP = null;
            this.Show();  
        }
        bool picClick;
        private void traitim()
        {
            //false la chua them
            if (picClick) //=true dang la tim do
            {
                Global.TimDen(picHeart);
                YeuThich yt = new YeuThich(sp.MaNguoiBan, tenTK, sp.MaSP);
                ytdao.XoaYeuThich(yt);
            }
            //ban dau la false nhan vao la true chuyen thanh mau do
            else
            {
                Global.TimDo(picHeart);
                YeuThich yt = new YeuThich(sp.MaNguoiBan, tenTK, sp.MaSP);
                ytdao.ThemYeuThich(yt);
            }
        }

        private void picHeart_Click(object sender, EventArgs e)
        {
            traitim();
        }

        private void UCSP_Load_1(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string sqlStr = string.Format("SELECT MaSanPham FROM YeuThich ");
                SqlDataAdapter adapter = new SqlDataAdapter(sqlStr, conn);
                DataSet dtSet = new DataSet();
                adapter.Fill(dtSet);

                // Tạo một danh sách các mã sản phẩm trong danh sách yêu thích
                List<string> maSanPhamYeuThich = new List<string>();
                foreach (DataRow row in dtSet.Tables[0].Rows)
                {
                    string maSP = row["MaSanPham"].ToString();
                    maSanPhamYeuThich.Add(maSP);
                }

                // Kiểm tra xem sản phẩm hiện tại có trong danh sách yêu thích không
                if (maSanPhamYeuThich.Contains(sp.MaSP))
                {
                    Global.TimDo(picHeart);
                    picClick = true;
                }
                else
                {
                    Global.TimDen(picHeart);
                    picClick = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
            }
            finally
            {
                conn.Close();
            }
            
        }
    }
}
