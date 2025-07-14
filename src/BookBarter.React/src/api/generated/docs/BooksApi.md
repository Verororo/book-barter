# BooksApi

All URIs are relative to *http://localhost*

|Method | HTTP request | Description|
|------------- | ------------- | -------------|
|[**apiBooksIdApprovePut**](#apibooksidapproveput) | **PUT** /api/Books/{id}/approve | |
|[**apiBooksIdDelete**](#apibooksiddelete) | **DELETE** /api/Books/{id} | |
|[**apiBooksIdGet**](#apibooksidget) | **GET** /api/Books/{id} | |
|[**apiBooksIdPut**](#apibooksidput) | **PUT** /api/Books/{id} | |
|[**apiBooksPagedModeratedPost**](#apibookspagedmoderatedpost) | **POST** /api/Books/paged/moderated | |
|[**apiBooksPagedPost**](#apibookspagedpost) | **POST** /api/Books/paged | |
|[**apiBooksPost**](#apibookspost) | **POST** /api/Books | |

# **apiBooksIdApprovePut**
> apiBooksIdApprovePut()


### Example

```typescript
import {
    BooksApi,
    Configuration
} from './api';

const configuration = new Configuration();
const apiInstance = new BooksApi(configuration);

let id: number; // (default to undefined)

const { status, data } = await apiInstance.apiBooksIdApprovePut(
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

# **apiBooksIdDelete**
> apiBooksIdDelete()


### Example

```typescript
import {
    BooksApi,
    Configuration
} from './api';

const configuration = new Configuration();
const apiInstance = new BooksApi(configuration);

let id: number; // (default to undefined)

const { status, data } = await apiInstance.apiBooksIdDelete(
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

# **apiBooksIdGet**
> BookDto apiBooksIdGet()


### Example

```typescript
import {
    BooksApi,
    Configuration
} from './api';

const configuration = new Configuration();
const apiInstance = new BooksApi(configuration);

let id: number; // (default to undefined)

const { status, data } = await apiInstance.apiBooksIdGet(
    id
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **id** | [**number**] |  | defaults to undefined|


### Return type

**BookDto**

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

# **apiBooksIdPut**
> apiBooksIdPut()


### Example

```typescript
import {
    BooksApi,
    Configuration,
    UpdateBookCommand
} from './api';

const configuration = new Configuration();
const apiInstance = new BooksApi(configuration);

let id: number; // (default to undefined)
let updateBookCommand: UpdateBookCommand; // (optional)

const { status, data } = await apiInstance.apiBooksIdPut(
    id,
    updateBookCommand
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **updateBookCommand** | **UpdateBookCommand**|  | |
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

# **apiBooksPagedModeratedPost**
> BookForModerationDtoPaginatedResult apiBooksPagedModeratedPost()


### Example

```typescript
import {
    BooksApi,
    Configuration,
    GetPagedBooksForModerationQuery
} from './api';

const configuration = new Configuration();
const apiInstance = new BooksApi(configuration);

let getPagedBooksForModerationQuery: GetPagedBooksForModerationQuery; // (optional)

const { status, data } = await apiInstance.apiBooksPagedModeratedPost(
    getPagedBooksForModerationQuery
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **getPagedBooksForModerationQuery** | **GetPagedBooksForModerationQuery**|  | |


### Return type

**BookForModerationDtoPaginatedResult**

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

# **apiBooksPagedPost**
> BookDtoPaginatedResult apiBooksPagedPost()


### Example

```typescript
import {
    BooksApi,
    Configuration,
    GetPagedBooksQuery
} from './api';

const configuration = new Configuration();
const apiInstance = new BooksApi(configuration);

let getPagedBooksQuery: GetPagedBooksQuery; // (optional)

const { status, data } = await apiInstance.apiBooksPagedPost(
    getPagedBooksQuery
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **getPagedBooksQuery** | **GetPagedBooksQuery**|  | |


### Return type

**BookDtoPaginatedResult**

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

# **apiBooksPost**
> number apiBooksPost()


### Example

```typescript
import {
    BooksApi,
    Configuration,
    CreateBookCommand
} from './api';

const configuration = new Configuration();
const apiInstance = new BooksApi(configuration);

let createBookCommand: CreateBookCommand; // (optional)

const { status, data } = await apiInstance.apiBooksPost(
    createBookCommand
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **createBookCommand** | **CreateBookCommand**|  | |


### Return type

**number**

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

