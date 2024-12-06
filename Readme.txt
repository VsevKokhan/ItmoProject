INSERT INTO "Courses" ("Name", "Mail", "Duration", "Description", "Link_For_Source")
VALUES
('Test Course', 'test@example.com', '2024-12-06 12:00:00+00', 'A sample course for testing.', 'https://example.com');
миграция
dotnet ef migrations add InitialCreate
