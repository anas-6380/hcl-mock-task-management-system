# Task Management App

A full-stack task management application built with ASP.NET Core Web API + MVC (backend), Angular (frontend), and MySQL via Entity Framework Core.

This project was implemented as a practical CRUD app with clear layering on the backend and a simple, maintainable UI flow on the frontend.

## What This Project Covers

- Create, read, update, and delete tasks
- Toggle task status (Completed/Pending)
- Backend API with layered architecture:
  - Controller -> Service -> Repository -> DbContext -> MySQL
- Angular standalone components with routing and reactive forms
- Optional MVC page for server-side rendering

## Tech Stack

### Backend

- .NET 10
- ASP.NET Core Web API + MVC
- Entity Framework Core 9
- Pomelo MySQL provider

### Frontend

- Angular 21 (standalone architecture)
- TypeScript
- RxJS
- Angular Reactive Forms

### Database

- MySQL

## Repository Structure

```text
.
├── backend/
│   ├── Controllers/
│   ├── Services/
│   ├── Repositories/
│   ├── Data/
│   ├── DTOs/
│   ├── Models/
│   ├── Views/
│   ├── Program.cs
│   └── TaskManagement.Api.csproj
├── frontend/
│   ├── src/
│   │   └── app/
│   │       ├── pages/
│   │       ├── services/
│   │       ├── models/
│   │       └── app.routes.ts
│   ├── angular.json
│   └── package.json
└── TaskManagement.slnx
```

## Architecture Overview

### Backend Request Flow

```text
HTTP Request
   -> TasksController
      -> TaskService
         -> TaskRepository
            -> TaskManagementDbContext
               -> MySQL (Tasks table)
```

### Frontend Flow

```text
Task List / Task Form Components
   -> TaskService (Angular)
      -> /api/tasks (proxied)
         -> Backend API
            -> Database
```

## API Endpoints

Base URL (local): `http://localhost:5213`

| Method | Endpoint | Description |
|---|---|---|
| GET | /api/tasks | Get all tasks |
| GET | /api/tasks/{id} | Get task by ID |
| POST | /api/tasks | Create task |
| PUT | /api/tasks/{id} | Update task |
| DELETE | /api/tasks/{id} | Delete task |

### Example Task Payload

```json
{
  "title": "Prepare demo",
  "description": "Finalize backend flow diagram",
  "isCompleted": false
}
```

## Local Setup

## 1) Prerequisites

- .NET SDK 10
- Node.js + npm
- MySQL Server

## 2) Database Setup (MySQL)

Create database and table if not already present:

```sql
CREATE DATABASE taskmanagementdb;
USE taskmanagementdb;

CREATE TABLE Tasks (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  Title VARCHAR(200) NOT NULL,
  Description VARCHAR(1000) NULL,
  IsCompleted BIT NOT NULL DEFAULT 0,
  CreatedAt DATETIME NOT NULL
);
```

## 3) Configure Backend Connection String

Update `backend/appsettings.json` with your MySQL connection string.

Current key:

```json
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;port=3306;database=taskmanagementdb;user=YOUR_USER;password=YOUR_PASSWORD;"
}
```

## 4) Run Backend

```bash
cd backend
dotnet restore
dotnet run
```

Backend URLs:

- API: `http://localhost:5213/api/tasks`
- MVC page: `http://localhost:5213/TaskPage/Index`

## 5) Run Frontend

In a second terminal:

```bash
cd frontend
npm install
npm start
```

Frontend URL:

- App: `http://localhost:4200`

Notes:

- Frontend uses a dev proxy (`frontend/proxy.conf.json`) so `/api/*` routes to backend on port 5213.

## Useful Commands

### Backend

```bash
cd backend
dotnet build
dotnet run
```

### Frontend

```bash
cd frontend
npm start
npm run build
npm test
```

## Troubleshooting

## Port already in use

If `4200` or `5213` is occupied, stop existing listeners and restart both apps.

## Restore/build issues after cleanup

If generated folders were cleaned, run:

```bash
cd backend
dotnet restore
dotnet build

cd ../frontend
npm install
```

## Common path issue

If old commands fail, make sure you are using renamed folder paths:

- `backend` (not `TaskManagement.Api`)
- `frontend` (not `task-management-ui`)

## Current Scope and Next Improvements

Implemented:

- Full CRUD task management
- Status toggling
- Basic validation on backend DTOs and frontend forms
- API + Angular + optional MVC page

Potential next steps:

- Authentication and user-specific task lists
- Pagination and filtering
- Better API error responses and centralized logging
- Docker setup for one-command local start

---

If you are reviewing this for interview/demo purposes, the best path is:

1. Run backend and frontend
2. Show create/edit/delete/toggle flows
3. Explain layered backend design and why DTO + DI are used
4. Show API endpoint contract and frontend proxy setup
