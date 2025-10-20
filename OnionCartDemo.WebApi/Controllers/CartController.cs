using Microsoft.AspNetCore.Mvc;
using OnionCartDemo.Application.DTOs;
using OnionCartDemo.Application.Services;
using System.Threading;

namespace OnionCartDemo.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController(CartApplicationService cartApplicationService)
    : ControllerBase
{
    private readonly CartApplicationService _cartApplicationService = cartApplicationService;


    [HttpGet("{id:int}")]
    public async Task<ActionResult<IReadOnlyCollection<CartDto>>> GetById(int id ,CancellationToken cancellationToken)
    {
        var cart = await _cartApplicationService.GetCart(id,cancellationToken);
        return Ok(cart);
    }


    [HttpPost]
    public async Task<IActionResult> AddItem( [FromBody]AddItemToCartDto dto ,CancellationToken cancellationToken)
    {
        await  _cartApplicationService.AddItemAsync(dto,cancellationToken);

        return Ok(new { message = "Item added successfully" });
    }


    [HttpDelete("{cartId}/Items/{cartItemId}")]
    public async Task<IActionResult> RemoveAsync(int cartId,int cartItemId ,CancellationToken cancellationToken)
    {
        await _cartApplicationService.RemoveItemAsync(cartId,cartItemId ,cancellationToken);
        return NoContent();
    }


    [HttpDelete("{cartId}/items")]
    public async Task<IActionResult> ClearAsync(int cartId ,CancellationToken cancellationToken)
    {
        await _cartApplicationService.ClearCartAsync(cartId,cancellationToken);
        return NoContent();
    }

    [HttpDelete("{cartId}")]
    public async Task<IActionResult> DeleteAsync(int cartId, CancellationToken cancellationToken)
    {
        await _cartApplicationService.DeleteCartAsync(cartId, cancellationToken);
        return NoContent();
    }

    [HttpPut("{cartId}/items/{cartItemId}/quantity")]
    public async Task<IActionResult> UpdateItemQuantity(int cartId,int cartItemId, [FromBody] int newQuantity,CancellationToken cancellationToken)
    {
        await _cartApplicationService.UpdateQuantityAsync(cartId, cartItemId, newQuantity, cancellationToken);
        return NoContent();
    }

}
