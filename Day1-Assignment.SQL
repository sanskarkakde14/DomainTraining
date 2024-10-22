
CREATE PROCEDURE GetCustomersByProduct
@ProductID INT  
AS
BEGIN
select C.customer_id AS CustomerID,       
CONCAT(C.first_name, ' ', C.last_name) AS CustomerName,  
O.order_date AS PurchaseDate       
from  
sales.customers C                  
INNER JOIN 
sales.orders O 
ON C.customer_id = O.customer_id  
INNER JOIN 
sales.order_items OI 
ON O.order_id = OI.order_id 
INNER JOIN 
production.products P 
ON OI.product_id = P.product_id  
WHERE 
OI.product_id = @ProductID          
order by 
O.order_date;                     
END;

EXEC GetCustomersByProduct 98;
Go;


/*Q--CREATE TABLE Department with the below columns
  ID,Name
populate with test data
 
 
CREATE TABLE Employee with the below columns
  ID,Name,Gender,DOB,DeptId
populate with test data*/




CREATE DATABASE school

CREATE TABLE Department(
Department_id INT PRIMARY KEY IDENTITY(1,1),
Name VARCHAR (50)
)

CREATE TABLE Employee(
Employee_id INT PRIMARY KEY IDENTITY(1,1),
Employee_name VARCHAR(50),
Employee_gender VARCHAR(10),
Employee_DOB DATE,
Department_id INT FOREIGN KEY REFERENCES Department(Department_id)
)

-- Insert test data into Department
INSERT INTO Department ( Name) VALUES ( 'HR');
INSERT INTO Department ( Name) VALUES ( 'IT');
INSERT INTO Department ( Name) VALUES ('Finance');
-- Insert test data into Employee
INSERT INTO Employee ( Employee_name,Employee_gender, Employee_DOB,Department_id ) 
VALUES 
('Sanskar Kakde', 'M', '1985-06-15', 2),
('David Beckham', 'M', '1990-11-25', 1),
('Johny Daniels', 'M', '1975-03-10', 3),
('Danny White', 'M', '1988-08-21', 2);
Go;

/*a) Create a procedure to update the Employee details in the Employee table based on the Employee id.*/

CREATE PROCEDURE UpdateEmployeeDetails
    @EmpId INT,
    @Name VARCHAR(50),
    @Gender CHAR(1),
    @DOB DATE,
    @DeptId INT
AS
BEGIN
    UPDATE Employee
    SET Employee_name = @Name, Employee_gender = @Gender, Employee_DOB = @DOB, Department_id = @DeptId
    WHERE Employee_id = @EmpId;
END;
EXEC UpdateEmployeeDetails 1, 'Sanskar Kakde', 'M', '2002-02-14', 2;
Go;
/*b) Create a Procedure to get the employee information bypassing the employee gender and department id from the Employee table*/

CREATE PROCEDURE GetEmployeeByGenderAndDeptId
    @Gender CHAR(1),
    @DeptId INT
AS
BEGIN
    SELECT Employee_id, Employee_name, Employee_gender, Employee_DOB, Department_id
    FROM Employee
    WHERE Employee_gender = @Gender AND Department_id = @DeptId;
END;

EXEC GetEmployeeByGenderAndDeptId 'F', 1;
Go;

/*c) Create a Procedure to get the Count of Employee based on Gender(input)*/
CREATE PROCEDURE GetEmployeeCountByGender
    @Gender CHAR(1)
AS
BEGIN
    SELECT COUNT(*) AS EmployeeCount
    FROM Employee
    WHERE Employee_gender = @Gender;
END;

EXEC GetEmployeeCountByGender 'M';



  /*Q3 ).... Create a user Defined function to calculate the TotalPrice based on productid and Quantity Products Table*/
  -- Calculate total price by multiplying the list_price by quantity
    SELECT @total_price = list_price * @quantity
    FROM production.products
    WHERE product_id = @product_id;

    RETURN @total_price;
END;


SELECT dbo.CalculateTotalPrice(1, 5) AS TotalPrice;

/*Q4)... create a function that returns all orders for a specific customer, including details such as OrderID, OrderDate, and the total amount of each order.*/
CREATE FUNCTION dbo.GetCustomerOrders (
    @customer_id INT
)
RETURNS TABLE
AS
RETURN
(
    SELECT 
        o.order_id,
        o.order_date,
        SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS total_amount
    FROM sales.orders o
    INNER JOIN sales.order_items oi ON o.order_id = oi.order_id
    WHERE o.customer_id = @customer_id
    GROUP BY o.order_id, o.order_date
);

SELECT *
FROM dbo.GetCustomerOrders(1);  -- Retrieves all orders for the customer with customer_id = 1




/*Q5...create a Multistatement table valued function that calculates the total sales for each product, considering quantity and price.*/

CREATE FUNCTION dbo.CalculateTotalSalesPerProduct ()
RETURNS @TotalSales TABLE (
    product_id INT,
    product_name VARCHAR(255),
    total_sales DECIMAL(10, 2)
)
AS
BEGIN
    -- Insert the calculated total sales for each product into the return table
    INSERT INTO @TotalSales (product_id, product_name, total_sales)
    SELECT 
        p.product_id,
        p.product_name,
        SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS total_sales
    FROM production.products p
    INNER JOIN sales.order_items oi ON p.product_id = oi.product_id
    GROUP BY p.product_id, p.product_name;

    -- Return the table with the calculated total sales
    RETURN;
END;
SELECT *
FROM dbo.CalculateTotalSalesPerProduct();



/*Q6)..create a  multi-statement table-valued function that lists all customers along with the total amount they have spent on orders.*/

CREATE FUNCTION dbo.GetCustomerTotalSpent ()
RETURNS @CustomerTotalSpent TABLE (
    customer_id INT,
    full_name VARCHAR(255),
    total_spent DECIMAL(10, 2)
)
AS
BEGIN
    -- Insert the calculated total amount spent by each customer into the return table
    INSERT INTO @CustomerTotalSpent (customer_id, full_name, total_spent)
    SELECT 
        c.customer_id,
        CONCAT(c.first_name, ' ', c.last_name) AS full_name,
        ISNULL(SUM(oi.quantity * oi.list_price * (1 - oi.discount)), 0) AS total_spent
    FROM sales.customers c
    LEFT JOIN sales.orders o ON c.customer_id = o.customer_id
    LEFT JOIN sales.order_items oi ON o.order_id = oi.order_id
    GROUP BY c.customer_id, c.first_name, c.last_name;

    -- Return the table with the calculated total amounts
    RETURN;
END;
SELECT *
FROM dbo.CalculateTotalSalesPerProduct();