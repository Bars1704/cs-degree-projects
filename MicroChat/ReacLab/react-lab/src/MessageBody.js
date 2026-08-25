import React, { Component, useEffect } from 'react'
import MessageBox from './MessageBox'
import InputMessage from './InputMessage'

class MessageBody extends Component {
    style = {
        div: {
            overflow: 'auto',
            padding: 10,
            margin: 'auto',
            marginTop: 0,
            height: '74vh',
            width: '50%',
            backgroundColor: 'lightgray',
            borderRadius: 5,
            borderStyle: ' solid,black'
        },

        header: {
            padding: 3,
            backgroundColor: 'lightgray',
            color: 'black',
            margin: 'auto',
            marginBottom: 0,
            width: '30%',
            fontSize: 30,
            paddingLeft: 10,
            borderRadius: 5,
        }
    }

    constructor(props) {
        super(props);
        this.state = {
            Messages: []
        }
    }

    ChangeMessages(x) {
        this.setState({ Messages: x });
    }

    sleep = (ms) => {
        return new Promise(resolve => setTimeout(resolve, ms));
    }

    componentDidMount() {
        this.update();
    }

    update = async () => {
        console.log('id:' + this.props.chatId);
        let response = await fetch('http://localhost:58196/Messages/GetNewMessages?ChatId=' + this.props.chatId);
        let text = await response.json();
        console.log(text);
        this.ChangeMessages(text);
        await this.sleep(1000);
        this.update();
    }



    render() {
        return (
            <div>
                <p style={this.style.header}>{"Chat ID:" + this.props.chatId}</p>
                <div style={this.style.div} scroll="yes">
                    {
                        this.state.Messages.length != 0 ?
                            this.state.Messages.map(
                                (x) => {
                                    return <MessageBox nickname={x.senderUsrName} key={x.id} text={x.text} time={x.sendingTime}></MessageBox>
                                })
                            : <p>there is nothing here yet</p>
                    }
                </div>
                <InputMessage chatname={this.props.chatId}></InputMessage>
            </div >)
    }
}
export default MessageBody