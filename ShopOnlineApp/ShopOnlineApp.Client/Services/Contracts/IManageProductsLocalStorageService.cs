using ShopOnlineApp.Models.Dtos;

namespace ShopOnlineApp.Client.Services.Contracts
{
    public interface IManageProductsLocalStorageService
    {
        Task<IEnumerable<ProductDto>> GetCollection();
        Task RemoveCollection();
    }
}
