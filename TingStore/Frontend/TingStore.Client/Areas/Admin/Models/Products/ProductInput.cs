using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TingStore.Client.Areas.Admin.Models.Products
{
    public class ProductInput
    {
        public string Name { get; set; } //
        public List<IFormFile> ProductFile { get; set; } //
        public string Brand { get; set; } //
        public string Model { get; set; }//
        public string Relaease { get; set; } //
        public string type { get; set; } //
        public string Size { get; set; } //
        public string Resolution { get; set; } //
        public string Dimensions { get; set; }
        public string Weight { get; set; }//
        public string Sim { get; set; } //
        public string Os { get; set; } //
        public string Chipset { get; set; } //
        public string ReslaeaseDate { get; set; } //
        public string Description { get; set; } //
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Category { get; set; } //
        public bool IsActive { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, " +
                   $"Brand: {Brand}, " +
                   $"Model: {Model}, " +
                   $"Release: {Relaease}, " +
                   $"Type: {type}, " +
                   $"Size: {Size}, " +
                   $"Resolution: {Resolution}, " +
                   $"Dimensions: {Dimensions}, " +
                   $"Weight: {Weight}, " +
                   $"Sim: {Sim}, " +
                   $"OS: {Os}, " +
                   $"Chipset: {Chipset}, " +
                   $"Release Date: {ReslaeaseDate}, " +
                   $"Description: {Description}, " +
                   $"Price: {Price}, " +
                   $"Stock: {Stock}, " +
                   $"Category : {Category}, " +
                   $"Is Active: {IsActive}, ";
        }

    }
}
