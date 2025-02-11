export class PaginatedResponse<T> {
    currentPage: number;
    data: T[];
    totalItems: number;
    totalPages: number;
  
    constructor(
      currentPage: number,
      data: T[],
      totalItems: number,
      totalPages: number
    ) {
      this.currentPage = currentPage;
      this.data = data;
      this.totalItems = totalItems;
      this.totalPages = totalPages;
    }
  }