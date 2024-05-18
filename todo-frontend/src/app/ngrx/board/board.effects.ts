import { inject } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import {  concatMap, map, switchMap, tap } from 'rxjs/operators';
import * as BoardActions from '../../ngrx/board/board.actions'
import { ApiService } from '../../services/api.service';
import { of } from 'rxjs';






export const setBoards = createEffect(
    () => {
        const actions$ = inject(Actions);
        const apiService = inject(ApiService);

        return actions$.pipe(
            ofType(BoardActions.getBoardsTest),
            switchMap(
                () => 
                    apiService.getData("https://localhost:7247/api/Boards").pipe(
                        concatMap((fetchedData) =>
                            of(BoardActions.AddBoards({ boards: fetchedData })).pipe(
                                tap(() => BoardActions.getBoards() ) 
                            )
                        )
                    )
            )
        )    
    }, {functional: true}
)