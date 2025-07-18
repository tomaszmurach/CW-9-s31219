# Trip Management API – ASP.NET Core Web API

RESTful API for managing tourist trips and clients.  
Built using ASP.NET Core Web API and Entity Framework (database-first approach).

## Features
- GET `/api/trips` with:
  - Pagination (`pageNum`, `pageSize`)
  - Sorting by trip start date (`DateFrom`)
- DTO mapping with AutoMapper
- Clean separation of concerns (DbService layer)
- Swagger UI available for testing

## Technologies
- ASP.NET Core Web API (.NET 7)
- Entity Framework Core (database-first)
- SQL Server LocalDB (`APBD10`)
- AutoMapper
- Swagger

## How to Run
1. Clone the repo
2. Open in Rider or Visual Studio
3. Check SQL Server LocalDB instance (`APBD10`)
4. Run the project – Swagger UI will launch automatically

## Author
Tomasz Murach – [LinkedIn](https://www.linkedin.com/in/tomasz-murach-5698b6375/)
