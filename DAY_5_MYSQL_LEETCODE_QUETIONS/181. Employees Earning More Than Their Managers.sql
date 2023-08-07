# Write your MySQL query statement below

SELECT e2.name as Employee FROM Employee e1 INNER JOIN  Employee e2 ON e1.id = e2.managerID 
where e1.salary<e2.salary

