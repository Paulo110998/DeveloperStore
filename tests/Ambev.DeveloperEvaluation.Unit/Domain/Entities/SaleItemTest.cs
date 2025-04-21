using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class SaleItemTest
{
    [Theory]
    [InlineData(3, 0)] // no discount (<4 items)
    [InlineData(4, 4)] // 10% discount (4 items)
    [InlineData(5, 5)] // 10% discount (5 items)
    [InlineData(9, 9)] // 10% discount (9 items)
    [InlineData(10, 20)] // 20% discount (10 items)
    [InlineData(15, 30)] // 20% discount (15 items)
    [InlineData(20, 40)] // 20% discount (20 items)
    public void ApplyDiscount_ShouldCalculateCorrectDiscount(int quantity, decimal expectedDiscount)
    {
        // Arrange
        var unitPrice = 10m;
        var item = new SaleItem { Quantity = quantity, UnitPrice = unitPrice };

        // Act
        item.ApplyDiscount();

        // Assert
        Assert.Equal(expectedDiscount, item.Discount);
    }

    [Fact]
    public void ApplyDiscount_ShouldThrowException_WhenQuantityExceeds20()
    {
        // Arrange
        var item = new SaleItem { Quantity = 21, UnitPrice = 10 };

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => item.ApplyDiscount());
        Assert.Equal("Invalid quantity for discount", exception.Message);
    }

    [Fact]
    public void Total_ShouldCalculateCorrectValue_WithDiscount()
    {
        // Arrange
        var item = new SaleItem { Quantity = 5, UnitPrice = 10 };
        item.ApplyDiscount(); // Discount = 5

        // Act & Assert
        Assert.Equal(45, item.Total); // (5*10) - 5 = 45
    }

    [Fact]
    public void Cancel_ShouldToggleIsCanceledStatus()
    {
        // Arrange
        var item = new SaleItem { IsCanceled = false };

        // Act - First (should cancel)
        item.Cancel();

        // Assert
        Assert.True(item.IsCanceled);

        // Act - Second (should uncancel)
        item.Cancel();

        // Assert
        Assert.False(item.IsCanceled);
    }

    [Fact]
    public void Total_ShouldCalculateCorrectValue_WithoutDiscount()
    {
        // Arrange
        var item = new SaleItem { Quantity = 3, UnitPrice = 10 };
        item.ApplyDiscount(); // Discount = 0

        // Act & Assert
        Assert.Equal(30, item.Total); // (3*10) - 0 = 30
    }
}