window.onload = function () {
    var selectObject = document.getElementById("SelectUser");
    var stat = document.getElementById("result");
    if (selectObject) {
        fetch("http://localhost:5109/api/user/alluser/")
            .then(response => response.json())
            .then(data => populateSelectUser(data));
    

        document.getElementById("AddUser").onclick = function () {
            const username = document.getElementById("username").value;
            const password = document.getElementById("password").value;

            if (username === "" || password === "") {
                alert("Input field(s) is/are empty.");
            }

            fetch(`http://localhost:5109/api/user/username/${username}/password/${password}`, {
                method: "PUT"
            })
                .then(response => response.json())
                .then(response => updateStatus("Status: " + response.statusText));
        };

        document.getElementById("Random").onclick = function () {
            
            
            fetch("http://localhost:5109/api/user/random", {
                method: "GET"
            })  
                .then(response => response.json())
                .then(data => localStorage.setItem("username", data[0].username)+console.log(data[0].username))
                .then(window.location.href = "http://127.0.0.1:5500/Project/View/html/playlist.html");
        };

        document.getElementById("AccessUser").onclick = function () {
            const username = document.getElementById("SelectUser").value;
            const password = document.getElementById("userPassword").value;

            if (username === "" || password === "") {
                alert("Input field(s) is/are empty.");
                return;
            }

            fetch(`http://localhost:5109/api/user/access/${username}/${password}`, {
                method: "GET"
            })
                //.then(response => response.json())
                .then(response =>{ 
                    updateStatus("Status: " + response.statusText);
                    if(response.statusText == "OK"){
                    localStorage.setItem("username", username);
                    window.location.href = "http://127.0.0.1:5500/Project/View/html/playlist.html";
                    }
                });
        };

        document.getElementById("DeleteUser").onclick = function () {
            const username = document.getElementById("SelectUser").value;
            const password = document.getElementById("userPassword").value;

            if (username === "" || password === "") {
                alert("Input field(s) is/are empty.");
                }

            fetch(`http://localhost:5109/api/user/deleteuser/${username}/${password}`, {
                method: "DELETE"
            })
                //.then(response => response.json())
                .then(response => updateStatus("Status: " + response.statusText))

        };

        function populateSelectUser(data) {
            data.forEach(user => {
                const newOption = document.createElement("option");
                newOption.innerHTML = user.username;
                selectObject.appendChild(newOption);
            });
        }

        function updateStatus(message) {
            stat.innerHTML = message;
            if(message !== "Status: OK");
            }
    
    }
};
