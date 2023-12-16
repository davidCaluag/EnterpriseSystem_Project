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
            
            //if values are empty
            if (username === "" || password === "") {
                alert("Input field(s) is/are empty.");
            }
            //fetch
            fetch(`http://localhost:5109/api/user/username/${username}/password/${password}`, {
                method: "PUT"
            })
                .then(response => response.json())
                .then(response => updateStatus("Status: " + response.statusText));
        };
        //this doesnt work
        document.getElementById("Random").onclick = function () {
            
            
            fetch("http://localhost:5109/api/user/random", {
                method: "GET"
            })  
                .then(response => response.json())
                .then(data => localStorage.setItem("username", data[0].username)+console.log(data[0].username))
                .then(window.location.href = "http://127.0.0.1:5500/Project/View/html/playlist.html");
        };
        //this works.
        document.getElementById("AccessUser").onclick = function () {
            //get values
            const username = document.getElementById("SelectUser").value;
            const password = document.getElementById("userPassword").value;
            //check value
            if (username === "" || password === "") {
                alert("Input field(s) is/are empty.");
                return;
            }
            //fetch
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
        //delete user
        document.getElementById("DeleteUser").onclick = function () {

            //set values

            const username = document.getElementById("SelectUser").value;
            const password = document.getElementById("userPassword").value;
            //check values

            if (username === "" || password === "") {
                alert("Input field(s) is/are empty.");
                }
            
            //fetch
            fetch(`http://localhost:5109/api/user/deleteuser/${username}/${password}`, {
                method: "DELETE"
            })
                //.then(response => response.json())
                .then(response => updateStatus("Status: " + response.statusText))
                //return a response

        };
        
        //function to populate the table
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
