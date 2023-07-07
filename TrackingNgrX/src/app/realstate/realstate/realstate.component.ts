import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { selectRealstate, selectRealstatebyID } from '../store/realstate.selector';
import { RealStateFetchAPISuccess,  getInvitationrealstate,  invokeDeleteStateAPI, invokeRealStateAPI, invokeSaveRealStateAPI, invokeupdateRealStateAPI } from '../store/realstate.action';
import { Realstate } from '../store/realstate';
import { selectAppState } from 'src/app/shared/store/app.selector';
import { setApiStatus } from 'src/app/shared/store/app.action';
import { ActivatedRoute, Router } from '@angular/router';
import { switchMap } from 'rxjs';
import { Appstate } from 'src/app/shared/store/appstate';
import { InvitedpersonService } from 'src/app/invitedperson.service';

declare var window: any;
@Component({
  selector: 'app-realstate',
  templateUrl: './realstate.component.html',
  styleUrls: ['./realstate.component.scss']
})

export class RealstateComponent implements OnInit,OnChanges {
  @Input() invitaionerPersonId:string="";

  xyz:any
  stateForm: Realstate = {
    id: 0,
    propertyName: '',
    price: 0,
    city: '',
    area: '',
    applicationUserId: ""
  }
  



  deleteModal: any;
  idToDelete: number = 0;

  constructor(private store: Store, private appstore: Store<Appstate>, private route: ActivatedRoute,private invitedperson:InvitedpersonService) { 
  
  }

  isObjectType(value: any): value is object {
    return typeof value === 'object' && value !== null;
  }

  state$ = this.store.pipe(select(selectRealstate));

ngOnChanges(changes: SimpleChanges): void {
  if (this.invitaionerPersonId != '') {
    this.store.dispatch(
      getInvitationrealstate({
       invitationerstateId: this.invitaionerPersonId,
      })
    );
  } else {
    this.store.dispatch(RealStateFetchAPISuccess({allStates:[]}));
  }
}



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