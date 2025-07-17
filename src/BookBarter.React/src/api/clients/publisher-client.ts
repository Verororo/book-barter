import { requestConfig } from "./common";
import { PublishersApi, type CreatePublisherCommand, type GetPagedPublishersForModerationQuery, type PublisherDto, type PublisherForModerationDtoPaginatedResult, type UpdatePublisherCommand } from "../generated";

const publishersApi = new PublishersApi(requestConfig)

export const createPublisherCommand = async (
  command: CreatePublisherCommand
): Promise<number | undefined> => {
  try {
    const response = await publishersApi.apiPublishersPost(command)
    return response.data
  } catch (error) {
    console.log(error)
    throw error
  }
}

export const approvePublisher = async (
  id: number
) => {
  try {
    await publishersApi.apiPublishersIdApprovePut(id)
  } catch (error) {
    console.log(error)
    throw error
  }
}

export const updatePublisher = async (
  query: UpdatePublisherCommand
) => {
  try {
    await publishersApi.apiPublishersIdPut(query.id!, query)
  } catch (error) {
    console.log(error)
    throw error
  }
}

export const deletePublisher = async (
  id: number
) => {
  try {
    await publishersApi.apiPublishersIdDelete(id)
  } catch (error) {
    console.log(error)
    throw error
  }
}

export const fetchPublishersForModeration = async (
  query: GetPagedPublishersForModerationQuery
): Promise<PublisherForModerationDtoPaginatedResult> => {
  try {
    const response = await publishersApi.apiPublishersPagedModeratedPost(query);
    return response.data;
  } catch (error) {
    console.log(error)
    throw error
  }
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
    throw error;
  }
}