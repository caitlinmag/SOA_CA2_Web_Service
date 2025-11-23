using APIService.Models;

namespace APIService.DTOs
{
    public class SupplierDTO
    {

        public int SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public string? Location { get; set; }
        public int StockLevel { get; set; }
    }
}
