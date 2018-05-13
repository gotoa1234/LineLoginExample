using CommonLibary.Model.ViewModel.LineLoginExample;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace LineLoginExample.Controllers
{
    public class SendMessageController: Controller
    {

        /// <summary>
        /// 發送按鈕
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index(string botUserId)
        {
            SendMessageViewModel resultViewModel = new SendMessageViewModel();

            //如果使用者有正確輸入 UserId才進行下列工作
            if (false == string.IsNullOrWhiteSpace(botUserId))
            {
                //回覆API
                LineMessagingWorking Service = new LineMessagingWorking();
                var result = await Service.PushMessage(botUserId, string.Format("現在時間:{0} 被機器人呼叫了", DateTime.Now));

                //回傳前端的資訊
                resultViewModel.userId = botUserId;
                resultViewModel.SendMessage = result.ToString();
            }

            return View(resultViewModel);
        }

        /// <summary>
        /// 工作類別
        /// </summary>
        public class LineMessagingWorking
        {
            //機器人Token : 仙草奶綠
            private readonly string MyToken = "o5a3d78ewLIaqKlYfmkaO9WxCtHCKiwOiKJCHiWWX1QbLPtuRgFz0Tt1JA5quL8BfiP6RlpfoVw5nklKPLelxNOScHR3aRyHcpRCkW5YumSuZi5Evuw5Uv7Js3/d9zAyLWrPLKOGNlUBMaC7gU2+ZAdB04t89/1O/w1cDnyilFU=";
            //API位置
            private readonly string PushMessageUrl = "https://api.line.me/v2/bot/message/push";
            //使用Post、Get等Http方法
            private HttpClient Client = new HttpClient();

            /// <summary>
            /// 建構式
            /// </summary>
            public LineMessagingWorking()
            {
                //ContentType
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //驗證Header
                Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {this.MyToken}");
            }

            /// <summary>
            /// <para>Line 傳送純文字的API</para>
            /// <para>的API官方文件說明: https://developers.line.me/en/docs/messaging-api/reference/#send-push-message</para>
            /// </summary>
            /// <param name="userId">※如果是群組隊向就是GroupId ，本範例以單一個人用戶為例</param>
            /// <param name="sendMessage">文本發送方法</param>
            /// <returns></returns>
            public async Task<HttpStatusCode> PushMessage(string userId, string sendMessage)
            {
                try
                {
                    var post = await Client.PostAsJsonAsync(PushMessageUrl, new {
                           to = userId,
                           messages = new[] {
                            new {
                                type="text",//指定傳送的型態 text =>文本
                                text =sendMessage,
                                }
                            }
                       });

                    return post.StatusCode;
                }
                catch(Exception x)
                {
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(x.Message) });
                }
            }
        }
    }
}