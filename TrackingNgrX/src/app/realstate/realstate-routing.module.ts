import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StoreModule } from '@ngrx/store';
import { RealstateReducer } from './store/realstate.reducer';
import { RealstateComponent } from './realstate/realstate.component';
import { EffectsModule } from '@ngrx/effects';
import { RealstateEffect } from './store/realstate.effect';
import { EditComponent } from './edit/edit.component';


const routes: Routes = [
{  path:'',
  component:RealstateComponent
},
{  path:'realstate',
  component:RealstateComponent
},
{path: 'edit/:id',component:EditComponent}
];

@NgModule({
  imports: [
  RouterModule.forChild(routes)  
  ],
  exports: [RouterModule],
})
export class RealstateRoutingModule { }
