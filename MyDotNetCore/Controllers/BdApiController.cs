using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyDotNetCore.Data;
using MyDotNetCore.Models;
using MyDotNetCore.Services;
using Newtonsoft.Json;
using MyDotNetCore.Models.BdViewModels;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace MyDotNetCore.Controllers
{
    public class BdApiController : Controller
    {
        public DataContext _DbContext { get; set; }
        //public string TokenRes = AccessToken.getAccessToken();
        public AccessToken _accessToken;

        public string Token
        {
            get
            {
                return _accessToken.TOKEN;
            }
        }
        public BdApiController(DataContext context, AccessToken accessToken)
        {
            _DbContext = context;
            _accessToken = accessToken;
        }
        public IActionResult Index()
        {
            return Json(new
            {
                token = Token
            });
        }
        [HttpPost]
        //[Route()]
        public IActionResult FaceDetect(string returnUrl = null)
        {
            //if (ModelState.IsValid)
            //{
            //    return Json(new
            //    {
            //        code = "0"
            //    });
            //}
            Stream s = Request.Body;
            string text = "";
            using (StreamReader reader = new StreamReader(s))
            {
                text = reader.ReadToEnd();
                BdTokenDetectViewModels m = JsonConvert.DeserializeObject<BdTokenDetectViewModels>(text);
                IFormCollection f = Request.Form;
                // f.Files.GetFile("file").
                IFormFileCollection files = f.Files;
                Stream sf = files.GetFile("file").OpenReadStream();
               byte[] byts = new byte[sf.Length];
                sf.Read(byts, 0, byts.Length);
                sf.Seek(0, SeekOrigin.Begin);
                var base64 = Convert.ToBase64String(byts);
                List<KeyValuePair<string, string>> dataList = new List<KeyValuePair<string, string>>
                {
                   new KeyValuePair<string, string>("image",base64)
                };
               // string d=_accessToken.GetFaceDetect(dataList);
                return Json(new
                {
                    code = "0",
                    msg = "成功",
                    returnUrl,
                    m,
                    text,
                    f.Count,
                    filecount = f.Files.Count,
                    dataList
                   
                });
            }

        }
        /// <summary>
        /// 图片 转为    base64编码的文本
        /// </summary>
        /// <param name="bmp">待转的Bitmap</param>
        /// <returns>转换后的base64字符串</returns>

    }
}