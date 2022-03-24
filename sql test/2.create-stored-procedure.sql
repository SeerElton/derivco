Use Northwind
Go

CREATE PROCEDURE pr_GetOrderSummary  
	@StartDate AS Date,
	@EndDate Date,
	@EmployeeID int,
	@CustomerID VARCHAR (10)
AS
	;with OrderDetailsCTE as (
		select 
		o.OrderID,
		c.CustomerID,
		e.EmployeeID,
		CONCAT(e.TitleOfCourtesy, ' ', e.FirstName, ' ', e.LastName) EmployeeFullName,
		s.CompanyName ShipperCompanyName,
		c.CompanyName CustomerCompanyName,
		od.Quantity,
		DATEADD(dd,0,DATEDIFF(dd,0,o.OrderDate)) Date, -- remove time from date to simply group by date
		o.Freight,
		od.ProductID,
		p.UnitPrice,
		p.UnitPrice * od.Quantity TotalValue
	from Orders o 
		join Shippers s on o.ShipVia = s.ShipperID
		join Customers c on c.CustomerID = o.CustomerID
		join Employees e on e.EmployeeID = o.EmployeeID
		join [Order Details] od on od.OrderID = o.OrderID
		join Products p on p.ProductID = od.ProductID 
	)
	select 
		temp.EmployeeFullName,
		temp.CustomerCompanyName,
		MAX(temp.ShipperCompanyName) ShipperCompanyName,
		COUNT(temp.OrderID) NumberOfOders,
		temp.Date, 
		SUM(temp.Freight) TotalFreightCost,
		COUNT(temp.ProductID) NumberOfDifferentProducts,
		SUM(temp.TotalValue) TotalOrderValue
	from OrderDetailsCTE temp
	where 
		(@EmployeeID is null OR (@EmployeeID is not null AND temp.EmployeeID = @EmployeeID)) AND
		(@CustomerID is null OR (@CustomerID is not null AND temp.CustomerID = @CustomerID)) AND
		(@StartDate is null OR (@StartDate is not null AND temp.Date >= @StartDate)) AND --i decided to make start and end date optional aswell
		(@EndDate is null OR (@EndDate is not null AND temp.Date <= @EndDate))
	group by temp.EmployeeFullName, temp.Date, temp.CustomerCompanyName
GO
