using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibary.Model.Common
{
    /// <summary>
    /// 資料表: Line機器人的連絡對象
    /// </summary>
    public class LineMemberTableModel
    {
        /// <summary>
        /// 流水號
        /// </summary>
        public int Sn { get; set; }

        /// <summary>
        /// 唯一碼
        /// </summary>
        public string Id { get; set;}

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set;}

        /// <summary>
        /// 最後登入時間戳
        /// </summary>
        public DateTime LastLoginTime { get; set; }

        /// <summary>
        /// 帳號餘額
        /// </summary>
        public double AccountBalance { get; set;}


        /// <summary>
        /// 用戶帳號
        /// </summary>
        public string Account { get; set;}

        /// <summary>
        /// 用戶密碼
        /// </summary>
        public string Password { get; set;}
    }
}
