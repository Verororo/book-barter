
import UserItem from "../../components/UserItem/UserItem"
import styles from './UserItemContainer.module.css'
import Pagination from "@mui/material/Pagination";
import type { User } from "../../components/UserItem/UserItem"
import { useEffect, useState } from "react"
import axios from "axios";

const UserItemContainer = () => {
  const pageSize = 10
  const [users, setUsers] = useState<User[]>([])
  const [total, setTotal] = useState(0)
  const [currentPage, setCurrentPage] = useState(1)

  useEffect(() => {
    const fetchUsers = async () => {
      try {
        const { data } = await axios.post<{
          items: User[]
          total: number
        }>(
          `${import.meta.env.VITE_API_BASE_URL}/users/paged`,
          {
            pageNumber: currentPage,
            pageSize,
            orderByProperty: 'lastOnlineDate',
            orderDirection: 'desc',
          },
          {
            headers: { 'Content-Type': 'application/json' },
          }
        )
        setUsers(data.items)
        setTotal(data.total)
      } catch (err) {
        console.error('Failed to fetch users:', err)
      }
    }

    fetchUsers()
  }, [currentPage])

  const scrollToTop = () =>
    window.scrollTo({ top: 0, behavior: 'smooth' })

  const handlePageChange = (_: any, page: number) => {
    setCurrentPage(page)
    setTimeout(scrollToTop, 50)
  }

  return (
    <div className={styles.userItemContainer}>
      {users.map(user => (
        <UserItem key={user.userName} user={user} />
      ))}

      <Pagination
        count={Math.ceil(total / pageSize)}
        page={currentPage}
        onChange={handlePageChange}
      />
    </div>
  )
}

export default UserItemContainer