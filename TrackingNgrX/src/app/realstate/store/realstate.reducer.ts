
import { createReducer, on } from "@ngrx/store";
import { Realstate } from "./realstate";
import { state } from "@angular/animations";
import { RealStateFetchAPISuccess, deleteStateAPISuccess, saveNewRealStateAPISucess, updateRealStateAPISucess } from "./realstate.action";

export const initialState: ReadonlyArray<Realstate> = [];

//For GetALLSTateS
export const RealstateReducer = createReducer(
  initialState,
  on(RealStateFetchAPISuccess, (state, { allStates }) => {
    return allStates;
  }),

  //
  on(saveNewRealStateAPISucess, (state, { newStates }) => {
    let newState = [...state];
    newState.unshift(newStates);
    return newState;
  }),

  on(updateRealStateAPISucess, (state, { updateState }) => {
    let newState = state.filter((_) => _.id != updateState.id);
    newState.unshift(updateState);
    return newState;
  }),

  on(deleteStateAPISuccess,(state,{ id }) => {
    let newState = state.filter((_)=> _.id != id);
    return newState;
  }),

  );

 
 
 

