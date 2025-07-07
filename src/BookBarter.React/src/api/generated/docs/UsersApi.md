# UsersApi

All URIs are relative to *http://localhost*

|Method | HTTP request | Description|
|------------- | ------------- | -------------|
|[**apiUsersIdDelete**](#apiusersiddelete) | **DELETE** /api/Users/{id} | |
|[**apiUsersIdGet**](#apiusersidget) | **GET** /api/Users/{id} | |
|[**apiUsersIdPut**](#apiusersidput) | **PUT** /api/Users/{id} | |
|[**apiUsersMeOwnedBooksBookIdDelete**](#apiusersmeownedbooksbookiddelete) | **DELETE** /api/Users/me/ownedBooks/{bookId} | |
|[**apiUsersMeOwnedBooksPost**](#apiusersmeownedbookspost) | **POST** /api/Users/me/ownedBooks | |
|[**apiUsersMeWantedBooksBookIdDelete**](#apiusersmewantedbooksbookiddelete) | **DELETE** /api/Users/me/wantedBooks/{bookId} | |
|[**apiUsersMeWantedBooksPost**](#apiusersmewantedbookspost) | **POST** /api/Users/me/wantedBooks | |
|[**apiUsersPagedPost**](#apiuserspagedpost) | **POST** /api/Users/paged | |

# **apiUsersIdDelete**
> apiUsersIdDelete()


### Example

```typescript
import {
    UsersApi,
    Configuration
} from './api';

const configuration = new Configuration();
const apiInstance = new UsersApi(configuration);

let id: number; // (default to undefined)

const { status, data } = await apiInstance.apiUsersIdDelete(
    id
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **id** | [**number**] |  | defaults to undefined|


### Return type

void (empty response body)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
|**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiUsersIdGet**
> UserDto apiUsersIdGet()


### Example

```typescript
import {
    UsersApi,
    Configuration
} from './api';

const configuration = new Configuration();
const apiInstance = new UsersApi(configuration);

let id: number; // (default to undefined)

const { status, data } = await apiInstance.apiUsersIdGet(
    id
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **id** | [**number**] |  | defaults to undefined|


### Return type

**UserDto**

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
|**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiUsersIdPut**
> apiUsersIdPut()


### Example

```typescript
import {
    UsersApi,
    Configuration,
    UpdateUserCommand
} from './api';

const configuration = new Configuration();
const apiInstance = new UsersApi(configuration);

let id: number; // (default to undefined)
let updateUserCommand: UpdateUserCommand; // (optional)

const { status, data } = await apiInstance.apiUsersIdPut(
    id,
    updateUserCommand
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **updateUserCommand** | **UpdateUserCommand**|  | |
| **id** | [**number**] |  | defaults to undefined|


### Return type

void (empty response body)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
|**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiUsersMeOwnedBooksBookIdDelete**
> apiUsersMeOwnedBooksBookIdDelete()


### Example

```typescript
import {
    UsersApi,
    Configuration,
    DeleteOwnedBookCommand
} from './api';

const configuration = new Configuration();
const apiInstance = new UsersApi(configuration);

let bookId: number; // (default to undefined)
let deleteOwnedBookCommand: DeleteOwnedBookCommand; // (optional)

const { status, data } = await apiInstance.apiUsersMeOwnedBooksBookIdDelete(
    bookId,
    deleteOwnedBookCommand
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **deleteOwnedBookCommand** | **DeleteOwnedBookCommand**|  | |
| **bookId** | [**number**] |  | defaults to undefined|


### Return type

void (empty response body)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
|**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiUsersMeOwnedBooksPost**
> apiUsersMeOwnedBooksPost()


### Example

```typescript
import {
    UsersApi,
    Configuration,
    AddOwnedBookCommand
} from './api';

const configuration = new Configuration();
const apiInstance = new UsersApi(configuration);

let addOwnedBookCommand: AddOwnedBookCommand; // (optional)

const { status, data } = await apiInstance.apiUsersMeOwnedBooksPost(
    addOwnedBookCommand
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **addOwnedBookCommand** | **AddOwnedBookCommand**|  | |


### Return type

void (empty response body)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
|**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiUsersMeWantedBooksBookIdDelete**
> apiUsersMeWantedBooksBookIdDelete()


### Example

```typescript
import {
    UsersApi,
    Configuration,
    DeleteWantedBookCommand
} from './api';

const configuration = new Configuration();
const apiInstance = new UsersApi(configuration);

let bookId: number; // (default to undefined)
let deleteWantedBookCommand: DeleteWantedBookCommand; // (optional)

const { status, data } = await apiInstance.apiUsersMeWantedBooksBookIdDelete(
    bookId,
    deleteWantedBookCommand
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **deleteWantedBookCommand** | **DeleteWantedBookCommand**|  | |
| **bookId** | [**number**] |  | defaults to undefined|


### Return type

void (empty response body)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
|**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiUsersMeWantedBooksPost**
> apiUsersMeWantedBooksPost()


### Example

```typescript
import {
    UsersApi,
    Configuration,
    AddWantedBookCommand
} from './api';

const configuration = new Configuration();
const apiInstance = new UsersApi(configuration);

let addWantedBookCommand: AddWantedBookCommand; // (optional)

const { status, data } = await apiInstance.apiUsersMeWantedBooksPost(
    addWantedBookCommand
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **addWantedBookCommand** | **AddWantedBookCommand**|  | |


### Return type

void (empty response body)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
|**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiUsersPagedPost**
> ListedUserDtoPaginatedResult apiUsersPagedPost()


### Example

```typescript
import {
    UsersApi,
    Configuration,
    GetPagedUsersQuery
} from './api';

const configuration = new Configuration();
const apiInstance = new UsersApi(configuration);

let getPagedUsersQuery: GetPagedUsersQuery; // (optional)

const { status, data } = await apiInstance.apiUsersPagedPost(
    getPagedUsersQuery
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **getPagedUsersQuery** | **GetPagedUsersQuery**|  | |


### Return type

**ListedUserDtoPaginatedResult**

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: text/plain, application/json, text/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
|**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

