import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CookBookComponent } from './components/cook-book/cook-book.component';

const routes: Routes = [/*{path:'',component:CookBookComponent}*/];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
