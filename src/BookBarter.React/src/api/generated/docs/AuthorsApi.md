# AuthorsApi

All URIs are relative to *http://localhost*

|Method | HTTP request | Description|
|------------- | ------------- | -------------|
|[**apiAuthorsPagedPost**](#apiauthorspagedpost) | **POST** /api/Authors/paged | |
|[**apiAuthorsPost**](#apiauthorspost) | **POST** /api/Authors | |

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
> apiAuthorsPost()


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

