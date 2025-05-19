
# 🔒 Blocked Countries API

> A .NET Core Web API for managing blocked countries and checking IP geolocation using in-memory storage.

---

## 📋 Description

This project implements a **.NET Core Web API** to manage a list of blocked countries and validate IP addresses using third-party geolocation APIs (e.g., [ipapi.co](https://ipapi.co) or [IPGeolocation.io](https://ipgeolocation.io)).

- ✅ No database used — all data is stored in memory using `ConcurrentDictionary`.
- ✅ Supports blocking countries permanently or temporarily.
- ✅ Background service automatically removes expired temporary blocks.
- ✅ Logs all blocked attempts with IP, timestamp, country code, and user agent.
- ✅ Swagger UI for full API documentation and testing.


---

## 🚀 How to Run

1. **Clone the repository (if applicable)**:
   ```bash
   git clone https://github.com/yourusername/blocked-countries-api.git
   ```

2. **Navigate to the project directory**:
   ```bash
   cd blocked-countries-api
   ```

3. **Restore packages**:
   ```bash
   dotnet restore
   ```

4. **Build the project**:
   ```bash
   dotnet build
   ```

5. **Run the application**:
   ```bash
   dotnet run
   ```

6. **Open in browser**:
   - Visit: `https://localhost:5001/swagger` to view and test the API endpoints.

---

## 🔐 Configuration

Update the `appsettings.json` file with your API key:

```json
{
  "IpGeoApi": {
    "BaseUrl": "https://ipapi.co",
    "ApiKey": "YOUR_API_KEY_HERE"
  }
}
```

> ⚠️ If you're using a different service like IPGeolocation.io, update the URL and keys accordingly.

---

## 🧪 Available Endpoints

| # | Endpoint | Description |
|---|----------|-------------|
| 1 | `POST /api/countries/block` | Add a country to the blocked list |
| 2 | `DELETE /api/countries/block/{countryCode}` | Remove a country from the blocked list |
| 3 | `GET /api/countries/blocked` | Get paginated list of blocked countries with search/filter |
| 4 | `GET /api/ip/lookup?ipAddress={ip}` | Get country info for an IP address |
| 5 | `GET /api/ip/check-block` | Check if current caller's country is blocked |
| 6 | `GET /api/logs/blocked-attempts` | Get log of blocked attempts |
| 7 | `POST /api/countries/temporal-block` | Temporarily block a country for a set duration |

---

## 📦 Project Structure

```
BlockedCountriesApi/
│
├── Controllers/               # API endpoints
├── Models/                    # Data models
├── Services/                  # Business logic
├── Storage/                   # In-memory storage
├── BackgroundTasks/           # Temporal block cleaner
├── Helpers/                   # Utility functions
├── Middleware/                # IP blocking middleware (optional)
├── Properties/                # appsettings.json
└── Program.cs                 # Entry point
```

---

## 📄 Swagger Documentation

- ✔️ Full Swagger integration for API documentation.
- ✔️ Interactive UI for testing endpoints directly.
- Access at: `https://localhost:7288/swagger/index.html`
 __________________________________________________
 ![image](https://github.com/user-attachments/assets/258e2b19-7f51-47db-904f-ab75b8e8d55f)

![image](https://github.com/user-attachments/assets/e390cc45-7f90-4dca-99a0-8175c589552b)



---

## ✅ Key Features

| Feature | Status |
|--------|--------|
| Pagination | ✅ Implemented |
| Filtering/Search | ✅ Implemented |
| Thread-safe Storage | ✅ Using `ConcurrentDictionary` |
| Temporal Blocking | ✅ With background cleanup |
| IP Auto-Detection | ✅ From HttpContext |
| Validation | ✅ Country code, IP format, duration limits |
| Middleware | ✅ Optional IP-based access restriction |

---

## 🙏 Developed by

- **Hagar Atia**
- **Contact**: shamsatia96@gmail.com
- **Date**: April 2025

---

