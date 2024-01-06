using SenderRabbitMQ.WebApi.Service.Dto;

namespace SenderRabbitMQ.WebApi.Service.Interfaces;

public interface IProducerService
{
    public void Send(Massage massage);
}
