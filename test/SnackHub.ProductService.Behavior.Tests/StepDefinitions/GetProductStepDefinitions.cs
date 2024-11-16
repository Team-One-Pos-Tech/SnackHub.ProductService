using System;
using System.Threading.Tasks;
using FluentAssertions;
using Reqnroll;
using SnackHub.ProductService.Application.Contracts;
using SnackHub.ProductService.Application.UseCases;
using SnackHub.ProductService.Behavior.Tests.Fixtures;
using SnackHub.ProductService.Domain.Contracts;
using SnackHub.ProductService.Domain.Entities;
using SnackHub.ProductService.Infra.Repositories.MongoDB;

namespace SnackHub.ProductService.Behavior.Tests.StepDefinitions;

[Binding]
public class GetProductStepDefinitions : MongoDbFixture
{
    
    private IProductRepository _productRepository;
    private IGetProductUseCase _getProductUseCase;
    private IGetByCategoryUseCase _getByCategoryUseCase;
    
    [BeforeScenario]
    public async Task Setup()
    {
        await BaseSetUp();
        
        _productRepository = new ProductRepository(MongoDatabase);
        _getProductUseCase = new GetProductUseCase(_productRepository);
        _getByCategoryUseCase = new GetByCategoryUseCase(_productRepository);
    }

    [Given(@"following products table")]
    public async Task GivenFollowingProductsTable(Table table)
    {
        var products = table.CreateSet<Product>();
        foreach (var product in products)
            await _productRepository.AddAsync(product);
    }

    [When("search for the product with id '(.*)' it should return a Product Response with name '(.*)'")]
    public async Task WhenSearchingForTheProductWithId(Guid productId, string productName)
    {
        var product = await _getProductUseCase.Execute(productId);
        product
            .Should()
            .NotBeNull();

        product?
            .Name
            .Should()
            .Be(productName);
    }
    
    [When("list all products, it should return a List of Product with (.*) elements")]
    public async Task WhenSearchingForTheProductWithId(int count)
    {
        var products = await _getProductUseCase.Execute();
        products
            .Should()
            .NotBeNull()
            .And
            .HaveCount(count);
    }

    [When(@"list all products by category '(.*)' , it should return a List of Product with '(.*)' elements")]
    public async Task WhenListAllProductsByCategoryItShouldReturnAListOfProductWithElements(Category category, int count)
    {
        var products = await _getByCategoryUseCase.Get(category);
        products
            .Should()
            .NotBeNull()
            .And
            .HaveCount(count);
    }
}