ALTER PROCEDURE p_update_offer_rate @OfferId INT
AS
UPDATE Offers
SET AverageRating =
(SELECT AVG(Rate)
FROM Orders
WHERE OfferId = @OfferId AND Rate IS NOT NULL)
WHERE Id = @OfferId;

DECLARE @userId nvarchar(450);
SET @userId = 
(SELECT UserId
FROM Offers
WHERE Id = @OfferId);

UPDATE AspNetUsers
SET AverageRating =
(SELECT AVG(Offers.AverageRating)
FROM AspNetUsers
INNER JOIN Offers on AspNetUsers.Id = Offers.UserId
WHERE AspNetUsers.Id = @userId AND Offers.AverageRating != 0)
WHERE AspNetUsers.Id = @userId;