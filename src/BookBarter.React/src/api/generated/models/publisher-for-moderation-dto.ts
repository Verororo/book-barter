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
import type { BookDto } from './book-dto';

/**
 *
 * @export
 * @interface PublisherForModerationDto
 */
export interface PublisherForModerationDto {
  /**
   *
   * @type {number}
   * @memberof PublisherForModerationDto
   */
  id?: number;
  /**
   *
   * @type {string}
   * @memberof PublisherForModerationDto
   */
  name?: string | null;
  /**
   *
   * @type {boolean}
   * @memberof PublisherForModerationDto
   */
  approved?: boolean;
  /**
   *
   * @type {string}
   * @memberof PublisherForModerationDto
   */
  addedDate?: string;
  /**
   *
   * @type {Array<BookDto>}
   * @memberof PublisherForModerationDto
   */
  books?: Array<BookDto> | null;
}
