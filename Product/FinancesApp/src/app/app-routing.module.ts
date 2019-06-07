import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { CategoriaComponent } from './categoria/categoria.component';
import { TransacaoComponent } from './transacao/transacao.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'categoria', component: CategoriaComponent },
  { path: 'transacao', component: TransacaoComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
