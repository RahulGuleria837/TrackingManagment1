import { createAction, props } from "@ngrx/store";
import { Realstate } from "./realstate";
import { LoginComponent } from "src/app/login/login.component";


export const invokeRealStateAPI = createAction(
    '[RealState API] Invoke RealState Fetch API'
  );
   
  //To get all States
  export const RealStateFetchAPISuccess = createAction(
    '[RealState API] Fetch API Success',
    props<{ allStates: Realstate[] }>()
  );

  //To create all states
  export const invokeSaveRealStateAPI = createAction(
    '[RealState API] Invoke save new State api',
    props<{newStates: Realstate }>()
  );

  export const saveNewRealStateAPISucess = createAction(
    '[RealState API] Invoke new State api success',
    props<{newStates: Realstate}>()
  );

  //To Update
  export const invokeupdateRealStateAPI = createAction(
    '[RealState API] update new state api success',
    props<{updateState : Realstate}>()
  );
   
  export const updateRealStateAPISucess = createAction(
    '[Books API] update state api success',
    props<{updateState: any}>()
  );

  //To DELETE
  export const invokeDeleteStateAPI = createAction(
    '[RealState API] invoke delete book api', 
    props<{id:number}>()
  );

  export const deleteStateAPISuccess = createAction(
    '[RealState API] delete State api success',
    props<{id:number}>()
  )

  //  //LOGIN
  //  export const NewLoginAPI = createAction(
  //   '[Login API] save new login api ',
  //   props<{ newLogin: any;  }>()
  // );

  // export const NewLoginAPISucess = createAction(
  //   '[Login API] save new login api success',
  //   props<{ newLogin: any; logout: boolean }>()
  // );


  //  export const Logout = createAction(
  //   '[Logout] Logout success',
  //   props<{ data: { result: any; logout: boolean } }>()
  // );