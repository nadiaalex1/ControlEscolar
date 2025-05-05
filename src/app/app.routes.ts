import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { ConsultasComponent } from './pages/consultas/consultas.component';
import { MateriasComponent } from './pages/materias/materias.component';
import { AlumnoComponent } from './pages/alumno/alumno.component';

export const routes: Routes = [

{ 
    path: 'login', 
    component: LoginComponent 
}, 
{ 
    path: 'consultas',
    loadComponent: () =>
        import('./pages/consultas/consultas.component').then((m) => m.ConsultasComponent),
    }, 
{ 
    path: 'materias',
    component: MateriasComponent
},
{ 
    path: 'alumno',
    component: AlumnoComponent
}

];
