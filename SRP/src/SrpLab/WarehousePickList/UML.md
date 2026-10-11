# Warehouse Pick List — UML


```mermaid
classDiagram
    class WarehousePickList {
        +AddNeed(string sku, string aisle, int bin, int qtyNeeded, int qtyOnHand) void
        +Allocate() IReadOnlyList
        +WalkingOrder() IReadOnlyList
        +PickerScript() string
        +WmsXmlBatch(string batchId) string
    }

    class PickLine {
        +string Sku
        +string Aisle
        +int Bin
        +int QtyNeeded
        +int QtyOnHand
    }

    class StockAllocator {
        +Allocate(IReadOnlyList lines) IReadOnlyList
        +ShortfallSkus(IReadOnlyList lines) IReadOnlyList
    }

    class WalkingOrderPlanner {
        +Plan(IReadOnlyList lines) IReadOnlyList
    }

    class PickerScriptGenerator {
        +Generate(IReadOnlyList walkingOrder, IReadOnlyList shortfallSkus) string
    }

    class WmsXmlBatchGenerator {
        +Generate(string batchId, IReadOnlyList allocations) string
    }

    WarehousePickList o-- PickLine : holds lines
    WarehousePickList --> StockAllocator : delegates allocation and shortages
    WarehousePickList --> WalkingOrderPlanner : delegates path order
    WarehousePickList --> PickerScriptGenerator : delegates picker instructions
    WarehousePickList --> WmsXmlBatchGenerator : delegates WMS XML
```

## Responsibilities

- **WarehousePickList:** Records the items that need to be picked and holds them as pick lines.
- **PickLine:** Represents one requested item with its aisle, bin, quantity needed, and quantity on hand.
- **StockAllocator:** Decides how much of each line can be allocated and which SKUs fall short.
- **WalkingOrderPlanner:** Orders the picks by aisle and bin and leaves out lines with nothing to pick.
- **PickerScriptGenerator:** Writes the step-by-step instructions and shortage warning for the picker.
- **WmsXmlBatchGenerator:** Builds the XML batch for the WMS integration.

## SRP Refactoring

Recording what needs picking is now separate from the allocation policy, the walking path, the picker's wording, and the WMS contract. A change to the allocation rules, the warehouse layout, the handheld instructions, or the XML format touches only its own class, and `WarehousePickList` stays focused on the list itself.

The original `WarehousePickList` methods remain available as delegating entry points, so existing callers do not change.