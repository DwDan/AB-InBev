import { Component, inject, OnInit } from '@angular/core';
import { User } from '../../models/user.model';
import { CommonModule } from '@angular/common';
import { Router, RouterOutlet } from '@angular/router';
import { ConfirmationDialogComponent } from '../../../core/shared/components/confirmation-dialog/confirmation-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MaterialModule } from '../../../core/shared/modules/material.module';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [CommonModule, RouterOutlet, MaterialModule, MatIconModule],
  templateUrl: './user-list.component.html',
  styleUrl: './user-list.component.scss',
})
export class UserListComponent implements OnInit {
  users: User[] = [];

  displayedColumns: string[] = [
    'id',
    'email',
    'firstname',
    'lastname',
    'actions'
  ];

  private dialog = inject(MatDialog);
  private service = inject(UserService);
  private router = inject(Router);

  ngOnInit() {
    this.loadUsers();
  }

  loadUsers() {
    this.service.getUsers().subscribe((data) => {
      this.users = data;
    });
  }

  addUser() {
    this.router.navigate(['users/add']);
  }

  deleteUser(id: number) {
    this.service.deleteUser(id).subscribe(() => {
      this.loadUsers(); 
    });
  }

  editUser(id: number) {
    this.router.navigate(['users/edit', id]);
  }

  openDeleteDialog(id: number) {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      data: { message: 'Deseja realmente excluir este produto?' },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.deleteUser(id);
      }
    });
  }
}
