# PublishersApi

All URIs are relative to *http://localhost*

|Method | HTTP request | Description|
|------------- | ------------- | -------------|
|[**apiPublishersPagedPost**](#apipublisherspagedpost) | **POST** /api/Publishers/paged | |

# **apiPublishersPagedPost**
> PublisherDtoPaginatedResult apiPublishersPagedPost()


### Example

```typescript
import {
    PublishersApi,
    Configuration,
    GetPagedPublishersQuery
} from './api';

const configuration = new Configuration();
const apiInstance = new PublishersApi(configuration);

let getPagedPublishersQuery: GetPagedPublishersQuery; // (optional)

const { status, data } = await apiInstance.apiPublishersPagedPost(
    getPagedPublishersQuery
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **getPagedPublishersQuery** | **GetPagedPublishersQuery**|  | |


### Return type

**PublisherDtoPaginatedResult**

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

