import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { RealstateService } from "../realstate.service";
import { Store, select } from "@ngrx/store";
import { EMPTY, map, mergeMap, withLatestFrom } from "rxjs";
import { selectRealstate } from "./realstate.selector";
import { RealStateFetchAPISuccess, invokeRealStateAPI } from "./realstate.action";

@Injectable()

export class RealstateEffect {

    constructor (private actions$:Actions, private stateService: RealstateService,private store:Store ){

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
    
}
