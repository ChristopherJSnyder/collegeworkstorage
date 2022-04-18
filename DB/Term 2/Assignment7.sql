-- Course Name: DBMS 01
-- Course Section: 2
-- Name: Chris Snyder
-- StudNo: 0296495
-- Date: November 24 2017

SET AUTOCOMMIT on;

--Question 1

--INSERT INTO Offices (City, Office, Region, Sales)
--VALUES ('Winnipeg', 35, 'Central', 0);


-- Question 2

--INSERT INTO Salesreps (Age, Hiredate, Manager, Name, Quota, RepOffice, Sales, Salesrep, Title)
--VALUES (NULL, CURRENT_DATE, NULL, 'Chris Snyder', NULL, 35, 0, NULL, NULL);


-- Question 3

SELECT Salesrep, Sales 
FROM Salesreps
WHERE Title = 'Sales Rep';

DELETE FROM Salesreps
WHERE Salesrep IN (SELECT Salesrep FROM Salesreps WHERE Title = 'Sales Rep') AND Sales = 0 AND HIREDATE < '1987-01-01';


-- Question 4

UPDATE Salesreps
SET Age = 41, RepOffice = 11, Title = 'Senior VP', Manager = 106, Quota = 1000
WHERE Salesrep = 37;


-- Question 5

UPDATE Customers
SET Creditlimit = Creditlimit * 1.25
WHERE HAVING = (SELECT Amount FROM Orders WHERE COUNT(Amount) > 2 AND Amount <= 250);


-- Question 6

UPDATE Customers
SET Creditlimit = (SELECT MAX(AMOUNT) + 1000 FROM Orders)
WHERE CreditLimit < (SELECT MAX(AMOUNT) FROM Orders);
