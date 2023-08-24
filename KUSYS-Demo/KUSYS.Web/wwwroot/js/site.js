// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



async function RequestGet(url) {
    var result = await fetch(url)
        .then(res => res.json())
        //.then(data => result = data)
        .catch(error => console.log(error));
    return result;
}

async function RequestPost(url, params) {
    var options = {
        method: "POST",
        body: JSON.stringify(user)
    };
    var result = await fetch(url, options)
        .then(res => res.json())
        //.then(data => result = data)
        .catch(error => console.log(error));
    return result;
}

async function RequestPut(url, params) {
    var options = {
        method: "PUT",
        body: JSON.stringify(user)
    };
    var result = await fetch(url, options)
        .then(res => res.json())
        //.then(data => result = data)
        .catch(error => console.log(error));
    return result;
}

async function RequestDelete(url) {
    var options = { method: "DELETE" };
    var result = await fetch(url, options)
        .then(res => res.json())
        //.then(data => result = data)
        .catch(error => console.log(error));
    return result;
}
