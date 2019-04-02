
import React from 'react';
import { Link } from 'react-router';

class FindPatiantJournal extends React.Component {
  render() {
    return (
      <div className="content-wrapper">

        <div className="row defaultMargin">
          <div className="col-md-offset-2 col-md-6">

                <div className="input-group">

                  <div className="input-group-btn">
                    <button
                      type="button"
                      className="btn btn-info dropdown-toggle"
                      data-toggle="dropdown"
                      aria-haspopup="true"
                      aria-expanded="false">
                      Filter <span className="caret"></span>
                    </button>
                    <ul className="dropdown-menu">
                      <li><a href="#">Action</a></li>
                      <li><a href="#">Another action</a></li>
                      <li><a href="#">Something else here</a></li>
                    </ul>
                  </div>

                  <input type="text" className="form-control" placeholder="Sök efter patient journal..." />

                  <span className="input-group-btn">
                    <button className="btn btn-success" type="button">Sök..</button>
                  </span>

                </div>

          </div>
        </div>


        <div className="panel panel-info">
          <div className="panel-heading">Journaler</div>
          <div className="panel-body">

            <div className="row rowPadding">

              <div className="col-md-4 journalStyle">
                <h1>Test</h1>
                <p>Recent animal patient cases goes here</p>
              </div>

              <div className="col-md-4 journalStyle">
                <h1>Test</h1>
                <p>Recent animal patient cases goes here</p>
              </div>

              <div className="col-md-4 journalStyle">
                <h1>Test</h1>
                <p>Recent animal patient cases goes here</p>
              </div>

              <div className="col-md-4 journalStyle">
                <h1>Test</h1>
                <p>Recent animal patient cases goes here</p>
              </div>

              <div className="col-md-4 journalStyle">
                <h1>Test</h1>
                <p>Recent animal patient cases goes here</p>
              </div>

              <div className="col-md-4 journalStyle">
                <h1>Test</h1>
                <p>Recent animal patient cases goes here</p>
              </div>

              <div className="col-md-4 journalStyle">
                <h1>Test</h1>
                <p>Recent animal patient cases goes here</p>
              </div>

              <div className="col-md-4 journalStyle">
                <h1>Test</h1>
                <p>Recent animal patient cases goes here</p>
              </div>

              <div className="col-md-4 journalStyle">
                <h1>Test</h1>
                <p>Recent animal patient cases goes here</p>
              </div>

              <div className="col-md-4 journalStyle">
                <h1>Test</h1>
                <p>Recent animal patient cases goes here</p>
              </div>

              <div className="col-md-4 journalStyle">
                <h1>Test</h1>
                <p>Recent animal patient cases goes here</p>
              </div>

              <div className="col-md-4 journalStyle">
                <h1>Test</h1>
                <p>Recent animal patient cases goes here</p>
              </div>

            </div>
          </div>
        </div>

      </div>
    );
  }
}

export default FindPatiantJournal;
