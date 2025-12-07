using APIService.Models;
using MongoDB.Driver;

namespace APIService.Services
{
    public class SupplierService
    {

        private readonly IMongoCollection<Supplier> _supplierCollection;

        public SupplierService(IMongoCollection<Supplier> supplierCollection)
        {
            _supplierCollection = supplierCollection;
        }

        public async Task<List<Supplier>> GetAllSuppliers() =>
            await _supplierCollection.Find(_ => true).ToListAsync();

        public async Task<Supplier?> GetSupplierById(string id) =>
            await _supplierCollection.Find(supplier => supplier.SupplierId == id).FirstOrDefaultAsync();

        public async Task CreateSupplier(Supplier newSupplier) =>
            await _supplierCollection.InsertOneAsync(newSupplier);

        public async Task UpdateSupplier(string id, Supplier updateSupplier) =>
            await _supplierCollection.ReplaceOneAsync(updateSupplier => updateSupplier.SupplierId == id, updateSupplier);

        public async Task DeleteSupplier(string id) =>
            await _supplierCollection.DeleteOneAsync(supplier => supplier.SupplierId == id);
    }
}
