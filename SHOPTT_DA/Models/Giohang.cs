using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SHOPTT_DA.Models
{
    public class Giohang
    {
        MyDataDataContext data = new MyDataDataContext();
        public int masanpham { get; set; }

        [Display(Name = "Tên sản phẩm")]
        public string tensanpham { get; set; }

        [Display(Name = "Ảnh sản phẩm")]
        public string hinh { get; set; }

        [Display(Name = "Giá bán")]
        public Double giaban { get; set; }

        [Display(Name = "Số lượng")]
        public int soluong { get; set; }

        [Display(Name = "Thành tiền")]
        public Double thanhtien { get { return soluong * giaban; } }

        public Giohang(int id)
        {
            masanpham = id;
            SanPham sp = data.SanPhams.Single(n => n.maSP == masanpham);
            tensanpham = sp.tenSP;
            hinh = sp.hinh;
            giaban = double.Parse(sp.giaBan.ToString());
            soluong = 1;
        }
    }
}