using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VideoManagementApp.Models;

namespace VideoManagementApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _env;
        public HomeController(IWebHostEnvironment env)
        {
            _env = env;
        }

        public IActionResult Index()
        {
            var mediaPath = Path.Combine(_env.WebRootPath, "media");
            var files = Directory.GetFiles(mediaPath, "*.mp4")
                .Select(f => new VideoFile
                {
                    FileName = Path.GetFileName(f),
                    FileSize = new FileInfo(f).Length
                }).ToList();

            return View(files);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
