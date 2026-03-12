import { useState } from 'react'
import { Link, useNavigate, useLocation } from 'react-router-dom'
import { useAuth } from '../context/AuthContext'
import { authApi } from '../api'
import AuthLayout from '../components/AuthLayout'
import Input from '../components/Input'
import Button from '../components/Button'
import styles from './Auth.module.css'

export default function LoginPage() {
  const { login } = useAuth()
  const navigate = useNavigate()
  const location = useLocation()

  const redirectTo = location.state?.from?.pathname || '/'

  const [form, setForm] = useState({
    email: '',
    password: ''
  })

  const [errors, setErrors] = useState({})
  const [apiError, setApiError] = useState('')
  const [loading, setLoading] = useState(false)

  const handleChange = (e) => {
    setForm((prev) => ({
      ...prev,
      [e.target.name]: e.target.value
    }))
    setErrors((prev) => ({
      ...prev,
      [e.target.name]: ''
    }))
    setApiError('')
  }

  const validate = () => {
    const newErrors = {}

    if (!form.email) {
      newErrors.email = 'Email is required'
    } else if (!/\S+@\S+\.\S+/.test(form.email)) {
      newErrors.email = 'Enter a valid email'
    }

    if (!form.password) {
      newErrors.password = 'Password is required'
    }

    return newErrors
  }

const handleSubmit = async (e) => {
  e.preventDefault()

  const validationErrors = validate()
  if (Object.keys(validationErrors).length > 0) {
    setErrors(validationErrors)
    return
  }

  setLoading(true)
  setApiError('')

  try {
    const data = await authApi.login({
      email: form.email,
      password: form.password
    })

    console.log("Login response:", data)

    // ✅ Correct mapping (IMPORTANT FIX)
    login(
      {
        name: data.user?.name || '',
        email: data.user?.email || '',
        expiration: data.expirationTime
      },
      data.token
    )

    navigate(redirectTo, { replace: true })

  } catch (err) {
    setApiError(err?.message || 'Login failed. Please try again.')
  } finally {
    setLoading(false)
  }
}

  return (
    <AuthLayout>
      <div className={styles.card}>
        <h1 className={styles.heading}>Welcome Back</h1>
        <p className={styles.sub}>
          Sign in to continue to your chat workspace.
        </p>

        <form onSubmit={handleSubmit} noValidate className={styles.form}>
          <Input
            label="Email"
            name="email"
            type="email"
            value={form.email}
            onChange={handleChange}
            placeholder="you@example.com"
            autoComplete="email"
            error={errors.email}
          />

          <Input
            label="Password"
            name="password"
            type="password"
            value={form.password}
            onChange={handleChange}
            placeholder="••••••••"
            autoComplete="current-password"
            error={errors.password}
          />

          {apiError && (
            <div className={styles.apiError}>
              {apiError}
            </div>
          )}

          <Button type="submit" loading={loading}>
            Sign In
          </Button>
        </form>

        <p className={styles.switchText}>
          Don’t have an account?{' '}
          <Link to="/register" className={styles.link}>
            Register
          </Link>
        </p>
      </div>
    </AuthLayout>
  )
}