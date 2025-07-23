# MessageDtoPaginatedResult

## Properties

| Name           | Type                                         | Description | Notes                             |
| -------------- | -------------------------------------------- | ----------- | --------------------------------- |
| **pageNumber** | **number**                                   |             | [optional] [default to undefined] |
| **pageSize**   | **number**                                   |             | [optional] [default to undefined] |
| **total**      | **number**                                   |             | [optional] [default to undefined] |
| **items**      | [**Array&lt;MessageDto&gt;**](MessageDto.md) |             | [optional] [default to undefined] |

## Example

```typescript
import { MessageDtoPaginatedResult } from './api';

const instance: MessageDtoPaginatedResult = {
  pageNumber,
  pageSize,
  total,
  items,
};
```

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)
