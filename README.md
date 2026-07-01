Overview
User Directory is a small full‑stack sample application that demonstrates a complete end‑to‑end workflow: a React single‑page frontend with plain CSS and a .NET 8 Web API backend using SQLite for persistence. The app provides two pages: List (view users) and Add (create users).

Application Structure
Repository layout
/backend        → .NET 8 Web API, EF Core, controllers, models, Dockerfile
/frontend       → Vite + React app, plain CSS, pages, components, Dockerfile
/docker-compose.yml
/data           → runtime SQLite DB file (./data/app.db)

Key backend files
Program.cs — app startup, DB configuration, Swagger
Data/AppDbContext.cs — EF Core DbContext and optional seed data
Models/User.cs — User entity and validation attributes
Controllers/UsersController.cs — CRUD endpoints: GET, POST, PUT, DELETE

Key frontend files

src/pages/List.jsx — loads GET /api/users, shows spinner, empty state, and errors
src/pages/Add.jsx — form with client validation using react-hook-form + yup, posts to POST /api/users
src/api/users.js — Axios wrappers for API calls
src/index.css — plain CSS styling for layout, forms, table, spinner, toast

Development and Data Flow
How data flows

Frontend calls GET /api/users to populate the List page.
Add page validates input client‑side and calls POST /api/users to create a user.
Backend validates server‑side, persists to SQLite, and returns appropriate HTTP status codes.
EF Core migrations are applied at startup; the SQLite file is created at ./data/app.db.

Validation and UX
Client validation: yup + react-hook-form with inline error messages.
Server validation: Data annotations on DTOs and models; invalid requests return 400 Bad Request.
User experience: Loading spinner, inline validation, success toast, redirect to List on success.

Prerequisites
.NET 8 SDK
Node 18+ and npm
