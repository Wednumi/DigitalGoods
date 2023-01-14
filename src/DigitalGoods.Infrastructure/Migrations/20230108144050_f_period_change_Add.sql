CREATE FUNCTION f_period_change(@offerId INT, @periodDays INT)
RETURNS REAL
AS
BEGIN

DECLARE @periodDate DATE;
SET @periodDate = DATEADD(DAY, -@periodDays, CURRENT_TIMESTAMP);

DECLARE @previousPeriodDate DATE;
SET @previousPeriodDate = DATEADD(DAY, -@periodDays * 2, CURRENT_TIMESTAMP);

DECLARE @previousCount INT;
SET @previousCount = (
SELECT COUNT(*)
FROM Orders
WHERE OfferId = @offerId
AND [Date] > @previousPeriodDate AND [Date] <= @periodDate);

RETURN(
	SELECT IIF(@previousCount > 0,
		(SELECT CAST(COUNT(*) AS REAL) / 
		@previousCount
		FROM Orders
		WHERE OfferId = @offerId
		AND [Date] > @periodDate),
		(SELECT 0))
)
END