INSERT INTO "Courses" ("Name", "Mail", "Duration", "Description")
VALUES
('Python', 'test@example.com', '2024-12-06 12:00:00+00', 'A sample course for testing.');
insert into "Modules" ("Course_Id", "Video_Name", "Name") Values (1, 'Vvod', 'ВВод вывод данных');
insert into "Modules" ("Course_Id", "Video_Name", "Name") Values (1, 'uslovn', 'Условный оператор');
insert into "Modules" ("Course_Id", "Video_Name", "Name") Values (1, 'tipy', 'Типы данных');
insert into "Modules" ("Course_Id", "Video_Name", "Name") Values (1, 'stringtype', 'Стрококвый тип данных');
insert into "Modules" ("Course_Id", "Video_Name", "Name") Values (1, 'listss', 'Списки');
insert into "Modules" ("Course_Id", "Video_Name", "Name") Values (1, 'listshard', 'Вложенные списки');
insert into "Modules" ("Course_Id", "Video_Name", "Name") Values (1, 'funs', 'Функции');
insert into "Modules" ("Course_Id", "Video_Name", "Name") Values (1, 'cycles', 'Циклы');
insert into "Modules" ("Course_Id", "Video_Name", "Name") Values (1, 'cortege', 'Кортежи');
insert into "Modules" ("Course_Id", "Video_Name", "Name") Values (1, 'bool', 'тип данных бул');

не заубдем все протестить и заодно добавить пользака

миграция
dotnet ef database update --project Data --startup-project Backend