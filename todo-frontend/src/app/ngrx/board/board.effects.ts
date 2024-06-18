import { inject } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { map, switchMap } from 'rxjs/operators';
import * as BoardActions from '../../ngrx/board/board.actions'
import { ApiService } from '../../services/api.service';

export const setBoards = createEffect(
    () => {
        const actions$ = inject(Actions);
        const apiService = inject(ApiService);

        return actions$.pipe(
            ofType(BoardActions.getBoardsApi),
            switchMap(() =>
                apiService.getData("https://localhost:7247/api/Boards/").pipe(
                    map(fetchedData => BoardActions.AddBoards({ boards: fetchedData })),
                )
            )
        );
    },
    { functional: true }
);

export const setBoard = createEffect(
    () => {
        const actions$ = inject(Actions);
        const apiService = inject(ApiService);

        return actions$.pipe(
            ofType(BoardActions.getBoardApi),
            switchMap(action =>
                apiService.getDataById("https://localhost:7247/api/Boards", action.boardId).pipe(
                    map(fetchedData => BoardActions.AddCurrentBoard({ currentBoard: fetchedData })),
                )
            )
        );
    },
    { functional: true }
);

export const addBoard = createEffect(
    () => {
        const actions$ = inject(Actions);
        const apiService = inject(ApiService);

        return actions$.pipe(
            ofType(BoardActions.postBoardApi),
            switchMap(action => 
                apiService.postData("https://localhost:7247/api/Boards", action.board).pipe(
                    map(() => BoardActions.getBoardsApi())
                )
            )
        )
    },
    {functional: true}
)

export const deleteBoard = createEffect(
    () => {
        const actions$ = inject(Actions);
        const apiService = inject(ApiService);

        return actions$.pipe(
            ofType(BoardActions.deleteBoardApi),
            switchMap(action =>
                apiService.deleteDataById("https://localhost:7247/api/Boards", action.boardId).pipe( 
                    map(() => BoardActions.getBoardsApi())
                )
            )
            
        )
    },
    { functional: true }
)

export const changeBoard = createEffect(
    () => {
        const actions$ = inject(Actions);
        const apiService = inject(ApiService);

        return actions$.pipe(
            ofType(BoardActions.patchBoardApi),
            switchMap(action => 
                apiService.patchData(`https://localhost:7247/api/Boards/${action.boardId}?title=${action.boardTitle}`, 1).pipe(
                    map(() => BoardActions.getBoardsApi())
                )
            )
        )
    },
    {functional: true}
)