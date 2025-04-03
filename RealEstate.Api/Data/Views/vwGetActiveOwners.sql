CREATE OR ALTER VIEW [vwGetActiveOwners] AS
    SELECT * FROM Owners WHERE IsActive = 1