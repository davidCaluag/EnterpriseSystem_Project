

fetch("http://localhost:5109/api/user/alluser/")
.then((response)=>{
    JSON.stringify(response);
    //checkIfItWorks(response);
    return response.json();
}).then(data=>{
    console.log(data);
    checkIfItWorks(data);
    populateSelectUser(data);
})

document.getElementById("Button").onclick = function(){
    //yerrrrrrrrr
}


function populateSelectUser(data){
    data.forEach(createForeach);
}
function createForeach(data,index,arr){
    var newOption = document.createElement("option");
    newOption.innerHTML = arr[index].username;
    const selectObject = document.getElementById("SelectUser");
    selectObject.appendChild(newOption);
}
function checkIfItWorks(data){
    const test = document.getElementById("TestPurposesOnly");
    test.innerHTML = data[0].username;
}
