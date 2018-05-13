using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibary.Model.ViewModel.LineLoginExample
{
    public class SendMessageViewModel
    {
        /// <summary>
        /// Line 的用戶ID
        /// </summary>
        public string userId { get; set; }

        /// <summary>
        /// Line 的傳遞訊息
        /// </summary>
        public string SendMessage { get; set; }

    }
}
