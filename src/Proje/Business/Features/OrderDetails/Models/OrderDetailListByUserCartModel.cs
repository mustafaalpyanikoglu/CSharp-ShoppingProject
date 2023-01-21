using Business.Features.OrderDetails.Dtos;
using Core.Persistence.Paging;

namespace Business.Features.OrderDetails.Models
{
    public class OrderDetailListByUserCartModel : BasePageableModel
    {
        public IList<OrderDetailListDtoForCustomer> Items { get; set; }
        public float AmountOfPayment { get; set; }
    }
}
