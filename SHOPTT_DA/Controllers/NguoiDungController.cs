using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using SHOPTT_DA.Models;

namespace SHOPTT_DA.Controllers
{
    public class NguoiDungController : Controller
    {
        // GET: NguoiDung
        MyDataDataContext data = new MyDataDataContext();
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(System.Web.Mvc.FormCollection collection, KhachHang kh)
        {
            var tenkh = collection["tenKH"];
            var tendangnhap = collection["tenDangNhap"];
            var matkhau = collection["matKhau"];
            var MatKhauXacNhan = collection["MatKhauXacNhan"];
            var email = collection["email"];
            var diachi = collection["diaChi"];
            var sdt = collection["sDT"];
            var ngaysinh = String.Format("{0:MM/dd/yyyy}", collection["ngaySinh"]);

            

            if (String.IsNullOrEmpty(MatKhauXacNhan))
            {
                ViewData["NhapMKXN"] = "Phải nhập mật khẩu xác nhận!";
            }
            else
            {
                if (!matkhau.Equals(MatKhauXacNhan))
                {
                    ViewData["MatKhauGiongNhau"] = "Mật khẩu và mật khẩu xác nhận phải giống nhau";
                }
                else
                {
                    kh.tenKH = tenkh;
                    kh.tenDangNhap = tendangnhap;
                    kh.matKhau = matkhau;
                    kh.email = email;
                    kh.diaChi = diachi;
                    kh.sDT = sdt;
                    kh.ngaySinh = DateTime.Parse(ngaysinh);
                    kh.maVaiTro = 2;
                    data.KhachHangs.InsertOnSubmit(kh);
                    data.SubmitChanges();
                    return RedirectToAction("DangNhap");
                }
            }
            return this.DangKy();
        }
   
        

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(System.Web.Mvc.FormCollection collection)
        {
            var tendangnhap = collection["tenDangNhap"];
            var matkhau = collection["matKhau"];
            string controller = "Sneaker";
            KhachHang kh = data.KhachHangs.SingleOrDefault(n => n.tenDangNhap == tendangnhap && n.matKhau == matkhau);
            if (kh != null && kh.maVaiTro == 1)
            {
                ViewBag.ThongBao = "Chúc mừng đăng nhập thành công";
                Session["TaiKhoan"] = kh;
                controller = "Home";
                
            }
            else if(kh != null && kh.maVaiTro == 2)
            {
                ViewBag.ThongBao = "Chúc mừng đăng nhập thành công";
                Session["TaiKhoan"] = kh;
                controller = "Sneaker";
            }    
            else
            {
                ViewBag.ThongBao = "Tên đăng nhập hoặc mật khấu không đúng";
            }
            return RedirectToAction("Index", controller);
            
        }

        public ActionResult DangXuat()
        {
            Session.Clear();
            return RedirectToAction("Index","Sneaker");
        }

    }
}