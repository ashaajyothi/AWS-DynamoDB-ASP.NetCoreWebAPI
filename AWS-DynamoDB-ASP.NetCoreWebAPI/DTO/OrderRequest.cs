namespace AWS_DynamoDB_ASP.NetCoreWebAPI.DTO
{
    public record OrderRequest(string Id, string Barcode, string Name, string Description, decimal Price);
    
}
