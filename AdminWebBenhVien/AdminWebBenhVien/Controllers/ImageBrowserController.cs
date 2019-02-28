using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace AdminWebBenhVien.Controllers
{
    public class ImageBrowserController : EditorImageBrowserController
    {
        private const string contentFolderRoot = "shared/";
        private const string folderName = "Images/";
        private static readonly string[] foldersToCopy = new[] { "shared/images/employees" };

        /// <summary>
        /// Gets the base paths from which content will be served.
        /// </summary>
        public override string ContentPath
        {
            get
            {
                return CreateUserFolder();
            }
        }

        public ImageBrowserController(IHostingEnvironment hostingEnvironment)
            : base(hostingEnvironment)
        {
        }

        [Route("ImageBrowser/GetImage/{id}")]
        [Route("images/UploadFiles/{id}")]
        [Route("{temp}/images/UploadFiles/{id}")]
        [Route("{temp}/ImageBrowser/GetImage/{id}")]
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
        

        private string _destination = @"D:\ImageWebBenhVien\images\UploadFiles";
        private string CreateUserFolder()
        {
            if (!System.IO.Directory.Exists(_destination))
            {
                System.IO.Directory.CreateDirectory(_destination);
            }
            return _destination;

            //var virtualPath = Path.Combine(contentFolderRoot, "UserFiles", folderName);
            //var path = HostingEnvironment.WebRootFileProvider.GetFileInfo(virtualPath).PhysicalPath;

            //if (!Directory.Exists(path))
            //{
            //    Directory.CreateDirectory(path);
            //    foreach (var sourceFolder in foldersToCopy)
            //    {
            //        CopyFolder(HostingEnvironment.WebRootFileProvider.GetFileInfo(sourceFolder).PhysicalPath, path);
            //    }
            //}
            //return virtualPath;
        }

        private void CopyFolder(string source, string destination)
        {
            if (!System.IO.Directory.Exists(destination))
            {
                System.IO.Directory.CreateDirectory(destination);
            }

            foreach (var file in System.IO.Directory.EnumerateFiles(source))
            {
                var dest = System.IO.Path.Combine(destination, System.IO.Path.GetFileName(file));
                System.IO.File.Copy(file, dest);
            }

            foreach (var folder in System.IO.Directory.EnumerateDirectories(source))
            {
                var dest = System.IO.Path.Combine(destination, System.IO.Path.GetFileName(folder));
                CopyFolder(folder, dest);
            }
        }

        public override ActionResult Upload(string path, IFormFile file)
        {
            return base.Upload(path, file);
        }
    }
}