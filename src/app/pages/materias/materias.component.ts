import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MateriaService, Materia } from './materia.service';

@Component({
  selector: 'app-materia',
  standalone: true,
  templateUrl: './materias.component.html',
  imports: [CommonModule, FormsModule]
})
export class MateriasComponent implements OnInit {
  materias: any[] = [];
 nuevaMateria: Materia = {
    idMateria: 0,
    nombre: '',
    costo: 0
  };
  idAlumno: number = 0;
  materiasDisponibles: Materia[] = [];
  idMateriaSeleccionada: number = 0;
  constructor(private materiaService: MateriaService) {}

  ngOnInit(): void {
    this.cargarMaterias();
  }

  cargarMaterias() {
    fetch('https://localhost:5242/api/materias') 
      .then(res => res.json())
      .then(data => this.materias = data);
  }


  guardar(): void {
    this.materiaService.insertarMateria(this.nuevaMateria).subscribe(() => {
      this.cargarMaterias();
      this.nuevaMateria = { idMateria: 0, nombre: '', costo: 0 };
    });
  }

  nombreBuscar: string = '';
  materiaEncontrada: Materia | null = null;
  
  buscarPorNombre(): void {
    this.materiaService.buscarNombre(this.nombreBuscar).subscribe({
      next: (materia) => this.materiaEncontrada = materia,
      error: () => this.materiaEncontrada = null
    });
  }
  
  eliminar(id: number): void {
    this.materiaService.eliminarMateria(id).subscribe(() => {
      this.cargarMaterias(); 
    });
  }
  
  agregar(): void {
    if (this.idAlumno && this.idMateriaSeleccionada) {
      this.materiaService.agregarMateriaAlumno(this.idAlumno, this.idMateriaSeleccionada)
        .subscribe(() => {
          alert("Materia agregada correctamente");
        });
      }
    }
}

