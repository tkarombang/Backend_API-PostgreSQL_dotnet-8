# Backend API - PostgreSQL with .NET 8

## 📋 Project Overview
This project is a backend API built with .NET 8 and PostgreSQL. It serves as a portfolio example showcasing CRUD operations, authentication, and API endpoint creation.

---

## 🛠️ Technologies Used
- **Framework:** .NET 8
- **Database:** PostgreSQL
- **Tools:** Visual Studio, Postman, Docker (optional)

---

## 🚀 Getting Started

### Prerequisites
1. **.NET SDK 8** - Download and install from [dotnet.microsoft.com](https://dotnet.microsoft.com/).
2. **PostgreSQL** - Download and install from [postgresql.org](https://www.postgresql.org/).
3. **IDE (e.g., Visual Studio)**.
4. **Postman** (optional for testing).

### Installation Steps
1. **Clone the Repository:**
   ```bash
   git clone https://github.com/yourusername/yourrepository.git
   cd yourrepository
   ```

2. **Set Up PostgreSQL Database:**
   - Create a new PostgreSQL database.
   - Update the `appsettings.json` file:
     ```json
     {
       "ConnectionStrings": {
         "DefaultConnection": "Host=localhost;Database=yourdbname;Username=yourusername;Password=yourpassword"
       }
     }
     ```

3. **Run Migrations:**
   ```bash
   dotnet ef database update
   ```

4. **Run the Application:**
   ```bash
   dotnet run
   ```

---

## 📚 Project Structure
```
|-- YourProjectName
    |-- Controllers
    |-- Models
    |-- Data
    |-- Services
    |-- Migrations
    |-- appsettings.json
    |-- Program.cs
    |-- Startup.cs
```

---

## 📋 API Endpoints
### Authentication
- **POST** `/api/auth/register` - Register a new user
- **POST** `/api/auth/login` - Authenticate a user

### CRUD Operations
- **GET** `/api/items` - Retrieve all items
- **GET** `/api/items/{id}` - Retrieve a specific item
- **POST** `/api/items` - Add a new item
- **PUT** `/api/items/{id}` - Update an existing item
- **DELETE** `/api/items/{id}` - Delete an item

---

## 🧪 Testing
- Use Postman to test API endpoints.
- Check logs in the console for debugging.

---

## 📄 API Documentation with Swagger
- **Swagger Integration:**
  - Swagger provides a user-friendly interface to explore and test API endpoints.
  - After running the application, access Swagger UI at:
    ```
    http://localhost:5020/swagger
    ```
  - It automatically generates interactive API documentation based on your code annotations.

---

## 🤝 Contributions
1. Fork the repository.
2. Create a feature branch.
3. Commit your changes.
4. Open a pull request.

---

## 📄 License
This project is licensed under the MIT License.

---

## 🧑‍💻 Author
**Muhammad Azwar Anas** - [LinkedIn](https://linkedin.com/in/muhanaz) | [GitHub](https://github.com/tkarombang)

