using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibary.Model.ViewModel.LineLoginExample
{
    public class LoginViewModel
    {
        /// <summary>
        /// 帳號
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        public string Passwrod { get; set;}

        /// <summary>
        /// 顯示資訊
        /// </summary>
        public string ShowMessage { get; set;}
    }
}
