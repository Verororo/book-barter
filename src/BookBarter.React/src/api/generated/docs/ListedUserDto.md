# ListedUserDto


## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**id** | **number** |  | [optional] [default to undefined]
**userName** | **string** |  | [optional] [default to undefined]
**cityName** | **string** |  | [optional] [default to undefined]
**lastOnlineDate** | **string** |  | [optional] [default to undefined]
**ownedBooks** | [**Array&lt;OwnedBookDto&gt;**](OwnedBookDto.md) |  | [optional] [default to undefined]
**wantedBooks** | [**Array&lt;WantedBookDto&gt;**](WantedBookDto.md) |  | [optional] [default to undefined]

## Example

```typescript
import { ListedUserDto } from './api';

const instance: ListedUserDto = {
    id,
    userName,
    cityName,
    lastOnlineDate,
    ownedBooks,
    wantedBooks,
};
```

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)
