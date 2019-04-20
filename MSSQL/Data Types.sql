
--Числовые типы
bit(1)
tinyint(1), smallint(2), int(4), bigint(8)
decimal(5-17)(precision(1-38), scale(0-precision)
numeric = decimal
smallmoney(4) = decimal(10,4)
money(8) = decimal(19,4)
float(4-8)(n =(0-58))
real(4) = float(24)
--Дата и время
Date(3)
Time(3-5) 
DateTime(8) 
DateTime2(6-8) 
SmallDateTime(от 4) 
DateTimeOffSet(10)
--Строковые типы данных
Char(1-8000) 1 символ 1 байт
VarChar(1-8000) до 2 гб VarChar(max)
NChar (1-4000) unicode
VarChar(1-4000) unicode VarChar(max)
--Бинарные типы
Binary(1-8000)
VarBinary(1-8000) до VarBinary(max)
--Остальные типы данных
UniqueIdentifier(16)
TimeStamp(8)
Cursor
Hierarchyid
SQL_Variant
XML до 2 гб
Table
Geography
Geometry