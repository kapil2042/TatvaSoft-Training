--Assignment 3

CREATE TABLE Department(
	dept_id int NOT NULL Primary Key,
	dept_name varchar(50) NOT NULL
);

CREATE TABLE Employee(
	emp_id int NOT NULL Primary Key,
	dept_id int NOT NULL,
	mngr_id int NOT NULL,
	emp_name varchar(50) NOT NULL,
	salary float NOT NULL,
	Foreign Key(dept_id) References Department(dept_id)
);


INSERT INTO Department 
VALUES	(1001,'finance'),
		(2001,'audit'),	
		(3001,'marketin'),
		(4001,'production');


 INSERT INTO Employee 
 VALUES	(1,1001,101,'john',35000),
		(2,2001,102,'mike',35433),
		(3,3001,103,'rohan',46000),
		(4,2001,101,'jeet',25400),
		(5,4001,101,'meet',61000),
		(6,1001,102,'kivi',33530),
		(7,2001,101,'neha',32320),
		(8,4001,103,'mena',45000),
		(9,3001,103,'tina',43220),
		(10,3001,102,'sohan',54320),
		(11,2001,102,'mira',23444),
		(12,1001,104,'mohan',54322);


--1st write a SQL query to find Employees who have the biggest salary in their Department

SELECT e.emp_name,
       e.dept_id,e.salary
FROM Employee e
WHERE e.salary IN
    (SELECT max(salary)
     FROM Employee
     GROUP BY dept_id HAVING dept_id=e.dept_id);


--2nd write a SQL query to find Departments that have less than 3 people in it

SELECT d.dept_name,COUNT(e.emp_id) 
FROM Employee e 
RIGHT OUTER JOIN Department d 
ON d.dept_id = e.dept_id 
GROUP BY d.dept_name 
HAVING COUNT(e.emp_id) < 3;

--3rd write a SQL query to find All Department along with the number of people there

SELECT d.dept_name,COUNT(e.emp_id) 
FROM Employee e 
RIGHT OUTER JOIN Department d 
ON d.dept_id = e.dept_id 
GROUP BY d.dept_name;

--4th write a SQL query to find All Department along with the total salary there

SELECT d.dept_name,SUM(e.salary) 
FROM Employee e 
RIGHT OUTER JOIN Department d 
ON d.dept_id = e.dept_id 
GROUP BY d.dept_name;