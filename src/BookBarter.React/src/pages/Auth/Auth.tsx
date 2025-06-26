// Auth.tsx
import { useState } from 'react'
import styles from './Auth.module.css'
import TextField from '@mui/material/TextField'
import Radio from '@mui/material/Radio'
import RadioGroup from '@mui/material/RadioGroup'
import FormControlLabel from '@mui/material/FormControlLabel'
import FormControl from '@mui/material/FormControl'
import Button from '@mui/material/Button'

import { useFormik } from 'formik'
import HomeButton from '../../components/Navigation/HomeButton'
import { useAuth } from '../../contexts/AuthContext'
import { useNavigate } from 'react-router-dom'

type FormValues = {
  email: string,
  password: string,
  confirmPassword: string,
  userName: string,
  city: string
}

type FormErrors = {
  email?: string,
  password?: string,
  confirmPassword?: string,
  userName?: string,
  city?: string
}

const Auth = () => {
  const [hasAccount, setHasAccount] = useState('yes')
  const { login, register, isAuthenticated, user } = useAuth()
  const navigate = useNavigate()

  const validate = (values: FormValues): FormErrors => {
    const errors: FormErrors = {}

    if (!values.email) {
      errors.email = "Email is required."
    } else if (!/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i.test(values.email)) {
      errors.email = "Invalid email address."
    }

    if (!values.password) {
      errors.password = "Password is required."
    } else if (values.password.length < 6) {
      errors.password = "Password must be at least 6 characters."
    }

    if (hasAccount === 'no') {
      if (values.confirmPassword != values.password) {
        errors.confirmPassword = "Passwords don't match."
      }

      if (!values.userName) {
        errors.userName = "User name is required."
      } else if (values.userName.length >= 20) {
        errors.userName = "User name length should not exceed 20 characters."
      }

      if (!values.city) {
        errors.city = "City is required."
      }
    }

    return errors
  }

  const formik = useFormik<FormValues>({
    initialValues: {
      email: '',
      password: '',
      confirmPassword: '',
      userName: '',
      city: ''
    },
    validate,
    onSubmit: async (values) => {
      try {
        if (hasAccount === 'yes') {
          await login({
            email: values.email, 
            password: values.password
          })
          navigate('/')
        } 
        else {
          await register({
            email: values.email,
            password: values.password,
            confirmPassword: values.confirmPassword,
            userName: values.userName,
            city: values.city
          })

          setHasAccount('yes')
          formik.setValues({
            email: values.email,
            password: '',
            confirmPassword: '',
            userName: '',
            city: ''
          })
        }
      } catch (error) {
        console.log(error)
      }
    }
  })

  const handleRadioChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setHasAccount(event.target.value)
  }

  if (isAuthenticated && user) {
    return (
      <div className={styles.authBox}>
        <HomeButton />
        <div className={styles.authBoxBody}>
          <h2 className={styles.title}>Welcome back, {user.userName}!</h2>
          <p className={styles.subtitle}>You are already signed in.</p>
          <Button
            variant="contained"
            onClick={() => navigate('/')}
            className={styles.submitButton}
            size="large"
          >
            Go to Home
          </Button>
        </div>
      </div>
    )
  }

  return (
    <div className={styles.authBox}>
      <HomeButton />

      <div className={styles.authBoxBody}>
        <h2 className={styles.title}>Welcome!</h2>
        <p className={styles.subtitle}>Enter your data below.</p>

        <form onSubmit={formik.handleSubmit}>
          <TextField
            id="email"
            name="email"
            label="Email"
            variant="standard"
            fullWidth
            value={formik.values.email}
            onChange={formik.handleChange}
            onBlur={formik.handleBlur}
            error={formik.touched.email && Boolean(formik.errors.email)}
            helperText={formik.touched.email && formik.errors.email}
          />

          <TextField
            id="password"
            name="password"
            label="Password"
            type="password"
            variant="standard"
            fullWidth
            value={formik.values.password}
            onChange={formik.handleChange}
            onBlur={formik.handleBlur}
            error={formik.touched.password && Boolean(formik.errors.password)}
            helperText={formik.touched.password && formik.errors.password}
          />

          <FormControl className={styles.radioGroup}>
            <RadioGroup
              value={hasAccount}
              onChange={handleRadioChange}
            >
              <FormControlLabel
                value="yes"
                control={<Radio size="small" />}
                label="I have an account"
              />
              <FormControlLabel
                value="no"
                control={<Radio size="small" />}
                label="I don't have an account"
              />
            </RadioGroup>
          </FormControl>

          {hasAccount === 'no' && (
            <div className={styles.additionalFields}>
              <TextField
                id="confirmPassword"
                name="confirmPassword"
                label="Confirm password"
                type="password"
                variant="standard"
                fullWidth
                value={formik.values.confirmPassword}
                onChange={formik.handleChange}
                onBlur={formik.handleBlur}
                error={formik.touched.confirmPassword && Boolean(formik.errors.confirmPassword)}
                helperText={formik.touched.confirmPassword && formik.errors.confirmPassword}
              />

              <TextField
                id="userName"
                name="userName"
                label="User name"
                variant="standard"
                fullWidth
                value={formik.values.userName}
                onChange={formik.handleChange}
                onBlur={formik.handleBlur}
                error={formik.touched.userName && Boolean(formik.errors.userName)}
                helperText={formik.touched.userName && formik.errors.userName}
              />

              <TextField
                id="city"
                name="city"
                label="City"
                variant="standard"
                fullWidth
                value={formik.values.city}
                onChange={formik.handleChange}
                onBlur={formik.handleBlur}
                error={formik.touched.city && Boolean(formik.errors.city)}
                helperText={formik.touched.city && formik.errors.city}
              />
            </div>
          )}

          <Button
            variant="contained"
            type="submit"
            className={styles.submitButton}
            size="large"
          >
            {hasAccount === 'yes' ? 'Sign in' : 'Sign up'}
          </Button>
        </form>
      </div>
    </div>
  )
}

export default Auth