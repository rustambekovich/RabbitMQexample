using SenderRabbitMQ.WebApi.Service.Dto;

namespace SenderRabbitMQ.WebApi.Service.Interfaces;

public interface IProducerService
{
    public Task SendAsync(Massage massage);
}
