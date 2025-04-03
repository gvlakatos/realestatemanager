CREATE OR ALTER VIEW [vwGetActiveTenants] AS
    SELECT * FROM Tenants WHERE IsActive = 1