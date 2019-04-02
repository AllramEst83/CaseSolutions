import React from "react";
import { render } from "react-dom";

class TestOutput extends React.Component {
  constructor(props) {
    super(props);
  }

  render() {
    return <div>Starting Unicorn race</div>;
  }
}
export default TestOutput;

render(<TestOutput />, document.getElementById("app"));
