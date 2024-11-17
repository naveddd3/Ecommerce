using Domain.Entities;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        Task<Response> PlaceOrder(CheckoutDetails checkoutDetails);

        Task<IEnumerable<OrderDetails>> GetAllOrders();
    }
}
