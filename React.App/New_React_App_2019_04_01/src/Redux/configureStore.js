import { createStore, applyMiddleware, compose } from "redux";
import rootreducer from "./reducers/index";
import reduxImmutbleStateInvariant from "redux-immutable-state-invariant";
import thunk from "redux-thunk";

export default function configureStore(initialState) {
  const composeEnhancers =
    window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;
  return createStore(
    rootreducer,
    initialState,
    composeEnhancers(applyMiddleware(thunk, reduxImmutbleStateInvariant()))
  );
}
