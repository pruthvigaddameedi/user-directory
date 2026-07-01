import createAuth0Client from "@auth0/auth0-spa-js";

let auth0 = null;
export async function initAuth() {
  auth0 = await createAuth0Client({
    domain: import.meta.env.VITE_AUTH0_DOMAIN,
    client_id: import.meta.env.VITE_AUTH0_CLIENT_ID,
    audience: import.meta.env.VITE_AUTH0_AUDIENCE,
    cacheLocation: "memory"
  });
  return auth0;
}
export function getAuth() {
  if (!auth0) throw new Error("Auth not initialized");
  return auth0;
}
