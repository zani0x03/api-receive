using apireceive.Models;
using apireceive.Producers.Interfaces;
using apireceive.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace api_receive.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsOrderController : ControllerBase
{
    private readonly ILogger<ProductsOrderController> _logger;
    private readonly IOrderRepository _orderRepository;
    private readonly IMessageProducer _messageProducer;

    public ProductsOrderController(
        ILogger<ProductsOrderController> logger,
        IOrderRepository orderRepository,
        IMessageProducer messageProducer
    )
    {
        _logger = logger;
        _orderRepository = orderRepository;
        _messageProducer = messageProducer;
    }

    [HttpPost]
    [Route("receiveorder")]
    public async Task<ActionResult> ReceiveOrder([FromBody]Order order){
        try{
            order.InitialDate = DateTime.UtcNow;
            order.Id = ObjectId.GenerateNewId();
            await _orderRepository.OrderInsert(order);
            await _messageProducer.SendMessage(order.Id.ToString());
            return Accepted();   
        }catch(Exception ex){
            _logger.LogError(ex, "General error");
            return StatusCode(500);     
        }
    }   

}