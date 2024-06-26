import { inject } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { ApiService } from "../../services/api.service";
import { map, switchMap } from "rxjs";
import * as ListActions from '../../ngrx/list/list.actions'
import * as BoardActions from '../../ngrx/board/board.actions'



export const deleteList = createEffect(
    () => {
        const actions$ = inject(Actions);
        const apiService = inject(ApiService);

        return actions$.pipe(
            ofType(ListActions.deleteListApi),
            switchMap(action =>
                apiService.deleteDataByIdManual(`https://localhost:7247/api/catalog/${action.listId}`).pipe( 
                    map(() => BoardActions.getBoardApi({boardId: action.boardId}))
                )
            )
            
        )
    },
    { functional: true }
)

export const addList = createEffect(
    () => {
        const actions$ = inject(Actions);
        const apiService = inject(ApiService);

        return actions$.pipe(
            ofType(ListActions.postListApi),
            switchMap(action =>
                apiService.postData(`https://localhost:7247/api/catalog`, action.list).pipe( 
                    map(() => BoardActions.getBoardApi({boardId: action.boardId}))
                )
            )
        )
    },
    { functional: true }
)

export const patchList = createEffect(
    () => {
        const actions$ = inject(Actions);
        const apiService = inject(ApiService);

        return actions$.pipe(
            ofType(ListActions.patchListApi),
            switchMap(action =>
                apiService.patchData(`https://localhost:7247/api/catalog/${action.list.id}?title=${action.newListTitle}`,1).pipe( 
                    map(() => BoardActions.getBoardApi({boardId: action.boardId}))
                )
            )   
        )
    },
    { functional: true }
)