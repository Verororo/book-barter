# AuthorsApi

All URIs are relative to *http://localhost*

|Method | HTTP request | Description|
|------------- | ------------- | -------------|
|[**apiAuthorsIdApprovePut**](#apiauthorsidapproveput) | **PUT** /api/Authors/{id}/approve | |
|[**apiAuthorsIdDelete**](#apiauthorsiddelete) | **DELETE** /api/Authors/{id} | |
|[**apiAuthorsIdPut**](#apiauthorsidput) | **PUT** /api/Authors/{id} | |
|[**apiAuthorsPagedModeratedPost**](#apiauthorspagedmoderatedpost) | **POST** /api/Authors/paged/moderated | |
|[**apiAuthorsPagedPost**](#apiauthorspagedpost) | **POST** /api/Authors/paged | |
|[**apiAuthorsPost**](#apiauthorspost) | **POST** /api/Authors | |

# **apiAuthorsIdApprovePut**
> apiAuthorsIdApprovePut()


### Example

```typescript
import {
    AuthorsApi,
    Configuration
} from './api';

const configuration = new Configuration();
const apiInstance = new AuthorsApi(configuration);

let id: number; // (default to undefined)

const { status, data } = await apiInstance.apiAuthorsIdApprovePut(
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

# **apiAuthorsIdDelete**
> apiAuthorsIdDelete()


### Example

```typescript
import {
    AuthorsApi,
    Configuration
} from './api';

const configuration = new Configuration();
const apiInstance = new AuthorsApi(configuration);

let id: number; // (default to undefined)

const { status, data } = await apiInstance.apiAuthorsIdDelete(
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

# **apiAuthorsIdPut**
> apiAuthorsIdPut()


### Example

```typescript
import {
    AuthorsApi,
    Configuration,
    UpdateAuthorCommand
} from './api';

const configuration = new Configuration();
const apiInstance = new AuthorsApi(configuration);

let id: number; // (default to undefined)
let updateAuthorCommand: UpdateAuthorCommand; // (optional)

const { status, data } = await apiInstance.apiAuthorsIdPut(
    id,
    updateAuthorCommand
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **updateAuthorCommand** | **UpdateAuthorCommand**|  | |
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

# **apiAuthorsPagedModeratedPost**
> AuthorForModerationDtoPaginatedResult apiAuthorsPagedModeratedPost()


### Example

```typescript
import {
    AuthorsApi,
    Configuration,
    GetPagedAuthorsForModerationQuery
} from './api';

const configuration = new Configuration();
const apiInstance = new AuthorsApi(configuration);

let getPagedAuthorsForModerationQuery: GetPagedAuthorsForModerationQuery; // (optional)

const { status, data } = await apiInstance.apiAuthorsPagedModeratedPost(
    getPagedAuthorsForModerationQuery
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **getPagedAuthorsForModerationQuery** | **GetPagedAuthorsForModerationQuery**|  | |


### Return type

**AuthorForModerationDtoPaginatedResult**

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

# **apiAuthorsPagedPost**
> AuthorDtoPaginatedResult apiAuthorsPagedPost()


### Example

```typescript
import {
    AuthorsApi,
    Configuration,
    GetPagedAuthorsQuery
} from './api';

const configuration = new Configuration();
const apiInstance = new AuthorsApi(configuration);

let getPagedAuthorsQuery: GetPagedAuthorsQuery; // (optional)

const { status, data } = await apiInstance.apiAuthorsPagedPost(
    getPagedAuthorsQuery
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **getPagedAuthorsQuery** | **GetPagedAuthorsQuery**|  | |


### Return type

**AuthorDtoPaginatedResult**

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

# **apiAuthorsPost**
> number apiAuthorsPost()


### Example

```typescript
import {
    AuthorsApi,
    Configuration,
    CreateAuthorCommand
} from './api';

const configuration = new Configuration();
const apiInstance = new AuthorsApi(configuration);

let createAuthorCommand: CreateAuthorCommand; // (optional)

const { status, data } = await apiInstance.apiAuthorsPost(
    createAuthorCommand
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **createAuthorCommand** | **CreateAuthorCommand**|  | |


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

