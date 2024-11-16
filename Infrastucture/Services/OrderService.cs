using Application.Interfaces;
using Dapper;
using Domain.Entities;
using Domain.Enum;
using Domain.Helper;
using System.Data;

namespace Infrastucture.Services
{
    public class OrderService : IOrderService
    {
        private readonly IDapperRepository _dapper;
        public OrderService(IDapperRepository dapper)
        {
            _dapper = dapper;
        }
        public async Task<Response> PlaceOrder(CheckoutDetails checkoutDetails)
        {
            var response = new Response()
            {
                ResponseText = "Unable to place order !",
                StatusCode = ResponseStatus.Failed
            };
            try
            {
                if (checkoutDetails.AddressID == 0)
                {
                    response.ResponseText = "Please Select Address";
                    return response;
                }
                else if (checkoutDetails.PaymentMode == 0 || checkoutDetails.PaymentMode == null)
                {
                    response.ResponseText = "Invalid Payment Mode";
                    return response;
                }
                else if (checkoutDetails.Cart == null || checkoutDetails.Cart.Items.Count() == 0)
                {
                    response.ResponseText = "Please Select Item";
                    return response;
                }
                var orderRequest = new List<OrderRequest>();
                string OrderID = Guid.NewGuid().ToString().ToUpper();
                foreach (var item in checkoutDetails.Cart.Items.Values)
                {
                    var product = item.Product;
                    orderRequest.Add(new OrderRequest
                    {
                        OrderID = OrderID,
                        ProductID = Convert.ToInt32(product.Product_Id),
                        UserId = 1,
                        Name = product.Name,
                        Unit = product.Unit,
                        Price = product.Price,
                        VarientID = product.VarientId,
                        Quantity = item.Quantity,
                        AddressID = checkoutDetails.AddressID,
                        PaymentModes = checkoutDetails.PaymentMode,
                        OrderStatus = OrderStatus.PLACED,
                        StatusRemark = (string?)null,

                    });
                }
                var orderTable = ConvertOrderRequestToDataTable(orderRequest);
                var parameters = new DynamicParameters();
                parameters.Add("@Orders", orderTable.AsTableValuedParameter("[dbo].[OrderType]"));
                var effectedRows = await _dapper.ExecuteAsync("Proc_SaveOrders", parameters);
                if (effectedRows > 1)
                {
                    response.ResponseText = "Order Placed";
                    response.StatusCode = ResponseStatus.Success;
                    return response;
                }
                return response;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        private DataTable ConvertOrderRequestToDataTable(List<OrderRequest> orderRequest)
        {
            var table = new DataTable();
            table.Columns.Add("OrderID", typeof(string));
            table.Columns.Add("ProductID", typeof(int));
            table.Columns.Add("UserId", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Unit", typeof(string));
            table.Columns.Add("Price", typeof(decimal));
            table.Columns.Add("VarientID", typeof(string));
            table.Columns.Add("Quantity", typeof(int));
            table.Columns.Add("AddressID", typeof(int));
            table.Columns.Add("PaymentModes", typeof(string));
            table.Columns.Add("OrderStatus", typeof(string));
            table.Columns.Add("StatusRemark", typeof(string));

            foreach (var item in orderRequest)
            {
                table.Rows.Add(
                    item.OrderID,
                    item.ProductID,
                    item.UserId,
                    item.Name,
                    item.Unit,
                    item.Price,
                    item.VarientID,
                    item.Quantity,
                    item.AddressID,
                    item.PaymentModes,
                    item.OrderStatus,
                    item.StatusRemark
                );
            }

            return table;
        }

    }
}
