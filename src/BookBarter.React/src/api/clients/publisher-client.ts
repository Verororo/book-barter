import { requestConfig } from "./common";
import { PublishersApi, type CreatePublisherCommand, type GetPagedPublishersForModerationQuery, type PublisherDto, type PublisherForModerationDtoPaginatedResult, type UpdatePublisherCommand } from "../generated";

const publishersApi = new PublishersApi(requestConfig)

export const createPublisherCommand = async (
  command: CreatePublisherCommand
): Promise<number | undefined> => {
  try {
    const response = await publishersApi.apiPublishersPost(command)
    return response.data
  }
  catch (error) {
    console.error(error)
    return undefined
  }
}

export const approvePublisher = async (
  id: number
) => {
  await publishersApi.apiPublishersIdApprovePut(id)
}

export const updatePublisher = async (
  query: UpdatePublisherCommand
) => {
  await publishersApi.apiPublishersIdPut(query.id!, query)
}

export const deletePublisher = async (
  id: number
) => {
  await publishersApi.apiPublishersIdDelete(id)
}

export const fetchPublishersForModeration = async (
  query: GetPagedPublishersForModerationQuery
): Promise<PublisherForModerationDtoPaginatedResult> => {
  const response = await publishersApi.apiPublishersPagedModeratedPost(query);

  return response.data;
}


export const fetchPagedPublishers = async (
  query: string
): Promise<PublisherDto[]> => {
  try {
      const response = await publishersApi.apiPublishersPagedPost({
      pageSize: 10,
      pageNumber: 1,
      orderByProperty: "name",
      orderDirection: "asc",
      query: query,
    })

    return response.data.items ?? [];

  } catch (error) {
    console.error(error);
    return [];
  }
}