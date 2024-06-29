import { inject } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { ApiService } from "../../services/api.service";
import { map, switchMap } from "rxjs";
import * as CardActions from '../../ngrx/card/card.actions'
import * as BoardActions from '../../ngrx/board/board.actions'

export const deleteCard = createEffect(
    () => {
        const actions$ = inject(Actions);
        const apiService = inject(ApiService);

        return actions$.pipe(
            ofType(CardActions.deleteCardApi),
            switchMap(action =>
                apiService.deleteDataById(`https://localhost:7247/api/cards`, action.cardId).pipe( 
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
            ofType(CardActions.postCardApi),
            switchMap(action =>
                apiService.postData(`https://localhost:7247/api/cards`, action.card).pipe( 
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
            ofType(CardActions.patchCardApi),
            switchMap(action =>
                apiService.patchData(`https://localhost:7247/api/cards`, action.card).pipe( 
                    map(() => BoardActions.getBoardApi({boardId: action.boardId}))
                )
            )   
        )
    },
    { functional: true }
)