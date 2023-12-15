window.onload = function(){
    const genre = localStorage.getItem("genre");


    fetch(`http://localhost:5109/api/browse/allGenres/${genre}`,{
        method: "GET"
    })
    .then(response=>response.json())
    .then(data=>localStorage.getItem("genre", data[0].genre) + console.log(data[0].genre))
    .then(window.location.href = "http://127.0.0.1:5500/Project/View/html/genrepage.html")
}