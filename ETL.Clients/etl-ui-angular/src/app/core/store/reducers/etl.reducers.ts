import { createReducer, on } from '@ngrx/store';
import * as ETLActions from '../actions/etl.actions';

export interface EtlState {
    message: string | null;
    error: string | null;
    loading: boolean;
}

export const initialState: EtlState = {
    message: null,
    error: null,
    loading: false,
};

export const etlReducer = createReducer(
    initialState,
    on(ETLActions.startETL, (state) => ({
        ...state,
        loading: true,
        error: null,
    })),
    on(ETLActions.startETLSuccess, (state, { message }) => ({
        ...state,
        loading: false,
        message,
    })),
    on(ETLActions.startETLFailure, (state, { error }) => ({
        ...state,
        loading: false,
        error,
    }))
);
