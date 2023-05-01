<<<<<<< Updated upstream:OrderServiceTest/readme.txt
1. Скачать проект с гит: git clone {url}
2. Настроить подключение к БД в файле appsettings.json (DbConnectingString)
=======
1. Скачать проект с гит: git clone https://github.com/meowkameow/orderService.git
2. Настроить подключение к БД в файле appsettings.json (DbConnectingString и DbSchemaName)
>>>>>>> Stashed changes:OrderServiceApp/readme.txt
3. Собрать приложение командой dotnet build (для разработки использовался net7.0)
4. Запустить сервис: OrderServiceTest.exe
5. Сервис релиазует следующее апи, согласно ТЗ:

POST: {hostname}/orders

GET: {hostname}/orders/{orderId}

DELETE: {hostname}/orders/{orderId}

PUT: {hostname}/orders/{orderId}
