using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ambev.DeveloperEvaluation.Domain.Events;

public abstract class SaleEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
    public int? SaleId { get; }

    protected SaleEvent(int? saleId)
    {
        SaleId = saleId;
    }
}

public class SaleCreatedEvent : SaleEvent
{
    public string ClientName { get; }
    public decimal TotalAmount { get; }

    public SaleCreatedEvent(int saleId, string clientName, decimal totalAmount)
        : base(saleId)
    {
        ClientName = clientName;
        TotalAmount = totalAmount;
    }
}

public class SaleModifiedEvent : SaleEvent
{
    public string ModifiedBy { get; }
    public decimal NewTotalAmount { get; }

    public SaleModifiedEvent(int saleId, string modifiedBy, decimal newTotalAmount)
        : base(saleId)
    {
        ModifiedBy = modifiedBy;
        NewTotalAmount = newTotalAmount;
    }
}

public class SaleCanceledEvent : SaleEvent
{
    public string Reason { get; }

    public SaleCanceledEvent(int? saleId, string reason = "")
        : base(saleId)
    {
        Reason = reason;
    }
}

public class SaleItemCanceledEvent : SaleEvent
{
    public int ItemId { get; }
    public string ProductName { get; }
    public string Reason { get; }

    public SaleItemCanceledEvent(int saleId, int itemId, string productName, string reason = "")
        : base(saleId)
    {
        ItemId = itemId;
        ProductName = productName;
        Reason = reason;
    }
}