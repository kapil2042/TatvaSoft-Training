--1st Create a stored procedure in the Northwind database that will calculate the average value of Freight for a specified customer.Then, a business rule will be added that will be triggered before every Update and Insert command in the Orders controller,and will use the stored procedure to verify that the Freight does not exceed the average freight. If it does, a message will be displayed and the command will be cancelled.

CREATE PROCEDURE spCheckFreightWithAvg
	@customer_id nvarchar(5),
	@avg_freight money output
AS
BEGIN
	SELECT @avg_freight = AVG(Freight) 
	FROM Orders
	WHERE CustomerID = @customer_id;
END

--insert query

INSERT INTO Orders("CustomerID","EmployeeID","OrderDate","RequiredDate",
				"ShippedDate","ShipVia","Freight","ShipName","ShipAddress",
				"ShipCity","ShipRegion","ShipPostalCode","ShipCountry")
		VALUES (N'BONAP',4,'5/6/1998','6/3/1998',NULL,2,1000,
			N'Bon app''',N'12, rue des Bouchers',N'Marseille',
			NULL,N'13008',N'France')

--update query

UPDATE Orders SET Freight = 200 WHERE OrderID=10286


--2nd write a SQL query to Create Stored procedure in the Northwind database to retrieve Employee Sales by Country

CREATE PROCEDURE spGetEmployeeSalesByCountry
	@country nvarchar(50)
AS
BEGIN
	SELECT e.Country, e.LastName, e.FirstName, o.OrderID, od.Quantity, od.UnitPrice, od.Quantity*od.UnitPrice AS [subtotal]
	FROM Employees e 
	INNER JOIN (Orders o INNER JOIN [Order Details] od ON o.OrderID = od.OrderID) 
	ON e.EmployeeID = o.EmployeeID
	WHERE e.Country = @country
END

spGetEmployeeSalesByCountry 'USA'


--3rd write a SQL query to Create Stored procedure in the Northwind database to retrieve Sales by Year

CREATE PROCEDURE spGetSalesByYear
	@year int
AS
BEGIN
	SELECT o.ShippedDate, o.OrderID, od.Quantity, od.UnitPrice, od.Quantity*od.UnitPrice AS [subtotal]
	FROM Orders o 
	INNER JOIN [Order Details] od 
	ON o.OrderID = od.OrderID
	WHERE YEAR(o.ShippedDate) = @year
END

spGetSalesByYear 1997


--4th write a SQL query to Create Stored procedure in the Northwind database to retrieve Sales By Category

CREATE PROCEDURE spGetSalesByCategory
	@category nvarchar(20)
AS
BEGIN
	SELECT c.CategoryName, o.OrderID, od.Quantity, od.UnitPrice, od.Quantity*od.UnitPrice AS [subtotal]
	FROM Orders o 
	INNER JOIN ([Order Details] od INNER JOIN (Products p INNER JOIN Categories c ON p.CategoryID = c.CategoryID) 
	ON od.ProductID = p.ProductID)
	ON o.OrderID = od.OrderID
	WHERE c.CategoryName = @category
END

spGetSalesByCategory 'Seafood'


--5th write a SQL query to Create Stored procedure in the Northwind database to retrieve Ten Most Expensive Products

CREATE PROCEDURE spTenMostExpensiveProduct
AS
SET ROWCOUNT 10
BEGIN
	SELECT UnitPrice, ProductID, ProductName FROM Products ORDER BY UnitPrice DESC
END

spTenMostExpensiveProduct


--6th write a SQL query to Create Stored procedure in the Northwind database to insert Customer Order Details

CREATE PROCEDURE spInsertCustomerOrderDetails
	@order_id int,
	@product_id int,
	@unit_price money,
	@qty smallint,
	@discount real
AS
BEGIN
	INSERT INTO [Order Details] VALUES(@order_id,@product_id,@unit_price,@qty,@discount);
	SELECT * FROM [Order Details] WHERE OrderID=@order_id 
									AND ProductID=@product_id 
									AND UnitPrice=@unit_price 
									AND Quantity=@qty 
									AND Discount=@discount;
END

spInsertCustomerOrderDetails 10522,12,20,30,0


--7th write a SQL query to Create Stored procedure in the Northwind database to update Customer Order Details

CREATE PROCEDURE spUpdateCustomerOrderDetails
	@order_id int,
	@product_id int,
	@unit_price money,
	@qty smallint,
	@discount real
AS
BEGIN
	UPDATE [Order Details] SET UnitPrice=@unit_price, 
								Quantity=@qty,
								Discount=@discount
						WHERE OrderID=@order_id AND ProductID=@product_id;
	SELECT * FROM [Order Details] WHERE OrderID=@order_id AND ProductID=@product_id;
END

spUpdateCustomerOrderDetails 10522,12,20,30,0