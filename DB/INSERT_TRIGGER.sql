--trigger for insert data in Orders Table
CREATE TRIGGER trVerifyOnInsert ON Orders 
INSTEAD OF INSERT
AS
BEGIN
	DECLARE @cust_id nvarchar(5)
	DECLARE @freight money
	DECLARE @avgFreight money
	SELECT @cust_id = CustomerID FROM INSERTED
	SELECT @freight = Freight FROM INSERTED
	
	EXEC spCheckFreightWithAvg @cust_id, @avgFreight output

	IF @freight IS NOT NULL AND @freight >= @avgFreight
	BEGIN
		RAISERROR ('Freight value exceeds the average freight value' ,10,1)
	END
	ELSE
	BEGIN
		INSERT INTO Orders("CustomerID","EmployeeID","OrderDate","RequiredDate",
					"ShippedDate","ShipVia","Freight","ShipName","ShipAddress",
					"ShipCity","ShipRegion","ShipPostalCode","ShipCountry")
				VALUES((SELECT CustomerID FROM INSERTED),(SELECT EmployeeID FROM INSERTED),
					(SELECT OrderDate FROM INSERTED),(SELECT RequiredDate FROM INSERTED),
					(SELECT ShippedDate FROM INSERTED),(SELECT ShipVia FROM INSERTED),
					(SELECT Freight FROM INSERTED),(SELECT ShipName FROM INSERTED),
					(SELECT ShipAddress FROM INSERTED),(SELECT ShipCity FROM INSERTED),
					(SELECT ShipRegion FROM INSERTED),(SELECT ShipPostalCode FROM INSERTED),
					(SELECT ShipCountry FROM INSERTED));
	END
END