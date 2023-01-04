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
(SELECT AVG(Orders.Rate)
FROM AspNetUsers
INNER JOIN Offers ON AspNetUsers.Id = Offers.UserId
INNER JOIN Orders ON Offers.Id = Orders.OfferId
WHERE AspNetUsers.Id =  @userId)
WHERE AspNetUsers.Id = @userId;
