CREATE FUNCTION f_most_profitable_offers (@TopRecords INT)
RETURNS TABLE
AS
RETURN
(
	SELECT TOP(@TopRecords) *
	FROM Offers
	WHERE [State] = 1
	ORDER BY
	Price - dbo.f_calculate_final_price(Price, Discount) DESC
)