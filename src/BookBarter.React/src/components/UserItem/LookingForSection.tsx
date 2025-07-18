import { useState, useEffect } from "react"
import { deleteBookFromWanted } from "../../api/clients/user-client"
import type { ListedBookDto } from "../../api/generated"
import type { ListedBook } from "../../api/view-models/listed-book"
import BookItem from "../BookItem/BookItem"
import styles from './LookingFor.module.css'
import { AnimatePresence } from "framer-motion"
import NewBookAutocomplete from "../BookItem/NewBookAutocomplete"
import { useNotification } from "../../contexts/Notification/UseNotification"

type LookingForSectionProps = {
  lookingForBooks: ListedBook[]
  customizable?: boolean
}

const LookingForSection = ({ lookingForBooks, customizable = false }: LookingForSectionProps) => {
  const [localBooks, setLocalBooks] = useState<ListedBook[]>(lookingForBooks);
  const [autocompleteKey, setAutocompleteKey] = useState(0);

  const { showNotification } = useNotification();

  useEffect(() => {
    setLocalBooks(lookingForBooks);
  }, [lookingForBooks]);

  const handleAdd = (listedBookDto: ListedBookDto) => {
    const authorsView = (listedBookDto.authors ?? [])
      .map(author => author.lastName)
      .join(", ")

    const listedBookView = {
      id: listedBookDto.id!,
      title: listedBookDto.title!,
      authors: authorsView,
      approved: listedBookDto.approved!,
      publicationYear: new Date(listedBookDto.publicationDate!).getFullYear(),
      publisherName: listedBookDto.publisherName ?? ""
    }

    setLocalBooks(current => [...current, listedBookView])
    setAutocompleteKey(prev => prev + 1);
  }

  const handleDelete = async (deletedBookId: number) => {
      try {
        await deleteBookFromWanted({ bookId: deletedBookId })
        showNotification("Succesfully deleted the book from the looking for section.", "success");
        setLocalBooks(current => current.filter(b => b.id !== deletedBookId))
      } catch (error) {
        showNotification("Failed to delete the book from the looking for section. Try again later.", "error")
      }
    };

  return (
    <div className={styles.lookingFor}>
      <span className={styles.lookingForHeader}>
        <img src='/BlueBookLookedFor.svg' />
        <p>is looking for...</p>
      </span>
      <div className={styles.bookItemContainer}>
        <AnimatePresence initial={false}>
          {localBooks.map(book => (
            <BookItem
              key={book.id}
              book={book}
              {...(customizable && { onBookDeleted: handleDelete })}
            />
          ))}
        </AnimatePresence>

        {customizable && (
          <NewBookAutocomplete
            key={autocompleteKey}
            onBookAdded={handleAdd}
          />
        )}
      </div>
    </div>
  )
}

export default LookingForSection