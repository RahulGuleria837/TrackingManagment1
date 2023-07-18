import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { RealstateComponent } from './realstate/realstate/realstate.component';
import { InviteComponent } from './invite/invite.component';
import { InvitedpersonComponent } from './invitedperson/invitedperson.component';
import { ConfirmationComponent } from './confirmation/confirmation.component';
import { AuthGuard } from './auth.guard';
import { audit } from 'rxjs';

const routes: Routes = [
  
  {path:'',component:RealstateComponent},
  {path:'realstate',component:RealstateComponent,canActivate:[AuthGuard]},
  {path:"register", component:RegisterComponent},
  {path:"login",component:LoginComponent},
  {path:"invite",component:InviteComponent,canActivate:[AuthGuard]},
  {path:"confirmation/:reciverId/:status",component:ConfirmationComponent},
  {path:"invitedperson",component:InvitedpersonComponent,canActivate:[AuthGuard]},
   {path:'',
  loadChildren:() =>
    import ('./realstate/realstate.module').then((b)=>
    b.RealstateModule )}
];

@NgModule({
  imports: [RouterModule.forRoot(routes,{onSameUrlNavigation: 'reload'})],

  exports: [RouterModule],

})
export class AppRoutingModule { }
