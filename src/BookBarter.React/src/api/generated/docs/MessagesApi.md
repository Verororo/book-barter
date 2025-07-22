# MessagesApi

All URIs are relative to *http://localhost*

|Method | HTTP request | Description|
|------------- | ------------- | -------------|
|[**apiMessagesPagedPost**](#apimessagespagedpost) | **POST** /api/Messages/paged | |
|[**apiMessagesPost**](#apimessagespost) | **POST** /api/Messages | |

# **apiMessagesPagedPost**
> MessageDtoPaginatedResult apiMessagesPagedPost()


### Example

```typescript
import {
    MessagesApi,
    Configuration,
    GetPagedMessagesQuery
} from './api';

const configuration = new Configuration();
const apiInstance = new MessagesApi(configuration);

let getPagedMessagesQuery: GetPagedMessagesQuery; // (optional)

const { status, data } = await apiInstance.apiMessagesPagedPost(
    getPagedMessagesQuery
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **getPagedMessagesQuery** | **GetPagedMessagesQuery**|  | |


### Return type

**MessageDtoPaginatedResult**

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

# **apiMessagesPost**
> number apiMessagesPost()


### Example

```typescript
import {
    MessagesApi,
    Configuration,
    CreateMessageCommand
} from './api';

const configuration = new Configuration();
const apiInstance = new MessagesApi(configuration);

let createMessageCommand: CreateMessageCommand; // (optional)

const { status, data } = await apiInstance.apiMessagesPost(
    createMessageCommand
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **createMessageCommand** | **CreateMessageCommand**|  | |


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

