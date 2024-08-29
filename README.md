AuthorizationServer Swagger UI доступен по url: [https://localhost:7132/swagger/index.html](https://localhost:7086/swagger/index.html)

EventsWebApp Swagger UI доступен по url: [https://localhost:7059/swagger/index.html](https://localhost:7054/swagger/index.html)

В БД будет присутствовать готовый пользователь с
{
  "username": "Administrator",
  "password": "admin"
}

Его используем для получения JWT токенов.

Шаги для запуска:
1. Клонируем репозиторий
2. Открываем IDE
3. В настройках решения указать на одновременный запуск проектов EventsWebApp и AuthorizationServer.
4. В appsettings нужно обновить ConnectionString в обоих проектах для использования локальной БД в MS Sql Server.
5. В терминале вводим dotnet ef database update - должны подтянуться миграции. Если с миграциями проблемы - создаём новую: dotnet ef migrations add InitialCreate.
6. Билдим, запускаем.
7. Заходим по предоставленным выше ссылкам и тестируем.
