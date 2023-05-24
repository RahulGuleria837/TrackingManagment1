import { Component, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { selectRealstate, selectRealstatebyID } from '../store/realstate.selector';
import { invokeRealStateAPI, invokeSaveRealStateAPI, invokeupdateRealStateAPI } from '../store/realstate.action';
import { Realstate } from '../store/realstate';
import { selectAppState } from 'src/app/shared/store/app.selector';
import { setApiStatus } from 'src/app/shared/store/app.action';
import { ActivatedRoute, Router } from '@angular/router';
import { switchMap } from 'rxjs';
import { Appstate } from 'src/app/shared/store/appstate';

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
    applicationUserId:"30ac1918-16b5-4231-ad14-bbc1878631b8"
  }
  constructor(private store: Store, private router: Router, private appstore: Store<Appstate>, private route: ActivatedRoute) { }



  books$ = this.store.pipe(select(selectRealstate));

  ngOnInit(): void {
    this.store.dispatch(invokeRealStateAPI());

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


  save() {
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


  

}