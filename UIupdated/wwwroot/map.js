let map, marker;

function initializeLocationsMap(locations) {
    console.log('initializeLocationsMap called with locations:', locations);

    const mapElement = document.getElementById('map-multiple');
    if (!mapElement) {
        console.error('Map element not found');
        return;
    }

    // Видаляємо попередню карту, якщо вона існує
    if (map) {
        map.remove();
    }

    // Ініціалізуємо карту
    map = L.map('map-multiple').setView([50.327925, 26.5119475], 6);

    // Додаємо шар карти
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 18,
        attribution: '&copy; OpenStreetMap contributors'
    }).addTo(map);

    // Додаємо маркери для кожної локації
    locations.forEach(location => {
        if (location.latitude && location.longitude) {
            const locMarker = L.marker([location.latitude, location.longitude]).addTo(map);
            locMarker.bindPopup(`<b>${location.name}</b><br>${location.description}`);
        } else {
            console.warn('Invalid location data:', location);
        }
    });
}

function initializeMap(lat, lng, name) {
    console.log(`initializeMap called with lat: ${lat}, lng: ${lng}, name: ${name}`);

    const mapElement = document.getElementById('map-single');
    if (!mapElement) {
        console.error('Map element not found');
        return;
    }

    // Видаляємо попередню карту, якщо вона існує
    if (map) {
        map.remove();
    }

    // Ініціалізуємо карту
    map = L.map('map-single').setView([lat, lng], 13);

    // Додаємо шар карти
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 18,
        attribution: '&copy; OpenStreetMap contributors'
    }).addTo(map);

    // Додаємо маркер
    marker = L.marker([lat, lng]).addTo(map).bindPopup(name).openPopup();
}

function onMapClick(dotNetHelper) {
    if (!dotNetHelper) {
        console.error('dotNetHelper is not defined');
        return;
    }

    if (!map) {
        console.error('Map is not initialized');
        return;
    }

    map.off('click');

    map.on('click', function (e) {
        const { lat, lng } = e.latlng;

        if (!marker) {
            marker = L.marker([lat, lng]).addTo(map);
        } else {
            marker.setLatLng([lat, lng]);
        }

        dotNetHelper.invokeMethodAsync('UpdateLocation', lat, lng)
            .catch(err => console.error('Error invoking UpdateLocation:', err));
    });
}

// Експортуємо функції для використання в Blazor
window.initializeLocationsMap = initializeLocationsMap;
window.initializeMap = initializeMap;
window.onMapClick = onMapClick;
