using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Web.Controllers
{
    public class ImageBrowserController : Controller
    {
        private string _destination = @"D:\ImageWebBenhVien\ImageWebBenhVien\images\UploadFiles";

        [Route("{temp}/images/UploadFiles/{id}")]
        [Route("images/UploadFiles/{id}")]
        public IActionResult GetImage(string id, string temp)
        {
            var file = System.IO.Path.Combine(_destination, id);

            var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(file, out contentType))
            {
                contentType = "application/octet-stream";
            }

            return File(new System.IO.FileStream(file, System.IO.FileMode.Open), contentType);
        }
    }
}