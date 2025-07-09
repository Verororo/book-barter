# GenresApi

All URIs are relative to *http://localhost*

|Method | HTTP request | Description|
|------------- | ------------- | -------------|
|[**apiGenresPagedPost**](#apigenrespagedpost) | **POST** /api/Genres/paged | |

# **apiGenresPagedPost**
> GenreDtoPaginatedResult apiGenresPagedPost()


### Example

```typescript
import {
    GenresApi,
    Configuration,
    GetPagedGenresQuery
} from './api';

const configuration = new Configuration();
const apiInstance = new GenresApi(configuration);

let getPagedGenresQuery: GetPagedGenresQuery; // (optional)

const { status, data } = await apiInstance.apiGenresPagedPost(
    getPagedGenresQuery
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **getPagedGenresQuery** | **GetPagedGenresQuery**|  | |


### Return type

**GenreDtoPaginatedResult**

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

