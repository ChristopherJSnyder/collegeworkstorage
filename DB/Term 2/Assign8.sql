-- Course Name: DBMS 01
-- Course Section: 2
-- Name: Chris Snyder
-- StudNo: 0296495
-- Date: November 29 2017

-- Part A
-- Question 1
CREATE VIEW RepCity AS
SELECT Name, City
FROM Salesreps, Offices
WHERE Office = Repoffice;


-- Question 2

CREATE VIEW CustOrders AS
SELECT Company AS "CustName", o.Product, Description, Qty, Amount AS "Value"
FROM Customers, Orders o, Products p
WHERE Custnum = Cust AND p.Product = o.Product;


-- Part B

---------------------------------------------------------
--Tables my account has access to:          Privileges given:
--1)ENGINEER                                Update, Delete
--2)MASTERTB                                None
---------------------------------------------------------
--Tables PUBLIC has access to:              Privileges given:
--1)MASTERTB                                Select

SELECT Column_Name, Data_Type, Data_Precision, Data_Scale, Char_Length, Nullable
FROM All_Tab_Columns
WHERE OWNER = 'BOB' AND Table_Name = 'ENGINEER';

SELECT Constraint_Name, Table_Name, Constraint_Type, Search_Condition
FROM All_Constraints
WHERE OWNER = 'BOB' AND Table_Name = 'ENGINEER';

CREATE TABLE ENGINEER
(
FIRSTN CHAR (25),
LASTN CHAR (35),
ENO NUMBER (11,0) NOT NULL,
SAL NUMBER (10, 2),
EPID NUMBER (17,0),
HIRED DATE, 

CONSTRAINT ENGINEP PRIMARY KEY(ENO),
CONSTRAINT CSAL CHECK (Sal < 55000),
CONSTRAINT EF FOREIGN KEY (EPID) REFERENCES (ENGINEP)
);
