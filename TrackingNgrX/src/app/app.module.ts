import { NgModule, isDevMode } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import { RegisterComponent } from './register/register.component';
import { RouterModule } from '@angular/router';
import{HttpClientModule,} from '@angular/common/http';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './realstate/home/home.component';
import { RealstateComponent } from './realstate/realstate/realstate.component';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';


@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    LoginComponent,
    RealstateComponent,
    HomeComponent
  
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    StoreModule.forRoot({},{}),
    EffectsModule.forRoot([]),
    StoreDevtoolsModule.instrument({ maxAge: 25, logOnly: !isDevMode() })


  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
