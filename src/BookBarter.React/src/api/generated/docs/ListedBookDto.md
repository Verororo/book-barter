# ListedBookDto

## Properties

| Name                | Type                                       | Description | Notes                             |
| ------------------- | ------------------------------------------ | ----------- | --------------------------------- |
| **id**              | **number**                                 |             | [optional] [default to undefined] |
| **title**           | **string**                                 |             | [optional] [default to undefined] |
| **publicationDate** | **string**                                 |             | [optional] [default to undefined] |
| **approved**        | **boolean**                                |             | [optional] [default to undefined] |
| **genreName**       | **string**                                 |             | [optional] [default to undefined] |
| **publisherName**   | **string**                                 |             | [optional] [default to undefined] |
| **authors**         | [**Array&lt;AuthorDto&gt;**](AuthorDto.md) |             | [optional] [default to undefined] |

## Example

```typescript
import { ListedBookDto } from './api';

const instance: ListedBookDto = {
  id,
  title,
  publicationDate,
  approved,
  genreName,
  publisherName,
  authors,
};
```

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)
