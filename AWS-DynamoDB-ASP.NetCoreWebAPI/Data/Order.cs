using Amazon.DynamoDBv2.DataModel;

namespace AWS_DynamoDB_ASP.NetCoreWebAPI.Models
{
    [DynamoDBTable("orders")]
    public class Order
    {
        [DynamoDBHashKey("id")]
        public string? Id { get; set; }
        [DynamoDBRangeKey("barcode")]
        public string? Barcode { get; set; }
        [DynamoDBProperty("name")]
        public string? Name { get; set; }
        [DynamoDBProperty("description")]
        public string? Description { get; set; }
        [DynamoDBProperty("price")]
        public decimal Price { get; set; }
    }
}
