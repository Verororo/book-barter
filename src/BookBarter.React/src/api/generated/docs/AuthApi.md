# AuthApi

All URIs are relative to *http://localhost*

|Method | HTTP request | Description|
|------------- | ------------- | -------------|
|[**apiAuthLoginPost**](#apiauthloginpost) | **POST** /api/Auth/login | |
|[**apiAuthRegisterPost**](#apiauthregisterpost) | **POST** /api/Auth/register | |

# **apiAuthLoginPost**
> string apiAuthLoginPost()


### Example

```typescript
import {
    AuthApi,
    Configuration,
    LoginCommand
} from './api';

const configuration = new Configuration();
const apiInstance = new AuthApi(configuration);

let loginCommand: LoginCommand; // (optional)

const { status, data } = await apiInstance.apiAuthLoginPost(
    loginCommand
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **loginCommand** | **LoginCommand**|  | |


### Return type

**string**

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

# **apiAuthRegisterPost**
> apiAuthRegisterPost()


### Example

```typescript
import {
    AuthApi,
    Configuration,
    RegisterCommand
} from './api';

const configuration = new Configuration();
const apiInstance = new AuthApi(configuration);

let registerCommand: RegisterCommand; // (optional)

const { status, data } = await apiInstance.apiAuthRegisterPost(
    registerCommand
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **registerCommand** | **RegisterCommand**|  | |


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

