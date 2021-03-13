
INSERT INTO [EmployeeAdmins] VALUES(

(SELECT [Id] FROM (
  SELECT
    ROW_NUMBER() OVER (ORDER BY [Id] ASC) AS rownumber,
    [Id]
  FROM [Employees]
) AS foo
WHERE rownumber = 2), 'admin1','admin1',123)




