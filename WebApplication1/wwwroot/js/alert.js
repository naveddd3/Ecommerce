//function QAlert(statusCode, message) {
//    var type;

//    switch (statusCode) {
//        case 1:
//            type = 'success';
//            break;
//        case -1:
//            type = 'danger';
//            break;
//        case 2:
//            type = 'info';
//            break;
//        default:
//            type = 'primary';
//            break;
//    }

//    var alertDiv = $('<div class="alert alert-' + type + ' text-bg-' + type + ' border-0 fade show" role="alert">' +
//        '<button type="button" class="btn-close btn-close-white" data-bs-dismiss="alert" aria-label="Close"></button>' +
//        message +
//        '</div>');

//    $('#alert-container').append(alertDiv);

//    // Animate the alertDiv
//    alertDiv.css('opacity', 0).animate({ opacity: 1 }, 500);

//    setTimeout(function () {
//        alertDiv.fadeOut('slow', function () {
//            $(this).remove();
//        });
//    }, 2000);
//}


function QAlert(statusCode, message) {
    var type;
    var icon;

    switch (statusCode) {
        case 1:
            type = 'success';
            icon = '<i class="ri-check-line me-1 align-middle font-16"></i>';
            break;
        case -1:
            type = 'danger';
            icon = '<i class="ri-close-circle-line me-1 align-middle font-16"></i>';
            break;
        case 2:
            type = 'info';
            icon = '<i class="ri-alert-line me-1 align-middle font-16"></i>';
            break;
        default:
            type = 'primary';
            icon = '<i class="ri-information-line me-1 align-middle font-16"></i>';
            break;
    }


    var alertDiv = $('<div class="alert alert-' + type + ' text-bg-' + type + ' border-0 fade show" role="alert">' +
        icon +
        message + ' <button type="button" class="btn-close btn-close-white" data-bs-dismiss="alert" aria-label="Close"></button>' +
        '</div>');

    $('#alert-container').append(alertDiv);
    alertDiv.hide().slideDown('slow');
    setTimeout(function () {
        alertDiv.slideUp('slow', function () {
            $(this).remove();
        });
    }, 2500);
}
