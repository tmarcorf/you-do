# 📝 You-Do
A simple To-Do app to help you manage and organize your daily tasks.

## Tech Stack
- Front-end: Angular 
- Back-end: .NET
- Database: PostgreSQL
- Infra: Docker

## 🚀 Running the App with Docker

To run the You-Do app using Docker, follow these steps:

### 1. Create a `.env` file
Create a `.env` file in the root of your project with the following environment variables:

```plaintext
DB_CONNECTION_STRING=yourConnectionsString
POSTGRES_PASSWORD=yourPassword
POSTGRES_USER=yourUser
POSTGRES_DB=yourDatabaseName
YOU_DO_SECRET_KEY=yourJwtSecretKey
```

Replace yourConnectionsString, yourPassword, yourUser, yourDatabaseName, and yourJwtSecretKey with your actual database connection details and secret key.

### 2. Build and Run the Docker Containers
Run the following command to build and start the Docker containers:

```bash
docker-compose up -d --build
```
This command will:

- Build the Docker images for the back-end and database.
- Start the PostgreSQL database.
- Run the .NET back-end API.

### 3. Access the Application
Once the containers are up and running, you can access the application:

- Back-end API: The .NET back-end API will be available at http://localhost:7065/swagger/index.html.
- Database: The PostgreSQL database will be accessible at localhost:5432.

### 4. Stopping the Containers
To stop the running containers, use the following command:

```bash
docker-compose down
```
This will stop and remove the containers, but it will retain the data in the PostgreSQL database.

### 5. (Optional) Cleaning Up
If you want to remove the Docker volumes (including the database data), you can run:

```bash
docker-compose down -v
```

### 📄 License
This project is licensed under the MIT License - see the LICENSE section for details.
