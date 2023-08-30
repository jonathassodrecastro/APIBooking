# API Booking Application

This is a sample API Booking application built using ASP.NET Core.

## Table of Contents

- [Description](#description)
- [Features](#features)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
- [Usage](#usage)
- [API Endpoints](#api-endpoints)
- [Contributing](#contributing)
- [License](#license)

## Description

The API Booking application is designed to manage reservations for houses. It provides endpoints to register, update, delete, and retrieve reservations, clients, and houses.

## Features

- Register new reservations
- Update existing reservations
- Delete reservations
- Retrieve a list of all reservations
- Retrieve details of a specific reservation
- Register new clients
- Delete clients
- Register new houses
- Retrieve details of a specific house
- Update house information
- Delete houses

## Getting Started

### Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/)
- Code editor (e.g., Visual Studio Code, Visual Studio)

### Installation

1. Clone this repository to your local machine:
  git clone <repository_url>

2. Navigate to the project directory:
  cd APIBooking

3. Restore the project dependencies:
  dotnet restore

4. Create and update the `appsettings.json` file with your PostgreSQL connection details.

5. Apply migrations to create the database tables:
  dotnet ef database update

6. Build and run the application:
dotnet run


## Usage

Once the application is running, you can access the API endpoints using a tool like Swagger.
![image](https://github.com/jonathassodrecastro/APIBooking/assets/62815490/df9d7862-c2da-48a5-9d65-a379d10936fe)



## API Endpoints

- **ReservationController**
- `POST /RegisterReservation`: Register a new reservation.
- `GET /FindReservationByID/{id}`: Retrieve details of a reservation by ID.
- `GET /GetAllReservations`: Retrieve a list of all reservations.
- `DELETE /DeleteReservation/{id}`: Delete a reservation by ID.
- `PUT /UpdateReservation/{id}`: Update a reservation by ID.

- **ClientController**
- `POST /RegisterClient`: Register a new client.
- `DELETE /DeleteClient/{id}`: Delete a client by ID.

- **HouseController**
- `POST /RegisterHouse`: Register a new house.
- `GET /FindHouse/{id}`: Retrieve details of a house by ID.
- `GET /GetAllHouses`: Retrieve a list of all houses.
- `DELETE /DeleteHouse/{id}`: Delete a house by ID.
- `PUT /UpdateHouse/{id}`: Update house information by ID.

## Contributing

Contributions are welcome! If you find any issues or want to add new features, feel free to submit a pull request.

## License

This project is licensed under the [MIT License](LICENSE).

