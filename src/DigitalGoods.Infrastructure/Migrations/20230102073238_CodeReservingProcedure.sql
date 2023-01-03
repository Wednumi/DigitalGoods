CREATE PROCEDURE p_reserve_activation_code @OfferId int, @OrderId INT
AS
UPDATE TOP (1) ActivationCodes
SET OrderId = @OrderId
WHERE OfferId = @OfferId AND OrderId = NULL
