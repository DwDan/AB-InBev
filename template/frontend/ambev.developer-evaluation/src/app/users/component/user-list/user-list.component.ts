import { Component, inject, OnInit } from '@angular/core';
import { User } from '../../models/user.model';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MaterialModule } from '../../../core/shared/modules/material.module';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [CommonModule, MaterialModule, MatIconModule],
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
    'actions',
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

  editUser(id: number) {
    this.router.navigate(['users/edit', id]);
  }

  deleteUser(id: number) {
    if (confirm('Deseja realmente excluir este produto?')) {
      this.service.deleteUser(id).subscribe(() => {
        this.loadUsers();
      });
    }
  }

  goToHome() {
    this.router.navigate(['/home']);
  }
}
