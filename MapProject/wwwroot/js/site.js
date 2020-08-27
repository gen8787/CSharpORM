// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function() {
    console.log("MapProject Ready")

// Create Map
    var mymap = L.map('mapid').setView([46.8427, -121.7382], 13);

// Add Custom Layer
    L.tileLayer('https://{s}.tile.opentopomap.org/{z}/{x}/{y}.png',
    {
    maxZoom: 17,
    attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, ' + '<a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, ' + 'Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
    })
    .addTo(mymap);

// Add Circle to Map
    var campMuir = L.circle([46.8355, -121.7326], {
        color: 'red',
        fillColor: 'red',
        fillOpacity: 1.0,
        radius: 5,
    }).addTo(mymap);

// Add Popup to Circle
    campMuir.bindPopup("Camp Muir - " + campMuir.latlng);

// Popup with Location on Mouse Click
    var popup = L.popup();

    function onMapClick(e) {
        popup
            .setLatLng(e.latlng)
            .setContent("You clicked the map at " + e.latlng.toString())
            .openOn(mymap);
    }
    mymap.on('click', onMapClick);

// 
    var newMarker = L.marker([46.8529, -121.7605], {
        title: 'Summit',
        draggable: true
    }).addTo(mymap);









































})