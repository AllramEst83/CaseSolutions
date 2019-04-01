

import React from 'react';

import UserLoginLogic from '../userLoginLogic/userLogInLogic';

class Logout extends React.Component {

  constructor(props, context) {
    super(props, context);

    this.state = {
      loggedIn: true
    };

    this.HandleClick = this.HandleClick.bind(this);

  }

  HandleClick(value) {
    this.setState(state => ({
      loggedIn: !state.loggedIn
    }));
  }

  render() {
    return (
      <div className="content-wrapper">
        <div className="row">
          <div className="col-md-offset-2 col-md-8">

            <div className="panel panel-info">
              <div className="panel-heading">Vill du Logga ut?</div>
              <div className="panel-body">
                <div className="buttonContainer">

                  <button
                    className="btn btn-danger"
                    type="button"
                    onClick={this.HandleClick}>

                  {this.state.loggedIn ? "Logga in":"Logga ut"}

                  </button>


                </div>
              </div>
            </div>

          </div>
        </div>
      </div>
    );
  }
}

export default Logout;
