-- Part B: Queries and Views 

CLEAR;
-- Question 1:

SELECT Name, ROUND (MONTHS_BETWEEN(CURRENT_DATE, Hiredate), 2) AS "Months Of Service" 
FROM Salesreps;

-- Question 2:

SELECT TRANSLATE(Company, 'A B C','-') AS "Edited Company Name"
FROM Customers
WHERE INSTR (Company, 'A') > 0 OR INSTR(Company, 'C') > 0 OR INSTR(Company, 'E') > 0;


-- Question 3:

SELECT Custnum, Company 
FROM Customers
WHERE EXISTS (SELECT Cust FROM Orders WHERE OrderDate > '1990-10-01');


-- Question 4:

INSERT INTO Orders (Ordernum, Orderdate, Cust, Rep, Mfr, Product, Qty, Amount)
VALUES (115005, CURRENT_DATE, 2114, NULL, 'BIC', 'L14', 1, 3.95);


-- Question 5

UPDATE Offices
SET Target = Target * 0.85
WHERE Target > (SELECT AVG(Quota)FROM Salesreps);


-- Question 6

DELETE FROM Orders
WHERE Cust IS NULL OR NOT EXISTS (SELECT Custnum FROM Customers);


-- Question 7

CREATE VIEW EasternCusts (City, Custrep) AS
SELECT City, COUNT(Custrep) AS "CustomerCount"
FROM Offices, Customers
WHERE Region = 'Eastern' 
GROUP BY City;



-- Part C: 

-- Question 1:

SELECT Table_Name
FROM All_Tables
WHERE OWNER = 'ERIC';

-- Question 2

SELECT Column_Name, Data_Type, Data_Length, Data_Precision, Data_Scale, Char_Length, Column_ID
FROM All_Tab_Columns
WHERE Owner = 'ERIC';

-- Question 3

SELECT Constraint_Name, Constraint_Type AS "TYPE"
FROM All_Constraints
WHERE OWNER = 'ERIC';