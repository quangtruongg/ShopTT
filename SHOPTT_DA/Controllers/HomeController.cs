using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SHOPTT_DA.Models;

namespace SHOPTT_DA.Controllers
{
    public class HomeController : Controller
    {
        MyDataDataContext data = new MyDataDataContext();
        /// <summary>
        /// LIST SAN PHAM
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var all_sanpham = from sp in data.SanPhams select sp;
            return View(all_sanpham);
        }
        public void SetViewBag(int? selectedId = null)
        {
            var dao = new SanPhamModel();
            ViewBag.maLoai = new SelectList(dao.listLoai(), "maLoai", "tenLoai", selectedId);
            ViewBag.maThuongHieu = new SelectList(dao.listTH(), "maThuongHieu", "tenThuongHieu", selectedId);
        }
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        [HttpPost]
        public ActionResult Create(System.Web.Mvc.FormCollection collection)
        {
            var E_tensanpham = collection["tenSP"];
            var maloai = Convert.ToInt32(collection["maLoai"]);
            var mathuonghieu = Convert.ToInt32(collection["maThuongHieu"]);
            var mota = collection["moTa"];
            var hinh = collection["hinh"];
            var giaban = Convert.ToDecimal(collection["giaBan"]);
            var ngaycapnhat = Convert.ToDateTime(collection["ngayCapNhat"]);
            if (string.IsNullOrEmpty(E_tensanpham))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                SanPham sp = new SanPham();
                sp.tenSP = E_tensanpham;
                sp.hinh = hinh;
                sp.giaBan = giaban;
                sp.ngayCapNhat = ngaycapnhat;
                sp.maLoai = maloai;
                sp.maThuongHieu = mathuonghieu;
                sp.moTa = mota;
                data.SanPhams.InsertOnSubmit(sp);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            SetViewBag();
            return this.Create();
        }

        public ActionResult Edit(int id)
        {
            var E_sanpham = data.SanPhams.First(m => m.maSP == id);
            SetViewBag(id);
            return View(E_sanpham);
        }
        [HttpPost]
        public ActionResult Edit(int id, System.Web.Mvc.FormCollection collection)
        {
            var E_sanpham = data.SanPhams.First(m => m.maSP == id);
            var E_maloai = Convert.ToInt32(collection["maLoai"]);
            var E_mathuonghieu = Convert.ToInt32(collection["maThuongHieu"]);
            var E_tensanpham = collection["tenSP"];
            var E_hinh = collection["hinh"];
            var E_giaban = Convert.ToDecimal(collection["giaBan"]);
            var E_ngaycapnhat = Convert.ToDateTime(collection["ngayCapNhat"]);
            var E_mota = collection["mota"];
            if (string.IsNullOrEmpty(E_tensanpham))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                E_sanpham.tenSP = E_tensanpham;
                E_sanpham.hinh = E_hinh;
                E_sanpham.giaBan = E_giaban;
                E_sanpham.ngayCapNhat = E_ngaycapnhat;
                E_sanpham.moTa = E_mota;
                E_sanpham.maLoai = E_maloai;
                E_sanpham.maThuongHieu = E_mathuonghieu;
                UpdateModel(E_sanpham);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            SetViewBag(id);
            return this.Edit(id);
        }

        public ActionResult Delete(int id)
        {
            var D_sp = data.SanPhams.First(m => m.maSP == id);
            return View(D_sp);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_sp = data.SanPhams.Where(m => m.maSP == id).First();
            data.SanPhams.DeleteOnSubmit(D_sp);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
        /// <summary>
        /// LIST LOAI SAN PHAM
        /// </summary>
        /// <returns></returns>
        public ActionResult listLoai()
        {
            var all_sanpham = from lsp in data.LoaiSPs select lsp;
            return View(all_sanpham);
        }

        public ActionResult CreateLoai()
        {
            //SetViewBag();
            return View();
        }
        [HttpPost]
        public ActionResult CreateLoai(System.Web.Mvc.FormCollection collection)
        {
            var E_tenloai = collection["tenLoai"];
            
            if (string.IsNullOrEmpty(E_tenloai))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                LoaiSP sp = new LoaiSP();
                sp.tenLoai = E_tenloai;
                
                data.LoaiSPs.InsertOnSubmit(sp);
                data.SubmitChanges();
                return RedirectToAction("listLoai");
            }
            //SetViewBag();
            return this.CreateLoai();
        }

