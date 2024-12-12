# Real Estate Advertisement Platform

An innovative platform designed to simplify the process of buying, selling, and renting real estate properties. Built using the latest web technologies, it offers a seamless and efficient experience for both users and administrators.

Projet Report : The project report includes software engineering diagrams (Use Case, Class, Sequence, UI Design) and requirements analysis (functional and non-functional requirements, system constraints). 
-

## **Note**
This project is a demo developed as part of a university course. It is designed to showcase foundational features and is open to further development and enhancement.

---

## **Features**

### **For Users:**
- **Browse Listings:** Explore a wide range of real estate advertisements with detailed descriptions, prices, and locations.
- **Advanced Search:** Filter properties by price, location, type, and more.
- **Favorites:** Save favorite properties for quick access later.

### **For Advertisers:**
- **Post Ads:** Create and manage property advertisements with ease.
- **Ad Management:** Edit or delete existing advertisements as needed.

### **For Administrators:**
- **Manage Listings:** Monitor and manage all platform advertisements.
- **User Management:** Oversee user registrations and permissions.

---

## **Tech Stack**

### **Backend:**
- **ASP.NET Core 7.0:** Provides high performance and flexibility using the MVC (Model-View-Controller) architecture.
- **Entity Framework Core:** An ORM (Object-Relational Mapper) to manage database operations with ease.
- **Microsoft SQL Server:** A robust relational database for storing all platform data.

### **Frontend:**
- **Bootstrap 5:** Ensures a clean, responsive, and modern design.
- **Razor Pages:** Simplifies server-side rendering for dynamic content.
- **JavaScript & jQuery:** Adds interactivity and dynamic functionality to the user interface.

### **Session Management:**
- ASP.NET Core **Session Middleware** ensures secure and efficient session handling.

---

## **Project Structure**

```
WAW
├── Controllers
├── Data
│   └── AppDbContext.cs
├── Migrations
├── Models
├── ViewModel
├── Views
├── appsettings.json
└── Program.cs
```

### Key Folders:
- **Controllers:** Handles the business logic and connects the UI with the backend.
- **Models:** Contains the data structures for the application.
- **Views:** Defines the user interface using Razor templates.
- **Data:** Manages the database context and migrations.

---

## **Database Design**

### Key Tables:
- **Users:** Stores user details and roles (e.g., Customers, Advertisers, Administrators).
- **Advertisements:** Holds all property advertisement data.
- **Sessions:** Manages active user sessions for secure access.

---

## **Setup Instructions**

1. **Prerequisites:**
   - Install **.NET SDK 7.0**.
   - Install **SQL Server**.

2. **Clone the Repository:**
   ```bash
   git clone https://github.com/yourusername/real-estate-platform.git
   cd real-estate-platform
   ```

3. **Configure the Database Connection:**
   - Update the connection string in `appsettings.json`:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Your-SQL-Server-Connection-String"
     }
     ```

4. **Apply Migrations:**
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

5. **Run the Application:**
   ```bash
   dotnet run
   ```

6. **Access the Platform:**
   - Open your browser and go to `http://localhost:5000`.

---

## **Future Enhancements**

- **Online Payment Integration:** Enable users to complete transactions securely.
- **AI-Powered Search:** Enhance property recommendations based on user preferences.
- **User Reviews and Ratings:** Allow users to rate and review properties and advertisers.

---

## **Contributions**
We welcome contributions! Please submit a pull request or open an issue to discuss your ideas.

---

## **License**
This project is licensed under the [MIT License](LICENSE).

---

**Developed with ❤️ using ASP.NET Core and modern web technologies!**
