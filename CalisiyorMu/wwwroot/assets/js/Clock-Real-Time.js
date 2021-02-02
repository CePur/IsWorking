//displaying a realtime clock
setInterval(showtime, 500);
setInterval(elapsedtime, 60000);

function showtime() {
    var time = new Date();
    var hrs = time.getHours();
    var mins = time.getMinutes();
    var secs = time.getSeconds();
    //var en = 'AM'
    //if (hrs>12){hrs = hrs - 12;en = 'PM'}
    //if (hrs == 0){hrs = 12;}
    if (hrs < 10) {
        hrs = "0" + hrs;
    }
    if (mins < 10) {
        mins = "0" + mins;
    }
    if (secs < 10) {
        secs = "0" + secs;
    }
    document.getElementById("time").innerHTML = hrs + ":" + mins + ":" + secs;
}


function elapsedtime() {
    var initialMin = parseInt(document.getElementById("elapsedMin").innerHTML) + 1;

    document.getElementById("elapsedMin").innerHTML = initialMin + "DK";
}

//setTimeout(reload, 60000);

//function reload() {
//    window.location.href = window.location.href;
//}