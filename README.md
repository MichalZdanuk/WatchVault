# 🎬 WatchVault

---

## 📝 Description  

**WatchVault** is a **full-stack movie tracker app** built with **Angular**, **.NET Web API**, and **PostgreSQL**.  
It allows users to:
- Browse trending movies shared within the community  
- Search movies by title  
- Add movies to personal watchlists (*Watched | To Watch*)  
- Browse watch history
- Analyze their watching trends, patterns, and habits through insightful visualizations (plot, line, and pie charts)

All movie data is powered by the **[Simkl API](https://simkl.com/api/)** — a public and free movie database API.  
The backend wraps this external API through a controlled caching and proxy layer, ensuring efficient and stable data access.

> ⚠️ **Disclaimer**  
> This application was developed **for self-learning purposes only**.  
> Its primary goal was to help me **learn Angular fundamentals** and **practice backend development** — areas where I already feel comfortable, but wanted to improve through real project experience.  
> The app is **not intended for commercial distribution**.

---

## 🌟 Purpose & Business Usefulness  

The concept behind *WatchVault* is inspired by popular movie-tracking platforms such as *TV Time*.  
While simple in functionality, it demonstrates a realistic **MVP (Minimum Viable Product)** for:
- Personalized movie tracking and recommendations
- Visualizing and analyzing user watching habits, patterns, and runtime trends through charts and statistics
- Seamless integration of front-end and back-end technologies  
- Scalable and clean full-stack architecture  

Such an application could easily be extended to a **subscription-based** or **ad-supported** platform, offering value in content personalization and entertainment analytics.

---

## 🖼️ System Demonstration  

Below are screenshots showcasing key parts of the application.  
All screenshots can also be found in the [`/SystemDemonstration`](./SystemDemonstration) directory.  

### 🏠 Landing Page  
![Landing Page](https://github.com/user-attachments/assets/2d8077a0-adcd-4beb-b488-efbcb86378f9)

### 🔍 Search Movies  
![Search Movies](https://github.com/user-attachments/assets/5e76864a-a083-4dd4-a392-e3592908628d)

### 🔥 Trending Movies  
![Trending Movies](https://github.com/user-attachments/assets/5ea9e95f-0a56-473b-8242-ccd41b3e95c5)

### 🎬 Movie Details  
![Movie Details](https://github.com/user-attachments/assets/49a43a97-edc0-4d2f-a31e-8480ebc1eacd)

### 🎞️ Watchlist Overview  
![Watchlist Overview](https://github.com/user-attachments/assets/6d1ce526-0ff8-46ec-a6a1-1913921ff145)

### 🕒 Watch History
![Watch History Page](https://github.com/user-attachments/assets/30968798-e552-4735-939e-1390389df33e)

### 📊 Watchlist Analysis

![Watchlist analysis - bar chart (runtime minutes)](https://github.com/user-attachments/assets/9ee40c83-6e12-4ede-835d-208d0fa9122d)

![Watchlist analysis - line chart (runtime minutes)](https://github.com/user-attachments/assets/2e3b430d-1278-4571-9cb1-6968912452bd)

![Watchlist analysis - genre insights](https://github.com/user-attachments/assets/493a44dd-1941-461a-b2b2-395e0e93a2fd)

![Watchlist analysis - favorites insights](https://github.com/user-attachments/assets/5e82c4a4-9f39-4e26-8f4f-fdd3f243bd2e)

### 👤 Profile Page  
![Profile Page](https://github.com/user-attachments/assets/5856dd5c-6ce1-4b4e-9b98-5795f74d900c)

### 🧾 Register Page  
![Register Page](https://github.com/user-attachments/assets/5c521df7-f9b9-4622-b010-d55c431b2c90)

### 🔐 Login Page  
![Login Page](https://github.com/user-attachments/assets/264fe856-af5b-42f3-b829-883262f193a9)

---

## 🧰 Tech Stack  

### 🖥️ Backend  
- **.NET 8**
  - Minimal API  
  - Entity Framework Core  
  - MediatR  
  - FluentValidation
  - HealthChecks
  - RateLimiting
- **Redis** (caching layer)  
- **PostgreSQL**  
- **Keycloak** (authentication & identity provider)  
- **Azure Blob Storage**
- **[Simkl API](https://simkl.com/api/)** *(external movie data provider, integrated via API layer)* 

### 💻 Frontend  
- **Angular 20**
  - Angular Router  
  - Angular Material  
  - Bootstrap  
  - RxJS
  - ng2-charts, chart.js

### 🐳 Containerization  
- **Docker** for full-stack orchestration  

---

## 🏗️ Architecture  

This MVP follows a **Monolithic** structure with **strict separation of concerns**, adhering to **Clean Architecture** and **Domain-Driven Design (DDD)** principles.  
This approach ensures:
- Modularity  
- Scalability  
- Maintainability  

![WatchVault Architecture](https://github.com/user-attachments/assets/058de4db-713f-40a5-98f0-c67ef58a6195)  

*High-level architecture overview.*

Full technical documentation can be found in the [`/Docs`](./Docs) directory.  

---

## ⚙️ Key Technical Decisions  

The main goal was to deliver a **fully functional MVP** quickly while keeping it **flexible** for future improvements.  

- **Authentication & Authorization:**  
  Implemented using **Keycloak**, an open-source Identity Provider.  
  It securely manages user registration, login, and JWT-based authentication.  
  The API acts as a proxy between the client and Keycloak, ensuring security and simplicity.

- **Movie Data & External API:**  
  The app integrates the **Simkl API**, chosen for its reliable and feature-rich dataset including user recommendations.  
  The free plan has a **1,000-request daily limit**, which is mitigated through caching.

- **Caching Strategy:**  
  Implemented via **Redis** to store responses from the Simkl API.  
  - Sliding expiration: 30 minutes  
  - Absolute expiration: 60 minutes  
  This efficiently reduces redundant requests and external API usage.

---

## 📝 Swagger

![Swagger](https://github.com/user-attachments/assets/6ad5c898-9bb1-4545-bd00-918469e774f1)  

---

## ❤️‍🩹 Health checks

![HealthChecks](https://github.com/user-attachments/assets/c066088a-851f-4dc1-aa54-2e0fbfc989d2)  

Health checks monitor the status of critical backend services such as PostgreSQL, Redis, Keycloak, and Azure Blob Storage.

![HealthChecksDashboard](https://github.com/user-attachments/assets/a065b09e-a1dd-4a9f-8f8d-ba98a1d43d43)  

The HealthChecks UI dashboard provides a visual overview of all service health metrics in real time.

---

## 🧩 Requirements  

There are two ways to run the application:  
1. Run the **entire app in containers**  
2. Run only infrastructure in containers and **API + client locally**

### 🐳 Prerequisites  
- [Docker](https://www.docker.com/) installed  
- A valid **Simkl API key** (ClientID) from [Simkl API documentation](https://simkl.docs.apiary.io/#)  

After obtaining your key, create a `.env` file in the root directory (same level as `docker-compose.yml`) and add:  

```bash
SIMKL_API_KEY=YOUR_CLIENT_ID
```
> 📝 Make sure .env is UTF-8 encoded.

## 🚀 How to Run

### Option 1 — Full Docker Setup

```bash
cd WatchVault
```

```bash
docker-compose -f docker-compose.yml -f docker-compose.override.yml up
```

### Option 2 — Use Script
Alternatively, use the helper script to run the app and view container logs automatically:

```bash
cd scripts
```

```bash
./watchvault-up.sh
```

## 📂 Repository Structure

```
WatchVault/
│── Docs/
│── scripts/
│── SystemDemonstration/
│── WatchVault/
│   ├── docker/
│   ├── src/
│   ├── docker-compose.yml
│   ├── docker-compose.override.yml
│   ├── .env
│── watchvault-fe/
│── README.md
│── LICENCE
```

## 🤝 License & Usage
This project is licensed for educational and personal use only.
Commercial usage of the Simkl API and related assets is not permitted.