        public ActionResult EditLoai(int id)
        {
            var E_sanpham = data.LoaiSPs.First(m => m.maLoai == id);
            //SetViewBag(id);
            return View(E_sanpham);
        }
        [HttpPost]
        public ActionResult EditLoai(int id, System.Web.Mvc.FormCollection collection)
        {
            var E_sanpham = data.LoaiSPs.First(m => m.maLoai == id);
            
            var E_tensanpham = collection["tenLoai"];
            
            if (string.IsNullOrEmpty(E_tensanpham))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                E_sanpham.tenLoai = E_tensanpham;
                
                UpdateModel(E_sanpham);
                data.SubmitChanges();
                return RedirectToAction("listLoai");
            }
            //SetViewBag(id);
            return this.Edit(id);
        }

        public ActionResult DeleteLoai(int id)
        {
            var D_sp = data.LoaiSPs.First(m => m.maLoai == id);
            return View(D_sp);
        }
        [HttpPost]
        public ActionResult DeleteLoai(int id, FormCollection collection)
        {
            var D_sp = data.LoaiSPs.Where(m => m.maLoai == id).First();
            data.LoaiSPs.DeleteOnSubmit(D_sp);
            data.SubmitChanges();
            return RedirectToAction("listLoai");
        }
        /// <summary>
        /// LIST THUONG HIEU
        /// </summary>
        /// <returns></returns>
        public ActionResult listTH()
        {
            var all_sanpham = from lth in data.ThuongHieus select lth;
            return View(all_sanpham);
        }

        public ActionResult CreateTH()
        {
            //SetViewBag();
            return View();
        }
        [HttpPost]
        public ActionResult CreateTH(System.Web.Mvc.FormCollection collection)
        {
            var E_tenloai = collection["tenThuongHieu"];

            if (string.IsNullOrEmpty(E_tenloai))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                ThuongHieu sp = new ThuongHieu();
                sp.tenThuongHieu = E_tenloai;

                data.ThuongHieus.InsertOnSubmit(sp);
                data.SubmitChanges();
                return RedirectToAction("listTH");
            }
            //SetViewBag();
            return this.CreateTH();
        }

        public ActionResult EditTH(int id)
        {
            var E_sanpham = data.ThuongHieus.First(m => m.maThuongHieu == id);
            //SetViewBag(id);
            return View(E_sanpham);
        }

        [HttpPost]
        public ActionResult EditTH(int id, System.Web.Mvc.FormCollection collection)
        {
            var E_sanpham = data.ThuongHieus.First(m => m.maThuongHieu == id);

            var E_tensanpham = collection["tenThuongHieu"];

            if (string.IsNullOrEmpty(E_tensanpham))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                E_sanpham.tenThuongHieu = E_tensanpham;

                UpdateModel(E_sanpham);
                data.SubmitChanges();
                return RedirectToAction("listTH");
            }
            //SetViewBag(id);
            return this.Edit(id);
        }

        public ActionResult DeleteTH(int id)
        {
            var D_sp = data.ThuongHieus.First(m => m.maThuongHieu == id);
            return View(D_sp);
        }

        [HttpPost]
        public ActionResult DeleteTH(int id, FormCollection collection)
        {
            var D_sp = data.ThuongHieus.Where(m => m.maThuongHieu == id).First();
            data.ThuongHieus.DeleteOnSubmit(D_sp);
            data.SubmitChanges();
            return RedirectToAction("listTH");
        }

        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/images/" + file.FileName));
            return "/Content/images/" + file.FileName;
        }

        /// <summary>
        /// LIST HÓA ĐƠN
        /// </summary>
        /// <returns></returns>
        public ActionResult listHD()
        {
            var all_hoadon = from lhd in data.LoaiSPs select lhd;
            return View(all_hoadon);
        }
    }
}