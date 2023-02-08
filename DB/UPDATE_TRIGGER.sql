--trigger for Update data in Orders Table
CREATE TRIGGER trVerifyOnUpdate ON Orders 
INSTEAD OF UPDATE
AS
BEGIN
	DECLARE @cust_id nvarchar(5)
	DECLARE @oldfreight money
	DECLARE @newfreight money
	DECLARE @avgFreight money
	SELECT @cust_id = CustomerID FROM INSERTED
	SELECT @oldfreight = Freight FROM Orders
	SELECT @newfreight = Freight FROM INSERTED

	IF @oldfreight = @newfreight
	BEGIN
		return;
	END
	ELSE
	BEGIN

		EXEC spCheckFreightWithAvg @cust_id, @avgFreight output

		IF @newfreight IS NOT NULL AND @newfreight >= @avgFreight
		BEGIN
			RAISERROR ('Freight value exceeds the average freight value' ,10,1)
		END
		ELSE
		BEGIN
			UPDATE Orders SET Freight = (SELECT Freight FROM INSERTED) WHERE CustomerID = @cust_id;
		END
	END
END