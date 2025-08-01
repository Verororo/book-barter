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
 * @interface GetPagedBooksQuery
 */
export interface GetPagedBooksQuery {
  /**
   *
   * @type {number}
   * @memberof GetPagedBooksQuery
   */
  pageSize?: number;
  /**
   *
   * @type {number}
   * @memberof GetPagedBooksQuery
   */
  pageNumber?: number;
  /**
   *
   * @type {string}
   * @memberof GetPagedBooksQuery
   */
  orderByProperty?: string | null;
  /**
   *
   * @type {string}
   * @memberof GetPagedBooksQuery
   */
  orderDirection?: string | null;
  /**
   *
   * @type {boolean}
   * @memberof GetPagedBooksQuery
   */
  skipLoggedInUserBooks?: boolean;
  /**
   *
   * @type {Array<number>}
   * @memberof GetPagedBooksQuery
   */
  idsToSkip?: Array<number> | null;
  /**
   *
   * @type {string}
   * @memberof GetPagedBooksQuery
   */
  title?: string | null;
  /**
   *
   * @type {number}
   * @memberof GetPagedBooksQuery
   */
  authorId?: number | null;
  /**
   *
   * @type {number}
   * @memberof GetPagedBooksQuery
   */
  genreId?: number | null;
  /**
   *
   * @type {number}
   * @memberof GetPagedBooksQuery
   */
  publisherId?: number | null;
}
