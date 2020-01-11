# АИС "Психея"

Автоматизирования информационная система "Психея" предназначена для авторматизации проведения тестирования пациентов в клинической психологии.
Имеются ряд тестов, которые не нуждаются в интерпретации. Система позволяет отказаться от бумажного тестирования во время приема пациентов.
Так же система имеет следующий функционал:
Ведение телефоного справочника организации.
Назначение время приема пациента.
Регистрация новых пациентов (редактирование данных уже существующих пациентов).
Проверка документов пациента в сторонних системах.

## Стек технологии

Для бекенда используется C# core 3.
Тест реалезованы через XUnit.
Для фронеда angular 8. С использованием следующих npm пакетов:
TODO: указать пакеты.
Для кеш хранилища используется оперативная память сервера(имеется возможно на переключения redis хранилища).
Брокер сообщений rabbitmq.

