import { Component, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { selectRealstate, selectRealstatebyID } from '../store/realstate.selector';
import { invokeDeleteStateAPI, invokeRealStateAPI, invokeSaveRealStateAPI, invokeupdateRealStateAPI } from '../store/realstate.action';
import { Realstate } from '../store/realstate';
import { selectAppState } from 'src/app/shared/store/app.selector';
import { setApiStatus } from 'src/app/shared/store/app.action';
import { ActivatedRoute, Router } from '@angular/router';
import { switchMap } from 'rxjs';
import { Appstate } from 'src/app/shared/store/appstate';

declare var window: any;
@Component({
  selector: 'app-realstate',
  templateUrl: './realstate.component.html',
  styleUrls: ['./realstate.component.scss']
})

export class RealstateComponent implements OnInit {

  stateForm: Realstate = {
    id: 0,
    propertyName: '',
    price: 0,
    city: '',
    area: '',
    applicationUserId: "30ac1918-16b5-4231-ad14-bbc1878631b8"
  }

  deleteModal: any;
  idToDelete: number = 0;

  constructor(private store: Store, private router: Router, private appstore: Store<Appstate>, private route: ActivatedRoute) { }



  state$ = this.store.pipe(select(selectRealstate));

  ngOnInit(): void {
    this.store.dispatch(invokeRealStateAPI());
    //delete
    this.deleteModal = new window.bootstrap.Modal(
      document.getElementById('deleteModal')
    );
  }

  save() {
    debugger
    console.log(this.stateForm.id);
    this.store.dispatch(invokeSaveRealStateAPI({ newStates: this.stateForm }));    
    let apiStatus$ = this.appstore.pipe(select(selectAppState));
    apiStatus$.subscribe((apState) => {
      if (apState.apiStatus == 'success') {
        this.appstore.dispatch(
          setApiStatus({ apiStatus: { apiResponseMessage: '', apiStatus: '' } })
        );

      }
    });
  }

  openDeleteModal(id:number){
    this.idToDelete = id;
    this.deleteModal.show();
  }

  delete(){
    debugger
    this.store.dispatch(
      invokeDeleteStateAPI({
        id: this.idToDelete,
      })
    );
    let apiStatus$ = this.appstore.pipe(select(selectAppState));
    apiStatus$.subscribe((apState)=>{
      if(apState.apiStatus == 'success'){
        this.deleteModal.hide();
        
      }
    })
  }




}