"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/workhub").build();

connection.on("ReceiveMessage", function () {
    window.location.href = window.location.href;
});

connection.start()
    .catch(err => console.error(err.toSring()));