import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../../../core/services/auth.service';
import { Router, RouterLink } from '@angular/router';
import { Login } from '../../../../shared/models/login';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-login.component',
  imports: [ReactiveFormsModule, CommonModule, RouterLink, MatIconModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup = new FormGroup({});
  errorMessage: string = '';
  hidePassword: boolean = true;

  constructor(
    private authService: AuthService,
    private formBuilder: FormBuilder,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  onSubmit(): void {
    if (this.loginForm.invalid) return;

    let login: Login = this.loginForm.value;

    this.authService.login(login).subscribe({
      next: () => {
        this.router.navigate(['/movies/trending']);
      },
      error: () => {
        this.errorMessage = 'Invalid login credentials.';
      },
    });
  }

  togglePassword(): void {
    this.hidePassword = !this.hidePassword;
  }
}
