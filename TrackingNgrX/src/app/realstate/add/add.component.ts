import { Component, OnInit } from '@angular/core';
import { Route, Router } from '@angular/router';
import { Store, select } from '@ngrx/store';
import { senderId } from '../store/realstate.selector';
import { invokeSaveRealStateAPI } from '../store/realstate.action';
import { selectAppState } from 'src/app/shared/store/app.selector';
import { Appstate } from 'src/app/shared/store/appstate';
import { setApiStatus } from 'src/app/shared/store/app.action';
import { Realstate } from '../store/realstate';
import { Location } from '@angular/common';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent {

  xyz:any
  stateForm: Realstate = {
    id: 0,
    propertyName: '',
    price: 0,
    city: '',
    area: '',
    applicationUserId: ""
  }
  
  constructor(private store:Store, private router:Router,private appstore: Store<Appstate>,private location:Location){}
  save() {
    this.store.pipe(select(senderId)).subscribe({
next:(data)=> {this.stateForm.applicationUserId=data}
})
    this.store.dispatch(invokeSaveRealStateAPI({ newStates: this.stateForm }));    
    let apiStatus$ = this.appstore.pipe(select(selectAppState));
this.location.back();
    apiStatus$.subscribe((apState) => {
      if (apState.apiStatus == 'success') {
        this.appstore.dispatch(
          setApiStatus({ apiStatus: { apiResponseMessage: '', apiStatus: '' }})
        );}
    });
  }
  cancel(){
 this.location.back();
  }
}
