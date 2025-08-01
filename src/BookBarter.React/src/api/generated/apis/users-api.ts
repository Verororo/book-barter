/* tslint:disable */
/* eslint-disable */
/**
 * BookBarter.API
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: 1.0
 *
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */

import type { Configuration } from '../configuration';
import type { AxiosPromise, AxiosInstance, RawAxiosRequestConfig } from 'axios';
import globalAxios from 'axios';
// Some imports not used depending on template conditions
// @ts-ignore
import {
  DUMMY_BASE_URL,
  assertParamExists,
  setApiKeyToObject,
  setBasicAuthToObject,
  setBearerAuthToObject,
  setOAuthToObject,
  setSearchParams,
  serializeDataIfNeeded,
  toPathString,
  createRequestFunction,
} from '../common';
// @ts-ignore
import {
  BASE_PATH,
  COLLECTION_FORMATS,
  type RequestArgs,
  BaseAPI,
  RequiredError,
  operationServerMap,
} from '../base';
// @ts-ignore
import type { AddOwnedBookCommand } from '../models';
// @ts-ignore
import type { AddWantedBookCommand } from '../models';
// @ts-ignore
import type { DeleteOwnedBookCommand } from '../models';
// @ts-ignore
import type { DeleteWantedBookCommand } from '../models';
// @ts-ignore
import type { GetPagedUsersQuery } from '../models';
// @ts-ignore
import type { ListedUserDtoPaginatedResult } from '../models';
// @ts-ignore
import type { MessagingUserDto } from '../models';
// @ts-ignore
import type { UpdateUserCommand } from '../models';
// @ts-ignore
import type { UserDto } from '../models';
/**
 * UsersApi - axios parameter creator
 * @export
 */
