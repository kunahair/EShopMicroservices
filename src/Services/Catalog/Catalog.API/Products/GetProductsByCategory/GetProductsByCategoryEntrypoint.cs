namespace Catalog.API.Products.GetProductsByCategory;

//public record GetProductsByCategoryRequest();
public record GetProductsByCategoryResponse(IEnumerable<Product> Products);

public class GetProductsByCategoryEntrypoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
        {
            var results = await sender.Send(new GetProductsByCategoryQuery(category));

            var response = results.Adapt<GetProductsByCategoryResponse>();

            return Results.Ok(response);
        })
        .WithName("GetProductsByCategory")
        .Produces<GetProductsByCategoryResponse>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products by Category")
        .WithDescription("Get Products by Category");
    }
}