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

// May contain unused imports in some cases
// @ts-ignore
import type { BookForModerationDto } from './book-for-moderation-dto';

/**
 *
 * @export
 * @interface BookForModerationDtoPaginatedResult
 */
export interface BookForModerationDtoPaginatedResult {
  /**
   *
   * @type {number}
   * @memberof BookForModerationDtoPaginatedResult
   */
  pageNumber?: number;
  /**
   *
   * @type {number}
   * @memberof BookForModerationDtoPaginatedResult
   */
  pageSize?: number;
  /**
   *
   * @type {number}
   * @memberof BookForModerationDtoPaginatedResult
   */
  total?: number;
  /**
   *
   * @type {Array<BookForModerationDto>}
   * @memberof BookForModerationDtoPaginatedResult
   */
  items?: Array<BookForModerationDto> | null;
}
