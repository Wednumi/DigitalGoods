CREATE PROCEDURE p_update_offer_rate @OfferId INT
AS
UPDATE Offers
SET AverageRating =
(SELECT AVG(Rate)
FROM Orders
WHERE OfferId = @OfferId AND Rate IS NOT NULL)
WHERE Id = @OfferId