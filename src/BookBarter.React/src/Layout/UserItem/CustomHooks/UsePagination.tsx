import { useMemo } from 'react'

export interface PaginationResult<T> {
  pageNumber: number
  pageSize: number
  total: number
  items: T[]
}

export const usePagination = <T,>(
  items: T[],
  currentPage: number,
  pageSize: number
): PaginationResult<T> => {
  const paginatedResult = useMemo(() => {
    const validPageSize = Math.max(1, pageSize)
    const validCurrentPage = Math.max(1, currentPage)

    const totalItems = items.length
    const startIndex = (validCurrentPage - 1) * validPageSize
    const endIndex = startIndex + validPageSize
    const paginatedItems = items.slice(startIndex, endIndex)

    return {
      pageNumber: validCurrentPage,
      pageSize: validPageSize,
      total: totalItems,
      items: paginatedItems,
    }
  }, [items, currentPage, pageSize])

  return paginatedResult
}

export default usePagination