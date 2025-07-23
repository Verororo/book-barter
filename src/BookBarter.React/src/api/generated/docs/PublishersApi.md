# PublishersApi

All URIs are relative to _http://localhost_

| Method                                                                  | HTTP request                             | Description |
| ----------------------------------------------------------------------- | ---------------------------------------- | ----------- |
| [**apiPublishersIdApprovePut**](#apipublishersidapproveput)             | **PUT** /api/Publishers/{id}/approve     |             |
| [**apiPublishersIdDelete**](#apipublishersiddelete)                     | **DELETE** /api/Publishers/{id}          |             |
| [**apiPublishersIdPut**](#apipublishersidput)                           | **PUT** /api/Publishers/{id}             |             |
| [**apiPublishersPagedModeratedPost**](#apipublisherspagedmoderatedpost) | **POST** /api/Publishers/paged/moderated |             |
| [**apiPublishersPagedPost**](#apipublisherspagedpost)                   | **POST** /api/Publishers/paged           |             |
| [**apiPublishersPost**](#apipublisherspost)                             | **POST** /api/Publishers                 |             |

# **apiPublishersIdApprovePut**

> apiPublishersIdApprovePut()

### Example

```typescript
import { PublishersApi, Configuration } from './api';

const configuration = new Configuration();
const apiInstance = new PublishersApi(configuration);

let id: number; // (default to undefined)

const { status, data } = await apiInstance.apiPublishersIdApprovePut(id);
```

### Parameters

| Name   | Type         | Description | Notes                 |
| ------ | ------------ | ----------- | --------------------- |
| **id** | [**number**] |             | defaults to undefined |

### Return type

void (empty response body)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

### HTTP response details

| Status code | Description | Response headers |
| ----------- | ----------- | ---------------- |
| **200**     | OK          | -                |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiPublishersIdDelete**

> apiPublishersIdDelete()

### Example

```typescript
import { PublishersApi, Configuration } from './api';

const configuration = new Configuration();
const apiInstance = new PublishersApi(configuration);

let id: number; // (default to undefined)

const { status, data } = await apiInstance.apiPublishersIdDelete(id);
```

### Parameters

| Name   | Type         | Description | Notes                 |
| ------ | ------------ | ----------- | --------------------- |
| **id** | [**number**] |             | defaults to undefined |

### Return type

void (empty response body)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

### HTTP response details

| Status code | Description | Response headers |
| ----------- | ----------- | ---------------- |
| **200**     | OK          | -                |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiPublishersIdPut**

> apiPublishersIdPut()

### Example

```typescript
import { PublishersApi, Configuration, UpdatePublisherCommand } from './api';

const configuration = new Configuration();
const apiInstance = new PublishersApi(configuration);

let id: number; // (default to undefined)
let updatePublisherCommand: UpdatePublisherCommand; // (optional)

const { status, data } = await apiInstance.apiPublishersIdPut(
  id,
  updatePublisherCommand,
);
```

### Parameters

| Name                       | Type                       | Description | Notes                 |
| -------------------------- | -------------------------- | ----------- | --------------------- |
| **updatePublisherCommand** | **UpdatePublisherCommand** |             |                       |
| **id**                     | [**number**]               |             | defaults to undefined |

### Return type

void (empty response body)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/\*+json
- **Accept**: Not defined

### HTTP response details

| Status code | Description | Response headers |
| ----------- | ----------- | ---------------- |
| **200**     | OK          | -                |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiPublishersPagedModeratedPost**

> PublisherForModerationDtoPaginatedResult apiPublishersPagedModeratedPost()

### Example

```typescript
import {
  PublishersApi,
  Configuration,
  GetPagedPublishersForModerationQuery,
} from './api';

const configuration = new Configuration();
const apiInstance = new PublishersApi(configuration);

let getPagedPublishersForModerationQuery: GetPagedPublishersForModerationQuery; // (optional)

const { status, data } = await apiInstance.apiPublishersPagedModeratedPost(
  getPagedPublishersForModerationQuery,
);
```

### Parameters

| Name                                     | Type                                     | Description | Notes |
| ---------------------------------------- | ---------------------------------------- | ----------- | ----- |
| **getPagedPublishersForModerationQuery** | **GetPagedPublishersForModerationQuery** |             |       |

### Return type

**PublisherForModerationDtoPaginatedResult**

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/\*+json
- **Accept**: text/plain, application/json, text/json

### HTTP response details

| Status code | Description | Response headers |
| ----------- | ----------- | ---------------- |
| **200**     | OK          | -                |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiPublishersPagedPost**

> PublisherDtoPaginatedResult apiPublishersPagedPost()

### Example

```typescript
import { PublishersApi, Configuration, GetPagedPublishersQuery } from './api';

const configuration = new Configuration();
const apiInstance = new PublishersApi(configuration);

let getPagedPublishersQuery: GetPagedPublishersQuery; // (optional)

const { status, data } = await apiInstance.apiPublishersPagedPost(
  getPagedPublishersQuery,
);
```

### Parameters

| Name                        | Type                        | Description | Notes |
| --------------------------- | --------------------------- | ----------- | ----- |
| **getPagedPublishersQuery** | **GetPagedPublishersQuery** |             |       |

### Return type

**PublisherDtoPaginatedResult**

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/\*+json
- **Accept**: text/plain, application/json, text/json

### HTTP response details

| Status code | Description | Response headers |
| ----------- | ----------- | ---------------- |
| **200**     | OK          | -                |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiPublishersPost**

> number apiPublishersPost()

### Example

```typescript
import { PublishersApi, Configuration, CreatePublisherCommand } from './api';

const configuration = new Configuration();
const apiInstance = new PublishersApi(configuration);

let createPublisherCommand: CreatePublisherCommand; // (optional)

const { status, data } = await apiInstance.apiPublishersPost(
  createPublisherCommand,
);
```

### Parameters

| Name                       | Type                       | Description | Notes |
| -------------------------- | -------------------------- | ----------- | ----- |
| **createPublisherCommand** | **CreatePublisherCommand** |             |       |

### Return type

**number**

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/\*+json
- **Accept**: text/plain, application/json, text/json

### HTTP response details

| Status code | Description | Response headers |
| ----------- | ----------- | ---------------- |
| **200**     | OK          | -                |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
