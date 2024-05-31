-- Создание таблицы Status
CREATE TABLE Status (
    StatusID INT IDENTITY(1,1) PRIMARY KEY,
    Title VARCHAR(255)
    CHECK (Title IN ('Холост', 'В браке', 'Разведен(а)', 'Вдовец/вдова', 'Сожительствует'))
);
