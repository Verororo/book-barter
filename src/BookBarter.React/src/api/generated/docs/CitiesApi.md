# CitiesApi

All URIs are relative to _http://localhost_

| Method                                        | HTTP request               | Description |
| --------------------------------------------- | -------------------------- | ----------- |
| [**apiCitiesPagedPost**](#apicitiespagedpost) | **POST** /api/Cities/paged |             |

# **apiCitiesPagedPost**

> CityDtoPaginatedResult apiCitiesPagedPost()

### Example

```typescript
import { CitiesApi, Configuration, GetPagedCitiesQuery } from './api';

const configuration = new Configuration();
const apiInstance = new CitiesApi(configuration);

let getPagedCitiesQuery: GetPagedCitiesQuery; // (optional)

const { status, data } =
  await apiInstance.apiCitiesPagedPost(getPagedCitiesQuery);
```

### Parameters

| Name                    | Type                    | Description | Notes |
| ----------------------- | ----------------------- | ----------- | ----- |
| **getPagedCitiesQuery** | **GetPagedCitiesQuery** |             |       |

### Return type

**CityDtoPaginatedResult**

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
