import { combineReducers } from 'redux';
import patientJournals from './patientJournalReducer';

const rootReducer = combineReducers({

    patientJournals: patientJournals

});

export default rootReducer;