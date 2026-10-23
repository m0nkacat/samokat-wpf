# samokat-wpf

Десктопное приложение для учёта службы доставки · *Desktop app for a delivery service*

`C#` `WPF` `.NET Framework 4.7.2` `Entity Framework 6 (Database First)` `SQL Server`

## Что сделано
- **4 раздела** с главного меню: клиенты, курьеры, транспорт, заказы
- **Таблица (DataGrid)** в каждом разделе: добавление, изменение, удаление
- **Поиск:**
  - клиенты и курьеры — по ФИО и телефону;
  - транспорт — по названию;
  - заказы — по адресу, статусу и имени клиента
- **Диалоги добавления и редактирования** с валидацией:
  - обязательные поля;
  - вес заказа и грузоподъёмность транспорта больше 0;
  - уникальный телефон курьера и уникальное название транспорта
- **Заказ:** дата, клиент, курьер, адрес доставки, вес и статус (`Создан`, `В пути`, `Доставлен`, `Отменён`)
- **Защита связанных данных:** при удалении клиента или курьера с заказами и транспорта, закреплённого за курьерами, приложение спрашивает подтверждение
- **Модель данных:** сгенерирована из готовой БД (EF6 Database First, `.edmx`)

## Структура
```
MainWindow.xaml(.cs)              # главное меню
ClientsWindow / ClientEditDialog       # клиенты: список, поиск, форма
CouriersWindow / CourierEditDialog     # курьеры: список, поиск, форма
TransportWindow / TransportEditDialog  # транспорт: список, поиск, форма
OrdersWindow / OrderEditDialog         # заказы: список, поиск, форма
SamokatEntities.edmx              # модель EF6, сгенерированная по БД
clients.cs, couriers.cs, transport.cs, orders.cs  # сгенерированные сущности
App.config                        # строка подключения samokatEntities
```

## Запуск
1. Создай БД `samokat` в SQL Server с таблицами `clients`, `couriers`, `transport`, `orders`.
2. При необходимости поправь строку подключения в `App.config` (по умолчанию `localhost\SQLEXPRESS`).
3. Открой `SamokatAppClean.sln` в Visual Studio, восстанови NuGet-пакеты и запусти.

---

## English

A WPF desktop app for running a delivery service.

### Features
- **4 sections** from the main menu: clients, couriers, vehicles, orders
- **DataGrid** in every section with add / edit / delete
- **Search**: clients and couriers by name and phone, vehicles by name, orders by address, status and client name
- **Add/edit dialogs with validation**: required fields, positive order weight and vehicle capacity, unique courier phone and vehicle name
- **Orders** have a date, client, courier, delivery address, weight and status (`Created`, `In transit`, `Delivered`, `Cancelled`)
- **Related-data safety**: deleting a client or courier with orders, or a vehicle used by couriers, asks for confirmation
- **Data model** generated from an existing database (EF6 Database First, `.edmx`)

### Structure
```
MainWindow                        # main menu
Clients/Couriers/Transport/OrdersWindow + *EditDialog  # list, search and form per entity
SamokatEntities.edmx              # EF6 model generated from the database
App.config                        # samokatEntities connection string
```

### Run
1. Create the `samokat` SQL Server database with `clients`, `couriers`, `transport`, `orders` tables.
2. Adjust the connection string in `App.config` if needed (default `localhost\SQLEXPRESS`).
3. Open `SamokatAppClean.sln` in Visual Studio, restore NuGet packages and run.
