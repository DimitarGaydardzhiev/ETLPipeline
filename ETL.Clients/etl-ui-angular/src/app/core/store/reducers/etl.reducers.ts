import { createReducer, on } from '@ngrx/store';
import * as ETLActions from '../actions/etl.actions';
import { Transaction } from '../../models/transaction.model';

export interface EtlState {
    error: string | null;
    loading: boolean;
    count: number | null;
    transactions: Transaction[];
}

export const initialState: EtlState = {
    error: null,
    loading: false,
    count: null,
    transactions: []
};

export const etlReducer = createReducer(
    initialState,
    on(ETLActions.startETL, (state) => ({
        ...state,
        loading: true,
        error: null,
    })),
    on(ETLActions.startETLSuccess, (state, { transactions }) => ({
        ...state,
        loading: false,
        transactions
    })),
    on(ETLActions.startETLFailure, (state, { error }) => ({
        ...state,
        loading: false,
        error,
    })),
    on(ETLActions.clearDataSuccess, (state) => ({
        ...state,
        loading: false,
        transactions: []
    }))
);
