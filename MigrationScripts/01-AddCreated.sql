ALTER TABLE BetaSignups ADD Created DATETIME2 not null default(getdate());
ALTER TABLE Stores ADD Created DATETIME2 not null default(getdate());
ALTER TABLE StoreItems ADD Created DATETIME2 not null default(getdate());
ALTER TABLE OpenIDs ADD Created DATETIME2 not null default(getdate());

ALTER TABLE Items ADD 
    Created DATETIME2 not null default(getdate()),
    LastUpdated DATETIME2 not null default(getdate());

ALTER TABLE Users ADD 
    Created DATETIME2 not null default(getdate()),
    LastUpdated DATETIME2 not null default(getdate());

ALTER TABLE ShoppingLists ADD 
    Created DATETIME2 not null default(getdate()),
    LastUpdated DATETIME2 not null default(getdate());