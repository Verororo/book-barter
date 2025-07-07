
import UserItem from "../../components/UserItem/UserItem"
import styles from './UserItemContainer.module.css'
import Pagination from "@mui/material/Pagination"
import { useEffect, useState } from "react"

import { fetchListedUsersPaginated } from "../../api/user-client"
import type { ListedUser } from "../../api/view-models/listed-user"
import LoadingSpinner from "../../components/LoadingSpinner/LoadingSpinner"

const UserItemContainer = () => {
  const pageSize = 10
  const [users, setUsers] = useState<ListedUser[]>([])
  const [total, setTotal] = useState(0)
  const [currentPage, setCurrentPage] = useState(1)

  const [loading, setLoading] = useState<boolean>(false)

  useEffect(() => {
    setLoading(true)
    fetchListedUsersPaginated(currentPage, pageSize, "lastOnlineDate", "desc")
      .then(({ items, total }) => {
        setUsers(items);
        setTotal(total);
      })
      .catch(console.error)
      .finally(() => { 
        setLoading(false)
      })
  }, [currentPage]);

  const scrollToTop = () =>
    window.scrollTo({ top: 0, behavior: 'smooth' })

  const handlePageChange = (_: any, page: number) => {
    setCurrentPage(page)
    setTimeout(scrollToTop, 50)
  }

  return (
    <div className={styles.userItemContainer}>
      { loading 
        ? <LoadingSpinner />
        : users.map(user => (
            <UserItem key={user.userName} user={user} />
          ))
      }
      

      <Pagination
        count={Math.ceil(total / pageSize)}
        page={currentPage}
        onChange={handlePageChange}
      />
    </div>
  )
}

export default UserItemContainer