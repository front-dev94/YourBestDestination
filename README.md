# Your Best Destination

Your Best Destination is a JSON REST web service for hotel search. The service has two API interfaces:

* CRUD interface for hotel data management.
* Search interface for hotel search based on user location and price.

## Project Structure

The project has a layered architecture (Onion) and the layer list is:

- API: This layer handles the HTTP requests and responses, and is responsible for the routing and handling of the API endpoints.
- Business Logic Layer (Services): This layer handles the business logic of the application. It contains the services that implement the core functionality of the application and is independent of the data access and presentation layers.
- Data Access Layer (Migrations, Repositories): This layer handles the data access and storage of the application. It contains the code that interacts with the database and is independent of the business logic and presentation layers.
- Domain (Interfaces, Models, Dtos, Extensions): This layer contains the interfaces and models that define the domain of the application. It's independent of the data access and presentation layers and defines the data structures and relationships that are used throughout the application.
- Infrastructure (Middlewares, Extensions): This layer contains the infrastructure and utility code that is used by the other layers. It includes the code for logging, caching, and other common functionality that is used throughout the application.
- Tests (Unit tests): This layer contains the unit tests for the application. It tests each layer of the application separately and is independent of the other layers.

## Getting Started

To run the application, you will need to have the .NET Core SDK and Docker installed.

1. Clone the repository

    ``` git clone https://github.com/front-dev94/YourBestDestination.git ```

2. Build the Docker image

    ``` docker-compose build ```

3. Start the container by running the command

    ``` docker-compose up ```

The application will be available on `https://localhost:7140`.

## Tests

To run the unit tests, navigate to the `Tests` folder and run the following command:

``` dotnet test ```

## Built With

* [.NET Core](https://dotnet.microsoft.com/)
* [Docker](https://www.docker.com/)
