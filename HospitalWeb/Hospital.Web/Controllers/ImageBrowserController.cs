using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Web.Controllers
{
    public class ImageBrowserController : Controller
    {
        private string _destination = @"C:\ProjectAT\ImageWebBenhVien\images\UploadFiles";

        [Route("ImageBrowser/GetImage/{*id}")]
        [Route("images/UploadFiles/{*id}")]
        [Route("{temp}/{temp1}/images/UploadFiles/{*id}")]
        [Route("{temp}/{temp1}/ImageBrowser/GetImage/{*id}")]
        [Route("{temp}/images/UploadFiles/{*id}")]
        [Route("{temp}/ImageBrowser/GetImage/{*id}")]
        public IActionResult GetImage(string id, string temp, string temp1)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }

            var file = System.IO.Path.Combine(_destination, id);

            var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(file, out contentType))
            {
                contentType = "application/octet-stream";
            }

            if (System.IO.File.Exists(file))
            {
                return File(new System.IO.FileStream(file, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read), contentType);
            }

            return null;
        }
    }
}