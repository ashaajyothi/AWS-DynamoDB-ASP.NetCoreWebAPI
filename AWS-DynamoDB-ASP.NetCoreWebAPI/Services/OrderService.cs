using Amazon.DynamoDBv2.DataModel;
using AWS_DynamoDB_ASP.NetCoreWebAPI.DTO;
using AWS_DynamoDB_ASP.NetCoreWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AWS_DynamoDB_ASP.NetCoreWebAPI.Services
{
    public class OrderService(IDynamoDBContext dynamoDBContext) : IOrderService
    {
        [HttpGet]
        public async Task<Order> CreateOrderAsync(OrderRequest request)
        {
            var order = await dynamoDBContext.LoadAsync<Order>(request.Id);
            if (order != null) throw new Exception($"Product with Id {request.Id} Already Exists");
            var productToCreate = new Order()
            {
                Id = request.Id,
                Name = request.Name,
                Price = request.Price,
            };
            await dynamoDBContext.SaveAsync(productToCreate);
            return productToCreate;
        }

        public async Task DeleteOrderAsync(string id)
        {
            var order = await dynamoDBContext.LoadAsync<Order>(id);
            if (order == null) throw new Exception($"Product with Id {id} Not Found");
            await dynamoDBContext.DeleteAsync(order);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            var products = await dynamoDBContext.ScanAsync<Order>(default).GetRemainingAsync();
            return products;
        }

        [HttpGet("{id}/{barcode}")]
        public async Task<Order> GetOrderByIdAsync(string id)
        {
            var order = await dynamoDBContext.LoadAsync<Order>(id);
            if (order == null) throw new Exception($"Product with Id {id} Not Found");
            return order;
        }

        public async Task<Order> UpdateOrderAsync(OrderRequest request)
        {
            var order = await dynamoDBContext.LoadAsync<Order>(request.Id);
            if (order == null) throw new Exception($"Product with Id {request.Id} Not Found");
            var productToUpdate = new Order()
            {
                Id = request.Id,
                Name = request.Name,
                Price = request.Price,
            };
            await dynamoDBContext.SaveAsync(productToUpdate);
            return productToUpdate;
        }

    }

}
