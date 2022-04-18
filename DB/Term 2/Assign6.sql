-- Course Name: DBMS 01
-- Course Section: 2
-- Name: Chris Snyder
-- StudNo: 0296495


CLEAR;

-- Question 1

SELECT COUNT(DISTINCT Title) AS "# Of Titles"
FROM Salesreps;

-- Question 2


SELECT DISTINCT Salesrep
FROM Salesreps, Customers
WHERE Salesrep = Custrep AND COMPANY IN (SELECT Company FROM Customers WHERE Company IN ('JCP Inc', 'ZetaCorp'));

-- Note: Should select only distinct answers, but the data itself appears to only have one rep-customer instance of each for zeta and jcp anyway...



-- Question 3



SELECT Rep, MIN(Amount) AS "Min Sales", MAX(Amount) AS "Max Sales", AVG(Amount)AS "Avg Sales"
FROM  Orders
WHERE OrderDate BETWEEN '1990-01-01' AND '1990-12-31'
GROUP BY Rep
ORDER BY Rep;



-- Question 4

SELECT DISTINCT Company, Name, COUNT(OrderNum) AS "Total Orders For Customer"
FROM Orders, Customers, Salesreps
WHERE Rep = CustRep AND Salesrep = Rep
GROUP BY Company, Name
ORDER BY Company, Name;


-- Question 5

SELECT Office, AVG(Quota) AS "Average Quota"
FROM Offices, Salesreps
WHERE Mgr = (Select Salesrep FROM Salesreps WHERE Name = 'Larry Finch')
GROUP BY Office;


-- Question 6

SELECT Salesrep, Name
FROM Salesreps
WHERE Salesrep IN (SELECT Custrep FROM Customers WHERE CreditLimit <= 500.00);


-- Question 7


SELECT Product, Mfr
FROM Products
WHERE NOT EXISTS (SELECT Product FROM Orders);


-- Question 8

SELECT RPAD(Product,7,'-') AS "Product", REPLACE (Mfr,'IMM', 'Immediate Manufacturer') AS "Manufacturer"
FROM Products
WHERE SOUNDEX(Mfr)
         = SOUNDEX('iam');
         
         -- Note for 8: Seems that there is a space at the end of each product entry, meaning padding to 7 chars only needs to add 2 dashes to the end.
        
-- Question 9

SELECT Salesrep, Name
FROM Salesreps
ORDER BY SUBSTR(Name,INSTR(NAME, ' ')) asc;


-- Question 10

SELECT Ordernum, Company, NEXT_DAY(OrderDate +3, 'Tuesday') AS "Call Date"
FROM Orders, Customers
WHERE Rep = Custrep AND Rep = '109'
ORDER BY OrderDate ASC;
