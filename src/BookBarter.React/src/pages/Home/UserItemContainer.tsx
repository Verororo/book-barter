
import UserItem from "../../components/UserItem/UserItem"
import styles from './UserItemContainer.module.css'
import Pagination from "@mui/material/Pagination";
import type { User } from "../../components/UserItem/UserItem"
import { useState } from "react"
import { usePagination } from "../../components/Pagination/CustomHooks/UsePagination"

const userList : User[] = [
  {
    userName: "Anton",
    city: "Chisinau",
    lastUpdateDate: new Date(2025, 5, 13, 15),
  },
  {
    userName: "Maria",
    city: "Bucharest",
    lastUpdateDate: new Date(2025, 5, 12, 10),
  },
  {
    userName: "Alex",
    city: "Kiev",
    lastUpdateDate: new Date(2025, 5, 11, 14),
  },
  {
    userName: "Elena",
    city: "Chisinau",
    lastUpdateDate: new Date(2025, 5, 10, 9),
  },
  {
    userName: "Dmitri",
    city: "Odesa",
    lastUpdateDate: new Date(2025, 5, 9, 16),
  },
  {
    userName: "Ana",
    city: "Bucharest",
    lastUpdateDate: new Date(2025, 5, 8, 11),
  },
  {
    userName: "Viktor",
    city: "Chisinau",
    lastUpdateDate: new Date(2025, 5, 7, 13),
  },
  {
    userName: "Irina",
    city: "Chisinau",
    lastUpdateDate: new Date(2025, 5, 6, 8),
  },
  {
    userName: "Sergei",
    city: "Kiev",
    lastUpdateDate: new Date(2025, 5, 5, 17),
  },
  {
    userName: "Natasha",
    city: "Timisoara",
    lastUpdateDate: new Date(2025, 5, 4, 12),
  },
  {
    userName: "Andrei",
    city: "Chisinau",
    lastUpdateDate: new Date(2025, 5, 3, 19),
  },
  {
    userName: "Cristina",
    city: "Cluj-Napoca",
    lastUpdateDate: new Date(2025, 5, 2, 14),
  },
  {
    userName: "Oleg",
    city: "Kharkiv",
    lastUpdateDate: new Date(2025, 5, 1, 7),
  },
  {
    userName: "Svetlana",
    city: "Bucharest",
    lastUpdateDate: new Date(2025, 4, 30, 20),
  },
  {
    userName: "Bogdan",
    city: "Balti",
    lastUpdateDate: new Date(2025, 4, 29, 11),
  },
  {
    userName: "Daniela",
    city: "Constanta",
    lastUpdateDate: new Date(2025, 4, 28, 16),
  },
  {
    userName: "Maxim",
    city: "Kiev",
    lastUpdateDate: new Date(2025, 4, 27, 9),
  },
  {
    userName: "Oxana",
    city: "Chisinau",
    lastUpdateDate: new Date(2025, 4, 26, 13),
  },
  {
    userName: "Radu",
    city: "Iasi",
    lastUpdateDate: new Date(2025, 4, 25, 18),
  },
  {
    userName: "Larisa",
    city: "Dnipro",
    lastUpdateDate: new Date(2025, 4, 24, 15),
  },
  {
    userName: "Mihai",
    city: "Bucharest",
    lastUpdateDate: new Date(2025, 4, 23, 21),
  },
  {
    userName: "Oksana",
    city: "Lviv",
    lastUpdateDate: new Date(2025, 4, 22, 6),
  },
  {
    userName: "Vladislav",
    city: "Tiraspol",
    lastUpdateDate: new Date(2025, 4, 21, 12),
  },
  {
    userName: "Alina",
    city: "Craiova",
    lastUpdateDate: new Date(2025, 4, 20, 10),
  }
]

const UserItemContainer = () => {
  const pageSize = 10
  const [currentPage, setCurrentPage] = useState(1)

  const paginatedResult = usePagination(userList, currentPage, pageSize)

  const scrollToTop = () => {
    window.scrollTo({
      top: 0,
      behavior: 'smooth'
    });
  };

  const handlePageChange = (page: number) => {
    setCurrentPage(page);
    setTimeout(scrollToTop, 50);
  };

  return (
    <>
      <div className={styles.userItemContainer}>
        {paginatedResult.items.map(user => (
          <UserItem
            key={`${user.userName}`}
            user={user}
          />
        ))}

        <Pagination
          count={Math.ceil(paginatedResult.total / paginatedResult.pageSize)}
          page={paginatedResult.pageNumber}
          onChange={(_event, page) => handlePageChange(page)}
        />
      </div>
    </>
  )
}

export default UserItemContainer