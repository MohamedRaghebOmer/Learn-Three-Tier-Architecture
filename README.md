# N-Tier Architecture Contacts Management Lab 🏗️

A professional-grade demonstration of the **n-Tier Architecture** pattern using C# and .NET. This project serves as a technical laboratory for building decoupled, maintainable, and secure database-driven applications.

## 🏛️ Architectural Design
The solution is strictly divided into multiple distinct layers (n-Tier) to ensure a professional separation of concerns. The primary strength of this project is the **multi-presentation capability**, where different UI technologies share the exact same logic and data layers.

### 1. Presentation Layer (Multiple Interfaces)
This project showcases versatility by providing two distinct user interfaces:

* **Windows Forms Version (`Contacts.WinForms`):** A rich desktop interface featuring a `DataGridView` for data management, image handling via `PictureBox`, and interactive forms for CRUD operations.
* **Console Application Version (`Contacts.ConsoleApp`):** A high-performance, command-line interface designed for efficiency. It features formatted text-based tables, colored menus, and robust input validation.

### 2. Business Logic Layer (`Contacts.Business`)
- Acts as the central brain and bridge between the UI and Data layers.
- Contains core logic, data validation rules, and business objects (`clsContact`, `clsCountry`).
- **Shared Logic:** This layer serves both the WinForms and Console applications simultaneously, ensuring consistent behavior and centralized maintenance.

### 3. Data Access Layer (`Contacts.Data`)
- Dedicated to database communication and CRUD operations.
- Implemented using **ADO.NET** with secure **SQL Parameterized Queries** to prevent SQL Injection.
- Handles direct interactions with SQL Server efficiently.

---

## 🛠️ Tech Stack & Patterns
- **Language:** C# 12.0 / .NET Framework 4.8
- **Database:** Microsoft SQL Server
- **Architectural Pattern:** N-Tier / 3-Tier Design
- **Data Access:** ADO.NET (Connected & Disconnected models)
- **UI Technologies:** WinForms (GDI+) & System.Console

## 🚀 Key Technical Features (The Power of This Repo)
- **UI Agnostic Design:** The ability to swap or run multiple UIs (Console & WinForms) without changing a single line of code in the Business or Data layers.
- **Data Integrity:** Strict validation within the Business Layer before any persistence occurs.
- **Secure Persistence:** Fully parameterized queries for all database interactions.
- **Clean Code & Scalability:** Designed to be easily extended (e.g., adding a Web API or WPF layer would require zero changes to the backend).

## ⚙️ Getting Started
1. **Database Setup:** Restore the provided `ContactsDB.bak` file to your local SQL Server instance.
2. **Configuration:** Update the connection string in `Contacts.Data/clsDataAccessSettings.cs` to match your local environment.
3. **Run:** Choose either `Contacts.WinForms` or `Contacts.ConsoleApp` as the startup project and run.

---

## ⚖️ License
This project is open-source and available under the MIT License.