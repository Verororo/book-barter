import { useEffect, useState } from "react"
import type { ListedBook } from "../../api/view-models/listed-book"
import BookItem from "../BookItem/BookItem"
import NewBookAutocomplete from "../BookItem/NewBookAutocomplete"
import styles from './GivingOut.module.css'
import type { ListedBookDto } from "../../api/generated"
import { deleteBookFromOwned } from "../../api/clients/user-client"
import { AnimatePresence } from "framer-motion"

type GivingOutSectionProps = {
  givingOutBooks: ListedBook[]
  customizable?: boolean
}

const GivingOutSection = ({ givingOutBooks, customizable = false }: GivingOutSectionProps) => {
  const [localBooks, setLocalBooks] = useState<ListedBook[]>(givingOutBooks);
  const [autocompleteKey, setAutocompleteKey] = useState(0);

  useEffect(() => {
    setLocalBooks(givingOutBooks);
  }, [givingOutBooks]);

  const handleAdd = (listedBookDto: ListedBookDto, bookStateName?: string) => {
    const authorsView = (listedBookDto.authors ?? [])
      .map(author => author.lastName)
      .join(", ")

    const listedBookView = {
      id: listedBookDto.id!,
      title: listedBookDto.title!,
      authors: authorsView,
      publicationYear: new Date(listedBookDto.publicationDate!).getFullYear(),
      publisherName: listedBookDto.publisherName ?? "",
      bookStateName: bookStateName
    }

    setLocalBooks(current => [...current, listedBookView])
    setAutocompleteKey(prev => prev + 1);
  }

  const handleDelete = async (deletedBookId: number) => {
    await deleteBookFromOwned({ bookId: deletedBookId })
    setLocalBooks(current => current.filter(b => b.id !== deletedBookId))
  };

  return (
    <div className={styles.givingOut}>
      <span className={styles.givingOutHeader}>
        <img src='/public/OrangeBookGivenOut.svg' />
        <p>is giving out...</p>
      </span>
      <div className={styles.bookItemContainer}>
        <AnimatePresence>
          {localBooks.map(book => (
            <BookItem
              key={book.id}
              listedBook={book}
              {...(customizable && { onBookDeleted: handleDelete })}
            />
          ))}
        </AnimatePresence>

        {customizable && (
          <NewBookAutocomplete
            key={autocompleteKey}
            isGivingOut
            onBookAdded={handleAdd}
          />
        )}
      </div>
    </div>
  )
}

export default GivingOutSection