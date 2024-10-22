Create PROCEDURE useDisplayMessage
AS
BEGIN 
PRINT 'hello'
END;

EXECUTE useDisplayMessage

--B

SELECT * FROM production.products;
Go;
CREATE proc uspProductList
AS
BEGIN
select Product_name,list_price from production.products
order by product_name
END

drop procedure uspProductList

exec uspProductList;

sp_help uspProductList

-- Alter procedure
Go;
ALTER PROC uspProductList
AS
BEGIN 
select Product_name,model_year, list_price from 
production.products order by list_price desc
END

-- To Show
uspProductList

exec sp_rename 'uspProductList' , 'uspMyProductList'

-- Parameter is stored procedure
--input and output parameter
Go;
CREATE PROC uspFindProducts (@modelyear as int)
AS
Begin 
SELECT * FROM production.products where model_year = @modelyear
END

--Ex4
-- using named parameters 
Go;
create proc uspFindProductsByName
(@minPrice as decimal = 2000,@maxPrice decimal, @name as varchar(max))
AS
BEGIN
select * from production.products where list_price>=@minPrice and list_price<=@maxPrice
and 
product_name like '%'+@name+'%'
END

uspFindProductsByName 100,1000,'Sun'
Go;
-- Optional parameter
ALTER PROC uspFindProductsByName
(@minPrice as decimal = 2000, @maxPrice decimal, @name as varchar(max))
AS
BEGIN
select * from production.products where list_price>=@maxPrice and 
list_price<=@maxPrice
and 
product_name like '%'+@name+'%'
END

uspFindProductsByName 100,1000,'Sun'
uspFindProductsByName @maxPrice = 3000, @name = 'Trek'
Go;
-- Out parameter
CREATE PROCEDURE uspFindProductCountByModelYear
(@modelyear int,
@productCount int OUTPUT)
AS
BEGIN
select Product_name, list_Price
from production.products
WHERE 
model_year = @modelyear

select @productCount = @@ROWCOUNT
END

DECLARE @count int;
EXEC uspFindProductCountByModelYear @modelyear = 2016,@productCount = @count OUT;
select @count as 'Number of Products Found'
go;
-- Can one stored procedure call another stored procedure 

CREATE PROC usp_GetAllCustomers
AS
BEGIN
select * from sales.customers
END

usp_GetAllCustomers

Go;
CREATE PROC usp_getCustomerOrders
@customerId int 
AS
BEGIN
select * from sales.orders
WHERE 
customer_id = @customerId
END

usp_getCustomerOrders 1

-- to call one into another
ALTER PROC usp_GetCustomerData
@customerId int 
AS 
BEGIN
EXEC usp_GetAllCustomers;
EXEC usp_getCustomerOrders @customerId;
END
exec usp_GetCustomerData 1