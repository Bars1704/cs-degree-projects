import React from 'react'


function MessageBox({ text, nickname, time }) {
    const style = {
        div: {
            margin: 'auto',
            minHeight: '4vh',
            width: '80%',
            backgroundColor: '#252525',
            borderRadius: '5px',
            marginBottom: '1vh',
            border: "1px solid black",
            borderLeft: "2px solid black",
        },
        nameBlock: {
            display: "flex",
        },
        name: {
            width: "100%",
            color: "#4CAF50",
            wordWrap: 'break-word',
            backgroundColor: '#353535',
            margin: 0,
            padding: 2,
        },
        p: {
            padding: 5,
            color: "#FFFFFF",
            marginTop: 0,
            wordWrap: 'break-word',
        },
        img: {
            height: '2hv',
            verticalAlign: 'bottom',
            borderRadius: '5px',
        }
    }

    function FormatDateTime(x){
let date  = x.slice(0,10);
let time  = x.slice(11,19);
return date+"   "+time;
    }
    return (
        <div>
            <div style={style.div}>
                <div style={style.nameBlock}>
                    <img style={style.img} src={'../ava.png'}></img>
                    <div style={style.name}>
                        <p style={style.name}>{nickname + " at " + FormatDateTime(time)}</p>
                    </div>
                </div>
                <p style={style.p}>   {text}</p>
            </div>
        </div>

    )
}

export default MessageBox