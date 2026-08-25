import React, { Component } from 'react'

class InputMessage extends Component {

    TextInput = React.createRef();
    UsrNameInput = React.createRef();

    async OnMessageSent() {
        let text = this.TextInput.current.value;
        let Nick = this.UsrNameInput.current.value;
        if (!text.trim()&&!Nick.trim()) {
            return;
        }
        await fetch(`http://localhost:58196/Messages/PostMessage?NickName=` + Nick + `&MessageText=` + text + `&ChatId=` + this.props.chatname, {
            method: 'POST',
            mode: 'no-cors',
            headers: {
                'Access-Control-Allow-Origin': '*',
                'Content-Type': 'application/json',
                'Accept': 'application/json',
                'Origin': 'http://localhost:3000'
            }
        });
    }

    render() {
        return (
            <div>
                <input placeholder="Message" type="text" ref={this.TextInput} ></input>
                <input placeholder="UserName" type="text" ref={this.UsrNameInput} ></input>
                <button onClick={() => this.OnMessageSent()}>Send</button>
            </div>
        )
    }
}

export default InputMessage
