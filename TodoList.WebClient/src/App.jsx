import React from "react";
import TodoList from "./components/TodoList";

export default function App() {
  return (
    <div className="app">
      <header>
        <h1>TodoList Web Client</h1>
      </header>
      <main>
        <TodoList />
      </main>
    </div>
  );
}
