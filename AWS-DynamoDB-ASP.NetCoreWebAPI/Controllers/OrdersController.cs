using Amazon.DynamoDBv2.DataModel;
using AWS_DynamoDB_ASP.NetCoreWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AWS_DynamoDB_ASP.NetCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IDynamoDBContext _dynamoDBContext;
        public OrdersController(IDynamoDBContext dynamoDBContext)
        {
            _dynamoDBContext = dynamoDBContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            var orders = await _dynamoDBContext.ScanAsync<Order>(default).GetRemainingAsync();
            return orders;
        }

        [HttpGet("{id}/{barcode}")]
        public async Task<Order> GetOrderByIdAsync(string id, string barcode)
        {
            var order = await _dynamoDBContext.LoadAsync<Order>(id, barcode);
            if (order == null) throw new Exception($"Order with Id {id} Not Found");
            return order;
        }

        [HttpPost]
        public async Task<Order> CreateOrderAsync(Order order)
        {
            var existingOrder = await _dynamoDBContext.LoadAsync<Order>(order.Id, order.Barcode);
            if (existingOrder != null) throw new Exception($"Order with Id {order.Id} Already Exists");
            await _dynamoDBContext.SaveAsync(order);
            return order;
        }

        [HttpPut]
        public async Task<Order> UpdateOrderAsync(Order order)
        {
            var existingOrder = await _dynamoDBContext.LoadAsync<Order>(order.Id, order.Barcode);
            if (existingOrder == null) throw new Exception($"Order with Id {order.Id} Not Found");
            await _dynamoDBContext.SaveAsync(order);
            return order;
        }

        [HttpDelete("{id}/{barcode}")]
        public async Task DeleteOrderAsync(string id, string barcode)
        {
            var order = await _dynamoDBContext.LoadAsync<Order>(id, barcode);
            if (order == null) throw new Exception($"Order with Id {id} Not Found");
            await _dynamoDBContext.DeleteAsync(order);
        }
    }
}
