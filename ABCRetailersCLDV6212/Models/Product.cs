﻿using Azure;
using Azure.Data.Tables;
using System.ComponentModel.DataAnnotations;

namespace ABCRetailersCLDV6212.Models
{
    public class Product : ITableEntity

    {
        public string PartitionKey { get; set; } = "Product";
        public string RowKey { get; set; } = Guid.NewGuid().ToString(); // This will be the ProductID
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        [Display(Name = "Product ID")]
        public string ProductID => RowKey;

        [Required(ErrorMessage = "Product Name is required")]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        [Display(Name = "Description")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        [Display(Name = "Price")]
        public double Price
        {
            get
            {
                return double.TryParse(PriceString, out var result) ? result : 0.0;
            }
            set
            {
                PriceString = value.ToString("F2");
            }
        }

        // backing storage for Price
        public string PriceString { get; set; } = "0.00";


        [Required(ErrorMessage = "Stock availability is required")]
        [Display(Name = "Stock Availability")]
        public int StockAvailable { get; set; }

        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; } = string.Empty;


    }


        }

