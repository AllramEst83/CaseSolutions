import React, { PropTypes } from 'react';
import { Link, IndexLink } from 'react-router';

const Header = () => {
  return (
    <div className="topBar">

      <div className="headerMainText">
        CaseSolutions - Veterinary
            <div className="Muybridge">
          🐎
        </div>
      </div>

      <div className="navElement">
        <nav>

          <IndexLink to="/" activeClassName="active">Hem</IndexLink>
          {" | "}
          <Link to="/about" activeClassName="active">Om</Link>
          
        </nav>
      </div>

    </div>
  );
};

export default Header;
