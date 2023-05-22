import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RealstateRoutingModule } from './realstate-routing.module';
import { EffectsModule } from '@ngrx/effects';
import { RealstateEffect } from './store/realstate.effect';
import { StoreModule } from '@ngrx/store';
import { RealstateReducer } from './store/realstate.reducer';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    
    RealstateRoutingModule,
    StoreModule.forFeature('mystate',RealstateReducer),
    EffectsModule.forFeature([RealstateEffect])
  ]
})
export class RealstateModule { }
