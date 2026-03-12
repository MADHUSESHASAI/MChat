import { useAuth } from '../context/AuthContext'
import { useNavigate } from 'react-router-dom'
import styles from './ChatPage.module.css'

export default function ChatPage() {
  const { user, logout } = useAuth()
  const navigate = useNavigate()

  const handleLogout = () => {
    logout()
    navigate('/login', { replace: true })
  }

  return (
    <div className={styles.root}>
      <div className={styles.placeholder}>
        <div className={styles.icon}>💬</div>
        <h1 className={styles.title}>Chat is coming soon</h1>

        <p className={styles.desc}>
          <strong>Name:</strong> {user?.name} <br />
          <strong>Email:</strong> {user?.email}
          <br /><br />
          Build your chat UI here once the backend is ready.
        </p>

        <button className={styles.logoutBtn} onClick={handleLogout}>
          Log out
        </button>
      </div>
    </div>
  )
}