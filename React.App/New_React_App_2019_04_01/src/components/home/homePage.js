import React from "react";

const HomePage = () => {
  return (
    <div className="container content-wrapper">
      <div className="row">
        <div className="col-md-6 offset-md-6">
          {/* SerachBar */}
          <div className="input-group">
            <input
              type="text"
              className="form-control"
              placeholder="Sök efter patient journal..."
            />

            <span className="input-group-btn">
              <button className="btn btn-success" type="button">
                Sök..
              </button>
            </span>
          </div>
          {/* SerachBar */}
        </div>

        <div className="row">Placera patientkorten här</div>
      </div>
    </div>
  );
};
export default HomePage;
