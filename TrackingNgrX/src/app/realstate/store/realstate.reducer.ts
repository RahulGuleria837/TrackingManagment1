
import { createReducer, on } from "@ngrx/store";
import { Realstate } from "./realstate";
import { state } from "@angular/animations";
import { RealStateFetchAPISuccess } from "./realstate.action";

export const initialState:ReadonlyArray<Realstate>=[];

export const RealstateReducer = createReducer(
    initialState,
    on(RealStateFetchAPISuccess,(state,{ allStates }) =>
    {
        return allStates;
    })
    );

