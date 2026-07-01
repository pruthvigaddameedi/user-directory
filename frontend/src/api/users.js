import axios from "axios";
import axios from "./axiosInstance";
export async function getUsers() { const res = await axios.get("/api/users"); return res.data; }
export async function createUser(payload) { const res = await axios.post("/api/users", payload); return res.data; }

const API_BASE = import.meta.env.VITE_API_BASE || "http://localhost:5000";

export async function getUsers() {
  const res = await axios.get(`${API_BASE}/api/users`);
  return res.data;
}

export async function createUser(payload) {
  const res = await axios.post(`${API_BASE}/api/users`, payload);
  return res.data;
}
