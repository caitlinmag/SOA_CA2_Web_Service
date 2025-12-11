using APIService.Models;

namespace APIService.Interfaces
{
    public interface ISupplierService
    {
        Task<List<Supplier>> GetAllSuppliers();
        Task<Supplier?> GetSupplierById(string id);
        Task CreateSupplier(Supplier newSupplier);
        Task UpdateSupplier(string id, Supplier updateSupplier);
        Task DeleteSupplier(string id);
    }
}
