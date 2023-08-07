# Write your MySQL query statement below
SELECT name as Customers FROM Customers A LEFT JOIN Orders B on  A.Id = B.CustomerId WHERE B.CustomerId is NULL