import './App.css';
import React, { State, Component } from 'react'
import MessageBody from './MessageBody'
import ChatManager from './ChatManager'

class App extends Component {

  state = {
    chatName: "ffff"
  };


  ChangeChat = (chatId) => {
    this.setState({
      chatName: chatId,
    });
    console.log(this.state);
  }

  render() {
    return (
      <div>
        <MessageBody chatId={this.state.chatName}></MessageBody>
        <ChatManager chatname={this.state.chatName} OnChatChanged={this.ChangeChat}></ChatManager>
      </div>
    );
  }

}

export default App;
