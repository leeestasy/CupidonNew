-- Создание таблицы Employee
CREATE TABLE Employee (
    EmployeeID INT IDENTITY(1,1) PRIMARY KEY,
    Surname VARCHAR(255) NOT NULL,
    [Name] VARCHAR(255) NOT NULL,
    Patronymic VARCHAR(255),
    [Login] VARCHAR(50) UNIQUE NOT NULL,
    Pwd VARCHAR(50) NOT NULL,
    CONSTRAINT CHK_Login_Length CHECK (LEN(Login) <= 50),
    CONSTRAINT CHK_Pwd_Length CHECK (LEN(Pwd) <= 50)
);
