using Microsoft.AspNetCore.Mvc;
using OnionCartDemo.Application.Commands;
using OnionCartDemo.Application.DTOs;
using OnionCartDemo.Application.Handlers;
using OnionCartDemo.Application.Queries;

namespace OnionCartDemo.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartsController : ControllerBase
{
    private readonly AddItemToCartCommandHandler _addItemToCartCommandHandler;
    private readonly GetCartByIdQueryHandler _getCartByIdQueryHandler;

    public CartsController(AddItemToCartCommandHandler addItemToCartCommandHandler, GetCartByIdQueryHandler getCartByIdQueryHandler)
    {
        _addItemToCartCommandHandler = addItemToCartCommandHandler;
        _getCartByIdQueryHandler = getCartByIdQueryHandler;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<IReadOnlyCollection<CartDto>>> GetById(int id ,CancellationToken cancellationToken)
    {
        var cart = await _getCartByIdQueryHandler.HandleAsync(new GetCartByIdQuery(id), cancellationToken);
        return Ok(cart);
    }

    [HttpPost]
    public async Task<IActionResult> AddItem( [FromBody]AddItemToCartCommand command ,CancellationToken cancellationToken)
    {
        await  _addItemToCartCommandHandler.HandleAsync(command,cancellationToken);

        return Ok(new { message = "Item added successfully" });
    }
}
