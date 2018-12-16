using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Ajax;

namespace CongTyGachMen.Helper
{
    public class MyAjaxOptions
    {
        public static AjaxOptions ForGet
        {
            get
            {
                return new AjaxOptions
                {
                    HttpMethod = "get",
                    OnBegin = "ajax.onBegin",
                    OnSuccess = "ajax.onSuccess",
                    OnFailure = "ajax.onFailure",
                    OnComplete = "ajax.onComplete",
                    LoadingElementId = "ajax-loading",
                };
            }
        }

        public static AjaxOptions ForPost
        {
            get
            {
                return new AjaxOptions
                {
                    HttpMethod = "post",
                    OnBegin = "ajax.onBegin",
                    OnSuccess = "ajax.onSuccess",
                    OnFailure = "ajax.onFailure",
                    OnComplete = "ajax.onComplete",
                    LoadingElementId = "ajax-loading",
                };
            }
        }

        public static AjaxOptions ForDelete
        {
            get
            {
                return new AjaxOptions
                {
                    Confirm = "Bạn có chắc chắn muốn xóa không?",
                    HttpMethod = "post",
                    OnBegin = "ajax.onBegin",
                    OnSuccess = "ajax.onSuccess",
                    OnFailure = "ajax.onFailure",
                    OnComplete = "ajax.onComplete",
                    LoadingElementId = "ajax-loading",
                };
            }
        }
    }
}