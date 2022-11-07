using apireceive.Models;

namespace apireceive.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task OrderInsert(Order order);
    }
}