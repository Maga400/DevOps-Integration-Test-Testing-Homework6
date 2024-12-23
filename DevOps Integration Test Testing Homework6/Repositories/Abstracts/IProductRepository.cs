using Entities;

namespace DevOps_Integration_Test_Testing_Homework6.Repositories.Abstracts
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product>? GetByIdAsync(int id);
        Task<Product>? AddAsync(Product product);
        Task<Product>? UpdateAsync(Product product);
        Task<bool> DeleteAsync(int id);
    }
}
