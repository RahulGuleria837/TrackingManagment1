import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { RealstateService } from "../realstate.service";
import { Store, select } from "@ngrx/store";
import { EMPTY, exhaustMap, map, mergeMap, switchMap, withLatestFrom } from "rxjs";
import { selectRealstate } from "./realstate.selector";
import {  RealStateFetchAPISuccess, deleteStateAPISuccess,  getInvitationrealstate,  invokeDeleteStateAPI, invokeRealStateAPI, invokeSaveRealStateAPI, invokeupdateRealStateAPI, saveNewRealStateAPISucess, updateRealStateAPISucess } from "./realstate.action";
import { setApiStatus } from "src/app/shared/store/app.action";
import { Appstate } from "src/app/shared/store/appstate";
import { Router } from "@angular/router";
import { InvitedpersonService } from "src/app/invitedperson.service";

@Injectable()

export class RealstateEffect {

    constructor (private actions$:Actions, private stateService: RealstateService,private store:Store,private appStore:Store<Appstate>,private route:Router,private invitationService:InvitedpersonService ){

    }

    loadAllstate$ = createEffect(() =>
    this.actions$.pipe(
      ofType(invokeRealStateAPI),
      withLatestFrom(this.store.pipe(select(selectRealstate))),
      switchMap(([, realformStore]) => {
        debugger
        if (realformStore.length > 0) {
          return EMPTY;
        }
        return this.stateService
          .get()
          .pipe(map((data) => RealStateFetchAPISuccess({ allStates: data })));
      })
    )
  );

  saveNewState$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(invokeSaveRealStateAPI),
      switchMap((action) => {
        this.store.dispatch(
          setApiStatus({ apiStatus: { apiResponseMessage: '', apiStatus: '' } })
        );
        return this.stateService.Create(action.newStates).pipe(
          map((data) => {
            this.appStore.dispatch(
              setApiStatus({
                apiStatus: { apiResponseMessage: '', apiStatus: 'success' },
              })
            );
          
            return  saveNewRealStateAPISucess({ newStates:  action.newStates });
         

          })
        );
      })
    );
  });


  updateRealStateAPI$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(invokeupdateRealStateAPI),
      switchMap((action) => {
        this.appStore.dispatch(
          setApiStatus({ apiStatus: { apiResponseMessage: '', apiStatus: '' } })
        );
        return this.stateService.update(action.updateState).pipe(
          map((data) => {
            this.appStore.dispatch(
              setApiStatus({
                apiStatus: { apiResponseMessage: '', apiStatus: 'success' },
              })
            );
            this.route.navigate(['/realstate']);
            return updateRealStateAPISucess({updateState : action.updateState });
 })
 ); 
  }) 
              );
  });

  deleteStateAPI$ = createEffect(()=>{
    return this.actions$.pipe(
      ofType(invokeDeleteStateAPI),
      switchMap((action)=>{
        this.appStore.dispatch(
          setApiStatus({apiStatus:{apiResponseMessage: '', apiStatus:''}})
        );
        
        return this.stateService.delete(action.id).pipe(
          map(()=>{
            this.appStore.dispatch(
              setApiStatus({
                apiStatus:{apiResponseMessage:'', apiStatus:'success'},
              })
            );
            return deleteStateAPISuccess({id: action.id});
          })
        );

      })
    );
  });
  //
  getInvitaionerState$ = createEffect(() =>
  this.actions$.pipe(
    ofType(getInvitationrealstate),
    switchMap((actions)=>{
      return this.invitationService.specificUserData(actions.ApplicationUserId).pipe(
        map((datas:any) => RealStateFetchAPISuccess({ allStates: datas })));
        
    })
  )
);

}
