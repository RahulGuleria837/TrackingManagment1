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

declare var window: any;
@Component({
  selector: 'app-realstate',
  templateUrl: './realstate.component.html',
  styleUrls: ['./realstate.component.scss']
})

export class RealstateComponent implements OnInit,OnChanges,OnDestroy {
  @Input() invitaionerPersonId:string="";
  books$:Observable<any>
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

  constructor(private store: Store, private appstore: Store<Appstate>,private router:Router,
     private route: ActivatedRoute,private invitedperson:InvitedpersonService)
 { 
  this.books$ = this.store.pipe(select(selectRealstate))
  }

  isObjectType(value: any): value is object {
    return typeof value === 'object' && value !== null;
  }

  // state$ = this.store.pipe(select(selectRealstate));

ngOnChanges(changes: SimpleChanges): void {
  if (this.invitaionerPersonId != "") {
    this.store.dispatch(
      getInvitationrealstate({
      ApplicationUserId:this.invitaionerPersonId
      })
    );
  
  } 
}
// this.store.dispatch(invokeSaveNewBookAPI({ newBook: this.bookForm }));
// let apiStatus$ = this.appStore.pipe(select(selectAppState));
// apiStatus$.subscribe((apState) => {
//   if (apState.apiStatus == 'success') {
//     // this.router.navigate(['home']);
//     this.locationService.back();


  ngOnInit(): void {
    this.store.dispatch(invokeRealStateAPI());
    //delete
    this.deleteModal = new window.bootstrap.Modal(
      document.getElementById('deleteModal')
     
    
    );
  
  }

ngOnDestroy(): void {
    
    this.store.dispatch(invokeRealStateAPI());
  }
 
save(){
  debugger
  this.store.dispatch(sendSenderId({ApplicationUserId:this.invitaionerPersonId}))
}
 
  
//   save() {
//     debugger
//     this.store.pipe(select(senderId)).subscribe(
// {
//   next:(data)=> {this.stateForm.applicationUserId=data}
// }
//     )
//     this.store.dispatch(invokeSaveRealStateAPI({ newStates: this.stateForm }));    
//     let apiStatus$ = this.appstore.pipe(select(selectAppState));
//     apiStatus$.subscribe((apState) => {
//       if (apState.apiStatus == 'success') {
//         this.appstore.dispatch(
//           setApiStatus({ apiStatus: { apiResponseMessage: '', apiStatus: '' } })
//         );

//       }
//     });
//   }

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