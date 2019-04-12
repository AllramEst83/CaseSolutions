import React from "react";
import PropTypes from "prop-types";
import { connect } from "react-redux";
import * as patientJournalActions from "../../Redux/actions/patientJournalActions";
import { bindActionCreators } from "redux";

class PatientJournalPage extends React.Component {
  render() {
    const rowStyle = {
      marginTop: "25px"
    };
    return (
      <div className="container">
        <div className="row" style={rowStyle}>
          <div className="col-md-8 offset-md-2">
            <h2>Lägg till patient</h2>

            {this.props.patientJournals.map(journal => (
              <div key={journal.firstName}>{journal.firstName}</div>
            ))}
          </div>
        </div>
      </div>
    );
  }
}

addOrEditPatientJournal.propTypes = {
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
)(addOrEditPatientJournal);
