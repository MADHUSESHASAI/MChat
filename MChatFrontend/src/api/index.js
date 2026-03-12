// ─────────────────────────────────────────────────────────────
// Base Configuration
// ─────────────────────────────────────────────────────────────

const BASE_URL = 'https://localhost:7260/api'

// ─────────────────────────────────────────────────────────────
// Token Storage Helpers
// ─────────────────────────────────────────────────────────────

function getToken() {
  return localStorage.getItem('chatapp_token')
}

function setToken(token) {
  if (token) localStorage.setItem('chatapp_token', token)
}

function removeToken() {
  localStorage.removeItem('chatapp_token')
}

// ─────────────────────────────────────────────────────────────
// HTTP Request Wrapper
// ─────────────────────────────────────────────────────────────

async function request(method, path, body = null, requiresAuth = true) {
  const headers = {
    'Content-Type': 'application/json'
  }

  // Attach JWT token
  if (requiresAuth) {
    const token = getToken()
    if (token) {
      headers['Authorization'] = `Bearer ${token}`
    }
  }

  const options = {
    method,
    headers
  }

  if (body) {
    options.body = JSON.stringify(body)
  }

  const response = await fetch(`${BASE_URL}${path}`, options)

  let data = {}
  try {
    data = await response.json()
  } catch {
    data = {}
  }

  if (!response.ok) {
    throw new Error(
      data?.message ||
      data?.error ||
      `Request failed with status ${response.status}`
    )
  }

  return data
}

// ─────────────────────────────────────────────────────────────
// Auth Response Normalizer
// Frontend receives unified structure
// ─────────────────────────────────────────────────────────────

function normalizeAuthResponse(data) {
  if (!data) return null

  // Store token automatically
  if (data.token) {
    setToken(data.token)
  }

  return {
    user: {
      name: data.personName || data.user?.personName || '',
      email: data.email || data.user?.email || ''
    },
    token: data.token || '',
    expirationTime: data.expirationTime || null
  }
}

// ─────────────────────────────────────────────────────────────
// Auth API
// ─────────────────────────────────────────────────────────────

export const authApi = {

  // Login API
  login: async (credentials) => {
    const data = await request(
      'POST',
      '/Auth/Login',
      credentials,
      false
    )

    return normalizeAuthResponse(data)
  },

  // Register API
  register: async (userData) => {
    const data = await request(
      'POST',
      '/Auth/Register',
      userData,
      false
    )

    return normalizeAuthResponse(data)
  },

  // Logout API
  logout: () => {
    removeToken()
  }
}