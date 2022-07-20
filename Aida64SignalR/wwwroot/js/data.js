﻿"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/dataHub").build();

connection.on("ReceiveData", function (data) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    const myJSON = JSON.stringify(data);
    li.textContent = `${myJSON}`;
});

connection.start().then(function () {
    document.getElementById("sendDataButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendDataButton").addEventListener("click", function (event) {
    connection.invoke("SendData").catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});