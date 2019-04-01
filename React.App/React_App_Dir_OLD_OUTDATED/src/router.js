import React from 'react';
import { Route, IndexRoute } from 'react-router';
import App from './components/app';

import FindPatiantJournal from './components/findPatiantJournal/findPatiantJournal';
import AddNewJournalEntry from './components/addNewJournalEntry/addNewJournalEntry';
import About from './components/about/about';
import UserLoginLogic from './components/userLoginLogic/userLogInLogic';

export default (
  <Route path="/" component={App}>
    <IndexRoute component={FindPatiantJournal} />
    <Route path="addNewJournalEntry" component={AddNewJournalEntry} />
    <Route path="about" component={About} />
    <Route path="userLoginLogic" component={UserLoginLogic} />
  </Route>
  );
