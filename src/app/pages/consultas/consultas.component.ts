
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-consultas',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './consultas.component.html',
  styleUrls: ['./consultas.component.css']
})
export class ConsultasComponent implements OnInit {

  idAlumno: number = 0;
  materiaSeleccionada: any = null;
  materias: any[] = [];
  materiasAgregadas: string[] = [];
  costoTotal: number = 0;
  mensaje: string = '';
  todasLasMaterias: any[] = [];

  ngOnInit(): void {
    this.obtenerTodasLasMaterias(); 
  }
  

  obtenerTodasLasMaterias(): void {
    fetch('https://localhost:5242/api/materia/obtener')
      .then(res => res.json())
      .then(data => {
        this.todasLasMaterias = data;
      })
      .catch(err => {
        console.error('Error al obtener materias:', err);
      });
  }
  
  consultarMaterias(): void {
    fetch(`https://localhost:5242/api/materia/materias-alumno/${this.idAlumno}`)
      .then(res => res.json())
      .then(data => {
        this.materias = data;
        this.mensaje = data.length ? '' : 'El alumno no tiene materias registradas';
      });
  }

  consultarCosto(): void {
    fetch(`https://localhost:5242/api/materia/costo-total/${this.idAlumno}`)
      .then(res => res.json())
      .then(data => {
        this.costoTotal = data;
      });
  }

  agregarMateria(): void {
    if (!this.materiaSeleccionada || !this.idAlumno) {
      alert('Debes seleccionar una materia y un ID de alumno.');
      return;
    }

    const dto = {
      idAlumno: this.idAlumno,
      nombreMateria: this.materiaSeleccionada.nombre
    };

    fetch('https://localhost:5242/api/materia/agregar-materia', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(dto)
    })
      .then(res => res.json())
      .then(data => {
        this.mensaje = data.mensaje;
        this.materiasAgregadas.push(this.materiaSeleccionada.nombre);
        this.materiaSeleccionada = null;
        this.consultarMaterias();
        this.consultarCosto();
      });
  }
}














