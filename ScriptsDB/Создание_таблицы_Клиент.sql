-- Создание таблицы Client
CREATE TABLE Client (
    RegistrNum INT IDENTITY(1,1) PRIMARY KEY,
    Surname VARCHAR(255) NOT NULL,
    [Name] VARCHAR(255) NOT NULL,
    Patronymic VARCHAR(255),
    Gender VARCHAR(10) CHECK (Gender = 'муж' OR Gender = 'жен'),
    Birthday DATE,
    [Height] INT CHECK (Height >= 0 AND Height <= 250),
    [Weight] INT CHECK ([Weight] >= 0),
    Country VARCHAR(50),
    City VARCHAR(50),
    Children BIT,
    [Description] TEXT,
    RegistrDate DATE,
    ZodiacSignID INT,
    EducationID INT,
    StatusID INT,
    Flag BIT,
    [Login] VARCHAR(100) UNIQUE NOT NULL,
    Pwd VARCHAR(55) NOT NULL
    FOREIGN KEY (ZodiacSignID) REFERENCES ZodiacSign(ZodiacSignID),
    FOREIGN KEY (EducationID) REFERENCES Education(EducationID),
    FOREIGN KEY (StatusID) REFERENCES Status(StatusID)
);
--Flag: 0 - если не в черном списке, 1 - если в черном списке
