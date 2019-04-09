import React from "react";

class HomePage extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      margin: "10px",
      marginInt: 10
    };

    this.SearchSource = this.SearchSource.bind(this);
  }

  SearchSource() {
    const isTrue = confirm(
      `Margin is set to : ${this.state.margin}, Increas by 10px?`
    );
    if (isTrue == true) {
      this.setState(prevState =>
        Object.assign({}, { margin: `${prevState.marginInt + 10}px` })
      );
      this.setState(prevState =>
        Object.assign({}, { marginInt: prevState.marginInt + 10 })
      );
    }
  }

  render() {
    return (
      <div className="container">
        <div className="row" style={{ margin: this.state.margin }}>
          <div className="col-md-8 offset-md-2">
            {/* SerachBar */}
            <div className="input-group">
              <input
                type="text"
                className="form-control"
                placeholder="Sök efter patient journaler..."
              />

              <span className="input-group-btn">
                <button
                  onClick={() => this.SearchSource()}
                  className="btn btn-success"
                  type="button"
                >
                  Sök..
                </button>
              </span>
            </div>
            {/* SerachBar */}

            <div className="row" style={this.state.rowStyle}>
              {/* Cards of patients goes here */}
            </div>
          </div>
        </div>
      </div>
    );
  }
}
export default HomePage;
