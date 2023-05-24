import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { RealstateService } from "../realstate.service";
import { Store, select } from "@ngrx/store";
import { EMPTY, map, mergeMap, switchMap, withLatestFrom } from "rxjs";
import { selectRealstate } from "./realstate.selector";
import { RealStateFetchAPISuccess, invokeRealStateAPI, invokeSaveRealStateAPI, invokeupdateRealStateAPI, saveNewRealStateAPISucess, updateRealStateAPISucess } from "./realstate.action";
import { setApiStatus } from "src/app/shared/store/app.action";
import { Appstate } from "src/app/shared/store/appstate";

@Injectable()

export class RealstateEffect {

    constructor (private actions$:Actions, private stateService: RealstateService,private store:Store,private appStore:Store<Appstate> ){

    }

    loadAllstate$ = createEffect(() =>
    this.actions$.pipe(
      ofType(invokeRealStateAPI),
      withLatestFrom(this.store.pipe(select(selectRealstate))),
      mergeMap(([, realformStore]) => {
        if (realformStore.length > 0) {
          return EMPTY;
        }
        return this.stateService
          .get()
          .pipe(map((data) => RealStateFetchAPISuccess({ allStates: data })));
      })
    )
  );

  saveNewBook$ = createEffect(() => {
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
            return  saveNewRealStateAPISucess({ newStates: data });
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
            return updateRealStateAPISucess({updateState : data });
          })
        );
      })
    );
  });

}
