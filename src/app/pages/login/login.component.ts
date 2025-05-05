import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  usuario: string = '';
  contrasenia: string = '';
  error = '';

  constructor(private http: HttpClient, private router: Router) {}

  ingresar() {
    this.http.post<any>('https://localhost:5242/api/auth/login', {
      usuario: this.usuario,
      contrasenia: this.contrasenia
    }).subscribe({
      next: (res) => this.router.navigate(['/alumno']),
      error: (err) => this.error = "Usuario o contraseña inválidos"
    });
  }
}