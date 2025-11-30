import React, { useState } from "react";

export default function TodoForm({ onCreate }) {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [userId, setUserId] = useState("");
  const [statusId, setStatusId] = useState("");

  function reset() {
    setTitle("");
    setDescription("");
    setUserId("");
    setStatusId("");
  }

  async function submit(e) {
    e.preventDefault();
    const todo = {
      title,
      description,
      userId: userId ? Number(userId) : null,
      statusId: statusId ? Number(statusId) : null,
    };
    await onCreate(todo);
    reset();
  }

  return (
    <form className="todo-form" onSubmit={submit}>
      <input
        value={title}
        onChange={(e) => setTitle(e.target.value)}
        placeholder="Title"
        required
      />
      <input
        value={description}
        onChange={(e) => setDescription(e.target.value)}
        placeholder="Description"
      />
      <input
        value={userId}
        onChange={(e) => setUserId(e.target.value)}
        placeholder="UserId (optional)"
      />
      <input
        value={statusId}
        onChange={(e) => setStatusId(e.target.value)}
        placeholder="StatusId (optional)"
      />
      <button type="submit">Add</button>
    </form>
  );
}
