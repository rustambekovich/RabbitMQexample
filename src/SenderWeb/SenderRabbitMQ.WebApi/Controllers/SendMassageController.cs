using Microsoft.AspNetCore.Mvc;

namespace SenderRabbitMQ.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SendMassageController : ControllerBase
{
    public SendMassageController()
    {
        
    }

    [HttpPost]
    public IActionResult SendMassage(string massage)
        => Ok();
}
