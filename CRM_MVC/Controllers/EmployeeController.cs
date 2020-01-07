using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_MVC.Common;
using CRM_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CRM_MVC.Controllers
{
    public class EmployeeController : Controller
    {

        #region 员工登录
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string username, string pwd)
        {
            string json = APIClient.GetApiResult("get", $"api/baseinfo/login?name={username}&pwd={pwd}");
            if (!string.IsNullOrEmpty(json))
            {
                HttpContext.Session.SetString("user", json);
                return RedirectToAction("Index");
            }

            return View();
        }
        #endregion

        #region 系统主界面
        public IActionResult Index()
        {
            string json = Encoding.UTF8.GetString(HttpContext.Session.Get("user"));
            EmployeeInfo em = JsonConvert.DeserializeObject<EmployeeInfo>(json);
            ViewBag.em = em;
            string result = APIClient.GetApiResult("get", "api/baseinfo/getmenu/" + em.EId);
            List<MenuInfo> list = JsonConvert.DeserializeObject<List<MenuInfo>>(result);
            ViewBag.menu = list;
            ViewBag.pmenu = list.Where(m => m.PId == 0).ToList();

            em.EName += "888";
            string upt = APIClient.GetApiResult("put", "api/baseinfo/UptEmp", em);
            ////string add = APIClient.GetApiResult("post", "/api/baseinfo/AddEMp/", em);
            //if (string.IsNullOrEmpty(upt))
            //{

            //}
            return View();
        }
        #endregion
    }


}