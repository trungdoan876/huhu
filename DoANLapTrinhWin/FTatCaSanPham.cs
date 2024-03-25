﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DoANLapTrinhWin
{
    public partial class FTatCaSanPham : Form
    {
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.connStr);
        public FTatCaSanPham()
        {
            InitializeComponent();
        }
        //đổ dữ liệu vào user control trong danh sách sản phẩm
        private void FTatCaSanPham_Load(object sender, EventArgs e)
        {
            try
            {
                panelTatCaSP.AutoScroll = true;
                conn.Open();
                string sqlStr = string.Format("SELECT *FROM SanPham");
                SqlDataAdapter adapter = new SqlDataAdapter(sqlStr, conn);
                DataSet dtSet = new DataSet();
                adapter.Fill(dtSet);
                int x = 0;
                int y  = 0;
                foreach (DataRow row in dtSet.Tables[0].Rows)
                {
                    string maSP = row["MaSanPham"].ToString();
                    string tenSP = row["TenSanPham"].ToString();
                    string giaTien = row["GiaBan"].ToString();
                    string giaGoc = row["GiaGoc"].ToString();
                    string moTaSP = row["MoTaSanPham"].ToString();
                    string tinhTrang = row["TinhTrang"].ToString();
                    string nganhHang = row["NganhHang"].ToString();
                    string xuatXu = row["XuatXu"].ToString();
                    string diaChi = row["DiaChi"].ToString() ;
                    string thoiGianSuDung = row["TGDSD"].ToString();
                    DateTime ngayDang = DateTime.Now;
                    SanPham sp = new SanPham(maSP,tenSP,giaTien,giaGoc,xuatXu,thoiGianSuDung,ngayDang,moTaSP,nganhHang,tinhTrang,"", diaChi);

                    UCSPBan ucSPBan = new UCSPBan(sp);
                    
                    ucSPBan.Location = new Point(x, y);
                    x += ucSPBan.Width += 5;
                    if (x == ucSPBan.Width * 3)
                    {
                        x = 0;
                        y += ucSPBan.Height + 5;
                    }
                    panelTatCaSP.Controls.Add(ucSPBan);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                conn.Close(); 
            }
        }
    }
}
