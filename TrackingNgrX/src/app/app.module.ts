import { NgModule, isDevMode } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import { RegisterComponent } from './register/register.component';
import { RouterModule } from '@angular/router';
import{HTTP_INTERCEPTORS, HttpClientModule,} from '@angular/common/http';
import { LoginComponent } from './login/login.component';
import { RealstateComponent } from './realstate/realstate/realstate.component';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { appReducer } from './shared/store/app.reducer';
import { InviteComponent } from './invite/invite.component';
import { LoginEffect } from './realstate/store/login.effect';
import { loginReducer } from './realstate/store/login.reducer';
import { NewrequestInterceptor } from 'src/INTERCEPTOR/newrequest.interceptor';
import { RealstateEffect } from './realstate/store/realstate.effect';
import { RealstateReducer, invitainSenderIdReducer } from './realstate/store/realstate.reducer';
import { InvitedpersonComponent } from './invitedperson/invitedperson.component';
import { ValuesPipe } from './values.pipe';
import { ConfirmationComponent } from './confirmation/confirmation.component';


@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    LoginComponent,
    RealstateComponent,
    InviteComponent,
    InvitedpersonComponent,
    ValuesPipe,
    ConfirmationComponent
  
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    StoreModule.forRoot({}),
    EffectsModule.forRoot({}),
    EffectsModule.forFeature(RealstateEffect),
    StoreModule.forFeature('mystate', RealstateReducer),
    StoreModule.forFeature('senderInvitaionerId',invitainSenderIdReducer),
    EffectsModule.forFeature(LoginEffect),
    StoreModule.forFeature('mylogins', loginReducer),
    StoreModule.forRoot({ appState: appReducer }),
    StoreDevtoolsModule.instrument({ maxAge: 25, logOnly: !isDevMode() }),
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: NewrequestInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
