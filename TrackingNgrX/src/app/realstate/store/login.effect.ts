import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { Action, Store } from "@ngrx/store";
import { LoginService } from "src/app/login.service";
import { invokeSaveNewLoginAPI, saveNewLoginAPISucess } from "./login.action";
import { EMPTY, catchError, map, switchMap } from "rxjs";
import { Appstate } from "src/app/shared/store/appstate";
import { setApiStatus } from "src/app/shared/store/app.action";
import { Router } from "@angular/router";

@Injectable()
export class LoginEffect {
    

    constructor( private actions$: Actions,
      private _login:LoginService,
      private store: Store,
      private appStore: Store<Appstate>){}







// login$ = createEffect(() => {
//     return this.actions$.pipe(
//       ofType(invokeSaveNewLoginAPI),
//       switchMap((action) => {
//         return this._login.login(action.newLogin).pipe(
//           map((data) => {
//             localStorage.setItem("currentUser",JSON.stringify(data))
//             this.appStore.dispatch(
//               setApiStatus({
//                 apiStatus: { apiResponseMessage: '', apiStatus: 'success' },
//               })
//             );
          
//             return saveNewLoginAPISucess({ newLogin: action.newLogin,logout:false });
//           }),
//           catchError((error: any) =>{ 
//             this.appStore.dispatch(
//               setApiStatus({
//                 apiStatus: {
//                   apiResponseMessage: error.error.data,
//                   apiStatus: 'failure',
//                 },
//               }));
//               return EMPTY;
//           //  return of(LoginActions.LoginFailure(error))
//           })
//         );
//       })
//     );
//   });
login$ = createEffect(() => {
  return this.actions$.pipe(
    ofType(invokeSaveNewLoginAPI),
    switchMap((action) => {
      this.appStore.dispatch(
        setApiStatus({ apiStatus: { apiResponseMessage: '', apiStatus: '' } })
      );
      return this._login.login(action.newLogin).pipe(
        map((data) => {
          localStorage.setItem("currentUser",JSON.stringify(data))
          console.log(localStorage.getItem("currentUser"));
          this.appStore.dispatch(
            setApiStatus({
              apiStatus: { apiResponseMessage: '', apiStatus: 'success' },
            })
          );
          return saveNewLoginAPISucess({ newLogin: action.newLogin,logout:false });
        }),
        catchError((error: any) =>{ 
          this.appStore.dispatch(
            setApiStatus({
              apiStatus: {
                apiResponseMessage: error.error.data,
                apiStatus: 'failure',
              },
            }));
            return EMPTY;
        //  return of(LoginActions.LoginFailure(error))
        })
      );
    })
  );
});

}