CREATE FUNCTION f_calculate_final_price(@price AS REAL, @discount AS INT)
RETURNS REAL
AS
BEGIN
RETURN(
@price * (100 - @discount) / 100
)
END