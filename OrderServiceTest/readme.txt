1. Скачать проект с гит: git clone {url}
2. Настроить подключение к БД в файле appsettings.json (DbConnectingString)
3. Собрать приложение командой dotnet build (для разработки использовался net6.0)
4. Запустить сервис: OrderServiceTest.exe
5. Сервис релиазует следующее апи, согласно ТЗ:

POST: {hostname}/orders

GET: {hostname}/orders/{orderId}

DELETE: {hostname}/orders/{orderId}

PUT: {hostname}/orders/{orderId}