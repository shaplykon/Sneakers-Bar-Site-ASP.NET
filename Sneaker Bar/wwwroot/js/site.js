function showNotification(title, options) {
    if (!("Notification" in window)) {
        alert('Ваш браузер не поддерживает HTML Notifications, его необходимо обновить.');
    }
    else if (window.Notification.permission === "granted") {
        var notification = new window.Notification(title, options);
        function clickFunc() {
            window.open("/");
        }
        notification.onclick = clickFunc;
    }

    else if (window.Notification.permission !== 'denied') {
        window.Notification.requestPermission(function (permission) {
            if (permission === "granted") {
                var notification = new window.Notification(title, options);
            } else {
                alert('Вы запретили показывать уведомления');
            }
        }
        );
    }
}

const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/notification")
    .build();

hubConnection.start();

hubConnection.on('Send', function (message) {
    showNotification("Sneakers notification", {
        body: message,
        icon: src ="https://mir-s3-cdn-cf.behance.net/project_modules/1400/4e483087953183.5dc80d7a56dbc.png",
        dir: 'auto'
    });
});

//function sendNotification(company, model) {
//    hubConnection.invoke("Send", company, model);
//}




