using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SHOPTT_DA.Models
{
    public class SanPhamModel
    {
        MyDataDataContext data = new MyDataDataContext(); 
        public List<LoaiSP> listLoai()
        {
            return data.LoaiSPs.ToList();
        }

        public List<ThuongHieu> listTH()
        {
            return data.ThuongHieus.ToList();
        }

        

    }
}