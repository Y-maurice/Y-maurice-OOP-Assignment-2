namespace SrpLab.CheckoutBasket;


public readonly record struct BasketLine(string Sku, decimal Price, int Qty);