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
                apiService.deleteDataById(`https://localhost:7247/api/v1/cards`, action.cardId).pipe( 
                    map(() => BoardActions.getBoardApi({boardId: action.boardId}))
                )
            )
        )
    },
    { functional: true }
)

export const addCard = createEffect(
    () => {
        const actions$ = inject(Actions);
        const apiService = inject(ApiService);

        return actions$.pipe(
            ofType(CardActions.postCardApi),
            switchMap(action =>
                apiService.postData(`https://localhost:7247/api/v1/cards`, action.card).pipe( 
                    map(() => BoardActions.getBoardApi({boardId: action.boardId}))
                )
            )
        )
    },
    { functional: true }
)

export const patchCard = createEffect(
    () => {
        const actions$ = inject(Actions);
        const apiService = inject(ApiService);

        return actions$.pipe(
            ofType(CardActions.patchCardApi),
            switchMap(action =>
                apiService.patchData(`https://localhost:7247/api/v1/cards`, action.card).pipe( 
                    map(() => BoardActions.getBoardApi({boardId: action.boardId}))
                )
            )   
        )
    },
    { functional: true }
)