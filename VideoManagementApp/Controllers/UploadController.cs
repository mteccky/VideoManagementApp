using Microsoft.AspNetCore.Mvc;

namespace VideoManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        public UploadController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost]
        [RequestSizeLimit(200 * 1024 * 1024)]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {           
            if (files == null || files.Count == 0)
            {
                return BadRequest(new { error = "No files received." });
            }

            foreach (var file in files)
            {              
                if (Path.GetExtension(file.FileName).ToLower() != ".mp4")
                {
                    return BadRequest(new { error = "Only MP4 files are allowed." });
                }

                var filePath = Path.Combine(_env.WebRootPath, "media", file.FileName);
                try
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { error = $"An error occurred while saving the file: {ex.Message}" });
                }
            }
            return Ok(new { message = "Files uploaded successfully." });
        }
    }
}
