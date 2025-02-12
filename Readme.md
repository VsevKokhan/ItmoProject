# Бэк сторона для приложения для микрообучения #

Всем привет!
Я решил нет ничего лучше, чем немного поработать над курсовой и представить ее как пет-проект.
Лучше всего - юзайте без миграций, сами все создававйте по следующей инстркуции:
Все должно заводиться с одного докер композа, но если вдруг вы хотите вручную создать бд (используя миграции) - следуйте простым инструкциям:
## Миграция ##
Если запускаем вручную, применяя миграцию:

проверяем appsettings.json - выставим локалхост, порт 5433 (в докеркомпоз пробрасываем порты для бд) и другие нужные нам параметры. Удалите postgredata (волум для бд, автоматически создаться новый)

применяем команду: dotnet ef database update --project Data --startup-project Backend

Если все ок:
вводим в пгадмине следующие запросы:

INSERT INTO "Courses" ("Name", "Duration", "Description")
VALUES
('Python', '2024-12-06 12:00:00+00', 'A sample course for testing.');
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

не забудем все протестить и заодно добавить пользака
На этом этапе все нужные таблицы заполнены, проверяйте!

Если делаем новую миграцию:

dotnet ef migrations add имя_миграции --project Data --startup-project Backend и только потом применем нужную миграцию.
## О самом приложении##

На что стоит взглянуть: система авторизации (юзаются обычные джвт без хранения в бд, так я сочёл лучшим решением, чтобы не усложнять жизнь если хотим иметь токены на разных браузерах(устройствах) ), грамотная упаковка в докер, архитектура самого веб-приложения. Возможно, некоторые моменты притянуты за уши (например можно было юзать только session_id для авторизации), но хотелось сделать все немного "поинтереснее".
