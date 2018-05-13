using Repository.Repository.LineMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Pattern.Factory
{
    /// <summary>
    /// 讀取資料庫工廠
    /// </summary>
    public static class RepostioryFactory
    {
        public static readonly LineMemberTable _LineMember;

        static RepostioryFactory()
        {
            _LineMember = new LineMemberTable();

        }

        /// <summary>
        /// Line機器人加入的會員資料表
        /// </summary>
        public static LineMemberTable LineMemberRepository { get { return _LineMember; } }


    }
}