using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Repository.DBConnection;
using Dapper;//Dapper 套件
using System.Data;
using CommonLibary.Model.Common;

namespace Repository.Repository.LineMember
{
    /// <summary>
    /// Line 紀錄的會員資料
    /// </summary>
    public class LineMemberTable
    {
        /// <summary>
        /// 回傳Line帳戶資料表資料
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LineMemberTableModel> GetAllData()
        {
            try
            {
                //連線
                using (IDbConnection cn = new SqlConnection(SqlDb.AzureSqlDb))
                {
                    cn.Open();
                    try
                    {
                        var rowsResult = cn.Query<LineMemberTableModel>
                        (
                            "select * from LineMemberTable"
                        );
                        return rowsResult;
                    }
                    catch (Exception ex)
                    { //發生例外時                    
                        throw ex;//將例外錯誤拋回上層
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;//將例外錯誤拋回上層
            }

        }

        /// <summary>
        /// 紀錄UserID
        /// </summary>
        /// <returns></returns>
        public bool UpdateData(string UserId)
        {
            try
            {
                //連線
                using (IDbConnection cn = new SqlConnection(SqlDb.AzureSqlDb))
                {
                    cn.Open();
                    try
                    {
                        var rowsResult = cn.Execute
                        (
                            @"update LineMemberTable  
                                 set Id = @UserId 
                               where sn =1 
                             ",
                            new { @UserId = UserId }
                        );
                        return true;
                    }
                    catch (Exception ex)
                    { //發生例外時                    
                        throw ex;//將例外錯誤拋回上層
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;//將例外錯誤拋回上層
            }

        }

        /// <summary>
        /// 更新對應帳號的金額
        /// </summary>
        /// <returns></returns>
        public bool UpdateAccountBalance(string UserId , double Balance)
        {
            try
            {
                //連線
                using (IDbConnection cn = new SqlConnection(SqlDb.AzureSqlDb))
                {
                    cn.Open();
                    try
                    {
                        var rowsResult = cn.Execute
                        (
                            @"update LineMemberTable  
                                 set AccountBalance = @balance 
                               where Id = @data
                             ",
                            new { @data = UserId , balance = Balance }
                        );
                        return true;
                    }
                    catch (Exception ex)
                    { //發生例外時                    
                        throw ex;//將例外錯誤拋回上層
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;//將例外錯誤拋回上層
            }

        }
        
        /// <summary>
        /// 更新帳號登入時間戳 
        /// </summary>
        /// <returns></returns>
        public bool UpdateLogin(string UserID)
        {
            try
            {
                //連線
                using (IDbConnection cn = new SqlConnection(SqlDb.AzureSqlDb))
                {
                    cn.Open();
                    try
                    {
                        var rowsResult = cn.Execute
                        (
                            @"update LineMemberTable  
                                 set LastLoginTime = @time
                               where Id = @data
                             ",
                            new { @data = UserID, time = DateTime.Now }
                        );
                        return true;
                    }
                    catch (Exception ex)
                    { //發生例外時                    
                        throw ex;//將例外錯誤拋回上層
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;//將例外錯誤拋回上層
            }

        }

        /// <summary>
        /// 更新帳號登出時間戳 
        /// </summary>
        /// <returns></returns>
        public bool UpdateSignOut(string UserID)
        {
            try
            {
                //連線
                using (IDbConnection cn = new SqlConnection(SqlDb.AzureSqlDb))
                {
                    cn.Open();
                    try
                    {
                        var rowsResult = cn.Execute
                        (
                            @"update LineMemberTable  
                                 set LastLoginTime = @time
                               where Id = @data
                             ",
                            new { @data = UserID, time = new DateTime(1900,1,1) }
                        );
                        return true;
                    }
                    catch (Exception ex)
                    { //發生例外時                    
                        throw ex;//將例外錯誤拋回上層
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;//將例外錯誤拋回上層
            }

        }
    }
}
