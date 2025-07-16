# BookForModerationDto


## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**id** | **number** |  | [optional] [default to undefined]
**isbn** | **string** |  | [optional] [default to undefined]
**title** | **string** |  | [optional] [default to undefined]
**publicationDate** | **string** |  | [default to undefined]
**addedDate** | **string** |  | [default to undefined]
**approved** | **boolean** |  | [optional] [default to undefined]
**genre** | [**GenreDto**](GenreDto.md) |  | [optional] [default to undefined]
**publisher** | [**PublisherDto**](PublisherDto.md) |  | [optional] [default to undefined]
**authors** | [**Array&lt;AuthorDto&gt;**](AuthorDto.md) |  | [optional] [default to undefined]
**ownedByUsers** | [**Array&lt;BookRelationshipWithUserDto&gt;**](BookRelationshipWithUserDto.md) |  | [optional] [default to undefined]
**wantedByUsers** | [**Array&lt;BookRelationshipWithUserDto&gt;**](BookRelationshipWithUserDto.md) |  | [optional] [default to undefined]

## Example

```typescript
import { BookForModerationDto } from './api';

const instance: BookForModerationDto = {
    id,
    isbn,
    title,
    publicationDate,
    addedDate,
    approved,
    genre,
    publisher,
    authors,
    ownedByUsers,
    wantedByUsers,
};
```

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)
