using CommonLibary.Model.ViewModel.LineLoginExample;
using Service.LineBot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LineLoginExample.Controllers
{
    public class LoginController : Controller
    {

        /// <summary>
        /// 登入首頁
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            
            return View(new LoginViewModel() { Account = "20180513", ShowMessage = "", Passwrod = "123456" });
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel login)
        {
            //Service工作
            LineBotMember _service = new LineBotMember();
            bool result = false;
            //呼叫登入邏輯
            result= _service.UpdateLogin(login.Account, login.Passwrod);
            //登入判斷
            if (true == result)
                login.ShowMessage = "登入成功!";
            else if (false == result)
                login.ShowMessage = "登入失敗";


            return View(login);
        }

    }
}