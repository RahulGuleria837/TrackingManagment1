
import { createReducer, on } from "@ngrx/store";
import { Realstate } from "./realstate";
import { state } from "@angular/animations";
import { RealStateFetchAPISuccess, deleteStateAPISuccess, normalreaslstate, saveNewRealStateAPISucess, sendSenderId, updateRealStateAPISucess } from "./realstate.action";

export const initialState: ReadonlyArray<Realstate> = [];

export const applicationSenderId:string="";

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

  on(normalreaslstate, (state, { newState }) => {
    let getState = [...state];
    return (getState = newState);
  })
);

export const invitainSenderIdReducer = createReducer(
  applicationSenderId,
  on(sendSenderId, (state, { ApplicationUserId }) => {
    debugger
    console.log(ApplicationUserId,"id")
    return ApplicationUserId;
  })
);

  

 
 
 

