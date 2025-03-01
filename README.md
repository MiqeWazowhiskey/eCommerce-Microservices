# Ecommerce Microservice

This project is a collection of microservices for an eCommerce platform, built using .NET. The services include **Basket**, **Discount**, **Catalog**, and **Order** services. These services work together to enable functionalities such as managing a product catalog, storing products in a user's basket, applying discounts, and processing orders.

## Architecture Overview

- **Basket Service**: Manages the products in the user's basket, storing information about items added for purchase.
- **Discount Service**: Handles applying discounts to baskets based on various criteria (e.g., promotional codes).
- **Catalog Service**: Stores and retrieves product data such as name, description, price, and availability.
- **Order Service**: Processes orders, tracks the status of each order, and manages order data.


### Design Patterns Used

- **Repository Pattern**: Used in the Basket service to abstract data access logic from business logic. This separation allows for easier maintenance and testing of the data access layer while ensuring that each service manages its own data access independently.

- **Decorator Pattern**: The **Cache Aside** pattern is implemented using the Decorator Pattern with Redis. It ensures that data is loaded into the cache when requested, and only fetched from the database if it’s not available in the cache. This improves performance by reducing the number of database queries for frequently accessed data.

- **CQRS (Command Query Responsibility Segregation)**: General requests are handled using **Command** and **Query** objects. This pattern allows the separation of read and write operations, which improves scalability and performance by enabling independent optimization of query and command processing.

- **DDD (Domain-Driven Design)**: The **Ordering Service** will follow Domain-Driven Design principles to model the domain and encapsulate business logic into domain entities, value objects, and aggregates. It will utilize rich domain models to manage business complexity effectively.

- **Clean Architecture**: The **Ordering Service** will follow Clean Architecture principles, organizing code into separate layers. This architecture promotes maintainability and testability by ensuring that each layer has a single responsibility and is decoupled from the others.

## Technologies Used

- **Redis**: In-memory cache to improve performance by storing frequently accessed data.
- **Scrutor**: Facilitates clean service registration by scanning assemblies and adding services to the container.
- **gRPC**: Provides high-performance, low-latency communication between services for synchronous communication.
- **SQLite** & **PostgreSQL**: Used for database management (SQLite for local development and PostgreSQL for production).
- **EntityFramework Core**: Simplifies object-relational mapping (ORM) for interactions with databases.
- **Carter**: A minimalistic API framework used to build fast and simple REST APIs.
- **Marten**: A document database for efficient data storage and retrieval.
- **Mapster**: Automates mapping, simplifying data transformation across layers.
- **FluentValidation**: Validates incoming requests to ensure data integrity before processing.

## Project Structure

- **Basket Service**: Manages the user's shopping basket.
- **Discount Service**: Calculates and applies discounts based on basket data.
- **Catalog Service**: Provides product information.
- **Order Service**: Manages order creation and tracking.

## gRPC Communication

Services communicate synchronously using gRPC. Communication is defined by the `.proto` files located in the `Proto` folder. These files define the message formats and services available for inter-service communication.

[Tutorial I followed](https://www.udemy.com/course/microservices-architecture-and-implementation-on-dotnet/)

## TODO

- [ ] **Soft Delete**: Implement a soft delete mechanism for entities that should not be permanently removed but marked as deleted.
- [ ] **Ordering Services**: Implement the full ordering workflow, including order processing, tracking, and notifications.
- [ ] **RabbitMQ**: Implement message queuing for asynchronous communication between services using RabbitMQ.
- [ ] **Rate Limiting**: Add rate limiting to restrict the number of requests a user can make within a time period.
