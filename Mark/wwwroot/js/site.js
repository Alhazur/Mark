// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


//function LinkCreate(urlPath) {
//    console.log('LinkCreate Url: ' + urlPath);
//    $.post(urlPath,
//        {
//            name: $('#sName').val(),
//            address: $('#sAddress').val(),
//            city: $('#sCity').val(),
//            zip: $('#sZip').val(),
//            country: $('#sCountry').val(),
//            country: $('#sLongitude').val(),
//            country: $('#sLatitude').val()
//        },
//        function (res) {
//            $('#AllStores').append(res);
//        });
//}

//Get
//function LinkUpdate(urlPath, targetUpdate) {
//    console.log('LinkUpdate urlPath: ' + urlPath);
//    console.log('Id targetUpdate: ' + targetUpdate);

//    $.get(urlPath,
//        function (res) {
//            $('#' + targetUpdate).replaceWith(res);
//        });
//}

//Post
//function LinkEdit(urlPath, targetUpdate, personId) {
//    console.log('LinkEdit urlPath: ' + urlPath);

//    $.post(urlPath,
//        {
//            Id: personId,
//            Name: $('#' + targetUpdate + 'Name').val(),
//            Addres: $('#' + targetUpdate + 'Addres').val(),
//            City: $('#' + targetUpdate + 'City').val(),
//            Zip: $('#' + targetUpdate + 'Zip').val(),
//            Country: $('#' + targetUpdate + 'Country').val()
//        },
//        function (res) {
//            $('#' + targetUpdate).replaceWith(res);
//        });
//}

//function BtnClick() {
//    $.ajax({
//        url: "/Store/",
//        method: "GET",
//        success: function (data) {
//            console.log(data)
//        },
//        error: function(err){
//            console.error(err);
//        }
//    })
//}






//Call geocode automatic
//geocode();

var locationForm = document.getElementById('location-form');

//Listen location form manuell reogiruet na sobitie submit i vizivaet fucn geocode
locationForm.addEventListener('submit', geocode);



function geocode(e) {
    //posle submita func geocode ne ischeznet potomuchto preventDefault ne dast
    //e.preventDefault();
    //alert("Working Start");

    //form manuell input
    var location = document.getElementById('location-input').value;//ne zabyt znachenie input value


    //this for Call geocode automatic
    //var location = ' 34231 gröna gatan Alvesta';    



    //var location = '34231 gröna gatan Alvesta';



    axios.get('https://maps.googleapis.com/maps/api/geocode/json', {
        params: {
            address: location,
            key: 'Key1'
        }
    })
        .then(function (response) {
            //log full response
            console.log("log full response: " + response);

            //formatted address
            var formattedAddress = response.data.results[0].formatted_address;
            var formattedAddressOutput = `
                    <ul class="list-group">
                        <li class="list-group-item">${formattedAddress}</li>
                    </ul>`;

            //address component
            var addressComponents = response.data.results[0].address_components;
            var addressComponentOutput = '<ul class="list-group">';
            for (var i = 0; i < addressComponents.length; i++) {
                addressComponentOutput += `
                        <li class="list-group-item">
                        <strong>
                            ${addressComponents[i].types[0]}
                            </strong>:
                        ${addressComponents[i].long_name}
                        </li>`;
            }
            addressComponentOutput += '</ul>';

            //geometry
            var lat = response.data.results[0].geometry.location.lat;
            var lng = response.data.results[0].geometry.location.lng;
            //var geometryOutput = `
            //            <ul class="list-group">

            //                    <li class="list-group-item"><strong>Latitude</strong>:${lat}</li>
            //                    <li class="list-group-item"><strong>Longitude</strong>:${lng}</li>
            //            </ul>
            //            `;

            //tak mojno otp znachenie lat v input znachenie
            document.getElementById('latValue').value = lat;
            document.getElementById('lngValue').value = lng;
            var latOutput = `
                        <ul class="list-group">

                                <li class="list-group-item"><strong>Latitude</strong>:${lat}</li>
                        </ul>
                        `;
            var lngOutput = `
                        <ul class="list-group">
                                <li class="list-group-item"><strong>Longitude</strong>:${lng}</li>
                        </ul>
                        `;

            var myLatLng = { lat: lat, lng: lng };

            var map = new google.maps.Map(document.getElementById('map'), {
                zoom: 11,
                center: myLatLng
            });

            var marker = new google.maps.Marker({
                position: myLatLng,
                map: map
            });

            //output to app
            document.getElementById('formatted-address').innerHTML = formattedAddressOutput;
            document.getElementById('address-components').innerHTML = addressComponentOutput;
            //document.getElementById('geometry').innerHTML = geometryOutput;
            document.getElementById('lat').innerHTML = latOutput;
            document.getElementById('lng').innerHTML = lngOutput;

            alert("Working End");

        })
        .catch(function (error) {
            console.log("error:--" + error);
        });
}