import { createAction, props } from '@ngrx/store';
import { Transaction } from '../../models/transaction.model';

export const startETL = createAction('[ETL] Start ETL');

export const startETLSuccess = createAction(
    '[ETL] Start ETL Success',
    props<{ transactions: Transaction[] }>()
);
export const startETLFailure = createAction(
    '[ETL] Start ETL Failure',
    props<{ error: string }>()
);

export const clearData = createAction('[ETL] Clear data');

export const clearDataSuccess = createAction(
    '[ETL] Clear Success'
);
export const clearDataFailure = createAction(
    '[ETL] Clear Failure',
    props<{ error: string }>()
);
