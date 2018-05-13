using Service.LineBot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace LineLoginExample.Controllers
{
    public class LineBotAPIController : ApiController
    {
        //機器人Token : 仙草奶綠
        string MyLineChannelAccessToken = "o5a3d78ewLIaqKlYfmkaO9WxCtHCKiwOiKJCHiWWX1QbLPtuRgFz0Tt1JA5quL8BfiP6RlpfoVw5nklKPLelxNOScHR3aRyHcpRCkW5YumSuZi5Evuw5Uv7Js3/d9zAyLWrPLKOGNlUBMaC7gU2+ZAdB04t89/1O/w1cDnyilFU=";
        /// <summary>
        /// Line機器人回覆API
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/LineBotApi/post")]
        public async void Post()
        {
            try
            {
                //取得 http Post RawData(should be JSON)
                string postData = Request.Content.ReadAsStringAsync().Result;
                //剖析JSON
                var ReceivedMessage = isRock.LineBot.Utility.Parsing(postData);
                var bot = new isRock.LineBot.Bot(this.MyLineChannelAccessToken);
                //解析使用者傳給Bot的文字訊息
                string userCommandString = ReceivedMessage.events[0].message.text;
                //Service工作
                LineBotMember _service = new LineBotMember();
                //Return Message
                string message = string.Empty;

                //字串為save表示紀錄 ※實際應用應該偷偷記錄User ID ，這邊是範例
                if (userCommandString == "save")
                {
                    //測試方法: 紀錄
                    _service.InsertID(ReceivedMessage.events[0].source.userId);

                    //回覆API
                    var call = Task.Run(() =>
                    {
                        bot.ReplyMessage(ReceivedMessage.events[0].replyToken,
                            string.Format("紀錄成功: {0}", ReceivedMessage.events[0].source.userId));
                    });
                }
                else if (userCommandString.Contains("取得用戶金額"))//1.取得帳號金額
                {
                    var result = _service.GetAccountBalance(ReceivedMessage.events[0].source.userId);

                    //取得更新後的錢
                    message = ( -1 == result) ? "登入失敗，請先登入帳戶" : string.Format("用戶當前金額: {0}", result);

                    //回覆API
                    var call = Task.Run(() =>
                    {
                        bot.ReplyMessage(
                          ReceivedMessage.events[0].replyToken,
                          message);
                    });
                }
                else if (userCommandString.Contains("更新金額"))//功能2 : 更新金額
                {
                    string get= userCommandString.Substring(4, userCommandString.Length-4);
                    try
                    {
                        double money = double.Parse(get);
                        //更新金額
                        var isSuccess = _service.UpdateAccountBalance(ReceivedMessage.events[0].source.userId, money);
                      
                        //取得更新後的錢
                        var result = _service.GetAccountBalance(ReceivedMessage.events[0].source.userId);

                        message =( false== isSuccess )? "登入失敗，請先登入帳戶" : string.Format("更新成功，當前金額: {0}", result);

                        //回覆API
                        var call = Task.Run(() =>
                        {
                            bot.ReplyMessage(ReceivedMessage.events[0].replyToken,
                                message);
                        });
                    }
                    catch (Exception ex)
                    {
                        //回覆API
                        var call = Task.Run(() =>
                        {
                            bot.ReplyMessage(ReceivedMessage.events[0].replyToken,
                                string.Format("資料格式錯誤"));
                        });
                    } 
                 
                }
                else if (userCommandString == "登出")// 功能4: 登出
                {
                    _service.UpdateSingOut(ReceivedMessage.events[0].source.userId);

                    //回覆API
                    var call = Task.Run(() =>
                    {
                        bot.ReplyMessage(ReceivedMessage.events[0].replyToken,
                            string.Format("登出成功: {0}", DateTime.Now));
                    });
                }


                }
            catch (Exception ex)
            {
                //取得 http Post RawData(should be JSON)
                string postData = Request.Content.ReadAsStringAsync().Result;
                //剖析JSON
                var ReceivedMessage = isRock.LineBot.Utility.Parsing(postData);
                var bot = new isRock.LineBot.Bot(this.MyLineChannelAccessToken);

                //回覆API
                var call = Task.Run(() =>
                {
                    bot.ReplyMessage(ReceivedMessage.events[0].replyToken,
                        string.Format("錯誤資訊:{0}", ex.Message));
                });
            }
        }


    }
}