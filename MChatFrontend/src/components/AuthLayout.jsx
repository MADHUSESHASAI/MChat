import styles from './AuthLayout.module.css'

export default function AuthLayout({ children }) {
  return (
    <div className={styles.root}>
      {/* Ambient orbs */}
      <div className={styles.orb1} aria-hidden="true" />
      <div className={styles.orb2} aria-hidden="true" />
      {/* Grid overlay */}
      <div className={styles.grid} aria-hidden="true" />

      <main className={styles.main}>
        <div className={styles.logo}>
          <span className={styles.logoDot} />
          ChatApp
          <span className={styles.logoDot} />
        </div>
        {children}
      </main>
    </div>
  )
}
