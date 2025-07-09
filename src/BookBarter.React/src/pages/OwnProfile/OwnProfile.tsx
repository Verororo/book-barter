import styles from './OwnProfile.module.css'
import GivingOutSection from '../../components/UserItem/GivingOutSection'
import LookingForSection from '../../components/UserItem/LookingForSection'
import Button from '@mui/material/Button'
import TextField from '@mui/material/TextField'
import Autocomplete from '@mui/material/Autocomplete'
import CircularProgress from '@mui/material/CircularProgress'
import { debounce } from '@mui/material'

import PersonIcon from '@mui/icons-material/Person'
import LocationOnIcon from '@mui/icons-material/LocationOn'

import { useAuth } from '../../contexts/Auth/UseAuth'
import { useEffect, useState } from 'react'
import { fetchUserById, updateUser } from '../../api/user-client'
import { Navigate } from 'react-router'
import type { User } from '../../api/view-models/user'
import LoadingSpinner from '../../components/LoadingSpinner/LoadingSpinner'
import HomeButton from '../../components/Navigation/HomeButton'
import { fetchPagedCities } from '../../api/city-client'
import type { CityDto } from '../../api/generated/models/city-dto'

const OwnProfile = () => {
  const { isAuthenticated, userAuthData, logout } = useAuth()
  if (!isAuthenticated || !userAuthData) {
    return <Navigate to="/auth" replace />
  }

  const [loading, setLoading] = useState(false)
  const [saving, setSaving] = useState(false)
  const [user, setUser] = useState<User>()
  const [editMode, setEditMode] = useState(false)

  const [aboutDraft, setAboutDraft] = useState('')
  const isAboutTooLong = aboutDraft.length > 300

  const [cityOptions, setCityOptions] = useState<CityDto[]>([])
  const [cityLoading, setCityLoading] = useState(false)
  const [cityInput, setCityInput] = useState('')
  const [cityOption, setCityOption] = useState<CityDto | null>(null)

  useEffect(() => {
    setLoading(true)
    fetchUserById(userAuthData.id)
      .then(user => {
        setUser(user)
        setAboutDraft(user.about)
      })
      .catch(console.error)
      .finally(() => setLoading(false))
  }, [userAuthData.id])

  useEffect(() => {
    if (!editMode) return

    if (cityInput.length < 2) {
      setCityOptions([])
      return
    }

    const handler = debounce((input: string) => {
      setCityLoading(true)
      fetchPagedCities(input)
        .then((cities: CityDto[]) => {
          setCityOptions(cities)
        })
        .catch(console.error)
        .finally(() => setCityLoading(false))
    }, 500)

    handler(cityInput)

    return () => {
      handler.clear()
    }
  }, [cityInput, editMode])

  const enterEdit = () => {
    setEditMode(true)
    setCityOption({
      id: user!.cityId,
      nameWithCountry: user!.cityNameWithCountry
    })
  }

  const handleSave = async () => {
    setSaving(true)
    updateUser({
      id: userAuthData.id,
      about: aboutDraft,
      cityId: cityOption?.id ?? user!.cityId
    })
      .then(() => {
        setUser(prev => prev && ({
          ...prev,
          about: aboutDraft,
          cityId: cityOption?.id ?? prev.cityId,
          cityNameWithCountry: cityOption?.nameWithCountry ?? prev.cityNameWithCountry
        }))
        setEditMode(false)
      })
      .catch(error => console.error(error))
      .finally(() => setSaving(false))
  }

  const handleCancel = () => {
    setEditMode(false)
    setAboutDraft(user!.about)
    setCityOption({
      id: user!.cityId,
      nameWithCountry: user!.cityNameWithCountry
    })
  }

  if (loading || !user) {
    return (
      <div className={styles.profileContainer}>
        <LoadingSpinner />
      </div>
    )
  }

  return (
    <div className={styles.profileContainer}>
      <div className={styles.profile}>
        <HomeButton />

        <div className={styles.profileHeader}>
          <div className={styles.profileHeaderLeft}>
            <p className={styles.profileName}>
              <PersonIcon fontSize="large" /> {user.userName}
            </p>
            <div className={styles.profileCity}>
              <LocationOnIcon fontSize="small" />
              {!editMode ? (
                <>{user.cityNameWithCountry}</>
              ) : (
                <Autocomplete
                  className={styles.citySearchBar}
                  value={cityOption}
                  onChange={(_event, newValue) => setCityOption(newValue)}
                  onInputChange={(_event, newInput) => setCityInput(newInput)}
                  options={cityOptions}
                  loading={cityLoading}
                  filterOptions={x => x}
                  getOptionLabel={option => option.nameWithCountry!}
                  renderInput={params => (
                    <TextField
                      {...params}
                      variant="standard"
                      slotProps={{
                        input: {
                          ...params.InputProps,
                          classes: {
                            input: styles.profileCity,
                          },
                          endAdornment: (
                            <>
                              {cityLoading && <CircularProgress size={20} />}
                              {params.InputProps.endAdornment}
                            </>
                          )
                        }
                      }}
                    />
                  )}
                />
              )}
            </div>
          </div>

          <div className={styles.profileHeaderRight}>
            <div className={styles.profileHeaderButtons}>
              {!editMode ? (
                <>
                  <Button
                    variant="contained"
                    onClick={enterEdit}
                  >
                    Settings
                  </Button>
                  <Button variant="outlined" onClick={logout}>
                    Log Out
                  </Button>
                </>
              ) : (
                <>
                  <Button
                    variant="contained"
                    onClick={handleSave}
                    disabled={isAboutTooLong || cityOption == null}
                    loading={saving}
                  >
                    Save
                  </Button>
                  <Button
                    variant="outlined"
                    onClick={handleCancel}
                    disabled={saving}
                  >
                    Cancel
                  </Button>
                </>
              )}
            </div>
          </div>
        </div>

        <div className={styles.profileAbout}>
          <b>About</b>
          {!editMode
            ? user.about
              ? <p className={styles.aboutParagraph}>{user.about}</p>
              : <p className={styles.aboutParagraph}>This user hasn't wrote about himself yet...</p>
            : (
              <TextField
                className={styles.aboutParagraph}
                multiline
                fullWidth
                minRows={4}
                maxRows={10}
                variant="outlined"
                value={aboutDraft}
                onChange={e => setAboutDraft(e.target.value)}
                error={isAboutTooLong}
                helperText={`${aboutDraft.length}/300`}
                disabled={saving}
              />
            )}
        </div>

        <GivingOutSection givingOutBooks={user.ownedBooks} />
        <LookingForSection lookingForBooks={user.wantedBooks} />
      </div>
    </div>
  )
}

export default OwnProfile
