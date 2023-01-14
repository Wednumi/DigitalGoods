CREATE FUNCTION f_weekly_sales_per_day(@offerId INT)
RETURNS REAL
AS
BEGIN

DECLARE @earliestDate DATE;
SET @earliestDate = 
(SELECT TOP(1) [Date]
FROM Orders
WHERE OfferId = @offerId
ORDER BY [Date]);

RETURN(
	SELECT (COUNT(*) / DATEDIFF(DAY, @earliestDate, CURRENT_TIMESTAMP))
	FROM Orders
	WHERE DATEDIFF(DAY, [Date], CURRENT_TIMESTAMP) <= 7
	AND OfferId = @offerId
)
END