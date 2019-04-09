import React from "react";
import PropTypes from "prop-types";
import { connect } from "react-redux";
import * as patientJournalActions from "../../Redux/actions/patientJournalActions";
import { bindActionCreators } from "redux";

class AddPatientJournal extends React.Component {
  state = {
    patientJournal: {
      firstName: "Förnamn",
      lastName: "Efternamn"
    }
  };

  handleChange = event => {
    const patientJournal = {
      ...this.state.patientJournal,
      firstName: event.target.value
    };
    this.setState({ patientJournal: patientJournal });
  };

  handleSubmit = event => {
    event.preventDefault();
    this.props.actions
      .ACTIONS_CreatePatientJournal(this.state.patientJournal)
      .then(
        alert(
          "Fix this submit button, Promise resolved: " +
            this.state.patientJournal.firstName
        )
      )
      .catch();
  };

  render() {
    const rowStyle = {
      marginTop: "25px"
    };
    return (
      <div className="container">
        <div className="row" style={rowStyle}>
          <div className="col-md-8 offset-md-2">
            <h2>Lägg till patient</h2>
            <form onSubmit={this.handleSubmit}>
              <div className="form-group">
                <label name="firstName">
                  <b>Förnamn</b>
                </label>
                <input
                  id="firstName"
                  type="text"
                  onChange={this.handleChange}
                  value={this.state.patientJournal.firstName}
                  className="form-control"
                />
              </div>

              <button
                onClick={this.handleSubmit}
                className="btn btn-success buttonMargin"
                type="submit"
              >
                Skicka
              </button>
            </form>
            {this.props.patientJournals.map(journal => (
              <div key={journal.firstName}>{journal.firstName}</div>
            ))}
          </div>
        </div>
      </div>
    );
  }
}

AddPatientJournal.propTypes = {
  patientJournals: PropTypes.array.isRequired,
  actions: PropTypes.object.isRequired
};

function mapStateToProps(state) {
  return {
    patientJournals: state.patientJournals
  };
}

function mapDispatchToProps(dispatch) {
  return {
    actions: bindActionCreators(patientJournalActions, dispatch)
  };
}

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(AddPatientJournal);
