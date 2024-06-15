# Motorcycle Rental

Backend system for managing motorcycle rentals.

API built in .Net 8, following a Clean Architecture structure divided into 4 layers: Domain, Application, Infrastructure, and Presentation(API).

*This repository originates from the https://github.com/SilvaLealTests/MotorcycleRental*

## Main resources used:

- ORM with EntityFramework
- CQRS with MediatR
- Security (Authentication and Authorization) with AspNetCore Identity and JWT
- Testing with Xunit
- Logging (Console and file) with Serilog
- Image handling with AWS S3
- Message Broker with RabbitMQ
- Documentation with Swagger
- Containerization with Docker Compose and Docker
- Postgres Database

## Project Composition

- Postgres - Relational Database Server
- PGAdmin - DBMS for Postgres management
- RabbitMQ - Message Broker
- Worker - RabbitMQ Consumer
- Motorcycle Rental API

## Execution

#### With Docker installed, run the following command in the project root directory:

```bash
docker-compose up -d
```

#### This command will initialize 3 services:

- Postgres (localhost:5432)
- PGAdmin (http://localhost:8081)
- RabbitMQ. AMQP server (localhost:5672) and Dashboard(http://localhost:15672)

#### You can start the Worker and the API directly in Visual Studio or using the .Net CLI

##### API:

```bash
dotnet run --project .\src\MotorcycleRental.API\MotorcycleRental.API.csproj --urls="https://localhost:5001;http://localhost:5000"
```

*The API will be available at the URL https://localhost:5001/swagger and can be accessed through the browser of your choice.

##### Worker(RabbitMQ Consumer):

```bash
dotnet run --project .\src\MotorcycleRentMessageBrokerConsumer1\
```

## Migrations and Seeds

To facilitate execution, testing, and deployment, automatic database structure creation using Migrations has been implemented. The Seeds feature was also used to insert the following basic records:

- Rental Plans
- Users: Admin and Biker (Courier)
- Roles: Admin and Biker
- Two example motorcycle records

## Testing Execution
For testing, you can use the endpoint api/identities/login with one of these payloads:

- Admin User(Admin Role)
```bash
{
  "email": "admin@test.com",
  "password": "Password!1"
}
```

- Biker User(Biker Role)
```bash
{
  "email": "biker@test.com",
  "password": "Password!1"
}
```






