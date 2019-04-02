import React from "react";
import { Route } from "react-router-dom";

import HomePage from "../components/home/homePage";
import AboutPage from "../components/about/aboutPage";
import HeaderPage from "./common/header";

function App() {
  return (
    <div className="container-fluid">
      <HeaderPage />
      <Route exact path="/" component={HomePage} />
      <Route exact path="/about" component={AboutPage} />
    </div>
  );
}
export default App;
