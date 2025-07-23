# CreateBookCommand

## Properties

| Name                | Type                    | Description | Notes                             |
| ------------------- | ----------------------- | ----------- | --------------------------------- |
| **isbn**            | **string**              |             | [optional] [default to undefined] |
| **title**           | **string**              |             | [optional] [default to undefined] |
| **publicationDate** | **string**              |             | [optional] [default to undefined] |
| **authorsIds**      | **Array&lt;number&gt;** |             | [optional] [default to undefined] |
| **genreId**         | **number**              |             | [optional] [default to undefined] |
| **publisherId**     | **number**              |             | [optional] [default to undefined] |

## Example

```typescript
import { CreateBookCommand } from './api';

const instance: CreateBookCommand = {
  isbn,
  title,
  publicationDate,
  authorsIds,
  genreId,
  publisherId,
};
```

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)
