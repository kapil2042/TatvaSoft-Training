--trigger for Update data in Orders Table
CREATE TRIGGER trVerifyOnUpdate ON Orders 
INSTEAD OF UPDATE
AS
BEGIN
	DECLARE @cust_id nvarchar(5)
	DECLARE @newfreight money
	DECLARE @avgFreight money
	SELECT @cust_id = CustomerID FROM INSERTED
	SELECT @newfreight = Freight FROM INSERTED

	EXEC spCheckFreightWithAvg @cust_id, @avgFreight output

	IF @newfreight IS NOT NULL AND @newfreight >= @avgFreight
	BEGIN
		RAISERROR ('Freight value exceeds the average freight value' ,10,1)
	END
	ELSE
	BEGIN
		UPDATE Orders
		SET CustomerID = ins.CustomerID,
			EmployeeID = ins.EmployeeID,
			OrderDate = ins.OrderDate,
			RequiredDate = ins.RequiredDate,
			ShippedDate = ins.ShippedDate,
			ShipVia = ins.ShipVia,
				Freight = ins.Freight,
			ShipName = ins.ShipName,
			ShipAddress = ins.ShipAddress,
			ShipCity = ins.ShipCity,
			ShipRegion = ins.ShipRegion,
			ShipPostalCode = ins.ShipPostalCode,
			ShipCountry = ins.ShipCountry
		FROM Orders
		INNER JOIN INSERTED AS ins
		ON Orders.orderID = ins.orderID
	END
END