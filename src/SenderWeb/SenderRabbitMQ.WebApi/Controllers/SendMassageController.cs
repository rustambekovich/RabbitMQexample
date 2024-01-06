using Microsoft.AspNetCore.Mvc;
using SenderRabbitMQ.WebApi.Service.Dto;
using SenderRabbitMQ.WebApi.Service.Interfaces;

namespace SenderRabbitMQ.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SendMassageController : ControllerBase
{
    private IProducerService _service;

    public SendMassageController(IProducerService service)
    {
        _service = service;
    }

    [HttpPost]
    public IActionResult SendMassage([FromForm] Massage massage)
    {
        _service.Send(massage);

        return Ok();
    }
}
