"use strict";

var signalR = new signalR.HubConnectionBuilder().withUrl("/workhub").build();
var connection = signalR;

connection.on("ReceiveMessage",
    function() {
        window.location.href = window.location.href;
    });

connection.start()
    .catch(err => console.error(err.toSring()));