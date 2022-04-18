-- Course Name: DBMS 01
-- Course Section: 2
-- Name: Chris Snyder
-- StudNo: 0296495



-- Question 1

SELECT Product, Ordernum, SUBSTR(Orderdate,1, 4) AS "Year", TO_CHAR(Amount,'L99G999D99MI',
   'NLS_NUMERIC_CHARACTERS = '',.''
   NLS_CURRENCY = ''$'' ') "Amount"
FROM Orders; 


-- Question 2

SELECT Company, CustNum, OrderNum, OrderDate, ADD_MONTHS(OrderDate, 2)AS "AccountOverdueDate"
FROM Customers, Orders
WHERE Custnum = Cust AND INSTR(Company, 'Corp') > 0 OR INSTR(Company, 'corp') > 0
ORDER BY CustNum;


-- Question 3

SELECT Custnum, CustRep NVL(CustRep, 'None')
FROM Customers;

