import React, { PropTypes } from 'react';
import { Link, IndexLink } from 'react-router';

import UserLoginLogic from '../userLoginLogic/userLogInLogic';

const Header = () => {
  return (
    <div className="topBar">

      <div className="headerMainText">
        CaseSolutions - Veterinär
        <div className="Muybridge">
          🐎
        </div>
      </div>

      <div className="navElement">
        <nav>

          <IndexLink to="/" activeClassName="active">Hitta Journal</IndexLink>
          {" | "}
          <Link to="/addNewJournalEntry" activeClassName="active">Ny Journal</Link>
          {" | "}
          <Link to="/about" activeClassName="active">Om CaseSolutions</Link>
          {" | "}
          <Link to="/userLoginLogic" activeClassName="active">Logga ut eller in </Link>

        </nav>
      </div>

    </div>
  );
};

export default Header;
