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
import type { AuthorDto } from './author-dto';

/**
 *
 * @export
 * @interface ListedBookDto
 */
export interface ListedBookDto {
  /**
   *
   * @type {number}
   * @memberof ListedBookDto
   */
  id?: number;
  /**
   *
   * @type {string}
   * @memberof ListedBookDto
   */
  title?: string | null;
  /**
   *
   * @type {string}
   * @memberof ListedBookDto
   */
  publicationDate?: string;
  /**
   *
   * @type {boolean}
   * @memberof ListedBookDto
   */
  approved?: boolean;
  /**
   *
   * @type {string}
   * @memberof ListedBookDto
   */
  genreName?: string | null;
  /**
   *
   * @type {string}
   * @memberof ListedBookDto
   */
  publisherName?: string | null;
  /**
   *
   * @type {Array<AuthorDto>}
   * @memberof ListedBookDto
   */
  authors?: Array<AuthorDto> | null;
}
