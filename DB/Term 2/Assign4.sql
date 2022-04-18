-- Course Name: DBMS 01
-- Course Section: 2
-- Name: Chris Snyder
-- StudNo: 0296495
-- Date: Oct 10 2017

--Question 1

SELECT OrderNum, Orderdate, o.Mfr, o.Product, Description, Amount
FROM Orders o, Products p
WHERE p.Product = o.product AND o.Mfr = p.Mfr
ORDER BY OrderNum;


-- Question 2

SELECT OrderNum, Orderdate, Mfr, Product, Description, Amount
FROM Orders 
NATURAL JOIN Products 
ORDER BY OrderNum;

-- Question 3

 SELECT Products.Mfr, Products.Product, Custnum, Company, Price
FROM Orders  JOIN Products  
ON (Products.Mfr = Orders.Mfr AND Products.Product = Orders.Product)
JOIN Customers
ON (Customers.Custnum = Orders.Cust)
WHERE CustNum IN (2107, 2110, 2111, 2124) AND Price BETWEEN 5.00 AND 25.00
ORDER BY COMPANY ASC, Price;
-- Question 4

SELECT Rep, Name, Ordernum, OrderDate
FROM Orders o, SalesReps s
WHERE o.Rep = s.SalesRep AND SalesRep IS NULL;