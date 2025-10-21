using Microsoft.AspNetCore.Mvc;
using OnionCartDemo.Application.DTOs;
using OnionCartDemo.Application.Interfaces;

namespace OnionCartDemo.WebApi.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController(IProductApplicationService productService) : ControllerBase
{
    private readonly IProductApplicationService _productService = productService;

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<ProductDto>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var products = await _productService.GetAllAsync(cancellationToken);
        return Ok(products);
    }


    [HttpGet("{id:int}" , Name = "GetProductById")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDto>> GetByIdAsync([FromRoute]int id, CancellationToken cancellationToken)
    {
        var product = await _productService.GetByIdAsync(id, cancellationToken);
        return Ok(product);
    }


    [HttpPost]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductDto>> CreateAsync([FromBody] CreateProductDto dto, CancellationToken cancellationToken)
    {
        var product = await _productService.AddAsync(dto, cancellationToken);

        return CreatedAtRoute("GetProductById", new { id = product.Id }, product);
       
    }

    
    [HttpPut("{id:int}/deactivate")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeactivateAsync(int id, CancellationToken cancellationToken)
    {
        await _productService.DeactivateAsync(id, cancellationToken);
        return NoContent();
    }

}