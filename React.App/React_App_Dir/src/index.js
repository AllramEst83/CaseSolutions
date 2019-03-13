import 'babel-polyfill';
import React from 'react';
import { render } from 'react-dom';
import { Router, browserHistory } from 'react-router';
import routes from './router';
import '../node_modules/bootstrap/dist/css/bootstrap.min.css';
import './styles/styles.css';
import { Provider } from 'react-redux';

//import configureStore from './store/configureStore';

//const store = configureStore();

render(
  <Provider>
    <Router history={browserHistory} routes={routes} />
  </Provider>,

  document.getElementById('app')
);
