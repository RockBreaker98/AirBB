// File: wwwroot/js/validateBuiltYear.js
$(document).ready(function () {
    $('form').on('submit', function () {
        const year = parseInt($('#BuiltYear').val());
        const currentYear = new Date().getFullYear();

        if (isNaN(year) || year >= currentYear || (currentYear - year) > 150) {
            alert('Built year must be in the past and not more than 150 years old.');
            return false; // block submission
        }

        return true;
    });
});
