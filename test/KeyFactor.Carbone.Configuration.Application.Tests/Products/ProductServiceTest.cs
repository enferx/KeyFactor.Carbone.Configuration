using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;
using Xunit;

namespace KeyFactor.Carbone.Configuration.Products
{
    public class ProductServiceTest : ConfigurationApplicationTestBase
    {
        private readonly IProductAppService _productAppService;
        private readonly IObjectMapper _objectMapper;

        public ProductServiceTest()
        {
            _productAppService = GetRequiredService<IProductAppService>();
            _objectMapper = GetRequiredService<IObjectMapper>();
        }

        [Fact]
        public async Task ShouldGetListOfBooks()
        {
            var list = await _productAppService.GetListAsync(new GetProductListDto
            {
                MaxResultCount = 10,
                SkipCount = 0
            });
            list.TotalCount.ShouldBeGreaterThan(0);
            list.Items.ShouldContain(x => x.Name == "PROD-10010");
        }

        [Fact]
        public async Task ShouldCreateAProduct()
        {
            var product = await _productAppService.CreateAsync(new CreateUpdateProductDto
            {
                Number = "PROD-112099",
                Name = "Cinta aisladora",
                FieldServiceProductType = FieldServiceProductType.Inventory,
                ProductStructure = ProductStructure.Product,
                DecimalPlaces = 2
            });
            product.Number.ShouldBe("PROD-112099");
        }

        [Fact]
        public async Task ShouldUpdateAProduct()
        {
            var productDto = await _productAppService.FindByNumber("PROD-10010");
            var product = _objectMapper.Map<ProductDto, CreateUpdateProductDto>(productDto);
            product.Description = "test description";
            var updatedProduct = await _productAppService.UpdateAsync(productDto.Id, product);
            updatedProduct.Description.ShouldBe("test description");
            updatedProduct.ConcurrencyStamp.ShouldNotBe(product.ConcurrencyStamp);
        }

        [Fact]
        public async Task ShouldEvalProductAlreadyExistsExceptionOnUpdateProduct() 
        {
            var productDto = await _productAppService.FindByNumber("PROD-10010");
            var product = _objectMapper.Map<ProductDto, CreateUpdateProductDto>(productDto);
            product.Number = "PROD-110010";
            Should.Throw<ProductNumberAlreadyExistsException>
            (
                async () => await _productAppService.UpdateAsync(productDto.Id, product)
            );
        }

        [Fact]
        public void ShouldEvalProductAlreadyExistsExceptionOnCreateProduct()
        {
            Should.Throw<ProductNumberAlreadyExistsException>(async () =>
                await _productAppService.CreateAsync(new CreateUpdateProductDto
                {
                    Number = "PROD-110010",
                    Name = "Cinta aisladora",
                    FieldServiceProductType = FieldServiceProductType.Inventory,
                    ProductStructure = ProductStructure.Product,
                    DecimalPlaces = 2
                })
            );
        }

    }
}
