import { useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import { useAuth } from '../context/AuthContext'
import { authApi } from '../api'
import AuthLayout from '../components/AuthLayout'
import Input from '../components/Input'
import Button from '../components/Button'
import styles from './Auth.module.css'

export default function RegisterPage() {
  const { login } = useAuth()
  const navigate = useNavigate()

  const [form, setForm] = useState({
    name: '',
    email: '',
    password: '',
    confirmPassword: ''
  })

  const [errors, setErrors] = useState({})
  const [apiError, setApiError] = useState('')
  const [loading, setLoading] = useState(false)

  const change = (e) => {
    setForm((f) => ({ ...f, [e.target.name]: e.target.value }))
    setErrors((err) => ({ ...err, [e.target.name]: '' }))
    setApiError('')
  }

  const validate = () => {
    const e = {}

    if (!form.PersonName.trim()) e.PersonName = 'Name is required'

    if (!form.email) e.email = 'Email is required'
    else if (!/\S+@\S+\.\S+/.test(form.email))
      e.email = 'Enter a valid email'

    if (!form.password) e.password = 'Password is required'
    else if (form.password.length < 6)
      e.password = 'Password must be at least 6 characters'

    if (!form.confirmPassword)
      e.confirmPassword = 'Please confirm your password'
    else if (form.password !== form.confirmPassword)
      e.confirmPassword = 'Passwords do not match'

    return e
  }

  const submit = async (e) => {
    e.preventDefault()

    const errs = validate()
    if (Object.keys(errs).length) {
      setErrors(errs)
      return
    }

    setLoading(true)
    setApiError('')

    try {
      const data = await authApi.register({
        PersonName: form.PersonName,
        email: form.email,
        password: form.password,
        confirmPassword:form.confirmPassword
      })

      // If backend returns token (auto-login after register)
      if (data.token) {
        const user = {
          personName: data.personName,
          email: data.email
        }

        login(user, data.token)
        navigate('/chat', { replace: true })
      } else {
        // If backend does NOT auto-login
        navigate('/login', {
          state: { registered: true },
          replace: true
        })
      }
    } catch (err) {
      setApiError(err.message || 'Registration failed. Please try again.')
    } finally {
      setLoading(false)
    }
  }

  return (
    <AuthLayout>
      <div className={styles.card}>
        <h1 className={styles.heading}>Create account</h1>
        <p className={styles.sub}>Get started — it's free.</p>

        <form onSubmit={submit} noValidate className={styles.form}>
          <Input
            label="Full Name"
            name="PersonName"
            value={form.PersonName}
            onChange={change}
            placeholder="Jane Doe"
            autoComplete="name"
            error={errors.PersonName}
          />

          <Input
            label="Email"
            name="email"
            type="email"
            value={form.email}
            onChange={change}
            placeholder="you@example.com"
            autoComplete="email"
            error={errors.email}
          />

          <Input
            label="Password"
            name="password"
            type="password"
            value={form.password}
            onChange={change}
            placeholder="••••••••"
            autoComplete="new-password"
            error={errors.password}
          />

          <Input
            label="Confirm Password"
            name="confirmPassword"
            type="password"
            value={form.confirmPassword}
            onChange={change}
            placeholder="••••••••"
            autoComplete="new-password"
            error={errors.confirmPassword}
          />

          {apiError && (
            <div className={styles.apiError}>{apiError}</div>
          )}

          <Button type="submit" loading={loading}>
            Create Account
          </Button>
        </form>

        <p className={styles.switchText}>
          Already have an account?{' '}
          <Link to="/login" className={styles.link}>
            Sign in
          </Link>
        </p>
      </div>
    </AuthLayout>
  )
}