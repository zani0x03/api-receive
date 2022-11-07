namespace apireceive.Producers.Interfaces;


public interface IMessageProducer
{
    Task SendMessage<T> (T message);
}