using Azure;
using Azure.Data.Tables;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Runtime.ExceptionServices;

namespace ABCRetailersCLDV6212.Models
{
    public class Customer : ITableEntity

    {
        public string PartitionKey { get; set; } = "Customer";
        public string RowKey { get; set; } = Guid.NewGuid().ToString(); // This will be the ProductID
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        [Display(Name = "Customer ID")]
        public string CustomerID => RowKey;

        [Required]
        [Display(Name = "First Name")]
        public string CustomerFirstName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Surname")]
        public string CustomerSurname { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Username")]
        public string CustomerUsername { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string CustomerEmail { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Shipping Address")]
        public string CustomerShippingAddress { get; set; } = string.Empty;
    }
}

