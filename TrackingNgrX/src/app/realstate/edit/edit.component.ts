import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { switchMap } from 'rxjs';
import { selectRealstatebyID } from '../store/realstate.selector';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { Store, select } from '@ngrx/store';
import { Appstate } from 'src/app/shared/store/appstate';
import { Realstate } from '../store/realstate';
import { getInvitationrealstate, invokeupdateRealStateAPI } from '../store/realstate.action';
import { setApiStatus } from 'src/app/shared/store/app.action';
import { selectAppState } from 'src/app/shared/store/app.selector';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent implements OnInit  { 
  stateForm: Realstate = {
    id: 0,
    propertyName: '',
    price: 0,
    city: '',
    area: '',
    applicationUserId:""
  }

 
  constructor(private store: Store, private router: Router, private appstore: Store<Appstate>, private route: ActivatedRoute){}


// ngOnChanges(changes: SimpleChanges): void {
//   if (this.invitaionerPersonId != "") {
//     this.store.dispatch(
//       getInvitationrealstate({
//        invitationerstateId: this.invitaionerPersonId,
//       })
//     );
//   } 
  
// }


  

  ngOnInit(): void {
    let fetchData$ = this.route.paramMap.pipe(
      switchMap((params) => {
        var id = Number(params.get('id'));
        console.log("id=",id)
        return this.store.pipe(select(selectRealstatebyID(id)));
      })
    );
    fetchData$.subscribe((data) => {
      if (data) {
        this.stateForm = { ...data };
      }
      else{
        this.router.navigate(['/']);
      }
    });
  }
  
  update() {
    debugger
    this.store.dispatch(
      invokeupdateRealStateAPI({ updateState: { ...this.stateForm } })
    );
    let apiStatus$ = this.appstore.pipe(select(selectAppState));
    apiStatus$.subscribe((apState) => {
      if (apState.apiStatus == 'success') {
        this.appstore.dispatch(
          setApiStatus({ apiStatus: { apiResponseMessage: '', apiStatus: '' }
         })
          );
      }
    })
  }

}