﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoANLapTrinhWin
{
    internal class GioHangDAO
    {
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.connStr);
        DBConnection tt = new DBConnection();
        public void ThemVaoGioHang(GioHang gh)
        {
            string anh = BitConverter.ToString(gh.Hinh).Replace("-", "");
            string sqlStr = string.Format("INSERT INTO GioHang(MaNguoiBan, MaNguoiMua, MaSanPham, " +
                "TenSanPham, SoLuong, GiaBan,Hinh,TrangThaiSP) VALUES ('{0}', N'{1}',N'{2}',N'{3}',N'{4}',N'{5}',0x{6},'{7}')", 
                gh.MaNB, gh.MaNM, gh.MaSP, gh.TenSP, gh.SoLuong, gh.GiaBan, anh, 1);
            tt.ThucThi(sqlStr);
        }
    }
}
