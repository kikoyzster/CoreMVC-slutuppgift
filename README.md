# Car Management System

## Overview
The **Car Management System** is a web application built using **ASP.NET Core MVC**. The application allows users to manage a collection of cars, register and login users, and perform CRUD operations on the car database. It also includes session management and user authentication via **ASP.NET Identity**.

---

## Features

### Car Management:
- View all cars in the system.
- Add new cars with brand, model, and year.
- Edit existing car information.
- Delete cars from the database.

### User Management:
- Register new users with email, password, and additional details such as first name, last name, and birthdate.
- Login and logout functionality.
- User session management with session timeout.

### Additional Features:
- Responsive design using Bootstrap.
- AJAX integration to dynamically load car lists without refreshing the page.
- Seeded data for initial setup.

---

## Technologies Used
- **ASP.NET Core MVC**: Web framework.
- **Entity Framework Core**: ORM for database interactions.
- **ASP.NET Identity**: User authentication and management.
- **SQL Server**: Database.
- **Bootstrap**: Frontend styling.
- **jQuery**: AJAX functionality.

---

## Installation

### Prerequisites:
1. **.NET SDK 6.0 or later**
2. **SQL Server**
3. **Visual Studio 2022** or any preferred IDE.

### Steps to Run:
1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/car-management-system.git
   ```
2. Navigate to the project directory:
   ```bash
   cd car-management-system
   ```
3. Update the `appsettings.json` file with your SQL Server connection string:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Your SQL Server Connection String Here"
     }
   }
   ```
4. Run database migrations (optional if using `EnsureCreated`):
   ```bash
   dotnet ef database update
   ```
5. Build and run the application:
   ```bash
   dotnet run
   ```
6. Open your browser and navigate to:
   ```
   http://localhost:5000
   ```

---

## Usage

### Access the Application:
- **Cars**: View, add, edit, and delete cars.
- **Users**: Register, login, and logout.

### AJAX Feature:
- Click the **Load Cars** button on the "Add New Car" page to dynamically fetch and display car data.

---

## Project Structure

### Key Files and Directories:
- **`Controllers/`**: Contains all controller logic (e.g., `CarController`, `AccountController`).
- **`Models/`**: Data models including `Car`, `ApplicationUser`, and `AppDbContext`.
- **`Views/`**: Razor views for rendering HTML (e.g., `Cars`, `Account` views).
- **`Services/`**: Contains service logic for `AccountService` and `CarService`.

---

## Contributions

Contributions are welcome! To contribute:
1. Fork the repository.
2. Create a new branch:
   ```bash
   git checkout -b feature-name
   ```
3. Commit your changes:
   ```bash
   git commit -m "Add feature description"
   ```
4. Push to the branch:
   ```bash
   git push origin feature-name
   ```
5. Open a pull request on GitHub.

---

## License

This project is licensed under the **MIT License**. Feel free to use and modify it as needed.

---

## Contact

For any questions or feedback, please reach out to:
- **Your Name**: [your.email@example.com](mailto:your.email@example.com)
- **GitHub**: [https://github.com/yourusername](https://github.com/yourusername)
