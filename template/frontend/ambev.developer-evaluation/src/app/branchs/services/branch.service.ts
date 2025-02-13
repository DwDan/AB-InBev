import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { Branch } from '../models/branch.model';
import { ApiService } from '../../core/services/api.service';
import { PaginatedResponse } from '../../core/shared/models/paginated-response.model';

@Injectable({
  providedIn: 'root',
})
export class BranchService {

  constructor(private apiService: ApiService) {}

  getBranches(): Observable<Branch[]> {
    return this.apiService.get<PaginatedResponse<Branch>>('branches').pipe(
      map(response => response?.data || []) 
    );
  }

  addBranch(branch: Branch): Observable<Branch> {
    return this.apiService.post<Branch>('branches', branch);
  }

  updateBranch(updatedBranch: Branch): Observable<Branch> {
    return this.apiService.put<Branch>('branches', updatedBranch.id, updatedBranch);
  }

  deleteBranch(id: number): Observable<boolean> {
    return this.apiService.delete('branches', id);
  }

  getBranchById(id: number): Observable<Branch | undefined> {
    return this.apiService.getById<Branch>('branches', id).pipe(
      map(response => response || []) 
    );
  }
}
