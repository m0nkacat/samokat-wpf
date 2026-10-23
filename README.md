# 🛴 Samokat: служба доставки (WPF)

![C#](https://img.shields.io/badge/C%23-239120?logo=csharp&logoColor=white)
![WPF](https://img.shields.io/badge/WPF-.NET_Framework_4.7.2-512BD4)
![Entity Framework](https://img.shields.io/badge/Entity_Framework_6-68217A)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?logo=microsoftsqlserver&logoColor=white)

**RU** | [EN](#english)

Десктопное приложение для учёта службы доставки: клиенты, курьеры, транспорт и заказы.

## Возможности
- 👤 Клиенты: ФИО, телефон, адрес
- 🚴 Курьеры с привязкой к транспорту
- 🚚 Транспорт с грузоподъёмностью
- 📦 Заказы: клиент, курьер, вес, адрес доставки, статус, дата
- ✏️ Отдельные окна и диалоги добавления и редактирования для каждой сущности

## Стек
C# · WPF (XAML) · .NET Framework 4.7.2 · Entity Framework 6 (Database First) · SQL Server

## Запуск
1. Создай БД `samokat` в SQL Server и при необходимости поправь строку подключения в `App.config`.
2. Открой `SamokatAppClean.sln` в Visual Studio, восстанови NuGet-пакеты и запусти.

---

## English

A WPF desktop app for a delivery service: clients, couriers, vehicles and orders, with add/edit dialogs for each entity. Built with Entity Framework 6 (Database First) on SQL Server.

**Run:** create the `samokat` database, adjust `App.config` if needed, open `SamokatAppClean.sln` in Visual Studio and start.
