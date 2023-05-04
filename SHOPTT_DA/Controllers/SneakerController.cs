using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SHOPTT_DA.Models;
using PagedList;

namespace SHOPTT_DA.Controllers
{
    public class SneakerController : Controller
    {
        //TAO 1 doi tuong quan ly csdl
        MyDataDataContext data = new MyDataDataContext();

        private List<SanPham> SPMOI(int count)
        {
            //sap xep loc sp moi ra
            return data.SanPhams.OrderByDescending(a => a.ngayCapNhat).Take(count).ToList();
        }

        // GET: Sneaker
        public ActionResult ListSP()
        {
            var all_sanpham = from sp in data.SanPhams select sp;
            return View(all_sanpham);
        }
        public ActionResult Index(int? page)
        {
            if (page == null) page = 1;
            var all_sach = (from s in data.SanPhams select s).OrderBy(m => m.maSP);
            int pageSize = 9;
            int pageNum = page ?? 1;
            return View(all_sach.ToPagedList(pageNum, pageSize));
        }

        public ActionResult LoaiSanPham()
        {
            var loaisanpham = from lsp in data.LoaiSPs select lsp;

            return PartialView(loaisanpham);
        }

        public ActionResult ThuongHieu()
        {
            var thuonghieu = from th in data.ThuongHieus select th;
            return PartialView(thuonghieu);
        }

        public ActionResult SPTheoLoai(int id,int? page)
        {
            if (page == null) page = 1;
            var sptl = from tl in data.SanPhams where tl.maLoai == id select tl;
            int pageSize = 9;
            int pageNum = page ?? 1;
            return PartialView(sptl.ToPagedList(pageNum, pageSize));
        }

        public ActionResult SPTheoTH(int id, int? page)
        {
            if (page == null) page = 1;
            var sptth = from tl in data.SanPhams where tl.maThuongHieu == id select tl;
            int pageSize = 9;
            int pageNum = page ?? 1;
            return PartialView(sptth.ToPagedList(pageNum, pageSize));
        }

        public ActionResult Details(int id)
        {
            var sp = from s in data.SanPhams where s.maSP == id select s;
            return View(sp.Single());
        }


    }
}