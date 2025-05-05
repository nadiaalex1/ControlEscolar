import { Component, OnInit } from '@angular/core';
import { AlumnoService, Alumno } from './alumno.services';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-alumno',
  standalone: true,
  templateUrl: './alumno.component.html',
  imports: [CommonModule, FormsModule]
})
export class AlumnoComponent implements OnInit {
  alumnos: Alumno[] = [];
  busqueda: string = '';
  nuevo: Alumno = { 
    idAlumno: 0,
    nombre: '', 
    apellidoPaterno: '',
    apellidoMaterno: '',
    usuario: '',
    contrasenia: ''
};

  constructor(private alumnoService: AlumnoService) {}

  ngOnInit(): void {
    this.cargarAlumnos();
  }

  cargarAlumnos(): void {
    this.alumnoService.obtenerAlumnos().subscribe(data => {
      this.alumnos = data;
    });
  }

  guardar(): void {
    this.alumnoService.insertarAlumno(this.nuevo).subscribe(() => {
      this.cargarAlumnos();
      this.nuevo = { idAlumno: 0, nombre: '', apellidoPaterno: '', apellidoMaterno: '', usuario:'', contrasenia: '' };
    });
  }

  buscarNombre(): void {
    if (!this.busqueda.trim()) {
      this.cargarAlumnos(); 
      return;
    }
  
    this.alumnoService.buscarAlumNombre(this.busqueda).subscribe(data => {
      this.alumnos = data;
    }, error => {
      console.error("Error al buscar alumnos", error);
    });
  }

  eliminar(id: number): void {
    if (confirm('¿Estás seguro de eliminar este alumno?')) {
      this.alumnoService.eliminarAlumno(id).subscribe(() => {
        this.cargarAlumnos();
      });
    }
  }
}
