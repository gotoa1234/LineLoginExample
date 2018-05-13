using CommonLibary.Model.Common;
using Repository.Repository.LineMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.LineBot
{
    /// <summary>
    /// Line 好友 機器人串接資料
    /// </summary>
    public class LineBotMember
    {
        /// <summary>
        /// 功能1 : 取得帳號金額
        /// </summary>
        /// <returns></returns>
        public double GetAccountBalance(string userID)
        {
            double result = 0;
            var allDatas = Repository.Pattern.Factory.RepostioryFactory.LineMemberRepository.GetAllData();
            //取得餘額
            if (true == IsVerification(userID))
            {
                result = allDatas.Where(o => o.Id == userID).FirstOrDefault().AccountBalance;
            }
            else
            {
                result = -1;
            }
            return result;
        }

       
        /// <summary>
        /// 功能2 : 更新金額
        /// </summary>
        /// <param name="usertoken"></param>
        /// <returns></returns>
        public bool UpdateAccountBalance(string userID , double balance)
        {
            bool result = false;
            var allDatas = Repository.Pattern.Factory.RepostioryFactory.LineMemberRepository.GetAllData();
            //更新餘額
            if (true == IsVerification(userID))
            {
                //更新餘額
                result=  Repository.Pattern.Factory.RepostioryFactory.LineMemberRepository.UpdateAccountBalance(userID, balance);
               
            }

            return result;
        }

        /// <summary>
        /// 功能3 : 登入
        /// </summary>
        /// <param name="usertoken"></param>
        /// <returns></returns>
        public bool UpdateLogin(string account ,string password )
        {
            bool result = false;
            var allDatas = Repository.Pattern.Factory.RepostioryFactory.LineMemberRepository.GetAllData();
            var getNowUser = allDatas.Where(o => o.Account == account && o.Password == password).FirstOrDefault();
            //判斷是否有正確帳號
            if (null != getNowUser)
            {
                //更新帳號時間戳-視為登入
                result = Repository.Pattern.Factory.RepostioryFactory.LineMemberRepository.UpdateLogin(getNowUser.Id);
            }

            return result;
        }

        /// <summary>
        /// 功能4 : 登出
        /// </summary>
        /// <param name="usertoken"></param>
        /// <returns></returns>
        public bool UpdateSingOut(string userID)
        {
            bool result = false;
            var allDatas = Repository.Pattern.Factory.RepostioryFactory.LineMemberRepository.GetAllData();
            //登出，時間戳設為過期時間
            if (true == IsVerification(userID))
            {
                result = Repository.Pattern.Factory.RepostioryFactory.LineMemberRepository.UpdateSignOut(userID);
            }

            return result;
        }

        /// <summary>
        /// 回傳所有帳號內的資料
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LineMemberTableModel> GetAllUserData()
        {
            return Repository.Pattern.Factory.RepostioryFactory.LineMemberRepository.GetAllData();
        }

        /// <summary>
        /// 測試方法 : 紀錄使用者ID
        /// </summary>
        /// <param name="usertoken"></param>
        /// <returns></returns>
        public bool InsertID(string userID)
        {
            bool result = false;

            //紀錄使用者ID
            result = Repository.Pattern.Factory.RepostioryFactory.LineMemberRepository.UpdateData(userID);

          
            return result;
        }


        /// <summary>
        /// 驗證時間戳是否正確
        /// </summary>
        /// <returns></returns>
        private bool IsVerification(string userID)
        {
            bool result = false;
            var allDatas = Repository.Pattern.Factory.RepostioryFactory.LineMemberRepository.GetAllData();
            //判斷時間戳是否有效
            if (allDatas.Where(o => o.Id == userID).FirstOrDefault() != null)
            {
                DateTime dt1 = allDatas.Where(o => o.Id == userID).FirstOrDefault().LastLoginTime;
                DateTime dt2 = DateTime.Now;
                TimeSpan ts = dt2 - dt1;
                //有效時間戳為60秒，60秒內都視為登入狀態
                if (ts.TotalSeconds <= 60)
                    result = true;
            }

            return result;
        }

        
    }
}
