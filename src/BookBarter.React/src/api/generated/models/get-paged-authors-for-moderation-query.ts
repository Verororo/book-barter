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

/**
 *
 * @export
 * @interface GetPagedAuthorsForModerationQuery
 */
export interface GetPagedAuthorsForModerationQuery {
  /**
   *
   * @type {number}
   * @memberof GetPagedAuthorsForModerationQuery
   */
  pageSize?: number;
  /**
   *
   * @type {number}
   * @memberof GetPagedAuthorsForModerationQuery
   */
  pageNumber?: number;
  /**
   *
   * @type {string}
   * @memberof GetPagedAuthorsForModerationQuery
   */
  orderByProperty?: string | null;
  /**
   *
   * @type {string}
   * @memberof GetPagedAuthorsForModerationQuery
   */
  orderDirection?: string | null;
  /**
   *
   * @type {boolean}
   * @memberof GetPagedAuthorsForModerationQuery
   */
  approved?: boolean;
  /**
   *
   * @type {string}
   * @memberof GetPagedAuthorsForModerationQuery
   */
  query?: string | null;
}
