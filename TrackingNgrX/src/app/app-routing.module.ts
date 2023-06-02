import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { RealstateComponent } from './realstate/realstate/realstate.component';
import { InviteComponent } from './invite/invite.component';
import { InvitedpersonComponent } from './invitedperson/invitedperson.component';

const routes: Routes = [
  {path:'',component:RealstateComponent},
  {path:'realstate',component:RealstateComponent},
  {path:"register", component:RegisterComponent},
  {path:"login",component:LoginComponent},
  {path:"invite",component:InviteComponent},
  {path:"invitedperson/:id/:id1/:id2",component:InvitedpersonComponent},
   {path:'',
  loadChildren:() =>
    import ('./realstate/realstate.module').then((b)=>
    b.RealstateModule )}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
