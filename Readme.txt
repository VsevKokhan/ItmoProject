INSERT INTO "Courses" ("Name", "Mail", "Duration", "Description", "Link_For_Source")
VALUES
('Python', 'test@example.com', '2024-12-06 12:00:00+00', 'A sample course for testing.', 'https://example.com');
insert into "Modules" ("Course_Id", "Video_Link", "Name") Values (1, 'Link_ForVideo', 'ВВод вывод данных');
insert into "Modules" ("Course_Id", "Video_Link", "Name") Values (1, 'Link_ForVideo', 'Условный оператор');
миграция
dotnet ef database update --project Data --startup-project Backend