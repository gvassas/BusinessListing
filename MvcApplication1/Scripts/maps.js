var map;

function setMap(long, lat) {
    var mapProp = {
        center: new google.maps.LatLng(long, lat),
        zoom: 12,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    map = new google.maps.Map(document.getElementById("googleMap"), mapProp);
}
function initializeMap() {
    codeAddress();
    Checkbox();

}
function initialize() {
    latitude = document.getElementById("latitude").textContent;
    longitude = document.getElementById("longitude").textContent;
    var myCenter = new google.maps.LatLng(latitude, longitude);

    var mapProp = {
        center: myCenter,
        zoom: 15,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    var map = new google.maps.Map(document.getElementById("googleMap")
        , mapProp);
    var marker = new google.maps.Marker({
        map: map,
        position: myCenter
    });
    marker.setMap(map);
}


function codeAddress() {

    var geocoder = new google.maps.Geocoder();

    var street = document.getElementById('Street_Address').value;
    var city = document.getElementById('City').value;
    var province = document.getElementById('Province').value;
    var country = document.getElementById('Country').value;
    var zip = document.getElementById('Postal_Code').value;
    var address = street + " " + city + " " + province + " " + country + " " + zip;

    geocoder.geocode({ 'address': address }, function (results, status) {

        if (status == google.maps.GeocoderStatus.OK) {
             var strLat = results[0].geometry.location.lat();
             var strLng = results[0].geometry.location.lng();

            $("#latitude").val(strLat);
            $("#longitude").val(strLng);

            setMap(strLng, strLat);
            map.setCenter(results[0].geometry.location);
            var marker = new google.maps.Marker({
                map: map,
                position: results[0].geometry.location
            });

        } else {
            alert('Geocode was not successful for the following reason: ' + status);
        }
    });
}

function Checkbox() {
    var box = document.getElementById("verified");
    if (box != null) {
        document.removeChild(box);
    }

    var checkbox = document.createElement('input');
    checkbox.type = "checkbox";
    checkbox.name = "name";
    checkbox.value = "value";
    checkbox.id = "verified";

    var label = document.createElement('label')
    label.htmlFor = "id";
    label.appendChild(document.createTextNode('Map Verified'));
    document.getElementById("gmap").parentNode.appendChild(checkbox);
    document.getElementById("gmap").parentNode.appendChild(label);

    checkbox.onchange = function () {
        var create = document.getElementById("create");
        if (checkbox.checked == true) {
            
            create.disabled = false;

        }
        else {
            create.disabled = true;
        }
    }
}

function clearForm() {

    document.getElementById("Name").value = "";
    document.getElementById('Street_Address').value = "";
    document.getElementById('City').value = "";
    document.getElementById('Province').value = "" ;
    document.getElementById('Country').value = "";
    document.getElementById('Postal_Code').value = "";
    document.getElementById("Contact_fName").value = "";
    document.getElementById("Contact_lName").value = "";
    document.getElementById("Phone").value = "";
}
