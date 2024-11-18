using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MassTransit;
using Moq;
using Reqnroll;
using SnackHub.ProductService.Application.Contracts;
using SnackHub.ProductService.Application.Models;
using SnackHub.ProductService.Application.UseCases;
using SnackHub.ProductService.Behavior.Tests.Fixtures;
using SnackHub.ProductService.Domain.Contracts;
using SnackHub.ProductService.Domain.Entities;
using SnackHub.ProductService.Infra.Repositories.MongoDB;

namespace SnackHub.ProductService.Behavior.Tests.StepDefinitions;

[Binding]
public class ManageProductsStepDefinitions : MongoDbFixture
{

    private IProductRepository _productRepository;
    private IManageProductUseCase _manageProductUseCase;
    
    private Mock<IPublishEndpoint> _publishEndpointMock;

    [BeforeScenario]
    public async Task Setup()
    {
        await BaseSetUp();
        _publishEndpointMock = new Mock<IPublishEndpoint>();
        
        _productRepository = new ProductRepository(MongoDatabase);
        _manageProductUseCase = new ManageProductUseCase(_productRepository, _publishEndpointMock.Object);
    }

    [Given("a product with name '(.*)', category '(.*)', price '(.*)', and description '(.*)'")]
    public async Task GivenAProductWith(string name, Category category, decimal price, string description)
    {
        var manageProductRequest = new ManageProductRequest
        {
            Name = name,
            Category = category,
            Price = price,
            Description = description
        };
        
        await _manageProductUseCase.AddAsync(manageProductRequest);
    }

    [Then("a product with name '(.*)' should exist")]
    public async Task ThenTheProductWithNameIsCreated(string productName)
    {
        var product = await _productRepository.GetProductByNameAsync(productName);
        product
            .Should()
            .NotBeNull();

        product
            .Name
            .Should()
            .Be(productName);
    }
    
    [Then("an event of type 'ProductCreated' is raised")]
    public void ThenProductCreatedEventIsRaised()
    {
        _publishEndpointMock
            .Verify(publishEndpoint => publishEndpoint.Publish(It.IsAny<ProductCreated>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [When(@"edit the product named '(.*)' to have the name '(.*)'")]
    public async Task WhenEditTheProductToHaveTheName(string oldName, string newName)
    {
        var product = await _productRepository.GetProductByNameAsync(oldName);
        
        var manageProductRequest = new ManageProductRequest
        {
            Name = newName,
        };

        await _manageProductUseCase.UpdateAsync(product.Id, manageProductRequest);
    }

    [Then(@"an event of type 'ProductUpdated' is raised")]
    public void ThenProductUpdatedEventIsRaised()
    {
        _publishEndpointMock
            .Verify(publishEndpoint => publishEndpoint.Publish(It.IsAny<ProductUpdated>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [When(@"delete the product with name '(.*)'")]
    public async Task WhenDeleteTheProductWithName(string productName)
    {
        var product = await _productRepository.GetProductByNameAsync(productName);
        await _manageProductUseCase.DeleteAsync(product.Id);
    }

    [Then(@"an event of type 'ProductDeleted' is raised")]
    public void ThenAnProductDeletedEventIsRaised()
    {
        _publishEndpointMock
            .Verify(publishEndpoint => publishEndpoint.Publish(It.IsAny<ProductDeleted>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Then(@"the product named '(.*)' should not exists anymore")]
    public async Task ThenItShouldNotExistsAnymore(string productName)
    {
        var product = await _productRepository.GetProductByNameAsync(productName);
        product
            .Should()
            .BeNull();
    }
}