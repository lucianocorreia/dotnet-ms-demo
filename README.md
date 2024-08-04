# dotnet-ms-demo

## Overview
This project is a set of microservices built using .NET 8. The microservices communicate with each other using RabbitMQ and are containerized using Docker. The entire system is orchestrated using Kubernetes.

## Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/get-started)
- [Kubernetes](https://kubernetes.io/docs/tasks/tools/)
- [RabbitMQ](https://www.rabbitmq.com/download.html)

## Architecture
The project consists of multiple microservices, each responsible for a specific domain. The microservices communicate asynchronously using RabbitMQ.

### Microservices
- **Service AuctionsService**: Crud operations for auctions using postgres.

### Communication
- **RabbitMQ**: Used for message brokering between microservices.

### Containerization
- **Docker**: Each microservice is containerized using Docker.

### Orchestration
- **Kubernetes**: Used for deploying, scaling, and managing the containerized applications.

## Getting Started

### Clone the Repository
```sh
git clone https://github.com/lucianocorreia/dotnet-ms-demo.git
cd dotnet-ms-demo
