import React from "react";
import { render } from "react-dom";
import { BrowserRouter as Router } from "react-router-dom";
import "bootstrap/dist/css/bootstrap.min.css";
import "./styles/appStyle.css";
import App from "./components/app";

render(
  <Router>
    <App />
  </Router>,
  document.getElementById("app")
);
