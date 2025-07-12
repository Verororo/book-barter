import { requestConfig } from "./common";
import { PublishersApi, type CreatePublisherCommand, type PublisherDto } from "../generated";

const publishersApi = new PublishersApi(requestConfig)

export const createPublisherCommand = async (command: CreatePublisherCommand): Promise<number | undefined> => {
  try {
    const response = await publishersApi.apiPublishersPost(command)
    return response.data
  }
  catch (error) {
    console.error(error)
    return undefined
  }
}

export const fetchPagedPublishers = async (query: string): Promise<PublisherDto[]> => {
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