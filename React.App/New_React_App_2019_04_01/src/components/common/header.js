import React from "react";
import { NavLink } from "react-router-dom";

const Header = () => {
  const activeStyleColor = { color: "#F15B2A" };
  return (
    <div className="topBar">
      <div className="headerMainText">
        CaseSolutions - Veterinär
        <div className="Muybridge">🐎</div>
      </div>

      <div className="navElements">
        <nav>
          <NavLink to="/" activeStyle={activeStyleColor} exact>
            Hitta Journal
          </NavLink>
          {" | "}
          <NavLink to="/add" activeStyle={activeStyleColor} exact>
            Lägg till patient
          </NavLink>
          {" | "}
          <NavLink to="/about" activeStyle={activeStyleColor} exact>
            Om CaseSolutions
          </NavLink>
        </nav>
      </div>
    </div>
  );
};
export default Header;
