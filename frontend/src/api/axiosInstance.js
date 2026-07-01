import axios from "axios";
import { getAuth } from "../auth/auth0Client";

const API_BASE = import.meta.env.VITE_API_BASE || "http://localhost:5000";
const instance = axios.create({ baseURL: API_BASE });

instance.interceptors.request.use(async (config) => {
  try {
    const auth = getAuth();
    const token = await auth.getTokenSilently();
    if (token) config.headers.Authorization = `Bearer ${token}`;
  } catch (e) {
    // no token available
  }
  return config;
});

export default instance;
