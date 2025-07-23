import { MessagesApi, type GetPagedMessagesQuery } from '../generated';
import { requestConfig } from './common';

const messagesApi = new MessagesApi(requestConfig);

export const fetchMessagesPaginatedResult = async (
  query: GetPagedMessagesQuery,
) => {
  const response = await messagesApi.apiMessagesPagedPost(query);
  return response.data;
};
