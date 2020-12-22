namespace KeyFactor.Carbone.Configuration.Products
{
    public class UpdateProductDto : PersistableProductDto
    {
        public string ConcurrencyStamp { get; set; }
    }
}
