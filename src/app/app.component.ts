import { Component, AfterViewInit } from '@angular/core';
import { AlumnoService, Alumno } from './pages/alumno/alumno.services';
import { Routes, Router, RouterOutlet,RouterLink, RouterLinkActive  } from '@angular/router';
import { MateriaService, Materia } from './pages/materias/materia.service'; 
import { CommonModule } from '@angular/common';

declare var $:any; 

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [  CommonModule, RouterOutlet, RouterLink, RouterLinkActive],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})


export class AppComponent {

  constructor(public router: Router) {}

  isHome(): boolean {
    return this.router.url === '/';
  }
}

