using DevOps_Integration_Test_Testing_Homework6;
using DevOps_Integration_Test_Testing_Homework6.Data;
using DevOps_Integration_Test_Testing_Homework6.Repositories.Abstracts;
using DevOps_Integration_Test_Testing_Homework6.Repositories.Concretes;
using DevOps_Integration_Test_Testing_Homework6.Services.Abstracts;
using DevOps_Integration_Test_Testing_Homework6.Services.Concretes;
using Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class ProductTests
    {
        private ProductDbContext _context;
        private IProductRepository _repository;
        private IProductService _service;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
            .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TestProductDB;Integrated Security=True;Trust Server Certificate=False;")
            .Options;

            _context = new ProductDbContext(options);

            _repository = new ProductRepository(_context);
            _service = new ProductService(_repository);
            _context.Database.EnsureCreated();
            _context.Products.RemoveRange(_context.Products);
            _context.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            _context.Products.RemoveRange(_context.Products);
            _context.SaveChanges();

            _context.Dispose();
        }

        [Test]
        public async Task GetAllProductsAsync_ShouldReturnAllProducts()
        {
            // Arrange
            _context.Products.AddRange(new List<Product>
            {
                new Product { Name = "Water",Description="nice",Quantity=5 },
                new Product { Name = "Bread",Description="yummy",Quantity=10 },
                //new Product { Name = "Bread",Description="yummy",Quantity=10 }
            });
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.That(result != null);
            Assert.That(2, Is.EqualTo(result.Count()));

        }

        [Test]
        public async Task GetByIdProductsAsync_SholudReturnProduct()
        {
            //Arrange
            _context.Products.AddRange(new List<Product>
            {
                new Product{ Name="Sprite", Description="bad drink", Quantity=10 }
            });
            await _context.SaveChangesAsync();
            var product = _context.Products.FirstOrDefault();

            //Assert
            if (product != null)
            {
                var result = await _service.GetByIdAsync(product.Id);
                Assert.That(result != null);

                Assert.That(result.Id, Is.EqualTo(product.Id));

            }
        }

        [Test]
        public async Task DeleteProductsAsync_SholudReturnResponse()
        {
            //Arrange
            _context.Products.AddRange(new List<Product>
            {
                new Product{ Name="Coca-Cola", Description="bad drink", Quantity=21 }
            });
            await _context.SaveChangesAsync();
            var product = _context.Products.FirstOrDefault();

            //Assert
            if (product != null)
            {
                var result = await _service.DeleteAsync(product.Id);
                Assert.That(result != false);

            }
        }

        [Test]
        public async Task AddProductsAsync_SholudReturnResponse()
        {
            //Arrange
            var newProduct = new Product
            {
                Name = "Pepsi",
                Description = "very bad drink",
                Quantity = 30,
            };

            //Act
            var result = await _service.AddAsync(newProduct);

            //Assert
            Assert.That(result != null);
        }

        [Test]
        public async Task UpdateProductsAsync_SholudReturnResponse()
        {
            //Arrange
            _context.Products.AddRange(new List<Product>
            {
                new Product{ Name="Coca-Cola", Description="bad drink", Quantity=21 }
            });
            await _context.SaveChangesAsync();
            var product = _context.Products.FirstOrDefault();


            //Assert
            if (product != null)
            {
                product.Name = "Fanta";
                product.Description = "Great";
                product.Quantity = 50;

                var result = await _service.UpdateAsync(product);
                Assert.That(result != null);
                Assert.That(product.Id, Is.EqualTo(result.Id));
                Assert.That(product.Name, Is.EqualTo(result.Name));

            }
        }

    }
}
