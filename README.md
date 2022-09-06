# Vending Machine API
CRUD resources associated with a sample vending machine.

## Resources
### NuGet Packages
- EntityFrameworkCore v6.0.8
- EntityFrameworkCore.Tools v6.0.8
- EntityFrameworkCore.Sqlite v6.0.8
- EntityFrameworkCore.Sqlite.Core v6.0.8
- VisualStudio.Web.CodeGeneration.Design v6.0.8
- VisualStudio.Azure.Containers.Tools.Targets v1.15.1
- Swashbuckle.AspNetCore v6.2.3

### Third Party Software
- SQLite ~v3
- (optional) DB Browser ([SQLite](https://sqlitebrowser.org/))

### Data Source
- Generate DB Schema and Initialize Database:  
```
dotnet ef migrations initial
dotnet ef database update
```
- Insert Mock Data. From a SQL Editor/Query Window:  
```
/Data/mock_data.sql
```
## Controllers
Base: API with actions using Entity Framework

## Models
Denomenation - currency  
Product - id : price : description   
Purchase - associate transactions with products  
Machine - associate location with machine inventory    
MachineInventory - associate machine inventory with line items  
MachineInventoryLineItem - associate inventory with line item details (product quantity)   
Transaction - associate machine with transaction line items  
TransactionLineItem -  associate transaction with line item details (product, quantity)

## Sample Purchase/Dispense payload  
```
{
  "id": 1,
  "transaction": {
    "id": 4,
    "transactionLineItem": [
      {
        "id": 7,
        "transactionId": 4,
        "productId": 3,
        "quantity": 1
      }
    ],
    "machineId": 1
  },
  "amountTendered": 35
}
```
