ALTER FUNCTION f_weighted_rating(@offerId INT)
RETURNS REAL
AS
BEGIN
RETURN(
5.1 - (SELECT AverageRating FROM Offers WHERE Id = @offerId) * 
POWER(
(SELECT COUNT(*)
FROM Orders
WHERE Orders.OfferId = @offerId AND Rate IS NOT NULL), 0.3)
)
END