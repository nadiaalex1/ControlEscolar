import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Materia {
  idMateria: number;
  nombre: string;
  costo: number;
}

@Injectable({ providedIn: 'root' })
export class MateriaService {
  private apiUrl = 'http://localhost:5242/api/materia';

  constructor(private http: HttpClient) {}

  insertarMateria(materia: Materia): Observable<any> {
    return this.http.post(`${this.apiUrl}/registrar`, materia);
  }

  obtenerMaterias(): Observable<Materia[]> {
    return this.http.get<Materia[]>(`${this.apiUrl}/obtener`);
  }

  buscarNombre(nombre: string): Observable<Materia> {
    return this.http.get<Materia>(`${this.apiUrl}/buscar/${nombre}`);
  }
  
  eliminarMateria(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
  obtenerCostoTotal(idAlumno: number): Observable<number> {
    return this.http.get<number>(`${this.apiUrl}/costo-total/${idAlumno}`);
  }
  
  obtenerMateriasDeAlumno(idAlumno: number): Observable<Materia[]> {
    return this.http.get<Materia[]>(`${this.apiUrl}/materias-alumno/${idAlumno}`);
  }
  
  agregarMateriaAlumno(idAlumno: number, idMateria: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/agregar-materia`, {
      idAlumno,
      idMateria
    });
  }
  

}
