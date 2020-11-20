function sendNotification(title, options) {
    // Проверим, поддерживает ли браузер HTML5 Notifications
    if (!("Notification" in window)) {
        alert('Ваш браузер не поддерживает HTML Notifications, его необходимо обновить.');
    }

    // Проверим, есть ли права на отправку уведомлений
    else if (window.Notification.permission === "granted") {
        // Если права есть, отправим уведомление
        var notification = new window.Notification(title, options);

        function clickFunc() { alert('Пользователь кликнул на уведомление'); }

        notification.onclick = clickFunc;
    }

    // Если прав нет, пытаемся их получить
    else if (window.Notification.permission !== 'denied') {
        window.Notification.requestPermission(function (permission) {
            // Если права успешно получены, отправляем уведомление
            if (permission === "granted") {
                var notification = new window.Notification(title, options);

            } else {
                alert('Вы запретили показывать уведомления'); // Юзер отклонил наш запрос на показ уведомлений
            }
        }
        );
    }
}

const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/notification")
    .build();



hubConnection.on('Send', function (message, userName) {
    // получение сообщения от сервера
    alert("hui");
    sendNotification('Верните Линуса!', {
        body: 'Тестирование HTML5 Notifications',
        icon: 'icon.jpg',
        dir: 'auto'
    });
});

hubConnection.start();




function openConnection() {
    hubConnection.invoke("Send", "123");
}