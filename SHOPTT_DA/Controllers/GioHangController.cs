using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using SHOPTT_DA.Models;

namespace SHOPTT_DA.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        public ActionResult Index()
        {
            return View();
        }
        MyDataDataContext  data = new MyDataDataContext();
        public List<Giohang> Laygiohang()
        {
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang == null)
            {
                lstGiohang = new List<Giohang>();
                Session["Giohang"] = lstGiohang;
            }
            return lstGiohang;
        }

        public ActionResult ThemGioHang(int id, string strURL)
        {
            List<Giohang> lstGiohang = Laygiohang();
            Giohang sp = lstGiohang.Find(n => n.masanpham == id);
            if (sp == null)
            {
                sp = new Giohang(id);
                lstGiohang.Add(sp);
                return Redirect(strURL);
            }
            else
            {
                sp.soluong++;
                return Redirect(strURL);
            }
        }

        private int TongSoLuong()
        {
            int tsl = 0;
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                tsl = lstGiohang.Sum(n => n.soluong);
            }
            return tsl;
        }

        private int TongSoluongSanPham()
        {
            int tsl = 0;
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                tsl = lstGiohang.Count();
            }
            return tsl;
        }


        private double TongTien()
        {
            double tt = 0;
            List<Giohang> lstGiohang = Session["GioHang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                tt = lstGiohang.Sum(n => n.thanhtien);
            }
            return tt;
        }
        public ActionResult GioHang()
        {
            List<Giohang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoluongSanPham();
            return View(lstGiohang);
        }

        public ActionResult GioHangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoluongSanPham();
            return PartialView();
        }

        public ActionResult XoaGiohang(int id)
        {
            List<Giohang> lstGiohang = Laygiohang();
            Giohang sanpham = lstGiohang.SingleOrDefault(n => n.masanpham == id);
            if (sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.masanpham == id);
                return RedirectToAction("GioHang");

            }
            return RedirectToAction("GioHang");
        }
        public ActionResult CapnhatGiohang(int id, FormCollection collection)
        {
            List<Giohang> lstGiohang = Laygiohang();
            Giohang sanpham = lstGiohang.SingleOrDefault(n => n.masanpham == id);
            if (sanpham != null)
            {
                sanpham.soluong = int.Parse(collection["txtSoluong"].ToString());
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult XoaTatCaGioHang()
        {
            List<Giohang> lstGiohang = Laygiohang();
            lstGiohang.Clear();
            return RedirectToAction("GioHang");
        }

        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "Sneaker");
            }
            List<Giohang> listGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoluongSanPham();
            return View(listGiohang);
        }

        public ActionResult DatHang(FormCollection collection)
        {
            HoaDon hd = new HoaDon();
            KhachHang kh = (KhachHang)Session["TaiKhoan"];
            SanPham s = new SanPham();
            List<Giohang> gh = Laygiohang();
            var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["NgayGiao"]);
            hd.maKH = kh.maKH;
            hd.ngayDat = DateTime.Now;
            hd.ngayGiao = DateTime.Parse(ngaygiao);
            hd.giaoHang = false;
            hd.thanhToan = false;
            data.HoaDons.InsertOnSubmit(hd);
            data.SubmitChanges();
            foreach (var item in gh)
            {
                ChiTietHoaDon ctdh = new ChiTietHoaDon();
                ctdh.maHD = hd.maHD;
                ctdh.maSP = item.masanpham;
                ctdh.soLuong = item.soluong;
                ctdh.gia = (decimal)item.giaban;
                //s = data.SanPhams.Single(n => n.maSP == item.masanpham);
                //s.soluongton = ctdh.soluong;
                data.SubmitChanges();
                data.ChiTietHoaDons.InsertOnSubmit(ctdh);
                MailMessage mail = new MailMessage("vantaypham100@gmail.com", kh.email, "XÁC NHẬN ĐƠN HÀNG", "Thông tin đơn hàng:" +"\nTên sản phẩm: " + item.tensanpham + "\nSố lượng: " + item.soluong+"\nGiá bán: " + item.giaban + "\nNgày đặt: " + hd.ngayDat + "\nThành tiền: "+ item.giaban*item.soluong);

                //gửi tin nhắn
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);//vì sử dụng Gmail nên mình dùng port 587 và smtp.gmail.com

                // thêm vào credential vì SMTP server cần nó để biết được email + password của email mà bạn đang dùng
                client.Credentials = new NetworkCredential("vantaypham100@gmail.com", "0972528961vantaypham");

                client.EnableSsl = true;//vì ta cần thiết lập kết nối SSL với SMTP server nên cần gán nó bằng true
                client.Send(mail);
            }
            data.SubmitChanges();
            Session["Giohang"] = null;
            
            return RedirectToAction("XacNhanDonHang");
        }

        public ActionResult XacNhanDonHang()
        {
            
            return View();
        }
    }
}