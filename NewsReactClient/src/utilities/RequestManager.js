export function fetchFromServer(url, method, action) {
    fetch(url, {
        method: method
    })
        .then(response => response.json())
        .then(responseData => {
            console.log(responseData);
            action(responseData);
        })
        .catch((error) => {
            console.log(error);
        });
}
