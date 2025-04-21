using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class SaleTest
{
    [Fact]
    public void Should_Apply_10_Percent_Discount_When_Quantity_Is_5()
    {
        // Arrange
        var item = new SaleItem { Quantity = 5, UnitPrice = 10 };

        // Act
        item.ApplyDiscount();

        // Assert
        Assert.Equal(5, item.Quantity);
        Assert.Equal(5, item.Discount); // 10% de 50
    }

    [Fact]
    public void AddItem_ShouldAddItem_WhenQuantityIsValid()
    {
        // Arrange
        var sale = new Sale { Items = new List<SaleItem>() };
        var item = new SaleItem { Quantity = 5, UnitPrice = 10 };

        // Act
        sale.AddItem(item);

        // Assert
        Assert.Single(sale.Items);
        Assert.Equal(5, item.Discount);
    }

    [Fact]
    public void AddItem_ShouldThrowException_WhenSingleItemExceeds20()
    {
        // Arrange
        var sale = new Sale { Items = new List<SaleItem>() };
        var item = new SaleItem { ProductName = "Beer", Quantity = 21, UnitPrice = 10 };

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => sale.AddItem(item));
        Assert.Equal("Maximum quantity per item entry is 20", exception.Message);
        Assert.Empty(sale.Items);
    }

    [Fact]
    public void AddItem_ShouldThrowException_WhenTotalQuantityExceeds20()
    {
        // Arrange
        var sale = new Sale { Items = new List<SaleItem>() };
        sale.AddItem(new SaleItem { ProductName = "Beer", Quantity = 15, UnitPrice = 10 });

        var newItem = new SaleItem { ProductName = "Beer", Quantity = 6, UnitPrice = 10 };

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => sale.AddItem(newItem));
        Assert.Contains("Total quantity for product Beer cannot exceed 20 units", exception.Message);
        Assert.Single(sale.Items);
    }

 
    [Fact]
    public void AddItem_ShouldAllowMultipleEntries_UpTo20Total()
    {
        // Arrange
        var sale = new Sale { Items = new List<SaleItem>() };

        // Act
        sale.AddItem(new SaleItem { ProductName = "Beer", Quantity = 10, UnitPrice = 10 });
        sale.AddItem(new SaleItem { ProductName = "Beer", Quantity = 10, UnitPrice = 10 });

        // Assert
        Assert.Equal(2, sale.Items.Count);
        Assert.Equal(20, sale.Items.Sum(i => i.Quantity));
    }

    [Fact]
    public void AddItem_ShouldThrow_WhenAddingToCanceledSale()
    {
        // Arrange
        var sale = new Sale { IsCanceled = true, Items = new List<SaleItem>() };
        var item = new SaleItem { Quantity = 5, UnitPrice = 10 };

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => sale.AddItem(item));
        Assert.Equal("Cannot add items to canceled sale", exception.Message);
    }

    [Fact]
    public void Cancel_ShouldToggleIsCanceledStatus()
    {
        // Arrange
        var sale = new Sale { IsCanceled = false };

        // Act - First call (should cancel)
        sale.Cancel();

        // Assert
        Assert.True(sale.IsCanceled);

        // Act - Second call (should uncancel)
        sale.Cancel();

        // Assert
        Assert.False(sale.IsCanceled);
    }

    [Fact]
    public void Total_ShouldIgnoreCanceledItems()
    {
        // Arrange
        var sale = new Sale
        {
            Items = new List<SaleItem>
            {
                new SaleItem { Quantity = 5, UnitPrice = 10, IsCanceled = false }, // 50
                new SaleItem { Quantity = 10, UnitPrice = 10, IsCanceled = true }, // 100 (cancelado)
                new SaleItem { Quantity = 3, UnitPrice = 10, IsCanceled = false }  // 30
            }
        };

        // Act & Assert
        Assert.Equal(80, sale.Total); // 50 + 30 (item cancelado não conta)
    }

    [Fact]
    public void Total_ShouldCalculateSumOfAllItems()
    {
        // Arrange
        var sale = new Sale
        {
            Items = new List<SaleItem>
            {
                new SaleItem { Quantity = 2, UnitPrice = 10, Discount = 0 },  // Total: 20
                new SaleItem { Quantity = 5, UnitPrice = 10, Discount = 5 }, // Total: 45
                new SaleItem { Quantity = 10, UnitPrice = 10, Discount = 20 } // Total: 80
            }
        };

        // Act & Assert
        Assert.Equal(145, sale.Total); // 20 + 45 + 80
    }

    [Fact]
    public void Total_ShouldReturnZero_WhenNoItems()
    {
        // Arrange
        var sale = new Sale { Items = new List<SaleItem>() };

        // Act & Assert
        Assert.Equal(0, sale.Total);
    }

    [Fact]
    public void AddItem_ShouldApplyDiscountAutomatically()
    {
        // Arrange
        var sale = new Sale { Items = new List<SaleItem>() };
        var item = new SaleItem { Quantity = 10, UnitPrice = 10 };

        // Act
        sale.AddItem(item);

        // Assert
        Assert.Equal(20, item.Discount); 
    }
}