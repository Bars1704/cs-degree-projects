import { render } from '@testing-library/react';
import React, { Component } from 'react'

class ChatManager extends Component {

    constructor(props) {
        super(props);
        this.Myprops = props;
        this.InputRef = React.createRef();
    }

    CreateChat = async () => {
        await fetch('http://localhost:58196/Messages/CreateChat?ChatId=' + this.InputRef.current.value, {
            method: 'POST',
            mode: 'no-cors',
            headers: {
                'Access-Control-Allow-Origin': '*',
                'Content-Type': 'application/json',
                'Accept': 'application/json',
                'Origin': 'http://localhost:3000'
            }
        });
        this.GetChat();
    };


    GetChat = () => {
        this.Myprops.OnChatChanged(this.InputRef.current.value);
    }

    render() {
        return (
            <div>
                <div>
                    <input  placeholder="Chat ID" ref={this.InputRef}></input>
                </div>
                <button onClick={()=>this.CreateChat()}>CreateChat</button>
                <button onClick={() => this.GetChat()}>EnterChat</button>
            </div>)
    }

}

export default ChatManager;