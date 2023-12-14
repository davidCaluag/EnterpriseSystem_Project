//wait for everything to load first.
window.onload = function(){
    const username = localStorage.getItem("username");

    // fetch(`http://localhost:5109/api/playlist/setuser/`+username,{
    //     method: "GET"
    // }).then(response=>console.log(response.statusText));
    
    const usertag = document.getElementById("usertag");

    usertag.innerHTML = username + "'s playlist list";

    fetch(`http://localhost:5109/api/playlist/getplaylist/${username}`)
            .then(response => response.json())
            .then(data => populateSelectPlaylist(data));

    function populateSelectPlaylist(data) {
        data.forEach(playlist => {
            const newOption = document.createElement("option");
            const selectObject = document.getElementById("playlistSelect");
            const textPlaylist = document.getElementById("selectedPlaylist");
            newOption.innerHTML = playlist.PlaylistTitle;
            textPlaylist.innerHTML = "Selected Playlist: "+ playlist.PlaylistTitle;
            selectObject.appendChild(newOption);
        });
    }

    document.getElementById("AddPlaylist").onclick = function(){
        //get playlist name
        const playlistName = document.getElementById("NewPlaylistName").value;
        //get username
        const username = localStorage.getItem("username");

        if(playlistName === "")
            alert("Input field is empty!")

        fetch(`http://localhost:5109/api/playlist/newPlaylist/${playlistName}/${username}`,{method:"PUT"})
        .then(response => alert(response.value));
    }
}