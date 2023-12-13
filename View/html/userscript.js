window.onload = function () {
    var selectObject = document.getElementById("SelectUser");

    if (selectObject) {
        fetch("http://localhost:5109/api/user/alluser/")
            .then(response => response.json())
            .then(data => populateSelectUser(data));
    

        document.getElementById("AddUser").onclick = function () {
            const username = document.getElementById("username").value;
            const password = document.getElementById("password").value;

            if (username === "" || password === "") {
                alert("Input field(s) is/are empty.");
                return;
            }

            fetch(`http://localhost:5109/api/user/username/${username}/password/${password}`, {
                method: "PUT"
            })
                .then(response => response.json())
                .then(response => updateStatus("Status: " + response.statusText));
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
                return;
            }

            fetch(`http://localhost:5109/api/user/${username}/${password}`, {
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
            const stat = document.getElementById("result");
            stat.innerHTML = message;
            if(message !== "Status: OK")
                alert(message);
        }
    
    }
};
