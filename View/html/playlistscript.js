//wait for everything to load first.
window.onload = function(){
    const username = localStorage.getItem("username");

    // fetch(`http://localhost:5109/api/playlist/setuser/`+username,{
    //     method: "GET"
    // }).then(response=>console.log(response.statusText));
    
    const usertag = document.getElementById("usertag");
    /*
    public Guid Id { get; set; } = Guid.NewGuid();
    public User? user {get; set;} = default;
    public string Title { get; set; } = "";
    public int SongCount {get; set; } = 0;
    public List<Song> ListOfSongs {get; set; } = new();
    public Genre PlaylistGenre {get; set; }

    <p id ="playlistId">Id</p>
    <p id ="playlistSongCount">Song count</p>
    <p id ="playlistGenre">Genre</p>
    */
    usertag.innerHTML = username + "'s playlist list";
    var selectObject = document.getElementById("playlistSelect");
    const selectIndex = document.getElementById("playlistSelect").selectedIndex;
    var playlistTable = document.getElementById("playlistTable");

    fetch(`http://localhost:5109/api/playlist/getplaylist/${username}`)
            .then(response => response.json())
            .then(data => populateSelectPlaylist(data));

    document.getElementById("AddPlaylist").onclick = function(){
        //get playlist name
        const playlistName = document.getElementById("NewPlaylistName").value;
        //get username
        const username = localStorage.getItem("username");
        const playlistGenre = document.getElementById("NewPlaylistGenre").value;

        if(playlistName === "")
            alert("Input field is empty!")
        console.log(playlistGenre);
        fetch(`http://localhost:5109/api/playlist/newPlaylist/${playlistName}/${username}/${playlistGenre}`,{method:"PUT"});
        //.then(response => alert(response.value));
    }

    document.getElementById("DeletePlaylist").onclick = function(){
        //get playlist name
        const playlistName = document.getElementById("playlistSelect").value;
        //get username
        const username = localStorage.getItem("username");

        fetch(`http://localhost:5109/api/playlist/deleteplaylist/${playlistName}/${username}/`,{method:"PUT"});
        //.then(response => alert(response));
    }

    function populateSelectPlaylist(data) {
        var newNumber = 0;
        data.forEach(playlist => {
            
            const newOption = document.createElement("option");
            var newRow = playlistTable.insertRow();
            newOption.innerHTML = playlist.title;

            newNumber++;
            var playlistTitleCell = newRow.insertCell(0);
            playlistTitleCell.innerHTML = "Title :"+playlist.title;


            var playlistIdCell = newRow.insertCell(1);
            playlistIdCell.innerHTML = "Id :"+ playlist.id;

            var playlistSongCountCell = newRow.insertCell(2);
            playlistSongCountCell.innerHTML = "Song Count :"+ playlist.songCount;

            var playlistGenreCell = newRow.insertCell(3);
            playlistGenreCell.innerHTML = "Genre :"+ playlist.playlistGenre.genreName;
            
            //playlistId.innerHTML = "Id :"+playlist.id;
            //playlistSongCount.innerHTML = "Song count :"+ playlist.songCount;
           //playlistGenre.innerHTML = "Playlist Genre :"+playlist.playlistGenre.genreName;
            selectObject.appendChild(newOption);
        });
    }
    
    function updateSelect(data){
        data.forEach(playlist=>(iterateItem))
    }
    function iterateItem(item, index, playlist) {
        if(index == selectIndex){
        //playlistId.innerHTML = "Id : "+ playlist[index].id;
        //playlistSongCount.innerHTML = "Song count :"+ playlist[index].songCount;
        //playlistGenre.innerHTML = "Playlist Genre : "+playlist[index].playlistGenre.genreName;
        }
      }
}