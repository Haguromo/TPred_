import React from 'react';
import { Button } from 'reactstrap'

interface IState {
  message: string;
}

class Hello extends React.Component<{}, IState> {
  componentWillMount = () => {
    this.setState({
      message: "Hello",
    });
  }

  render = () => {
    return (
      <div>{this.state.message}
        <Button onClick={this.buttonClick} color="info">
          asdsda
        </Button>
      </div>
    )
  }

  private buttonClick = () => {
    console.log('click');
  }
}

export default Hello;