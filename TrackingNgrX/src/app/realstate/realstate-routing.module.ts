import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StoreModule } from '@ngrx/store';
import { RealstateReducer } from './store/realstate.reducer';
import { RealstateComponent } from './realstate/realstate.component';
import { EffectsModule } from '@ngrx/effects';
import { RealstateEffect } from './store/realstate.effect';

const routes: Routes = [
{  path:'',
  component:RealstateComponent
},
];

@NgModule({
  imports: [
  RouterModule.forChild(routes),
  EffectsModule.forFeature([RealstateEffect]),
  StoreModule.forFeature('mystate',RealstateReducer)
  
  ],
  exports: [RouterModule],
})
export class RealstateRoutingModule { }
