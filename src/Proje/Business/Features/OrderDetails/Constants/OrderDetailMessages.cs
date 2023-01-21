namespace Business.Features.OrderDetails.Constants
{
    public static class OrderDetailMessages
    {
        public const string AddedOrderDetail = "added order detail";
        public const string DeletedOrderDetail = "deleted order detail";
        public const string UpdatedOrderDetail = "updated order detail";
        public const string OrderDetailNameAlreadyExists = "order detail name already exists";
        public const string OperationFailed = "operation failed";
        public const string OrderDetailAvaliable = "order detail avaliable";
        public const string OrderDetailNotFound = "order detail not found";
        public const string RequestedProductQuantityIsNotInStock = "requested product quantity is not in stock";
        public const string OrderDetailNumberIsNotUnique = "order detail number is not unique";
        public const string OrderDetailHasAlreadyBeenConfirmed = "order detail has already been confirmed";
        public const string ThereAreProductsInTheCart = "You cannot delete the cart while there are items in the cart.";
        public const string ThereAreNoItemsInTheCart = "There are no items in the cart";
    }
}
