import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { RealstateComponent } from './realstate/realstate/realstate.component';

const routes: Routes = [
  {path:"register", component:RegisterComponent},
  {path:"login",component:LoginComponent},
   {path:'',
  loadChildren:() =>
    import ('./realstate/realstate.module').then((b)=>
    b.RealstateModule )
}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
