// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const connection = new signalR.HubConnectionBuilder()
    .withUrl(document.getElementById("signalRUrl").value)
    .configureLogging(signalR.LogLevel.Information)
    .build();

connection.on("TagValueUpdated", (tagId, value) => {
    const element = document.querySelector(`.tag-value[data-tag-id="${tagId}"]`);
    if (element) {
        element.textContent = value;
        element.classList.add("value-updated");
        setTimeout(() => element.classList.remove("value-updated"), 1000);
    }
});

async function startConnection() {
    try {
        await connection.start();
        console.log("SignalR Connected");

        // Subscribe to tags after connection
        document.querySelectorAll("[data-tag-id]").forEach(el => {
            const tagId = el.getAttribute("data-tag-id");
            connection.invoke("SubscribeToTag", parseInt(tagId));
        });
    } catch (err) {
        console.log(err);
        setTimeout(startConnection, 5000);
    }
}

//Test SignalR connection - Add this to site.js temporarily:
console.log("SignalR loaded:", typeof signalR !== 'undefined');
// end Test

startConnection();
