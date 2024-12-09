set search_path to itmo;
INSERT INTO "Courses" ("Name", "Mail", "Duration", "Description")
VALUES
('Python', 'test@example.com', '2024-12-06 12:00:00+00', 'A sample course for testing.');
insert into "Modules" ("Course_Id", "Video_Link", "Name") Values (1, 'Link_ForVideo', 'ВВод вывод данных');
insert into "Modules" ("Course_Id", "Video_Link", "Name") Values (1, 'Link_ForVideo', 'Условный оператор');
после пользака
insert into "UserModules" ("User_Id", "Module_Id") values (1,1);
миграция
dotnet ef database update --project Data --startup-project Backend