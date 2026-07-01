import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import { createUser } from "../api/users";
import Toast from "../components/Toast";

const schema = yup.object({
  name: yup.string().required().min(2).max(100),
  age: yup.number().required().min(0).max(120),
  city: yup.string().required(),
  state: yup.string().required(),
  pincode: yup.string().required().min(4).max(10),
}).required();

export default function Add() {
  const navigate = useNavigate();
  const [toast, setToast] = useState("");
  const { register, handleSubmit, formState: { errors, isSubmitting } } = useForm({
    resolver: yupResolver(schema)
  });

  async function onSubmit(data) {
    try {
      await createUser(data);
      setToast("User created successfully");
      setTimeout(() => navigate("/list"), 900);
    } catch (e) {
      setToast("Failed to create user");
      setTimeout(() => setToast(""), 2000);
    }
  }

  return (
    <div className="card">
      <h2 style={{marginBottom:12}}>Add User</h2>
      <form onSubmit={handleSubmit(onSubmit)}>
        <div className="form-row">
          <label>Name</label>
          <input className="input" {...register("name")} />
          {errors.name && <div className="error">{errors.name.message}</div>}
        </div>

        <div className="form-row">
          <label>Age</label>
          <input type="number" className="input" {...register("age")} />
          {errors.age && <div className="error">{errors.age.message}</div>}
        </div>

        <div className="form-row">
          <label>City</label>
          <input className="input" {...register("city")} />
          {errors.city && <div className="error">{errors.city.message}</div>}
        </div>

        <div className="form-row">
          <label>State</label>
          <input className="input" {...register("state")} />
          {errors.state && <div className="error">{errors.state.message}</div>}
        </div>

        <div className="form-row">
          <label>Pincode</label>
          <input className="input" {...register("pincode")} />
          {errors.pincode && <div className="error">{errors.pincode.message}</div>}
        </div>

        <div style={{marginTop:12}}>
          <button className="btn" disabled={isSubmitting}>{isSubmitting ? "Saving..." : "Save"}</button>
        </div>
      </form>

      <Toast message={toast} />
    </div>
  );
}
