using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongTyGachMen.Models;
using System.Web.Routing;

namespace CongTyGachMen.Areas.Admin.Controllers
{
    
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = Session["TaiKhoan"];
            if (session == null)
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                { Controller = "QuanLy", Action = "Dangnhap", Areas = "Admin" }));
            base.OnActionExecuting(filterContext);
        }
        public string GetName()
        {
            if(Session["Ten"] != null)
            {
                string ten = Session["Ten"].ToString();
                return ten;
            }
            return "";
        }
        public string GetImage()
        {
            if (Session["Anh"] != null)
            {
                string anh = Session["Anh"].ToString();
                return anh;
            }
            return "user3.jpg";
        }
    }
}