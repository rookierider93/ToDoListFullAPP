import React, { useState } from "react";

export default function TodoItem({ todo, onDelete, onUpdate }) {
  const [editing, setEditing] = useState(false);
  const [title, setTitle] = useState(todo.title);
  const [description, setDescription] = useState(todo.description || "");

  async function save(e) {
    e.preventDefault();
    await onUpdate(todo.id, { ...todo, title, description });
    setEditing(false);
  }

  return (
    <li className="todo-item">
      {editing ? (
        <form onSubmit={save} className="edit-form">
          <input
            value={title}
            onChange={(e) => setTitle(e.target.value)}
            required
          />
          <input
            value={description}
            onChange={(e) => setDescription(e.target.value)}
          />
          <button type="submit">Save</button>
          <button type="button" onClick={() => setEditing(false)}>
            Cancel
          </button>
        </form>
      ) : (
        <div className="view">
          <div className="meta">
            <strong>{todo.title}</strong>
            <span className="small">#{todo.id}</span>
          </div>
          <p>{todo.description}</p>
          <div className="actions">
            <button onClick={() => setEditing(true)}>Edit</button>
            <button onClick={() => onDelete(todo.id)}>Delete</button>
          </div>
        </div>
      )}
    </li>
  );
}
