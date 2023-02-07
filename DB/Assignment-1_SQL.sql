--assignment 1

CREATE DATABASE Northwind;

--create table

create table Products(
	ProductID int not null,
	ProductName varchar(40) not null,
	SupplierID int not null,
	CategoryID int not null,
	QuantityPerUnit varchar(20) not null,
	UnitPrice Decimal(10,4) not null,
	UnitsInStock int not null,
	UnitsOnOrder int not null,
	ReorderLevel int not null,
	Discontinued bit
)


--data insert

insert into Products values 
	(1, 'Chai', 1, 1, '10 boxes - 20 bags', 18.0000, 39, 0, 10, 0),
	(2, 'Chang', 1, 1, '24 - 12 oz bottles', 19.0000, 17, 40, 25, 0),
	(3, 'Aniseed Syrup', 1, 2, '12 - 550 ml bottles', 10.0000, 13, 70, 25, 0),
	(4, 'Chef Antons Cajun Seasoning', 2, 2, '48 - 6 oz jars', 22.0000, 53, 0, 0, 0),
	(5, 'Chef Antons Gumbo Mix', 2, 2, '36 boxes', 21.3500, 0, 0, 0, 1),
	(6, 'Grandmas Boysenberry Spread', 3, 2, '12 - 8 oz jars', 25.0000, 120, 0, 25, 1),
	(7, 'Uncle Bobs Organic Dried Pears', 3, 7, '12 - 1 lb pkgs', 30.0000, 15, 0, 10, 0),
	(8, 'Northwoods Cranberry Sauce', 3, 2, '12 - 12 oz jars', 40.0000, 6, 0, 0, 0),
	(9, 'Mishi Kobe Niku', 4, 6, '18 - 500 g pkgs', 97.0000, 29, 0, 0, 0),
	(10, 'Ikura', 4, 8, '12 - 200 ml jars', 31.0000, 31, 0, 0, 0),
	(11, 'Queso Cabrales', 5, 4, '1 kg pkg', 21.0000, 22, 30, 30, 0),
	(12, 'Queso Manchego La Pastora', 5, 4, '10 - 500 g pkgs', 38.0000, 86, 0, 0, 1),
	(13, 'Konbu', 6, 8, '2 kg box', 6.0000, 24, 0, 5, 0),
	(14, 'Tofu', 6, 7, '40 - 100 g pkgs', 23.2500, 35, 0, 0, 0),
	(15, 'Genen Shouyu', 6, 2, '24 - 250 ml bottles', 15.5000, 39, 0, 5, 0);


select * from Products;


--1st

SELECT ProductID,ProductName,UnitPrice 
FROM Products
WHERE ((UnitPrice < 20) AND (Discontinued = 0));


--2nd

SELECT ProductID,ProductName,UnitPrice 
FROM Products
WHERE ((UnitPrice < 25) AND (UnitPrice > 15));


--3rd

SELECT ProductID,ProductName,UnitPrice 
FROM Products
WHERE UnitPrice > (SELECT avg(UnitPrice) FROM Products);


--4th

SELECT DISTINCT ProductName, UnitPrice 
FROM Products AS a
WHERE 10 >= (SELECT COUNT(UnitPrice) FROM Products AS b WHERE b.UnitPrice >= a.UnitPrice);


--5th

SELECT COUNT(ProductName)
FROM Products
GROUP BY Discontinued;


--6th

SELECT ProductName, UnitsOnOrder, UnitsINStock
FROM Products
WHERE ((UnitsInStock < UnitsOnOrder) AND (Discontinued = 0));