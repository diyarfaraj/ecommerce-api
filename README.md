# ecommerce-api

Ecommerce-api is a .NET 7 RESTful API built to provide a platform for online shopping. This API is designed to be used with Docker or just run in debug mode in Visual Studio, making it easy to deploy and run in a variety of environments.

See [demo]https://ecommerce-client-diyar.azurewebsites.net/)

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

- [Docker](https://www.docker.com/products/docker-desktop)
- [Docker Compose](https://docs.docker.com/compose/install/)
- [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)

### Installation

1. Clone the repository to your local machine using `git clone https://github.com/diyarfaraj/ecommerce-api.git`

2. Navigate to the cloned directory using `cd ecommerce-api`

3. Build and run the Docker containers using the following command: `docker-compose up -d`

4. To stop the containers use the following command: `docker-compose down`


The API should now be running on `http://localhost:5000/api/`.

## Endpoints

The following is a list of available endpoints for the ecommerce-api:

- `GET /api/products` - Retrieve a list of all products
- `GET /api/products/{id}` - Retrieve a specific product by its ID
- `POST /api/products` - Create a new product
- `PUT /api/products/{id}` - Update an existing product
- `DELETE /api/products/{id}` - Delete a product

- `POST /api/account/login` - Login and retrieve the current user's information, including the user's basket
- `POST /api/account/register` - Register a new user
- `GET /api/account/currentUser` - Retrieve the information for the current user, including the user's basket
- `GET /api/account/savedAddress` - Retrieve the saved address for the current user


## Contributing

We welcome contributions to the ecommerce-api! If you have an idea for a new feature or have found a bug, please open an issue. If you would like to contribute code, fork the repository and submit a pull request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

