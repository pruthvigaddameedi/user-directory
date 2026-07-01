import React from "react";
import { NavLink } from "react-router-dom";

export default function NavBar() {
  return (
    <nav className="nav">
      <div className="nav-inner">
        <NavLink to="/list" className={({isActive}) => isActive ? "active" : ""}>List</NavLink>
        <NavLink to="/add" className={({isActive}) => isActive ? "active" : ""}>Add</NavLink>
      </div>
    </nav>
  );
}
