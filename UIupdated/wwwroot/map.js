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
        if (location.latitute && location.longitute) {
            const locMarker = L.marker([location.latitute, location.longitute]).addTo(map);
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

window.initializeLocationsMapWithSingleCallback = function (locations, dotNetHelper) {
    const mapElement = document.getElementById('map-multiple');
    if (!mapElement) return;

    if (window._leafletMap) {
        window._leafletMap.remove();
    }
    window._leafletMap = L.map('map-multiple').setView([50.327925, 26.5119475], 6);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 18,
        attribution: '&copy; OpenStreetMap contributors'
    }).addTo(window._leafletMap);

    window._locationMarkers = [];

    locations.forEach(function (loc) {
        if (loc.latitute && loc.longitute) {
            const marker = L.marker([loc.latitute, loc.longitute]).addTo(window._leafletMap)
                .bindPopup(loc.name);
            marker.on('click', function () {
                if (dotNetHelper) {
                    dotNetHelper.invokeMethodAsync('ShowLocationModal', loc.id);
                }
            });
            window._locationMarkers.push({ id: loc.id, marker: marker, lat: loc.latitute, lng: loc.longitute });
        }
    });
};

window.focusLocationOnMap = function (lat, lng, zoom = 14) {
    if (window._leafletMap) {
        window._leafletMap.setView([lat, lng], zoom);
    }
};
window.initializeLocationSelectionMap = function (locations, dotNetRef) {
    const mapElement = document.getElementById('location-selection-map');
    if (!mapElement) {
        console.error('location-selection-map element not found');
        return;
    }

    // Видаляємо попередню карту, якщо вона існує
    if (window._locationSelectionMap) {
        window._locationSelectionMap.remove();
        window._locationSelectionMap = null;
    }

    // Створюємо нову карту
    window._locationSelectionMap = L.map('location-selection-map').setView([50.327925, 26.5119475], 6);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 18,
        attribution: '&copy; OpenStreetMap contributors'
    }).addTo(window._locationSelectionMap);

    // Додаємо маркери для всіх локацій
    locations.forEach(function (loc) {
        if (loc.latitute && loc.longitute) {
            const marker = L.marker([loc.latitute, loc.longitute]).addTo(window._locationSelectionMap)
                .bindPopup(loc.name);
            marker.on('click', function () {
                if (dotNetRef) {
                    dotNetRef.invokeMethodAsync('OnLocationSelectedFromMap', loc.id);
                }
            });
        }
    });
};



// Експортуємо функції для використання в Blazor
window.initializeLocationsMap = initializeLocationsMap;
window.initializeMap = initializeMap;
window.onMapClick = onMapClick;
