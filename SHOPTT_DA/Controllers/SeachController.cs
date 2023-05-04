using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SHOPTT_DA.Models;

namespace SHOPTT_DA.Controllers
{
    public class SeachController : Controller
    {
        MyDataDataContext data = new MyDataDataContext();
        // GET: Seach
        public ActionResult Seach(string searchString)
        {
            var links = from l in data.SanPhams select l;// lấy toàn bộ liên kết

            if (!String.IsNullOrEmpty(searchString)) // kiểm tra chuỗi tìm kiếm có rỗng/null hay không
            {
                links = links.Where(s => s.tenSP == searchString || s.LoaiSP.tenLoai == searchString || s.ThuongHieu.tenThuongHieu == searchString); //lọc theo chuỗi tìm kiếm
            }

            return View(links); //trả về kết quả
        }
    }
}