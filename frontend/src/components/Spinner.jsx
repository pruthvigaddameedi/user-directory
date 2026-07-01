import React from "react";

export default function Spinner() {
  return (
    <div style={{display:"flex", justifyContent:"center", padding:24}}>
      <div className="spinner" role="status" aria-label="loading"></div>
    </div>
  );
}
