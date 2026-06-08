# 🚗 Console Car Game — C# OOP Project

A console-based car racing game built in **C#** using a **3-Tier Architecture** and full **Object-Oriented Programming** principles. The game tracks player scores and stores them in a **MySQL database**, making it a complete end-to-end academic project.

Built as a university project for the **OOP course** at **UET (University of Engineering and Technology)** — Semester 2, BS CS (Gaming & Animation).

---

## 🎮 Gameplay

- The player controls a car moving on a console-rendered road
- Obstacles appear and must be avoided
- The player's **name is collected at the start** (letters only — validated)
- The **score increases** as the game progresses
- Final score is **saved to a MySQL database** automatically

---

## 🏗️ Architecture — 3-Tier Design

The project is structured into **three separate layers**, each with its own responsibility:

| Layer | Folder/Files | Responsibility |
|-------|-------------|----------------|
| **Presentation Layer (UI)** | `Program.cs` | Entry point, displays menus, handles input/output |
| **Business Logic Layer (BL)** | `GameEngine.cs` | Core game loop, collision detection, score logic |
| **Data Access Layer (DL)** | `DatabaseManager.cs` | MySQL connection, saving & retrieving scores |

Supporting classes:
- `Car.cs` — Player car object (position, movement)
- `Obstacle.cs` — Obstacle objects on the road
- `Road.cs` — Road/boundary rendering
- `ScoreRecord.cs` — Model class representing a score entry

---

## 🧠 OOP Concepts Applied

- ✅ **Encapsulation** — Private fields with controlled access
- ✅ **Classes & Objects** — Car, Obstacle, Road, ScoreRecord
- ✅ **Constructors** — Initializing game objects with default state
- ✅ **Separation of Concerns** — 3-tier architecture enforcing single responsibility
- ✅ **Modularity** — Each class in its own separate file

---

## 🗄️ Database Integration

- **Database:** MySQL
- Scores are saved with player name and final score after each game session
- Connection uses a full connection string including host, port, database name, username, and password

**Table structure:**
```sql
CREATE TABLE scores (
    id INT AUTO_INCREMENT PRIMARY KEY,
    player_name VARCHAR(50),
    score INT
);
```

---

## 🛠️ Technologies Used

- **Language:** C#
- **IDE:** Visual Studio
- **Framework:** .NET (Console Application)
- **Database:** MySQL
- **Library:** MySql.Data (MySQL Connector for .NET)

---

## 🚀 How to Run

1. Clone the repository:
   ```bash
   git clone https://github.com/abduu-codes/OOP_game.git
   ```

2. Open the project in **Visual Studio**.

3. Install the MySQL NuGet package if not already installed:
   ```
   Tools → NuGet Package Manager → Manage NuGet Packages
   Search: MySql.Data → Install
   ```

4. Set up your MySQL database:
   ```sql
   CREATE DATABASE car_game;
   USE car_game;
   CREATE TABLE scores (
       id INT AUTO_INCREMENT PRIMARY KEY,
       player_name VARCHAR(50),
       score INT
   );
   ```

5. Update the connection string in `DatabaseManager.cs` with your MySQL credentials:
   ```csharp
   string connectionString = "server=localhost;port=3306;database=car_game;user=root;password=yourpassword;";
   ```

6. Press `F5` to build and run.

---

## 📁 Project Structure

```
OOP_game/
└── game/
    ├── Program.cs           # Entry point & UI layer
    ├── GameEngine.cs        # Business logic & game loop
    ├── Car.cs               # Player car class
    ├── Obstacle.cs          # Obstacle class
    ├── Road.cs              # Road rendering class
    ├── ScoreRecord.cs       # Score model class
    └── DatabaseManager.cs   # Data access layer (MySQL)
```

---

## 👤 Author

**Abduu**
- GitHub: [@abduu-codes](https://github.com/abduu-codes)
- University: UET — BS CS (Gaming & Animation), Semester 2

---

## 📝 License

This project is for **educational purposes** only.
