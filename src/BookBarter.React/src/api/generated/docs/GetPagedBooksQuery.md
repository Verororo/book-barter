# GetPagedBooksQuery


## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**pageSize** | **number** |  | [optional] [default to undefined]
**pageNumber** | **number** |  | [optional] [default to undefined]
**orderByProperty** | **string** |  | [optional] [default to undefined]
**orderDirection** | **string** |  | [optional] [default to undefined]
**skipLoggedInUserBooks** | **boolean** |  | [optional] [default to undefined]
**idsToSkip** | **Array&lt;number&gt;** |  | [optional] [default to undefined]
**title** | **string** |  | [optional] [default to undefined]
**authorId** | **number** |  | [optional] [default to undefined]
**genreId** | **number** |  | [optional] [default to undefined]
**publisherId** | **number** |  | [optional] [default to undefined]

## Example

```typescript
import { GetPagedBooksQuery } from './api';

const instance: GetPagedBooksQuery = {
    pageSize,
    pageNumber,
    orderByProperty,
    orderDirection,
    skipLoggedInUserBooks,
    idsToSkip,
    title,
    authorId,
    genreId,
    publisherId,
};
```

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)
