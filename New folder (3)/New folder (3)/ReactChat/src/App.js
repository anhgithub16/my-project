import React from 'react';
import './App.css';

class ManHinhChat extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      text: ''
    };
  }
  componentDidMount() {
    window.socket.on('re_message', function (msg) {
      let list = document.getElementById("list-message");
      let item = document.createElement("LI");
      item.appendChild(document.createTextNode(msg));
      list.appendChild(item);
    });
    
    window.socket.on('user_online', function (msg) {
      let list = document.getElementById("list-message");
      let item = document.createElement("LI");
      item.appendChild(document.createTextNode(msg));
      list.appendChild(item);
    });
  }
  // componentWillUnmount(){
  //   window.socket.emit("ngat-ket-noi", window.socket.id + ' is disconnect');
  // }
  addData(data) {
    let list = document.getElementById("list-message");
    let item = document.createElement("LI");
    item.appendChild(document.createTextNode(data));
    list.appendChild(item);
  }

  sendData() {
    //emit:đẩy đi 
    window.socket.emit("message", this.state.text);
    //add message list messages
    this.addData("Toi: " + this.state.text);
    //clear input
    this.setState({text: ''});
  }
  render() {
    return (
      <div className="search-container">
        <div id="list-message"></div>
        <input className="ipchat" value={this.state.text} onChange={(e) => {
          this.setState({ text: e.target.value })
        }} onKeyPress={(e) => {
          if (e.key === 'Enter') {
            this.sendData()
          }
        }} type="text" placeholder="Nói đi ....."></input>
      </div>
    );
  }

}

function App() {
  return (
    <div className="App">
      <ManHinhChat />
    </div>
  );
}

export default App;
