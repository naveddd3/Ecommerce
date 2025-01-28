"use strict";

let valiadteInputs = () => {
    let isValid = false;
    $('input:required, select:required').removeClass("is-invalid");
    $('input:required, select:required').addClass("is-valid");
    $('input:required, textarea:required').addClass("is-valid");
    let inputs = $('input:required, select:required,textarea:required');
    let filteredInputs = inputs.filter(function () {
        let currentEle = $(this);
        let value = currentEle.val();
        if (this.tagName.toUpperCase() === 'INPUT' && this.type.toUpperCase() === 'NUMBER' && this.min && value) {
            return parseInt(value) < parseInt(this.min);
        }
        else {
            return value === "";
        }
    });

    if (filteredInputs.length > 0) {
        filteredInputs.each(function () {
            isValid = false;
            var input = $(this);
            if (input[0].required) {
                if (input[0].value != '') {
                    input.removeClass("is-invalid");
                    input.addClass("is-valid");
                }
                else {
                    input.addClass("is-invalid");
                }
            }
        });
    }
    else {
        isValid = true;
    }
    return isValid;
}

var userCoords = {};

async function getCoordinates() {
    try {
        const coords = await new Promise((resolve, reject) => {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(
                    (position) => {
                        resolve({
                            latitude: position.coords.latitude,
                            longitude: position.coords.longitude,
                        });
                    },
                    (error) => {
                        reject("Error occurred: " + error.message);
                    }
                );
            } else {
                reject("Geolocation is not supported by this browser.");
            }
        });
        userCoords = coords;
        sessionStorage.setItem('location', JSON.stringify(userCoords));
        return coords;
    } catch (error) {
        console.error("Error fetching coordinates:", error);
        return error;
    }
}
var userLocation = () => {
    $.post('/Website/UserAddress').done((result) => {
        $('#showModal').html(result);
        $('#userlocationModal').modal('show');
    }).fail((xhr) => {
        QAlert(-1, 'Something went wrong !');
    })
}

// Example usage:
//(async () => {
//    userCoords = await getCoordinates();
//})();



