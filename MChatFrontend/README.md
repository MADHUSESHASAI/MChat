# ChatApp Frontend

A React + Vite frontend for a realtime chat application.

## Tech Stack

- **React 18** — UI
- **Vite** — Dev server & bundler
- **React Router v6** — Client-side routing
- **CSS Modules** — Scoped component styling

## Project Structure

```
src/
├── api/
│   └── index.js          # All API fetch wrappers (auth + future endpoints)
├── components/
│   ├── AuthLayout.jsx     # Shared background/layout for auth pages
│   ├── Button.jsx         # Reusable button with loading state
│   ├── Input.jsx          # Reusable input with error display
│   └── ProtectedRoute.jsx # Redirect unauthenticated users to /login
├── context/
│   └── AuthContext.jsx    # Global auth state (user, token, login, logout)
├── pages/
│   ├── LoginPage.jsx      # /login
│   ├── RegisterPage.jsx   # /register
│   └── ChatPage.jsx       # /chat (placeholder — replace with real UI)
├── App.jsx                # Route definitions
├── main.jsx               # React root mount
└── index.css              # Global CSS variables & reset
```

## Getting Started

```bash
npm install
npm run dev
```

## Backend Integration

### 1. Set your backend URL

Edit `vite.config.js` — the dev server proxies `/api` to your backend:

```js
proxy: {
  '/api': {
    target: 'http://localhost:5000', // 👈 your backend port
    changeOrigin: true,
  },
},
```

### 2. API response shape expected

**POST /api/login** and **POST /api/register** should return:

```json
{
  "user": { "id": "...", "name": "Jane", "email": "jane@example.com" },
  "token": "your-jwt-token"
}
```

If your response shape differs, update `src/pages/LoginPage.jsx` and `RegisterPage.jsx` where the response is destructured.

### 3. Adding new pages

1. Create `src/pages/YourPage.jsx`
2. Add the route in `src/App.jsx`
3. Wrap with `<ProtectedRoute>` if it requires auth
4. Add any new API calls to `src/api/index.js`

### 4. Socket.IO (for realtime chat)

When ready, install and use the socket in your chat page:

```bash
npm install socket.io-client
```

```js
import { io } from 'socket.io-client'

const socket = io('http://localhost:5000', {
  auth: { token: localStorage.getItem('chatapp_token') }
})
```
