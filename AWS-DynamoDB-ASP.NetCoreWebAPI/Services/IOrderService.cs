using AWS_DynamoDB_ASP.NetCoreWebAPI.DTO;
using AWS_DynamoDB_ASP.NetCoreWebAPI.Models;

namespace AWS_DynamoDB_ASP.NetCoreWebAPI.Services
{
    public interface IOrderService
    {
        public Task<Order> CreateOrderAsync(OrderRequest request);
        public Task<Order> GetOrderByIdAsync(string id);
        public Task<IEnumerable<Order>> GetAllOrdersAsync();
        public Task DeleteOrderAsync(string id);
        public Task<Order> UpdateOrderAsync(OrderRequest request);
    }
}
