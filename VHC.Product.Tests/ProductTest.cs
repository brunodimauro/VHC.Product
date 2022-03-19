using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using VHC.Product.Api.Controllers;
using VHC.Product.Helpers.Repository;
using VHC.Product.Infrastructure;
using VHC.Product.Infrastructure.Data;
using VHC.Product.Services;
using Xunit;

namespace VHC.Product.Tests
{
    public class ProductTest
    {
        private readonly DbContextOptions<DataContext> _contextOptions;
        ProductService _productService;
        ProductRepository _productRepository;

        public ProductTest()
        {
            // Creating new database for perform the tests
            _contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("VHCProductTest")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
        }

        DataContext CreateContext() => new DataContext(_contextOptions, (context, modelBuilder) =>
        {
            modelBuilder.Entity<Domain.Product>()
                .ToInMemoryQuery(() => context.Product);
        });

        [Fact]
        public async void GetProductById_ShouldReturnProductObj()
        {
            Guid productId = new System.Guid("A7F443B9-D67C-4791-AD87-083E02614F17");

            _productRepository = new ProductRepository(CreateContext());
            _productService = new ProductService(_productRepository);

            await _productService.Insert(GetFakeProduct(productId));
            Domain.Product? result = await _productService.Get(productId);

            Domain.Product expected = GetFakeProduct(productId);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async void UpdateProductPrice_ShouldUpdateObj()
        {
            Guid productId = new System.Guid("050CF2A5-9052-4101-B868-FA82BE582924");

            _productRepository = new ProductRepository(CreateContext());
            _productService = new ProductService(_productRepository);

            Domain.Product fakeProduct = GetFakeProduct(productId);
            await _productService.Insert(fakeProduct);
            
            fakeProduct.Price = 300;
            await _productService.Update(fakeProduct);
            
            Domain.Product? result = await _productService.Get(productId);

            Domain.Product expected = new Domain.Product
            {
                Name = "Test product",
                Currency = "EUR",
                Price = 300,
                ProductId = productId,
                ProductGroupId = new System.Guid("E6DE26ED-BEC6-467F-A772-6937600D1170")
            };

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async void DeleteProduct_ShouldNotFindObj()
        {
            Guid productId = new System.Guid("81BB13DF-8AF3-41EA-86B1-3EA0032AD915");

            _productRepository = new ProductRepository(CreateContext());
            _productService = new ProductService(_productRepository);

            Domain.Product fakeProduct = GetFakeProduct(productId);
            await _productService.Insert(fakeProduct);

            await _productService.Delete(productId);
            Domain.Product? result = await _productService.Get(productId);

            Domain.Product? expected = null;

            result.Should().BeEquivalentTo(expected);
        }

        private static Domain.Product GetFakeProduct(Guid productId)
        {
            return new Domain.Product
            {
                Name = "Test product",
                Currency = "EUR",
                Price = 20,
                ProductId = productId,
                ProductGroupId = new System.Guid("E6DE26ED-BEC6-467F-A772-6937600D1170")
            };
        }
    }
}