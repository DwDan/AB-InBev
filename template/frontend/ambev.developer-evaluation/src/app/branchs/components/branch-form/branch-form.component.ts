import { Component, OnInit } from '@angular/core';
import {
  FormGroup,
  FormBuilder,
  Validators,
  ReactiveFormsModule,
  FormsModule,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../../../core/shared/modules/material.module';
import { Branch } from '../../models/branch.model';
import { BranchService } from '../../services/branch.service';

@Component({
  selector: 'app-branch-form',
  standalone: true,
  imports: [CommonModule, MaterialModule, FormsModule, ReactiveFormsModule],
  templateUrl: './branch-form.component.html',
  styleUrls: ['./branch-form.component.scss'],
})
export class BranchFormComponent implements OnInit {
  branchForm!: FormGroup;
  isEditMode = false;
  branchId!: number;
  branch?: Branch;

  constructor(
    private fb: FormBuilder,
    private branchService: BranchService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.branchForm = this.fb.group({
      name: ['', Validators.required],
    });

    this.route.params.subscribe((params) => {
      if (params['id']) {
        this.isEditMode = true;
        this.branchId = +params['id'];
        this.branchService.getBranchById(this.branchId).subscribe((branch) => {
          if (branch) {
            this.branch = branch;
            this.branchForm.patchValue({
              ...branch,
            });
          }
        });
      }
    });
  }

  onSubmit() {
    if (this.branchForm.valid) {
      const formValue = this.branchForm.value;

      if (this.isEditMode && this.branch) {
        this.branchService.updateBranch({
          id: this.branch.id,
          ...formValue,
        }).subscribe(() => {
          this.router.navigate(['/branches']);
        });
      } else {
        this.branchService.addBranch(formValue).subscribe(() => {
          this.router.navigate(['/branches']);
        });
      }
    }
  }

  onCancel() {
    this.router.navigate(['/branches']);
  }
}
