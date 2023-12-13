//wait for everything to load first.
window.onload = function(){
    const username = localStorage.getItem("username");
    fetch(`http://localhost:5109/api/playlist/setuser/`+username,{
        method: "GET"
    }).then(response=>console.log(response.statusText));
    
    const usertag = document.getElementById("usertag");

    usertag.innerHTML = username + "'s playlist list";

    fetch("http://localhost:5109/api/playlist/")
            .then(response => response.json())
            .then(data => populateSelectPlaylist(data));

    function populateSelectPlaylist(data) {
        data.forEach(playlist => {
            const newOption = document.createElement("option");
            newOption.innerHTML = playlist.Title;
            selectObject.appendChild(newOption);
        });
    }

    document.getElementById("AddPlaylist").onclick() = function(){
        
        fetch("",{method:"PUT"})
    }
}