
using System.Text.Json;
using apireceive.Producers.Interfaces;
using RabbitMQ.Client;

namespace apireceive.Producers;


public class HabbitMQ: IMessageProducer{

    private ConnectionFactory _connectionFactory = null;


    public HabbitMQ(){
        if (_connectionFactory == null){
            _connectionFactory = new ConnectionFactory() { HostName = Environment.GetEnvironmentVariable("HabbitIp") };
        }
    }

    public async Task SendMessage<T>(T message)
    {
        //RetConnectionFactory();
        using(var connection = _connectionFactory.CreateConnection())
        using(var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: Environment.GetEnvironmentVariable("HabbitTopicProcess"),
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var body = JsonSerializer.SerializeToUtf8Bytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: Environment.GetEnvironmentVariable("HabbitTopicProcess"),
                                 basicProperties: null,
                                 body: body);
        }

    }
}