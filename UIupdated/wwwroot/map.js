let map, marker;

function initializeMap(lat, lng, name) {
    map = L.map('map').setView([lat, lng], 13);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 18,
        attribution: '&copy; OpenStreetMap contributors'
    }).addTo(map);

    marker = L.marker([lat, lng]).addTo(map).bindPopup(name).openPopup();
}

function onMapClick(dotNetHelper) {
    map.on('click', function (e) {
        const { lat, lng } = e.latlng;
        marker.setLatLng([lat, lng]);
        dotNetHelper.invokeMethodAsync('UpdateLocation', lat, lng);
    });
}

// Expose functions to the global scope
window.initializeMap = initializeMap;
window.onMapClick = onMapClick;
