-- Таблица "Образование"
CREATE TABLE Education (
    EducationID INT IDENTITY(1,1) PRIMARY KEY,
    Title VARCHAR(255),
    CHECK (Title IN ('Начальное', 'Основное', 'Среднее', 'Среднее профессиональное', 'Высшее', 'Ученая степень'))
);
