

window.onload = function(){
//This is automatic. Eager-loading

//Only fetch if this specific element, the user select, exists.
var selectObject = document.getElementById("SelectUser");
if(selectObject){
    fetch("http://localhost:5109/api/user/alluser/")
    .then((response)=>{
        JSON.stringify(response);
        //checkIfItWorks(response);
        return response.json();
    }).then(data=>{
        console.log(data);
        //checkIfItWorks(data);
        populateSelectUser(data);
    })
}


//Lazy-loading. When adding a user and all that
document.getElementById("AddUser").onclick = function(){
    const username = document.getElementById("username").value;
    const password = document.getElementById("password").value;

    if(username == null || password == null)
        alert("input field is/are empty.")

    fetch("http://localhost:5109/api/user/username/"+username+"/password/"+password,{
        method:"PUT"
    })
    .then((response)=> {
        JSON.stringify(response);
        const stat = document.getElementById("result");
        console.log("username",username.value, "password",password.value, "response", response.statusText);
        stat.innerHTML = ("Status: "+ response.statusText);
    })
    //yerrrrrrrrr
}


//TO DO

//Access the user's playlist
document.getElementById("AccessUser").onclick = function(){
    const username = document.getElementById("SelectUser");
    const password = document.getElementById("userPassword");

    console.log("http://localhost:5109/api/user/access/"+username.value+"/"+password.value);
    if(username.value === "" || password.value === "")
        alert("input field is/are empty.")

    fetch("http://localhost:5109/api/user/access/"+username.value+"/"+password.value,{
        method: "GET"
    })
    .then((response)=> {
        JSON.stringify(response);
        const stat = document.getElementById("result");
        console.log("username",username.value, "password",password.value, "response", response.statusText);
        stat.innerHTML = ("Status: "+ response.statusText);
    })
    //yerrrrrrrrr
}
//Delete user
document.getElementById("DeleteUser").onclick = function(){
    const username = document.getElementById("SelectUser");
    const password = document.getElementById("userPassword");

    if(username == null || password == null)
        alert("input field is/are empty.")

    fetch("http://localhost:5109/api/user/"+username.value+"/"+password.value,{
        method: "DELETE"
    })
    .then((response)=> {
        JSON.stringify(response);
        const stat = document.getElementById("result");
        console.log("username",username.value, "password",password.value, "response", response.statusText);
        stat.innerHTML = ("Status: "+ response.statusText);
    })
}
//Add user

function populateSelectUser(data){
    data.forEach(createForeach);
}
function createForeach(data,index,arr){
    const newOption = document.createElement("option");
    newOption.innerHTML = arr[index].username;
    //const selectObject = document.getElementById("SelectUser");
    selectObject.appendChild(newOption);
}
function checkIfItWorks(data){
    const test = document.getElementById("TestPurposesOnly");
    test.innerHTML = data[0].username;
}

}
