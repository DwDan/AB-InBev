import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterOutlet } from '@angular/router';
import { ConfirmationDialogComponent } from '../../../core/shared/components/confirmation-dialog/confirmation-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MaterialModule } from '../../../core/shared/modules/material.module';
import { Branch } from '../../models/branch.model';
import { BranchService } from '../../services/branch.service';

@Component({
  selector: 'app-branch-list',
  standalone: true,
  imports: [CommonModule, RouterOutlet, MaterialModule, MatIconModule],
  templateUrl: './branch-list.component.html',
  styleUrl: './branch-list.component.scss',
})
export class BranchListComponent implements OnInit {
  branches: Branch[] = [];

  displayedColumns: string[] = [
    'id',
    'name',
    'actions',
  ];

  private dialog = inject(MatDialog);
  private service = inject(BranchService);
  private router = inject(Router);

  ngOnInit() {
    this.loadBranches();
  }

  loadBranches() {
    this.service.getBranches().subscribe((data) => {
      this.branches = data;
    });
  }

  addBranch() {
    this.router.navigate(['branches/add']);
  }

  deleteBranch(id: number) {
    this.service.deleteBranch(id).subscribe(() => {
      this.loadBranches(); 
    });
  }

  editBranch(id: number) {
    this.router.navigate(['branches/edit', id]);
  }

  openDeleteDialog(id: number) {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      data: { message: 'Deseja realmente excluir este filial?' },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.deleteBranch(id);
      }
    });
  }

  goToHome() {
    this.router.navigate(['/home']);
  }
}
