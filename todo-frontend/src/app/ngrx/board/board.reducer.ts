import { createFeature, createReducer, on } from "@ngrx/store"
import * as BoardActions from "./board.actions";

export const BOARD_FEATURE_KEY = 'boards';

export interface BoardState {
  currentBoard: any;
  boards: any;
}


export const initialState: BoardState = {
    currentBoard: null,
    boards: null
}


export const boardReducers = createFeature({
  name: BOARD_FEATURE_KEY,
  reducer: createReducer(
    initialState,
    on(BoardActions.getState, state => ({...state})),
    on(BoardActions.AddCurrentBoard, (state, {currentBoard} ) => ({...state, currentBoard: currentBoard})),
    on(BoardActions.AddBoards, (state, {boards}) => ({...state, boards: boards})),
  )
});
    
