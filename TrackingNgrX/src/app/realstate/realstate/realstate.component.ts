import { Component, Input, OnChanges, OnDestroy, OnInit, SimpleChanges } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { selectRealstate, selectRealstatebyID, senderId } from '../store/realstate.selector';
import { RealStateFetchAPISuccess,  getInvitationrealstate,  invokeDeleteStateAPI, invokeRealStateAPI, invokeSaveRealStateAPI, invokeupdateRealStateAPI, sendSenderId } from '../store/realstate.action';
import { Realstate } from '../store/realstate';
import { selectAppState } from 'src/app/shared/store/app.selector';
import { setApiStatus } from 'src/app/shared/store/app.action';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, switchMap } from 'rxjs';
import { Appstate } from 'src/app/shared/store/appstate';
import { InvitedpersonService } from 'src/app/invitedperson.service';
import { applicationSenderId } from '../store/realstate.reducer';
import { RealstateService } from '../realstate.service';

declare var window: any;
@Component({
  selector: 'app-realstate',
  templateUrl: './realstate.component.html',
  styleUrls: ['./realstate.component.scss']
})

export class RealstateComponent implements OnInit,OnChanges,OnDestroy,OnInit {

  @Input() invitaionerPersonId:string="";
  state$:Observable<any>
  userId:any
  logginUser:any;
     buttonLabeltracking='Show Table'
  deleteModal: any;
  idToDelete: number = 0;

  trackingUser = {
    trackingDetails: [
      {
        dataChangeUser: {
          userName: ''
        },
        userActions: 0, // 1 for 'Add', 2 for 'Update', 3 for 'Delete'
        trackingDate: new Date()
      }]
  };
  
  stateForm: Realstate = {
    id: 0,
    propertyName: '',
    price: 0,
    city: '',
    area: '',
    applicationUserId: ""
  }

  constructor(private store: Store, private appstore: Store<Appstate>,private router:Router,
     private route: ActivatedRoute,private invitedperson:InvitedpersonService,private realstateService:RealstateService)
 { 
  this.state$ = this.store.pipe(select(selectRealstate))
  this.state$.subscribe((data)=>{
      // console.log(data,"test");
  })
  }
  
  isObjectType(value: any): value is object {
    return typeof value === 'object' && value !== null;
  }

ngOnChanges(changes: SimpleChanges): void {
  if (this.invitaionerPersonId != "") {
    this.store.dispatch(
      getInvitationrealstate({
      ApplicationUserId:this.invitaionerPersonId
      })
    );}}


  ngOnInit(): void {
     this.store.dispatch(invokeRealStateAPI());

    //delete
    this.deleteModal = new window.bootstrap.Modal(
      document.getElementById('deleteModal'));
    var currentUser = localStorage.getItem('currentUser');
    if (currentUser) {
      this.logginUser = JSON.parse(currentUser);}}

ngOnDestroy(): void {
this.store.dispatch(invokeRealStateAPI());
 }
 

save(){
  this.store.dispatch(sendSenderId({ApplicationUserId:this.invitaionerPersonId})) 
}

  openDeleteModal(id:number){
    this.idToDelete = id;
    this.deleteModal.show();
  }

  delete(){
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
  
  specificTracking(id:number,applicationUserId:string){
    debugger
    this.realstateService.showSpecificTrackingData(id, applicationUserId).subscribe(
      (data)=>{
        this.trackingUser = data;
        console.log(data,"lastride")        
   })}
}
