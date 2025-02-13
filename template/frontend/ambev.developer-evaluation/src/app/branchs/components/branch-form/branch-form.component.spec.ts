import { ComponentFixture, TestBed } from '@angular/core/testing';
import { BranchFormComponent } from './branch-form.component';
import { BranchService } from '../../services/branch.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { of } from 'rxjs';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { Branch } from '../../models/branch.model';

describe('BranchFormComponent', () => {
  let component: BranchFormComponent;
  let fixture: ComponentFixture<BranchFormComponent>;
  let branchServiceSpy: jasmine.SpyObj<BranchService>;
  let routerSpy: jasmine.SpyObj<Router>;
  let activatedRouteSpy: jasmine.SpyObj<ActivatedRoute>;

  beforeEach(async () => {
    branchServiceSpy = jasmine.createSpyObj('BranchService', [
      'getBranchById',
      'addBranch',
      'updateBranch',
    ]);

    routerSpy = jasmine.createSpyObj('Router', ['navigate']);
    activatedRouteSpy = {
      params: of({ id: 1 }),
    } as any;

    await TestBed.configureTestingModule({
      imports: [
        BranchFormComponent,
        ReactiveFormsModule,
        BrowserAnimationsModule,
      ],
      providers: [
        { provide: BranchService, useValue: branchServiceSpy },
        { provide: Router, useValue: routerSpy },
        { provide: ActivatedRoute, useValue: activatedRouteSpy },
        FormBuilder,
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(BranchFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize form with empty values', () => {
    expect(component.branchForm.value).toEqual({ name: '' });
  });

  it('should populate form when editing', () => {
    const mockBranch = { id: 1, name: 'Branch A' };
    branchServiceSpy.getBranchById.and.returnValue(of(mockBranch));

    component.ngOnInit();

    expect(branchServiceSpy.getBranchById).toHaveBeenCalledWith(1);
  });

  it('should call addBranch when creating a new branch', () => {
    const mockBranch = <Branch>{ name: 'New Branch' };
    branchServiceSpy.addBranch.and.returnValue(of(mockBranch));

    component.branchForm.setValue(mockBranch);
    component.isEditMode = false;
    component.onSubmit();

    expect(branchServiceSpy.addBranch).toHaveBeenCalledWith(mockBranch);
    expect(routerSpy.navigate).toHaveBeenCalledWith(['/branches']);
  });

  it('should call updateBranch when editing a branch', () => {
    const mockBranch = { id: 1, name: 'Updated Branch' };
    branchServiceSpy.updateBranch.and.returnValue(of(mockBranch));

    component.isEditMode = true;
    component.branch = { id: 1, name: 'Old Branch' };
    component.branchForm.setValue({ name: 'Updated Branch' });
    component.onSubmit();

    expect(branchServiceSpy.updateBranch).toHaveBeenCalledWith(mockBranch);
    expect(routerSpy.navigate).toHaveBeenCalledWith(['/branches']);
  });

  it('should navigate back when onCancel is called', () => {
    component.onCancel();
    expect(routerSpy.navigate).toHaveBeenCalledWith(['/branches']);
  });
});
