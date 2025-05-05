import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Alumno {
  idAlumno: number;
  nombre: string;
  apellidoPaterno: string;
  apellidoMaterno: string;
  usuario:string;
  contrasenia: string;
}

@Injectable({ providedIn: 'root' })
export class AlumnoService {

  private apiUrl = 'http://localhost:5242/api/alumno';

  constructor(private http: HttpClient) {}

  insertarAlumno(alumno: Alumno): Observable<any> {
    return this.http.post(`${this.apiUrl}/registrar`, alumno);
  }

  obtenerAlumnos(): Observable<Alumno[]> {
    return this.http.get<Alumno[]>(`${this.apiUrl}/obtener`);
  }

  eliminarAlumno(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/eliminar/${id}`);
  }
  
  actualizarAlumno(id: number, alumno: Alumno): Observable<any> {
    return this.http.put(`${this.apiUrl}/actualizar/${id}`, alumno);
  }
  
  buscarAlumNombre(nombre: string): Observable<Alumno[]> {
    return this.http.get<Alumno[]>(`${this.apiUrl}/buscar?nombre=${nombre}`);
  }
  
}