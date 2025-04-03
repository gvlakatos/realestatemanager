CREATE OR ALTER VIEW [vwGetPropertiesByStatus] AS
SELECT PropertyStatus AS Status, COUNT(*) AS Count
FROM Properties
GROUP BY PropertyStatus