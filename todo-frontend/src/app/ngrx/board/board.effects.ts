import { inject } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import {  map, switchMap } from 'rxjs/operators';
import * as BoardActions from '../../ngrx/board/board.actions'
import { ApiService } from '../../services/api.service';


export const getBoards = createEffect(
    () => {
        const actions$ = inject(Actions);
        const apiService = inject(ApiService);

        
          
        return actions$.pipe(
            ofType(BoardActions.getBoardsTest),
            switchMap(
                () => apiService.getData("https://localhost:7247/api/Boards").pipe(
                    map((fetchedData) =>{
                        return BoardActions.AddBoards({boards: fetchedData})
                    } )
                )
            )

        )

        
    }, {functional: true}

)


    



    