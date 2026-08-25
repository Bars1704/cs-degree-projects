import React, { useEffect } from 'react'

function Updator(onChange, chatId) {

    const [FirstTime, ChangeFirstTime] = React.useState(true);
    useEffect(
        () => {
         //   update();
        }, [FirstTime]
    )

    async function GetChat() {

    }



    return () => { <p></p> }
}

export default Updator