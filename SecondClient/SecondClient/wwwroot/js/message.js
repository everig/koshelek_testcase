"use strict";

let _connection;

function buildConnection() {
    _connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:7434/messagehub", { 
            skipNegotiation: false,
            transport: signalR.HttpTransportType.WebSockets| 
            signalR.HttpTransportType.LongPolling |
            signalR.HttpTransportType.serverSentEvents,
        })
        .build();
    _connection.on("Recieve", (No, DateLabel, Text) => {

        const tr = document.createElement("tr");

        const no = document.createElement("td");
        no.textContent = `${No}`;
        const date = document.createElement("td");
        date.textContent = new Date(DateLabel).toLocaleString();
        const text = document.createElement("td");
        text.textContent = `${Text}`;

        tr.appendChild(no);
        tr.appendChild(date);
        tr.appendChild(text);

        document.getElementById("messages").appendChild(tr);

    });
    _connection.start();
}

buildConnection();
