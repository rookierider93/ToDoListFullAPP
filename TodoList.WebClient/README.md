# TodoList Web Client

Minimal Vite + React client for the TodoList API.

Quick start (PowerShell):

```powershell
cd "c:\ROHAN\Study\ToDoList\TodoList.WebClient"
npm install
# Optionally set API base URL; default is https://localhost:5001
# Create a .env file with: VITE_API_BASE_URL=https://localhost:5001
npm run dev
```

Notes:

- The client uses `VITE_API_BASE_URL` environment variable to locate the API. If your API runs on a different port, set that variable.
- The API must allow CORS for the client origin. For development you can enable CORS in the ASP.NET app or run the client on the same origin.
- If your API uses HTTPS with a self-signed dev cert, you may need to accept the certificate in your browser.

Files of interest:

- `src/api.js` — axios instance and base URL.
- `src/components/*` — `TodoList`, `TodoItem`, `TodoForm`.