export const UsersApiAxiosParamCreator = function (
  configuration?: Configuration,
) {
  return {
    /**
     *
     * @param {number} id
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    apiUsersIdDelete: async (
      id: number,
      options: RawAxiosRequestConfig = {},
    ): Promise<RequestArgs> => {
      // verify required parameter 'id' is not null or undefined
      assertParamExists('apiUsersIdDelete', 'id', id);
      const localVarPath = `/api/Users/{id}`.replace(
        `{${'id'}}`,
        encodeURIComponent(String(id)),
      );
      // use dummy base URL string because the URL constructor only accepts absolute URLs.
      const localVarUrlObj = new URL(localVarPath, DUMMY_BASE_URL);
      let baseOptions;
      if (configuration) {
        baseOptions = configuration.baseOptions;
      }

      const localVarRequestOptions = {
        method: 'DELETE',
        ...baseOptions,
        ...options,
      };
      const localVarHeaderParameter = {} as any;
      const localVarQueryParameter = {} as any;

      // authentication Bearer required
      // http bearer authentication required
      await setBearerAuthToObject(localVarHeaderParameter, configuration);

      setSearchParams(localVarUrlObj, localVarQueryParameter);
      let headersFromBaseOptions =
        baseOptions && baseOptions.headers ? baseOptions.headers : {};
      localVarRequestOptions.headers = {
        ...localVarHeaderParameter,
        ...headersFromBaseOptions,
        ...options.headers,
      };

      return {
        url: toPathString(localVarUrlObj),
        options: localVarRequestOptions,
      };
    },
    /**
     *
     * @param {number} id
     * @param {boolean} [excludeUnapprovedBooks]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    apiUsersIdGet: async (
      id: number,
      excludeUnapprovedBooks?: boolean,
      options: RawAxiosRequestConfig = {},
    ): Promise<RequestArgs> => {
      // verify required parameter 'id' is not null or undefined
      assertParamExists('apiUsersIdGet', 'id', id);
      const localVarPath = `/api/Users/{id}`.replace(
        `{${'id'}}`,
        encodeURIComponent(String(id)),
      );
      // use dummy base URL string because the URL constructor only accepts absolute URLs.
      const localVarUrlObj = new URL(localVarPath, DUMMY_BASE_URL);
      let baseOptions;
      if (configuration) {
        baseOptions = configuration.baseOptions;
      }

      const localVarRequestOptions = {
        method: 'GET',
        ...baseOptions,
        ...options,
      };
      const localVarHeaderParameter = {} as any;
      const localVarQueryParameter = {} as any;

      // authentication Bearer required
      // http bearer authentication required
      await setBearerAuthToObject(localVarHeaderParameter, configuration);

      if (excludeUnapprovedBooks !== undefined) {
        localVarQueryParameter['excludeUnapprovedBooks'] =
          excludeUnapprovedBooks;
      }

      setSearchParams(localVarUrlObj, localVarQueryParameter);
      let headersFromBaseOptions =
        baseOptions && baseOptions.headers ? baseOptions.headers : {};
      localVarRequestOptions.headers = {
        ...localVarHeaderParameter,
        ...headersFromBaseOptions,
        ...options.headers,
      };

      return {
        url: toPathString(localVarUrlObj),
        options: localVarRequestOptions,
      };
    },
    /**
     *
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    apiUsersMeChatsGet: async (
      options: RawAxiosRequestConfig = {},
    ): Promise<RequestArgs> => {
      const localVarPath = `/api/Users/me/chats`;
      // use dummy base URL string because the URL constructor only accepts absolute URLs.
      const localVarUrlObj = new URL(localVarPath, DUMMY_BASE_URL);
      let baseOptions;
      if (configuration) {
        baseOptions = configuration.baseOptions;
      }

      const localVarRequestOptions = {
        method: 'GET',
        ...baseOptions,
        ...options,
      };
      const localVarHeaderParameter = {} as any;
      const localVarQueryParameter = {} as any;

      // authentication Bearer required
      // http bearer authentication required
      await setBearerAuthToObject(localVarHeaderParameter, configuration);

      setSearchParams(localVarUrlObj, localVarQueryParameter);
      let headersFromBaseOptions =
        baseOptions && baseOptions.headers ? baseOptions.headers : {};
      localVarRequestOptions.headers = {
        ...localVarHeaderParameter,
        ...headersFromBaseOptions,
        ...options.headers,
      };

      return {
        url: toPathString(localVarUrlObj),
        options: localVarRequestOptions,
      };
    },
    /**
     *
     * @param {number} bookId
     * @param {DeleteOwnedBookCommand} [deleteOwnedBookCommand]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    apiUsersMeOwnedBooksBookIdDelete: async (
      bookId: number,
      deleteOwnedBookCommand?: DeleteOwnedBookCommand,
      options: RawAxiosRequestConfig = {},
    ): Promise<RequestArgs> => {
      // verify required parameter 'bookId' is not null or undefined
      assertParamExists('apiUsersMeOwnedBooksBookIdDelete', 'bookId', bookId);
      const localVarPath = `/api/Users/me/ownedBooks/{bookId}`.replace(
        `{${'bookId'}}`,
        encodeURIComponent(String(bookId)),
      );
      // use dummy base URL string because the URL constructor only accepts absolute URLs.
      const localVarUrlObj = new URL(localVarPath, DUMMY_BASE_URL);
      let baseOptions;
      if (configuration) {
        baseOptions = configuration.baseOptions;
      }

      const localVarRequestOptions = {
        method: 'DELETE',
        ...baseOptions,
        ...options,
      };
      const localVarHeaderParameter = {} as any;
      const localVarQueryParameter = {} as any;

      // authentication Bearer required
      // http bearer authentication required
      await setBearerAuthToObject(localVarHeaderParameter, configuration);

      localVarHeaderParameter['Content-Type'] = 'application/json';

      setSearchParams(localVarUrlObj, localVarQueryParameter);
      let headersFromBaseOptions =
        baseOptions && baseOptions.headers ? baseOptions.headers : {};
      localVarRequestOptions.headers = {
        ...localVarHeaderParameter,
        ...headersFromBaseOptions,
        ...options.headers,
      };
      localVarRequestOptions.data = serializeDataIfNeeded(
        deleteOwnedBookCommand,
        localVarRequestOptions,
        configuration,
      );

      return {
        url: toPathString(localVarUrlObj),
        options: localVarRequestOptions,
      };
    },
    /**
     *
     * @param {AddOwnedBookCommand} [addOwnedBookCommand]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    apiUsersMeOwnedBooksPost: async (
      addOwnedBookCommand?: AddOwnedBookCommand,
      options: RawAxiosRequestConfig = {},
    ): Promise<RequestArgs> => {
      const localVarPath = `/api/Users/me/ownedBooks`;
      // use dummy base URL string because the URL constructor only accepts absolute URLs.
      const localVarUrlObj = new URL(localVarPath, DUMMY_BASE_URL);
      let baseOptions;
      if (configuration) {
        baseOptions = configuration.baseOptions;
      }

      const localVarRequestOptions = {
        method: 'POST',
        ...baseOptions,
        ...options,
      };
      const localVarHeaderParameter = {} as any;
      const localVarQueryParameter = {} as any;

      // authentication Bearer required
      // http bearer authentication required
      await setBearerAuthToObject(localVarHeaderParameter, configuration);

      localVarHeaderParameter['Content-Type'] = 'application/json';

      setSearchParams(localVarUrlObj, localVarQueryParameter);
      let headersFromBaseOptions =
        baseOptions && baseOptions.headers ? baseOptions.headers : {};
      localVarRequestOptions.headers = {
        ...localVarHeaderParameter,
        ...headersFromBaseOptions,
        ...options.headers,
      };
      localVarRequestOptions.data = serializeDataIfNeeded(
        addOwnedBookCommand,
        localVarRequestOptions,
        configuration,
      );

      return {
        url: toPathString(localVarUrlObj),
        options: localVarRequestOptions,
      };
    },
    /**
     *
     * @param {UpdateUserCommand} [updateUserCommand]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    apiUsersMePut: async (
      updateUserCommand?: UpdateUserCommand,
      options: RawAxiosRequestConfig = {},
    ): Promise<RequestArgs> => {
      const localVarPath = `/api/Users/me`;
      // use dummy base URL string because the URL constructor only accepts absolute URLs.
      const localVarUrlObj = new URL(localVarPath, DUMMY_BASE_URL);
      let baseOptions;
      if (configuration) {
        baseOptions = configuration.baseOptions;
      }

      const localVarRequestOptions = {
        method: 'PUT',
        ...baseOptions,
        ...options,
      };
      const localVarHeaderParameter = {} as any;
      const localVarQueryParameter = {} as any;

      // authentication Bearer required
      // http bearer authentication required
      await setBearerAuthToObject(localVarHeaderParameter, configuration);

      localVarHeaderParameter['Content-Type'] = 'application/json';

      setSearchParams(localVarUrlObj, localVarQueryParameter);
      let headersFromBaseOptions =
        baseOptions && baseOptions.headers ? baseOptions.headers : {};
      localVarRequestOptions.headers = {
        ...localVarHeaderParameter,
        ...headersFromBaseOptions,
        ...options.headers,
      };
      localVarRequestOptions.data = serializeDataIfNeeded(
        updateUserCommand,
        localVarRequestOptions,
        configuration,
      );

      return {
        url: toPathString(localVarUrlObj),
        options: localVarRequestOptions,
      };
    },
    /**
     *
     * @param {number} bookId
     * @param {DeleteWantedBookCommand} [deleteWantedBookCommand]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    apiUsersMeWantedBooksBookIdDelete: async (
      bookId: number,
      deleteWantedBookCommand?: DeleteWantedBookCommand,
      options: RawAxiosRequestConfig = {},
    ): Promise<RequestArgs> => {
      // verify required parameter 'bookId' is not null or undefined
      assertParamExists('apiUsersMeWantedBooksBookIdDelete', 'bookId', bookId);
      const localVarPath = `/api/Users/me/wantedBooks/{bookId}`.replace(
        `{${'bookId'}}`,
        encodeURIComponent(String(bookId)),
      );
      // use dummy base URL string because the URL constructor only accepts absolute URLs.
      const localVarUrlObj = new URL(localVarPath, DUMMY_BASE_URL);
      let baseOptions;
      if (configuration) {
        baseOptions = configuration.baseOptions;
      }

      const localVarRequestOptions = {
        method: 'DELETE',
        ...baseOptions,
        ...options,
      };
      const localVarHeaderParameter = {} as any;
      const localVarQueryParameter = {} as any;

      // authentication Bearer required
      // http bearer authentication required
      await setBearerAuthToObject(localVarHeaderParameter, configuration);

      localVarHeaderParameter['Content-Type'] = 'application/json';

      setSearchParams(localVarUrlObj, localVarQueryParameter);
      let headersFromBaseOptions =
        baseOptions && baseOptions.headers ? baseOptions.headers : {};
      localVarRequestOptions.headers = {
        ...localVarHeaderParameter,
        ...headersFromBaseOptions,
        ...options.headers,
      };
      localVarRequestOptions.data = serializeDataIfNeeded(
        deleteWantedBookCommand,
        localVarRequestOptions,
        configuration,
      );

      return {
        url: toPathString(localVarUrlObj),
        options: localVarRequestOptions,
      };
    },
    /**
     *
     * @param {AddWantedBookCommand} [addWantedBookCommand]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    apiUsersMeWantedBooksPost: async (
      addWantedBookCommand?: AddWantedBookCommand,
      options: RawAxiosRequestConfig = {},
    ): Promise<RequestArgs> => {
      const localVarPath = `/api/Users/me/wantedBooks`;
      // use dummy base URL string because the URL constructor only accepts absolute URLs.
      const localVarUrlObj = new URL(localVarPath, DUMMY_BASE_URL);
      let baseOptions;
      if (configuration) {
        baseOptions = configuration.baseOptions;
      }

      const localVarRequestOptions = {
        method: 'POST',
        ...baseOptions,
        ...options,
      };
      const localVarHeaderParameter = {} as any;
      const localVarQueryParameter = {} as any;

      // authentication Bearer required
      // http bearer authentication required
      await setBearerAuthToObject(localVarHeaderParameter, configuration);

      localVarHeaderParameter['Content-Type'] = 'application/json';

      setSearchParams(localVarUrlObj, localVarQueryParameter);
      let headersFromBaseOptions =
        baseOptions && baseOptions.headers ? baseOptions.headers : {};
      localVarRequestOptions.headers = {
        ...localVarHeaderParameter,
        ...headersFromBaseOptions,
        ...options.headers,
      };
      localVarRequestOptions.data = serializeDataIfNeeded(
        addWantedBookCommand,
        localVarRequestOptions,
        configuration,
      );

      return {
        url: toPathString(localVarUrlObj),
        options: localVarRequestOptions,
      };
    },
    /**
     *
     * @param {GetPagedUsersQuery} [getPagedUsersQuery]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    apiUsersPagedPost: async (
      getPagedUsersQuery?: GetPagedUsersQuery,
      options: RawAxiosRequestConfig = {},
    ): Promise<RequestArgs> => {
      const localVarPath = `/api/Users/paged`;
      // use dummy base URL string because the URL constructor only accepts absolute URLs.
      const localVarUrlObj = new URL(localVarPath, DUMMY_BASE_URL);
      let baseOptions;
      if (configuration) {
        baseOptions = configuration.baseOptions;
      }

      const localVarRequestOptions = {
        method: 'POST',
        ...baseOptions,
        ...options,
      };
      const localVarHeaderParameter = {} as any;
      const localVarQueryParameter = {} as any;

      // authentication Bearer required
      // http bearer authentication required
      await setBearerAuthToObject(localVarHeaderParameter, configuration);

      localVarHeaderParameter['Content-Type'] = 'application/json';

      setSearchParams(localVarUrlObj, localVarQueryParameter);
      let headersFromBaseOptions =
        baseOptions && baseOptions.headers ? baseOptions.headers : {};
      localVarRequestOptions.headers = {
        ...localVarHeaderParameter,
        ...headersFromBaseOptions,
        ...options.headers,
      };
      localVarRequestOptions.data = serializeDataIfNeeded(
        getPagedUsersQuery,
        localVarRequestOptions,
        configuration,
      );

      return {
        url: toPathString(localVarUrlObj),
        options: localVarRequestOptions,
      };
    },
  };
};

/**
 * UsersApi - functional programming interface
 * @export
 */
export const UsersApiFp = function (configuration?: Configuration) {
  const localVarAxiosParamCreator = UsersApiAxiosParamCreator(configuration);
  return {
    /**
     *
     * @param {number} id
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    async apiUsersIdDelete(
      id: number,
      options?: RawAxiosRequestConfig,
    ): Promise<
      (axios?: AxiosInstance, basePath?: string) => AxiosPromise<void>
    > {
      const localVarAxiosArgs =
        await localVarAxiosParamCreator.apiUsersIdDelete(id, options);
      const localVarOperationServerIndex = configuration?.serverIndex ?? 0;
      const localVarOperationServerBasePath =
        operationServerMap['UsersApi.apiUsersIdDelete']?.[
          localVarOperationServerIndex
        ]?.url;
      return (axios, basePath) =>
        createRequestFunction(
          localVarAxiosArgs,
          globalAxios,
          BASE_PATH,
          configuration,
        )(axios, localVarOperationServerBasePath || basePath);
    },
    /**
     *
     * @param {number} id
     * @param {boolean} [excludeUnapprovedBooks]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    async apiUsersIdGet(
      id: number,
      excludeUnapprovedBooks?: boolean,
      options?: RawAxiosRequestConfig,
    ): Promise<
      (axios?: AxiosInstance, basePath?: string) => AxiosPromise<UserDto>
    > {
      const localVarAxiosArgs = await localVarAxiosParamCreator.apiUsersIdGet(
        id,
        excludeUnapprovedBooks,
        options,
      );
      const localVarOperationServerIndex = configuration?.serverIndex ?? 0;
      const localVarOperationServerBasePath =
        operationServerMap['UsersApi.apiUsersIdGet']?.[
          localVarOperationServerIndex
        ]?.url;
      return (axios, basePath) =>
        createRequestFunction(
          localVarAxiosArgs,
          globalAxios,
          BASE_PATH,
          configuration,
        )(axios, localVarOperationServerBasePath || basePath);
    },
    /**
     *
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    async apiUsersMeChatsGet(
      options?: RawAxiosRequestConfig,
    ): Promise<
      (
        axios?: AxiosInstance,
        basePath?: string,
      ) => AxiosPromise<Array<MessagingUserDto>>
    > {
      const localVarAxiosArgs =
        await localVarAxiosParamCreator.apiUsersMeChatsGet(options);
      const localVarOperationServerIndex = configuration?.serverIndex ?? 0;
      const localVarOperationServerBasePath =
        operationServerMap['UsersApi.apiUsersMeChatsGet']?.[
          localVarOperationServerIndex
        ]?.url;
      return (axios, basePath) =>
        createRequestFunction(
          localVarAxiosArgs,
          globalAxios,
          BASE_PATH,
          configuration,
        )(axios, localVarOperationServerBasePath || basePath);
    },
    /**
     *
     * @param {number} bookId
     * @param {DeleteOwnedBookCommand} [deleteOwnedBookCommand]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    async apiUsersMeOwnedBooksBookIdDelete(
      bookId: number,
      deleteOwnedBookCommand?: DeleteOwnedBookCommand,
      options?: RawAxiosRequestConfig,
    ): Promise<
      (axios?: AxiosInstance, basePath?: string) => AxiosPromise<void>
    > {
      const localVarAxiosArgs =
        await localVarAxiosParamCreator.apiUsersMeOwnedBooksBookIdDelete(
          bookId,
          deleteOwnedBookCommand,
          options,
        );
      const localVarOperationServerIndex = configuration?.serverIndex ?? 0;
      const localVarOperationServerBasePath =
        operationServerMap['UsersApi.apiUsersMeOwnedBooksBookIdDelete']?.[
          localVarOperationServerIndex
        ]?.url;
      return (axios, basePath) =>
        createRequestFunction(
          localVarAxiosArgs,
          globalAxios,
          BASE_PATH,
          configuration,
        )(axios, localVarOperationServerBasePath || basePath);
    },
    /**
     *
     * @param {AddOwnedBookCommand} [addOwnedBookCommand]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    async apiUsersMeOwnedBooksPost(
      addOwnedBookCommand?: AddOwnedBookCommand,
      options?: RawAxiosRequestConfig,
    ): Promise<
      (axios?: AxiosInstance, basePath?: string) => AxiosPromise<void>
    > {
      const localVarAxiosArgs =
        await localVarAxiosParamCreator.apiUsersMeOwnedBooksPost(
          addOwnedBookCommand,
          options,
        );
      const localVarOperationServerIndex = configuration?.serverIndex ?? 0;
      const localVarOperationServerBasePath =
        operationServerMap['UsersApi.apiUsersMeOwnedBooksPost']?.[
          localVarOperationServerIndex
        ]?.url;
      return (axios, basePath) =>
        createRequestFunction(
          localVarAxiosArgs,
          globalAxios,
          BASE_PATH,
          configuration,
        )(axios, localVarOperationServerBasePath || basePath);
    },
    /**
     *
     * @param {UpdateUserCommand} [updateUserCommand]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    async apiUsersMePut(
      updateUserCommand?: UpdateUserCommand,
      options?: RawAxiosRequestConfig,
    ): Promise<
      (axios?: AxiosInstance, basePath?: string) => AxiosPromise<void>
    > {
      const localVarAxiosArgs = await localVarAxiosParamCreator.apiUsersMePut(
        updateUserCommand,
        options,
      );
      const localVarOperationServerIndex = configuration?.serverIndex ?? 0;
      const localVarOperationServerBasePath =
        operationServerMap['UsersApi.apiUsersMePut']?.[
          localVarOperationServerIndex
        ]?.url;
      return (axios, basePath) =>
        createRequestFunction(
          localVarAxiosArgs,
          globalAxios,
          BASE_PATH,
          configuration,
        )(axios, localVarOperationServerBasePath || basePath);
    },
    /**
     *
     * @param {number} bookId
     * @param {DeleteWantedBookCommand} [deleteWantedBookCommand]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    async apiUsersMeWantedBooksBookIdDelete(
      bookId: number,
      deleteWantedBookCommand?: DeleteWantedBookCommand,
      options?: RawAxiosRequestConfig,
    ): Promise<
      (axios?: AxiosInstance, basePath?: string) => AxiosPromise<void>
    > {
      const localVarAxiosArgs =
        await localVarAxiosParamCreator.apiUsersMeWantedBooksBookIdDelete(
          bookId,
          deleteWantedBookCommand,
          options,
        );
      const localVarOperationServerIndex = configuration?.serverIndex ?? 0;
      const localVarOperationServerBasePath =
        operationServerMap['UsersApi.apiUsersMeWantedBooksBookIdDelete']?.[
          localVarOperationServerIndex
        ]?.url;
      return (axios, basePath) =>
        createRequestFunction(
          localVarAxiosArgs,
          globalAxios,
          BASE_PATH,
          configuration,
        )(axios, localVarOperationServerBasePath || basePath);
    },
    /**
     *
     * @param {AddWantedBookCommand} [addWantedBookCommand]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    async apiUsersMeWantedBooksPost(
      addWantedBookCommand?: AddWantedBookCommand,
      options?: RawAxiosRequestConfig,
    ): Promise<
      (axios?: AxiosInstance, basePath?: string) => AxiosPromise<void>
    > {
      const localVarAxiosArgs =
        await localVarAxiosParamCreator.apiUsersMeWantedBooksPost(
          addWantedBookCommand,
          options,
        );
      const localVarOperationServerIndex = configuration?.serverIndex ?? 0;
      const localVarOperationServerBasePath =
        operationServerMap['UsersApi.apiUsersMeWantedBooksPost']?.[
          localVarOperationServerIndex
        ]?.url;
      return (axios, basePath) =>
        createRequestFunction(
          localVarAxiosArgs,
          globalAxios,
          BASE_PATH,
          configuration,
        )(axios, localVarOperationServerBasePath || basePath);
    },
    /**
     *
     * @param {GetPagedUsersQuery} [getPagedUsersQuery]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    async apiUsersPagedPost(
      getPagedUsersQuery?: GetPagedUsersQuery,
      options?: RawAxiosRequestConfig,
    ): Promise<
      (
        axios?: AxiosInstance,
        basePath?: string,
      ) => AxiosPromise<ListedUserDtoPaginatedResult>
    > {
      const localVarAxiosArgs =
        await localVarAxiosParamCreator.apiUsersPagedPost(
          getPagedUsersQuery,
          options,
        );
      const localVarOperationServerIndex = configuration?.serverIndex ?? 0;
      const localVarOperationServerBasePath =
        operationServerMap['UsersApi.apiUsersPagedPost']?.[
          localVarOperationServerIndex
        ]?.url;
      return (axios, basePath) =>
        createRequestFunction(
          localVarAxiosArgs,
          globalAxios,
          BASE_PATH,
          configuration,
        )(axios, localVarOperationServerBasePath || basePath);
    },
  };
};

/**
 * UsersApi - factory interface
 * @export
 */
export const UsersApiFactory = function (
  configuration?: Configuration,
  basePath?: string,
  axios?: AxiosInstance,
) {
  const localVarFp = UsersApiFp(configuration);
  return {
    /**
     *
     * @param {number} id
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    apiUsersIdDelete(
      id: number,
      options?: RawAxiosRequestConfig,
    ): AxiosPromise<void> {
      return localVarFp
        .apiUsersIdDelete(id, options)
        .then((request) => request(axios, basePath));
    },
    /**
     *
     * @param {number} id
     * @param {boolean} [excludeUnapprovedBooks]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    apiUsersIdGet(
      id: number,
      excludeUnapprovedBooks?: boolean,
      options?: RawAxiosRequestConfig,
    ): AxiosPromise<UserDto> {
      return localVarFp
        .apiUsersIdGet(id, excludeUnapprovedBooks, options)
        .then((request) => request(axios, basePath));
    },
    /**
     *
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    apiUsersMeChatsGet(
      options?: RawAxiosRequestConfig,
    ): AxiosPromise<Array<MessagingUserDto>> {
      return localVarFp
        .apiUsersMeChatsGet(options)
        .then((request) => request(axios, basePath));
    },
    /**
     *
     * @param {number} bookId
     * @param {DeleteOwnedBookCommand} [deleteOwnedBookCommand]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    apiUsersMeOwnedBooksBookIdDelete(
      bookId: number,
      deleteOwnedBookCommand?: DeleteOwnedBookCommand,
      options?: RawAxiosRequestConfig,
    ): AxiosPromise<void> {
      return localVarFp
        .apiUsersMeOwnedBooksBookIdDelete(
          bookId,
          deleteOwnedBookCommand,
          options,
        )
        .then((request) => request(axios, basePath));
    },
    /**
     *
     * @param {AddOwnedBookCommand} [addOwnedBookCommand]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    apiUsersMeOwnedBooksPost(
      addOwnedBookCommand?: AddOwnedBookCommand,
      options?: RawAxiosRequestConfig,
    ): AxiosPromise<void> {
      return localVarFp
        .apiUsersMeOwnedBooksPost(addOwnedBookCommand, options)
        .then((request) => request(axios, basePath));
    },
    /**
     *
     * @param {UpdateUserCommand} [updateUserCommand]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    apiUsersMePut(
      updateUserCommand?: UpdateUserCommand,
      options?: RawAxiosRequestConfig,
    ): AxiosPromise<void> {
      return localVarFp
        .apiUsersMePut(updateUserCommand, options)
        .then((request) => request(axios, basePath));
    },
    /**
     *
     * @param {number} bookId
     * @param {DeleteWantedBookCommand} [deleteWantedBookCommand]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    apiUsersMeWantedBooksBookIdDelete(
      bookId: number,
      deleteWantedBookCommand?: DeleteWantedBookCommand,
      options?: RawAxiosRequestConfig,
    ): AxiosPromise<void> {
      return localVarFp
        .apiUsersMeWantedBooksBookIdDelete(
          bookId,
          deleteWantedBookCommand,
          options,
        )
        .then((request) => request(axios, basePath));
    },
    /**
     *
     * @param {AddWantedBookCommand} [addWantedBookCommand]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    apiUsersMeWantedBooksPost(
      addWantedBookCommand?: AddWantedBookCommand,
      options?: RawAxiosRequestConfig,
    ): AxiosPromise<void> {
      return localVarFp
        .apiUsersMeWantedBooksPost(addWantedBookCommand, options)
        .then((request) => request(axios, basePath));
    },
    /**
     *
     * @param {GetPagedUsersQuery} [getPagedUsersQuery]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    apiUsersPagedPost(
      getPagedUsersQuery?: GetPagedUsersQuery,
      options?: RawAxiosRequestConfig,
    ): AxiosPromise<ListedUserDtoPaginatedResult> {
      return localVarFp
        .apiUsersPagedPost(getPagedUsersQuery, options)
        .then((request) => request(axios, basePath));
    },
  };
};

/**
 * UsersApi - object-oriented interface
 * @export
 * @class UsersApi
 * @extends {BaseAPI}
 */
export class UsersApi extends BaseAPI {
  /**
   *
   * @param {number} id
   * @param {*} [options] Override http request option.
   * @throws {RequiredError}
   * @memberof UsersApi
   */
  public apiUsersIdDelete(id: number, options?: RawAxiosRequestConfig) {
    return UsersApiFp(this.configuration)
      .apiUsersIdDelete(id, options)
      .then((request) => request(this.axios, this.basePath));
  }

  /**
   *
   * @param {number} id
   * @param {boolean} [excludeUnapprovedBooks]
   * @param {*} [options] Override http request option.
   * @throws {RequiredError}
   * @memberof UsersApi
   */
  public apiUsersIdGet(
    id: number,
    excludeUnapprovedBooks?: boolean,
    options?: RawAxiosRequestConfig,
  ) {
    return UsersApiFp(this.configuration)
      .apiUsersIdGet(id, excludeUnapprovedBooks, options)
      .then((request) => request(this.axios, this.basePath));
  }

  /**
   *
   * @param {*} [options] Override http request option.
   * @throws {RequiredError}
   * @memberof UsersApi
   */
  public apiUsersMeChatsGet(options?: RawAxiosRequestConfig) {
    return UsersApiFp(this.configuration)
      .apiUsersMeChatsGet(options)
      .then((request) => request(this.axios, this.basePath));
  }

  /**
   *
   * @param {number} bookId
   * @param {DeleteOwnedBookCommand} [deleteOwnedBookCommand]
   * @param {*} [options] Override http request option.
   * @throws {RequiredError}
   * @memberof UsersApi
   */
  public apiUsersMeOwnedBooksBookIdDelete(
    bookId: number,
    deleteOwnedBookCommand?: DeleteOwnedBookCommand,
    options?: RawAxiosRequestConfig,
  ) {
    return UsersApiFp(this.configuration)
      .apiUsersMeOwnedBooksBookIdDelete(bookId, deleteOwnedBookCommand, options)
      .then((request) => request(this.axios, this.basePath));
  }

  /**
   *
   * @param {AddOwnedBookCommand} [addOwnedBookCommand]
   * @param {*} [options] Override http request option.
   * @throws {RequiredError}
   * @memberof UsersApi
   */
  public apiUsersMeOwnedBooksPost(
    addOwnedBookCommand?: AddOwnedBookCommand,
    options?: RawAxiosRequestConfig,
  ) {
    return UsersApiFp(this.configuration)
      .apiUsersMeOwnedBooksPost(addOwnedBookCommand, options)
      .then((request) => request(this.axios, this.basePath));
  }

  /**
   *
   * @param {UpdateUserCommand} [updateUserCommand]
   * @param {*} [options] Override http request option.
   * @throws {RequiredError}
   * @memberof UsersApi
   */
  public apiUsersMePut(
    updateUserCommand?: UpdateUserCommand,
    options?: RawAxiosRequestConfig,
  ) {
    return UsersApiFp(this.configuration)
      .apiUsersMePut(updateUserCommand, options)
      .then((request) => request(this.axios, this.basePath));
  }

  /**
   *
   * @param {number} bookId
   * @param {DeleteWantedBookCommand} [deleteWantedBookCommand]
   * @param {*} [options] Override http request option.
   * @throws {RequiredError}
   * @memberof UsersApi
   */
  public apiUsersMeWantedBooksBookIdDelete(
    bookId: number,
    deleteWantedBookCommand?: DeleteWantedBookCommand,
    options?: RawAxiosRequestConfig,
  ) {
    return UsersApiFp(this.configuration)
      .apiUsersMeWantedBooksBookIdDelete(
        bookId,
        deleteWantedBookCommand,
        options,
      )
      .then((request) => request(this.axios, this.basePath));
  }

  /**
   *
   * @param {AddWantedBookCommand} [addWantedBookCommand]
   * @param {*} [options] Override http request option.
   * @throws {RequiredError}
   * @memberof UsersApi
   */
  public apiUsersMeWantedBooksPost(
    addWantedBookCommand?: AddWantedBookCommand,
    options?: RawAxiosRequestConfig,
  ) {
    return UsersApiFp(this.configuration)
      .apiUsersMeWantedBooksPost(addWantedBookCommand, options)
      .then((request) => request(this.axios, this.basePath));
  }

  /**
   *
   * @param {GetPagedUsersQuery} [getPagedUsersQuery]
   * @param {*} [options] Override http request option.
   * @throws {RequiredError}
   * @memberof UsersApi
   */
  public apiUsersPagedPost(
    getPagedUsersQuery?: GetPagedUsersQuery,
    options?: RawAxiosRequestConfig,
  ) {
    return UsersApiFp(this.configuration)
      .apiUsersPagedPost(getPagedUsersQuery, options)
      .then((request) => request(this.axios, this.basePath));
  }
}
