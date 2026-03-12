import { createContext, useContext, useState } from 'react'

const AuthContext = createContext(null)

export function AuthProvider({ children }) {
  // Restore user from localStorage on page refresh
  const [user, setUser] = useState(() => {
    try {
      const storedUser = localStorage.getItem('chatapp_user')
      return storedUser ? JSON.parse(storedUser) : null
    } catch {
      return null
    }
  })

  const [token, setToken] = useState(() => {
    return localStorage.getItem('chatapp_token') || null
  })


  // Login function
  const login = (userData, authToken) => {
    console.log(userData)
    setUser(userData)
    setToken(authToken)

    localStorage.setItem('chatapp_user', JSON.stringify(userData))
    localStorage.setItem('chatapp_token', authToken)
  }

  // Logout function
  const logout = () => {
    setUser(null)
    setToken(null)

    localStorage.removeItem('chatapp_user')
    localStorage.removeItem('chatapp_token')
  }

  const isAuthenticated = !!user && !!token

  return (
    <AuthContext.Provider
      value={{
        user,
        token,
        login,
        logout,
        isAuthenticated
      }}
    >
      {children}
    </AuthContext.Provider>
  )
}

export function useAuth() {
  const context = useContext(AuthContext)
  if (!context) {
    throw new Error('useAuth must be used within AuthProvider')
  }
  return context
}