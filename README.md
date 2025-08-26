# Support System

Support System is a web application for managing support tickets.  
It is built with **.NET 8 Web API** following **Clean Architecture**, and a client (Angular) to consume the API.

---

## Features
- Create, update, and close support tickets.  
- Role-based access (Admin, Manager, Client).  
- File attachments with tickets.  
- Notifications (Email/SMS).  
- Dashboard to track ticket status.  

---

## Architecture
- **Domain**: Core entities and business rules.  
- **Application**: Use cases, DTOs, interfaces, validation.  
- **Infrastructure**: Database (EF Core), external services (email, storage).  
- **Presentation (API)**: ASP.NET Core Web API.  
- **Client**: Frontend app consuming the API.  

---
