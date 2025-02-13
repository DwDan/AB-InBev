import { TestBed } from '@angular/core/testing';
import { BranchService } from './branch.service';
import { ApiService } from '../../core/services/api.service';
import { of } from 'rxjs';
import { Branch } from '../models/branch.model';

describe('BranchService', () => {
  let service: BranchService;
  let apiServiceSpy: jasmine.SpyObj<ApiService>;

  beforeEach(() => {
    apiServiceSpy = jasmine.createSpyObj('ApiService', [
      'get',
      'post',
      'put',
      'delete',
      'getById',
    ]);

    TestBed.configureTestingModule({
      providers: [
        BranchService,
        { provide: ApiService, useValue: apiServiceSpy },
      ],
    });

    service = TestBed.inject(BranchService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should fetch all branches', (done) => {
    const mockBranches: Branch[] = [
      { id: 1, name: 'Branch A' },
      { id: 2, name: 'Branch B' },
    ];

    apiServiceSpy.get.and.returnValue(of({ data: mockBranches }));

    service.getBranches().subscribe((branches) => {
      expect(branches.length).toBe(2);
      expect(branches).toEqual(mockBranches);
      done();
    });
  });

  it('should add a new branch', (done) => {
    const newBranch: Branch = { id: 3, name: 'Branch C' };

    apiServiceSpy.post.and.returnValue(of(newBranch));

    service.addBranch(newBranch).subscribe((branch) => {
      expect(branch).toEqual(newBranch);
      done();
    });
  });

  it('should update an existing branch', (done) => {
    const updatedBranch: Branch = { id: 1, name: 'Updated Branch' };

    apiServiceSpy.put.and.returnValue(of(updatedBranch));

    service.updateBranch(updatedBranch).subscribe((branch) => {
      expect(branch.name).toBe('Updated Branch');
      done();
    });
  });

  it('should delete a branch', (done) => {
    apiServiceSpy.delete.and.returnValue(of(true));

    service.deleteBranch(1).subscribe((result) => {
      expect(result).toBe(true);
      done();
    });
  });

  it('should get branch by ID', (done) => {
    const mockBranch: Branch = { id: 1, name: 'Branch A' };

    apiServiceSpy.getById.and.returnValue(of(mockBranch));

    service.getBranchById(1).subscribe((branch) => {
      expect(branch).toEqual(mockBranch);
      done();
    });
  });
});
