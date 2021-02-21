import { Filter } from "./filter";

export class GridData<T> {
    data: T[];
    filters: Filter[];
    sortBy: string;
    sortType: sortType;
    pageSize: number;
    pageNumber: number;
    count: number;
}

export enum sortType {
    Asc,
    Desc,
}
