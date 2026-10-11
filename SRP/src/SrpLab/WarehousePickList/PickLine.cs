namespace SrpLab.WarehousePickList;


public readonly record struct PickLine(string Sku, string Aisle, int Bin, int QtyNeeded, int QtyOnHand);