import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { MaterialModule } from '../../../core/shared/modules/material.module';
import { Branch } from '../../models/branch.model';
import { BranchService } from '../../services/branch.service';

@Component({
  selector: 'app-branch-list',
  standalone: true,
  imports: [CommonModule, MaterialModule, MatIconModule],
  templateUrl: './branch-list.component.html',
  styleUrl: './branch-list.component.scss',
})
export class BranchListComponent implements OnInit {
  branches: Branch[] = [];

  displayedColumns: string[] = ['id', 'name', 'actions'];

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

  editBranch(id: number) {
    this.router.navigate(['branches/edit', id]);
  }

  deleteBranch(id: number) {
    if (confirm('Deseja realmente excluir este filial?')) {
      this.service.deleteBranch(id).subscribe(() => {
        this.loadBranches();
      });
    }
  }

  goToHome() {
    this.router.navigate(['/home']);
  }
}
