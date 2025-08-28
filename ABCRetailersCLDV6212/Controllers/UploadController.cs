using Microsoft.AspNetCore.Mvc;
using ABCRetailersCLDV6212.Models;
using ABCRetailersCLDV6212.Services;

namespace ABCRetailersCLDV6212.Controllers
{
    public class UploadController : Controller
    {
        private readonly IAzureStorageService _storageService;

        public UploadController(IAzureStorageService storageService)
        {
            _storageService = storageService;
        }
        public IActionResult Index()
        {
            return View(new FileUploadModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(FileUploadModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.ProofOfPayment != null && model.ProofOfPayment.Length > 0)
                    { //uploading to blob
                        var fileName = await _storageService.UploadImageAsync(model.ProofOfPayment, "payment-proofs");

                        await _storageService.UploadToFileShareAsync(model.ProofOfPayment, "contracts", "payments");

                        TempData["Success"] = $"File '{fileName}' uploaded successfully.";

                        return View(new FileUploadModel());
                    }
                    else
                    {
                        ModelState.AddModelError("Proof of Payment", "Please select a file to upload.");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred while uploading the file: {ex.Message}");
                }
            }  return View(model);
        }
    }
}
