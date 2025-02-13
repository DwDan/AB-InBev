import { ComponentFixture, TestBed } from '@angular/core/testing';
import { BranchListComponent } from './branch-list.component';
import { BranchService } from '../../services/branch.service';
import { Router } from '@angular/router';
import { of } from 'rxjs';
import { MatIconModule } from '@angular/material/icon';
import { MaterialModule } from '../../../core/shared/modules/material.module';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('BranchListComponent', () => {
  let component: BranchListComponent;
  let fixture: ComponentFixture<BranchListComponent>;
  let branchServiceSpy: jasmine.SpyObj<BranchService>;
  let routerSpy: jasmine.SpyObj<Router>;

  beforeEach(async () => {
    branchServiceSpy = jasmine.createSpyObj('BranchService', [
      'getBranches',
      'deleteBranch',
    ]);

    branchServiceSpy.getBranches.and.returnValue(of([
      { id: 1, name: 'Branch A' },
      { id: 2, name: 'Branch B' },
    ]));

    routerSpy = jasmine.createSpyObj('Router', ['navigate']);

    await TestBed.configureTestingModule({
      imports: [
        BranchListComponent,
        CommonModule,
        MaterialModule,
        MatIconModule,
        BrowserAnimationsModule,
      ],
      providers: [
        { provide: BranchService, useValue: branchServiceSpy },
        { provide: Router, useValue: routerSpy },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(BranchListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  // it('should load branches on init', () => {
  //   const mockBranches = [
  //     { id: 1, name: 'Branch A' },
  //     { id: 2, name: 'Branch B' },
  //   ];
  //   branchServiceSpy.getBranches.and.returnValue(of(mockBranches));

  //   component.ngOnInit();

  //   expect(branchServiceSpy.getBranches).toHaveBeenCalled();
  //   expect(component.branches).toEqual(mockBranches);
  // });

  it('should navigate to add branch', () => {
    component.addBranch();
    expect(routerSpy.navigate).toHaveBeenCalledWith(['branches/add']);
  });

  it('should navigate to edit branch', () => {
    component.editBranch(1);
    expect(routerSpy.navigate).toHaveBeenCalledWith(['branches/edit', 1]);
  });

  it('should delete branch after confirmation', () => {
    spyOn(window, 'confirm').and.returnValue(true);
    branchServiceSpy.deleteBranch.and.returnValue(of(true));

    component.deleteBranch(1);

    expect(branchServiceSpy.deleteBranch).toHaveBeenCalledWith(1);
  });

  it('should not delete branch if user cancels', () => {
    spyOn(window, 'confirm').and.returnValue(false);
    component.deleteBranch(1);
    expect(branchServiceSpy.deleteBranch).not.toHaveBeenCalled();
  });

  it('should navigate to home', () => {
    component.goToHome();
    expect(routerSpy.navigate).toHaveBeenCalledWith(['/home']);
  });
});
