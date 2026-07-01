import React, { useEffect, useState } from "react";
import { getUsers } from "../api/users";
import Spinner from "../components/Spinner";

export default function List() {
  const [users, setUsers] = useState(null);
  const [error, setError] = useState("");

  useEffect(() => {
    let mounted = true;
    async function load() {
      try {
        const data = await getUsers();
        if (mounted) setUsers(data);
      } catch (e) {
        setError("Failed to load users.");
      }
    }
    load();
    return () => (mounted = false);
  }, []);

  if (error) {
    return <div className="card"><div className="error">{error}</div></div>;
  }

  if (users === null) return <Spinner />;

  if (users.length === 0) {
    return <div className="empty">No users found. Add one from the Add page.</div>;
  }

  return (
    <div className="card">
      <table className="table">
        <thead>
          <tr>
            <th>Name</th><th>Age</th><th>City</th><th>State</th><th>Pincode</th>
          </tr>
        </thead>
        <tbody>
          {users.map(u => (
            <tr key={u.id}>
              <td>{u.name}</td>
              <td>{u.age}</td>
              <td>{u.city}</td>
              <td>{u.state}</td>
              <td>{u.pincode}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
