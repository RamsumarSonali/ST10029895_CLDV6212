using Microsoft.AspNetCore.Mvc;
using ABCRetailersCLDV6212.Models;
using ABCRetailersCLDV6212.Services;

namespace ABCRetailersCLDV6212.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IAzureStorageService _storageService;
        //private readonly ILogger<CustomerController> _logger;

        public CustomerController(IAzureStorageService storageService)
        {
            _storageService = storageService;
           // _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var customer = await _storageService.GetAllEntitiesAsync<Customer>();
            return View(customer);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    await _storageService.AddEntityAsync(customer);
                    TempData["Success"] = $"Customer '{customer.CustomerFirstName}' created successfully.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // _logger.LogError(ex, "Error creating customer");
                    ModelState.AddModelError("", $"Error creating customer: {ex.Message}");
                }
            }

            return View(customer);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var customer = await _storageService.GetEntityAsync<Customer>("Customer", id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Get the original entity
                    var originalCustomer = await _storageService.GetEntityAsync<Customer>("Customer", customer.RowKey);
                    if (originalCustomer == null)
                    {
                        return NotFound();
                    }

                    // Update fields
                    originalCustomer.CustomerFirstName = customer.CustomerFirstName;
                    originalCustomer.CustomerSurname = customer.CustomerSurname;
                    originalCustomer.CustomerEmail = customer.CustomerEmail;
                    originalCustomer.CustomerUsername = customer.CustomerUsername;
                    originalCustomer.CustomerShippingAddress = customer.CustomerShippingAddress;

                    // Update the Azure table
                    await _storageService.UpdateEntityAsync(originalCustomer);
                    TempData["Success"] = "Customer updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error updating customer: {ex.Message}");
                }
            }

            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _storageService.DeleteEntityAsync<Customer>("customer", id);
                TempData["Success"] = "Customer deleted successfully.";
            }
            catch (Exception ex)
            {
                //  _logger.LogError(ex, "Error deleting product");
                TempData["Error"] = $"Error deleting customer: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}