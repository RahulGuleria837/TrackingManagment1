import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RealstateRoutingModule } from './realstate-routing.module';
import { EffectsModule } from '@ngrx/effects';
import { RealstateEffect } from './store/realstate.effect';
import { StoreModule } from '@ngrx/store';
import { RealstateReducer } from './store/realstate.reducer';
import { EditComponent } from './edit/edit.component';
import { FormsModule } from '@angular/forms';
import { AddComponent } from './add/add.component';


@NgModule({
  declarations: [
  
    EditComponent,
       AddComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    RealstateRoutingModule,
    // StoreModule.forFeature('mystate',RealstateReducer),
    // EffectsModule.forFeature(RealstateEffect)
  ]
})
export class RealstateModule { }
