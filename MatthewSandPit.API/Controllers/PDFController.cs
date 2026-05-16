using MatthewSandPit.API.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MatthewSandPit.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PDFController : Controller
    {
        private readonly ILogger<PDFController> _logger;
        private readonly IPDFManager _pdfManager;
        public PDFController(ILogger<PDFController> logger, IPDFManager pdfManager)
        {
            this._logger = logger;
            this._pdfManager = pdfManager;
        }

        [HttpGet(Name = "GetPDF")]
        public IActionResult Get()
        {
            // For demonstration, we'll return a simple PDF file from the wwwroot folder.
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "sample.pdf");
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("PDF file not found.");
            }
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/pdf", "sample.pdf");
        }

        [HttpPost(Name = "MergePDF")]
        public IActionResult MergePDF(List<IFormFile> files)
        {
            if(files == null || files.Count == 0)
            {
                return BadRequest("No files uploaded.");
            }
            else if(files.Count < 2)
            {
                return BadRequest("At least two PDF files are required for merging.");
            }

            byte[] result = this._pdfManager.MergePDFs(files.Select(x => x.OpenReadStream()));

            return File(result, "application/pdf", "merged.pdf");
        }
    }
}
