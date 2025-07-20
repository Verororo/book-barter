import { useCallback, useState } from 'react'
import styles from './Auth.module.css'
import TextField from '@mui/material/TextField'
import Radio from '@mui/material/Radio'
import RadioGroup from '@mui/material/RadioGroup'
import FormControlLabel from '@mui/material/FormControlLabel'
import FormControl from '@mui/material/FormControl'
import Button from '@mui/material/Button'

import { useFormik } from 'formik'
import HomeButton from '../../components/Navigation/HomeButton'
import { useAuth } from '../../contexts/Auth/UseAuth'
import { useNavigate } from 'react-router-dom'
import type { CityDto } from '../../api/generated/models/city-dto'
import { fetchPagedCities } from '../../api/clients/city-client'
import SingleSearchBar from '../../components/SearchBars/SingleSearchBar'

type AuthValues = {
  email: string,
  password: string,
  confirmPassword: string,
  userName: string,
  city: CityDto | null
}

type AuthErrors = {
  email?: string,
  password?: string,
  confirmPassword?: string,
  userName?: string,
  city?: string
}

const Auth = () => {
  const [hasAccount, setHasAccount] = useState(true)
  const [loading, setLoading] = useState(false)
  const { login, register, isAuthenticated, userAuthData } = useAuth()
  const navigate = useNavigate()

  const validate = (values: AuthValues): AuthErrors => {
    const errors: AuthErrors = {}

    if (!values.email) errors.email = "Email is required."
    else if (!/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i.test(values.email)) errors.email = "Invalid email address."

    if (!values.password) errors.password = "Password is required."

    if (!hasAccount) {
      if (values.password.length < 6) errors.password = "Password must be at least 6 characters."
      else if (!/[A-Z]/.test(values.password)) errors.password = "Password must contain at least one uppercase character."
      else if (!/[a-z]/.test(values.password)) errors.password = "Password must contain at least one lowercase character."
      else if (!/[\d]/.test(values.password)) errors.password = "Password must contain at least one digit."
      else if (!/[^A-Za-z0-9]/.test(values.password)) errors.password = "Password must contain at least one non-alphanumeric character."

      if (values.confirmPassword != values.password) errors.confirmPassword = "Passwords don't match."

      if (!values.userName) errors.userName = "User name is required."
      else if (values.userName.length >= 20) errors.userName = "User name length should not exceed 20 characters."

      if (!values.city) errors.city = "City is required."
    }

    return errors
  }

  const formik = useFormik<AuthValues>({
    initialValues: {
      email: '',
      password: '',
      confirmPassword: '',
      userName: '',
      city: null
    },
    validate,
    validateOnChange: false,
    validateOnBlur: true,
    onSubmit: async (values) => {
      try {
        setLoading(true)
        if (hasAccount) {
          await login({
            email: values.email,
            password: values.password
          })
          navigate('/')
        } else {
          await register({
            email: values.email,
            password: values.password,
            userName: values.userName,
            cityId: values.city?.id
          })

          setHasAccount(true)
          formik.setValues({
            email: values.email,
            password: '',
            confirmPassword: '',
            userName: '',
            city: null
          })
          formik.setFieldTouched("password", false, /* shouldValidate = */ false);
        }
      } finally {
        setLoading(false)
      }
    }
  })

  const handleRadioChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setHasAccount(JSON.parse(event.target.value))
  }

  const onCityChange = useCallback(
      (_event: any, newValue: any) => {
        formik.setFieldValue("city", newValue, /* shouldValidate = */ true);
        formik.setFieldTouched("city", true, /* shouldValidate = */ false);
      },
      [formik]
    );

  const displayedErrorFields = Object
    .keys(formik.errors)
    .filter(field => formik.touched[field as keyof AuthValues]);

  const submitDisabled = displayedErrorFields.length > 0;

  if (isAuthenticated && userAuthData) {
    return (
      <div className={styles.authBox}>
        <HomeButton />
        <div className={styles.authBoxBody}>
          <h2 className={styles.title}>Welcome back, {userAuthData.userName}!</h2>
          <p className={styles.subtitle}>You are already signed in.</p>
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
                value={true}
                control={<Radio size="small" />}
                label="I have an account"
              />
              <FormControlLabel
                value={false}
                control={<Radio size="small" />}
                label="I don't have an account"
              />
            </RadioGroup>
          </FormControl>

          {!hasAccount && (
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

              <SingleSearchBar
                id="city"
                label="City"
                placeholder="Search for cities..."
                value={formik.values.city}
                getOptionLabel={(option) => option.nameWithCountry!}
                onChange={onCityChange}
                fetchMethod={fetchPagedCities}
                styles={styles}
                error={formik.touched.city && Boolean(formik.errors.city)}
                helperText={formik.touched.city && formik.errors.city}
                onBlur={() => formik.setFieldTouched('city', true)}
                variant={'standard'}
              />
            </div>
          )}

          <Button
            variant="contained"
            type="submit"
            className={styles.submitButton}
            size="large"
            disabled={submitDisabled}
            loading={loading}
          >
            {hasAccount ? 'Sign in' : 'Sign up'}
          </Button>
        </form>
      </div>
    </div>
  )
}

export default Auth