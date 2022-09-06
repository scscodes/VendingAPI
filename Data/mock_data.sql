/* PRODUCT */
INSERT INTO Product(Id, Name, Description, Price) VALUES (1, "Light", "Rail mounted light", 99.99);
INSERT INTO Product(Id, Name, Description, Price) VALUES (3, "Bucket", "Good for holding things. Not a helmet", 9.99);
INSERT INTO Product(Id, Name, Description, Price) VALUES (40, "Wheelbarrow", "Disappointing dump truck", 29.99);
INSERT INTO Product(Id, Name, Description, Price) VALUES (7, "Wrench", "Is not the size you need", 5.99);
INSERT INTO Product(Id, Name, Description, Price) VALUES (16, "Wire", "Solid copper core, 50 feet", 150.00);
INSERT INTO Product(Id, Name, Description, Price) VALUES (15, "Stamps", "Antique piece of history", 2.99);

/* MACHINE INVENTORY */
INSERT INTO MachineInventory(Id) VALUES (1);
INSERT INTO MachineInventory(Id) VALUES (2);

/* MACHINE LINE ITEMS */
INSERT INTO MachineInventoryLineItem(Id, MachineInventoryId, ProductId, CurrentQuantity) VALUES (1, 1, 1, 100);
INSERT INTO MachineInventoryLineItem(Id, MachineInventoryId, ProductId, CurrentQuantity) VALUES (2, 1, 3, 50);
INSERT INTO MachineInventoryLineItem(Id, MachineInventoryId, ProductId, CurrentQuantity) VALUES (3, 1, 40, 50);
INSERT INTO MachineInventoryLineItem(Id, MachineInventoryId, ProductId, CurrentQuantity) VALUES (4, 1, 7, 0);
INSERT INTO MachineInventoryLineItem(Id, MachineInventoryId, ProductId, CurrentQuantity) VALUES (5, 1, 16, 3);
INSERT INTO MachineInventoryLineItem(Id, MachineInventoryId, ProductId, CurrentQuantity) VALUES (6, 1, 15, 0);

INSERT INTO MachineInventoryLineItem(Id, MachineInventoryId, ProductId, CurrentQuantity) VALUES (7, 2, 1, 100);
INSERT INTO MachineInventoryLineItem(Id, MachineInventoryId, ProductId, CurrentQuantity) VALUES (8, 2, 3, 50);
INSERT INTO MachineInventoryLineItem(Id, MachineInventoryId, ProductId, CurrentQuantity) VALUES (9, 2, 40, 50);
INSERT INTO MachineInventoryLineItem(Id, MachineInventoryId, ProductId, CurrentQuantity) VALUES (10, 2, 7, 0);
INSERT INTO MachineInventoryLineItem(Id, MachineInventoryId, ProductId, CurrentQuantity) VALUES (11, 2, 16, 3);
INSERT INTO MachineInventoryLineItem(Id, MachineInventoryId, ProductId, CurrentQuantity) VALUES (12, 2, 15, 0);

/* MACHINE */
INSERT INTO Machine(Id, Name, Description, Location, MachineInventoryId) VALUES (1, "Mach1", "Machine one beta", "columbus, oh", 1);
INSERT INTO Machine(Id, Name, Description, Location, MachineInventoryId) VALUES (2, "Mach2", "Machine two charlie", "cleveland, oh", 2);

/* TRANSACTION */
INSERT INTO "Transaction"(Id, MachineId) VALUES (1, 1);
INSERT INTO "Transaction"(Id, MachineId) VALUES (2, 2);
INSERT INTO "Transaction"(Id, MachineId) VALUES (3, 1);

/* TRANSACTION LINE ITEM */
INSERT INTO TransactionLineItem(Id, TransactionId, ProductId, Quantity) VALUES (1, 1, 1, 2);
INSERT INTO TransactionLineItem(Id, TransactionId, ProductId, Quantity) VALUES (2, 1, 40, 1);
INSERT INTO TransactionLineItem(Id, TransactionId, ProductId, Quantity) VALUES (3, 1, 7, 1);

INSERT INTO TransactionLineItem(Id, TransactionId, ProductId, Quantity) VALUES (4, 2, 16, 2);
INSERT INTO TransactionLineItem(Id, TransactionId, ProductId, Quantity) VALUES (5, 2, 2, 11);

INSERT INTO TransactionLineItem(Id, TransactionId, ProductId, Quantity) VALUES (6, 3, 1, 5);


/* PURCHASE */