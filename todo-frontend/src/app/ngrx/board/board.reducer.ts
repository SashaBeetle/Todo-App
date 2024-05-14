import { createFeature, createReducer, on } from "@ngrx/store"
import * as BoardActions from "./board.actions";

export const BOARD_FEATURE_KEY = 'board';

export interface BoardState {
  board: any;
}


export const initialState: BoardState = {
    board: null
}


export const boardReducers = createFeature({
  name: BOARD_FEATURE_KEY,
  reducer: createReducer(
    initialState,
    on(BoardActions.getBoard, state => ({...state})),
    on(BoardActions.AddBoard, (state, {board} ) => ({...state, board: board}))
  )
});
    
