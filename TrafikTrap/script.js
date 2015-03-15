if ('serviceWorker' in navigator) {
    navigator.serviceWorker.register('/sw.js').catch(function (why) {
        console.log(why);
    });
}

var T = true,
	i = 0,
	level = null,
	lastPos = null;

function ToggleSound() {
    var sound = document.getElementById("sound");

    if (sound.src === "http://localhost:9000/svg/volume-up.svg") sound.src = "/svg/volume-mute.svg";
    else sound.src = "/svg/volume-up.svg";
}

function GetLocation() {
    navigator.geolocation.getCurrentPosition(showPosition);
}
function showPosition(position) {
    var gmap = document.querySelector("google-map"),
		selfMarker = document.getElementById("self");

    gmap.setAttribute("latitude", position.coords.latitude);
    gmap.setAttribute("longitude", position.coords.longitude);

    selfMarker.setAttribute("latitude", position.coords.latitude);
    selfMarker.setAttribute("longitude", position.coords.longitude);

    setLevel();
}
function setLevel() {
    var copMarkers = document.querySelectorAll("google-map-marker.cop"),
		selfMarker = document.getElementById("self"),
		selfLatitude = selfMarker.getAttribute("latitude"),
		selfLongitude = selfMarker.getAttribute("longitude"),
		warning = document.getElementById("warning"),
		add = document.getElementById("add"),
		done = document.getElementById("done");

    for (var i = 0; i < copMarkers.length; i++) {
        var directionsService = new google.maps.DirectionsService(),
			copLatitude = copMarkers[i].getAttribute("latitude"),
			copLongitude = copMarkers[i].getAttribute("longitude"),
			request = {
			    origin: selfLatitude + "," + selfLongitude,
			    destination: copLatitude + "," + copLongitude,
			    travelMode: google.maps.TravelMode.DRIVING
			};

        if (request.origin !== lastPos) {
            directionsService.route(request, function (response, status) {
                console.log(status);
                if (status === "OK") {
                    var distance = response.routes[0].legs[0].distance.value;

                    if (distance < 500) level = 2;
                    else if (distance < 2000 && level <= 1) level = 1;
                    else if (distance > 2000 && level <= 0) level = 0;

                    if (copMarkers[(copMarkers.length - 1)].getAttribute("latitude") + "," + copMarkers[(copMarkers.length - 1)].getAttribute("longitude") === request.destination) setAlert();

                }
                else {

                }
            });
        };
        lastPos = request.origin;
    };
}
function setAlert() {
    var statusBar = document.querySelector("meta[name='theme-color']");

    if (level === 2) {
        if (add.getAttribute("class") !== "hide") {
            window.setTimeout(function () {
                warning.setAttribute("class", "level2");
                statusBar.setAttribute("content", "#F44336");
                window.setTimeout(function () {
                    add.setAttribute("class", "hide");
                    window.setTimeout(function () {
                        done.removeAttribute("class");
                    }, 300);
                }, 300);
            }, 300);
        };
    }
    else if (level === 1) {
        warning.setAttribute("class", "level1");
        add.removeAttribute("class");
        done.setAttribute("class", "hide");
        statusBar.setAttribute("content", "#F44336");
    }
    else if (level === 0) {
        warning.setAttribute("class", "hide");
        add.removeAttribute("class");
        done.setAttribute("class", "hide");
        statusBar.setAttribute("content", "#616161");
    }
    else {
        console.log(":(");
    }
    GetLocation();
}

function Status(icon) {
    var status = document.createElement("div"),
	queue = document.getElementById("queue");

    status.setAttribute("data-icon", icon);

    if (level !== 2) {
        queue.appendChild(status);
    };

}

document.getElementById("sound").addEventListener("click", function () { ToggleSound() });

if (!navigator.geolocation) Status("/svg/gps-off.svg");
if (!navigator.onLine) Status("/svg/signal-wifi-off.svg");

GetLocation();

var UpdateCops = function () {
    setTimeout(function () {

        var xhr = new XMLHttpRequest(),
		    Cops = [],
		    Cop = document.createElement("google-map-marker"),
		    googleMap = document.querySelector("google-map");

        console.log(":)");

        navigator.geolocation.getCurrentPosition(function (pos) {

            xhr.onload = function() {
                console.log(this);
                Cops = JSON.parse(this.response);

                googleMap.innerHTML = "";

                Cop.setAttribute("latitude", pos.coords.latitude);
                Cop.setAttribute("longitude", pos.coords.longitude);
                Cop.setAttribute("id", "self");
                Cop.setAttribute("icon", "icon='/svg/room.svg'");
                googleMap.appendChild(Cop);

                Cop.removeAttribute("id");
                Cop.setAttribute("class", "cop");
                Cop.setAttribute("icon", "icon='/svg/red-room.svg'");

                for (var i = 0; i < Cops.length; i++) {
                    Cop.setAttribute("latitude", Cops[i].Latitude);
                    Cop.setAttribute("longitude", Cops[i].Longitude);
                    googleMap.appendChild(Cop);
                };
            };
            xhr.open("GET", "https://traficservice.nanoplex.dk/?lat=" + pos.coords.latitude + "&lng=" + pos.coords.longitude, true);
            xhr.send();

        });

        UpdateCops();
    }, 60000);
};

UpdateCops();

var t = setInterval(function () {

    var queue = document.querySelectorAll("#queue div"),
		status = document.getElementById("status");

    if (queue[0] !== undefined) {

        status.querySelector("img").src = queue[0].getAttribute("data-icon");

        status.setAttribute("class", "show");

        window.setTimeout(function () {
            status.setAttribute("class", "hide");
            document.getElementById("queue").removeChild(queue[0]);
        }, 3000);
    };

}, 6000);

t;

