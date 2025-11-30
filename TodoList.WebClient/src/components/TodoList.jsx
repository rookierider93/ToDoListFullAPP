import React, { useEffect, useState } from "react";
import api from "../api";
import axios from "axios";
import TodoItem from "./TodoItem";
import TodoForm from "./TodoForm";

export default function TodoList() {
  const [todos, setTodos] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  async function fetchTodos() {
    try {
      setLoading(true);
      debugger;
      const res = await api.get("/todos");
      setTodos(res.data);
    } catch (err) {
      setError(err.message || "Failed to load");
    } finally {
      setLoading(false);
    }
  }

  useEffect(() => {
    fetchTodos();
  }, []);

  async function handleCreate(todo) {
    await api.post("/todos", todo);
    await fetchTodos();
  }

  async function handleDelete(id) {
    await api.delete(`/todos/${id}`);
    setTodos((t) => t.filter((x) => x.id !== id));
  }

  async function handleUpdate(id, updated) {
    await api.put(`/todos/${id}`, updated);
    await fetchTodos();
  }

  if (loading) return <div>Loading...</div>;
  if (error) return <div>Error: {error}</div>;

  return (
    <div className="todo-list">
      <TodoForm onCreate={handleCreate} />
      <ul>
        {todos.map((t) => (
          <TodoItem
            key={t.id}
            todo={t}
            onDelete={handleDelete}
            onUpdate={handleUpdate}
          />
        ))}
      </ul>
    </div>
  );
}
