import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import NavBar from "./components/NavBar";
import List from "./pages/List";
import Add from "./pages/Add";

export default function App() {
  return (
    <div>
      <NavBar />
      <main className="container">
        <Routes>
          <Route path="/" element={<Navigate to="/list" replace />} />
          <Route path="/list" element={<List />} />
          <Route path="/add" element={<Add />} />
        </Routes>
      </main>
    </div>
  );
}
