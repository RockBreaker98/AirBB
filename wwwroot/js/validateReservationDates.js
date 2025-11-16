document.addEventListener("DOMContentLoaded", function () {
    const start = document.getElementById("StartDate");
    const end = document.getElementById("EndDate");

    function validate() {
        if (!start.value || !end.value) return;

        const startDate = new Date(start.value);
        const endDate = new Date(end.value);

        if (endDate <= startDate) {
            end.setCustomValidity("End date must be at least 1 day after start date.");
        } else {
            end.setCustomValidity("");
        }
    }

    start.addEventListener("change", validate);
    end.addEventListener("change", validate);
});
