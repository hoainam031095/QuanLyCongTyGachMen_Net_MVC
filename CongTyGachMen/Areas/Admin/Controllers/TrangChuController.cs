using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CongTyGachMen.Areas.Admin.Controllers
{
    public class TrangChuController : BaseController
    {
        // GET: Admin/TrangChu
        public ActionResult Index()
        {
            return View();
        }
    }
}