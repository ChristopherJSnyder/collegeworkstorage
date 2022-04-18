-- Course Name: DBMS 01
-- Course Section: 2
-- Name: Chris Snyder
-- StudNo: 0296495

-- Question 1
SELECT Description, Price 
FROM Products
ORDER BY Description DESC;

--Question 2
SELECT OrderNum, OrderDate, Cust, Qty
FROM Orders
WHERE Product = 'L12' OR Product = 'L14'
ORDER BY OrderDate ASC;

--Question 3
SELECT Name, Age, Hiredate
FROM SalesReps
WHERE RepOffice IN ('13', '30', '21');

-- Question 4
SELECT SalesRep, Name
FROM SalesReps
WHERE Manager IS NULL;

-- Question 5
SELECT Name
FROM SalesReps
WHERE NAME LIKE '% Clark';

-- Question 6
SELECT DISTINCT Title
FROM SalesReps
WHERE Title IS NOT NULL;
