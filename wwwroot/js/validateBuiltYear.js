// Client-side validation for BuiltYearWithin150
(function ($) {

    // Validation method
    $.validator.addMethod("builtyearwithin150", function (value, element, params) {
        if (this.optional(element)) return true;    // Allow empty for optional fields

        var year = parseInt(value, 10);
        if (isNaN(year)) return false;

        var min = parseInt(params.minyear, 10);
        var max = parseInt(params.maxyear, 10);

        return year >= min && year <= max;
    });

    // Adapter to read data-val-* attributes
    $.validator.unobtrusive.adapters.add(
        "builtyearwithin150",
        ["minyear", "maxyear"],
        function (options) {
            options.rules["builtyearwithin150"] = {
                minyear: options.params.minyear,
                maxyear: options.params.maxyear
            };

            options.messages["builtyearwithin150"] = options.message;
        }
    );

})(jQuery);
