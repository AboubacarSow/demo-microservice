# demo-microservice

This repository contains a **microservice demo project** built following the YouTube tutorial:

**Completely 🚀Master .NET 8 Microservices with Ocelot: implement Auth, Gateway, Caching & More🌟** by **Netcode-Hub**.

It demonstrates microservice architecture concepts including authentication, API gateway, caching, and shared utilities.

## Features

* **Multiple Microservices**:

  * **authentication** – User registration, login, and authentication logic.
  * **gateway** – API gateway with routing and middleware support using Ocelot.
  * **identity** – Identity management and user-related operations.
  * **weather** – Example domain service providing weather-related data.
  * **shared** – Reusable middlewares and utilities for all services.
* **REST API Endpoints**: Demonstrates CRUD operations and API communication.
* **Inter-service Communication**: Services communicate via HTTPS through the gateway.
* **Middleware Examples**: Logging, error handling, and basic rate limiting.

## Project Structure

```text
demo-microservice/
├─ authentication/       # Handles user auth (Controllers, Dtos, Models, Services)
├─ gateway/              # API gateway (Middlewares)
├─ identity/             # Identity service
├─ shared/               # Shared utilities and middlewares
└─ weather/              # Sample weather service
```

> Note: `bin` and `obj` folders are build artifacts and can be ignored in source control.

## Prerequisites

* [.NET 9 SDK](https://dotnet.microsoft.com/)
* [Postman](https://www.postman.com/) or similar tool for API testing

## Running Locally

1. Clone the repository:

```bash
git clone https://github.com/yourusername/demo-microservice.git
cd demo-microservice
```

2. Run individual services with HTTPS URLs:

```bash
cd authentication
dotnet run --urls https://localhost:7001

cd ../gateway
dotnet run --urls https://localhost:7000

cd ../identity
dotnet run --urls https://localhost:7002

cd ../weather
dotnet run --urls https://localhost:7003
```

3. Access APIs via the gateway or directly via each service endpoint.

4. (Optional) Use Docker Compose to run all services together if configured.


## Sample API Endpoints

| Service            | Endpoint               | Method | Description                         |
| ------------------ | ---------------------- | ------ | ----------------------------------- |
| **Authentication** | `/api/account/register`   | POST   | Register a new user                 |
| **Authentication** | `/api/account/login`      | POST   | Authenticate a user                 |
| **Identity**       | `/api/users`      | GET    | Get all users              |
| **Weather**        | `/api/weathe` | GET    | Get weather data            |
| **Gateway**        | `/api/account/register`   | POST   | Forwarded to authentication service |
| **Gateway**        | `/api/weather` | GET    | Forwarded to weather service        |

> All endpoints are served over HTTPS. Make sure your clients trust the development certificates.

## Notes

* This project follows the **Netcode-Hub tutorial** for implementing microservices with .NET and Ocelot.
* It includes authentication, gateway routing, caching,rate limiting.


