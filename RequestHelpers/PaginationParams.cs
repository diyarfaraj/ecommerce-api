namespace ecommerceApi.RequestHelpers
{
    public class PaginationParams
    {
        private const int MaxProductsPerPage = 50;
        public int PageNumber { get; set; } = 1;

        private int _productsPerPage = 6;
        public int ProductsPerPage
        {
            get => _productsPerPage;
            set => _productsPerPage = value > MaxProductsPerPage ? MaxProductsPerPage : value;
        }

    }
}
