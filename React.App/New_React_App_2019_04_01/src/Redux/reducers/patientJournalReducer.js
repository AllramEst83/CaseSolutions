
import initialState from './initialState';
import * as actionTypes from '../actions/actionTypes';

export default function patientJournalReducer(state = initialState, action) {

    switch (action) {

        case actionTypes.LOAD_PATIENTJOURNALS_SUCCESS:
            return action.patientJournals;

        case actionTypes.CREATE_PATENTJOURNAL_SUCCESS:
            return [...state, Object.assign({}, action.patientJournals)];

        default:
            return state;
    }

}