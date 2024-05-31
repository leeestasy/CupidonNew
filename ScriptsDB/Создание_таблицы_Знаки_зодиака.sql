--Таблица "Знаки зодиака"
CREATE TABLE ZodiacSign (
    ZodiacSignID INT IDENTITY(1,1) PRIMARY KEY,
    Title VARCHAR(255),
    CHECK (Title IN ('Овен', 'Телец', 'Близнецы', 'Рак', 'Лев', 'Дева', 'Весы', 'Скорпион', 'Стрелец', 'Козерог', 'Водолей', 'Рыбы'))
);
