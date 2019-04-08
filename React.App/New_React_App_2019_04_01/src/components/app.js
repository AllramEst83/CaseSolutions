import React from "react";
import { Route, Switch } from "react-router-dom";

import HomePage from "../components/home/homePage";
import AboutPage from "../components/about/aboutPage";
import HeaderPage from "./common/header";
import PageNotFound from "./pagNotFound";

function App() {
  return (
    <div>
      <HeaderPage />
      <Switch>
        <Route exact path="/" component={HomePage} />
        <Route exact path="/about" component={AboutPage} />

        {/* If no route is matched PageNotFound will show */}
        <Route component={PageNotFound} />
      </Switch>
    </div>
  );
}
export default App;
