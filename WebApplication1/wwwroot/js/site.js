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


let userCoords = {};
async function getLocation() {
    return new Promise((resolve, reject) => {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(
                function (position) {
                    let latitude = position.coords.latitude;
                    let longitude = position.coords.longitude;
                    resolve({ latitude, longitude }); // Resolving the promise with the object
                },
                function (error) {
                    reject("Error occurred: " + error.message); // Rejecting the promise if there's an error
                }
            );
        } else {
            reject("Geolocation is not supported by this browser."); // Reject if geolocation isn't supported
        }
    });
}

async function getUserCoordinates() {
    try {
        userCoords = await getLocation();
        console.log(userCoords.latitude, userCoords.longitude);
    } catch (error) {
        console.log(error);
    }
}



