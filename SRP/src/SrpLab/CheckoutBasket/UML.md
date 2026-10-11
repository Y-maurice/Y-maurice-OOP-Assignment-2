# Checkout Basket — UML


```mermaid
classDiagram
    class CheckoutBasket {
        +AddLine(string sku, decimal price, int qty) void
        +ApplyCouponText(string couponText) void
        +EnableGiftWrap() void
        +SubTotal() decimal
        +DiscountAmount() decimal
        +GrandTotal() decimal
        +GiftMessageCard(string fromName) string
        +AuthorizePaymentStub(string cardLast4) string
    }

    class BasketLine {
        +string Sku
        +decimal Price
        +int Qty
    }

    class BasketPricingCalculator {
        +SubTotal(IReadOnlyList lines) decimal
        +GrandTotal(decimal subTotal, decimal discount, bool giftWrap) decimal
    }

    class CouponDiscountCalculator {
        +Calculate(string couponText, decimal subTotal) decimal
    }

    class GiftMessageCardGenerator {
        +Generate(string fromName, IEnumerable skus, decimal grandTotal) string
    }

    class PaymentAuthorizer {
        +Authorize(decimal grandTotal, string cardLast4, int lineCount) string
    }

    CheckoutBasket o-- BasketLine : holds lines
    CheckoutBasket --> BasketPricingCalculator : delegates subtotal and total
    CheckoutBasket --> CouponDiscountCalculator : delegates coupon parsing
    CheckoutBasket --> GiftMessageCardGenerator : delegates gift message
    CheckoutBasket --> PaymentAuthorizer : delegates payment authorization
```

## Responsibilities

- **CheckoutBasket:** Holds the basket lines, the coupon text, and the gift-wrap choice, and validates added lines.
- **BasketLine:** Represents one SKU line with its price and quantity.
- **BasketPricingCalculator:** Calculates the subtotal and the grand total, including the gift-wrap fee.
- **CouponDiscountCalculator:** Interprets coupon text and returns the discount amount.
- **GiftMessageCardGenerator:** Produces the customer-facing gift message.
- **PaymentAuthorizer:** Produces the payment authorization code.

## SRP Refactoring

Basket contents are now separate from pricing, coupon rules, gift-message copy, and payment authorization. A change to a coupon format, the gift-wrap fee, the card wording, or the gateway touches only its own class, and `CheckoutBasket` stays focused on what is in the basket.

The original `CheckoutBasket` methods remain available as delegating entry points, so existing callers do not change.