import initialState from "./initialState";
import * as actionTypes from "../actions/actionTypes";

export default function patientJournalReducer(
  state = initialState.patientJournals,
  action
) {
  switch (action.type) {
    case actionTypes.LOAD_PATIENTJOURNALS_SUCCESS:
      return action.journals;

    case actionTypes.CREATE_PATENTJOURNAL_SUCCESS:
      return [...state, { ...action.journal }];

    default:
      return state;
  }
}
