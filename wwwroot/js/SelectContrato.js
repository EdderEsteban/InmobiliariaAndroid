console.log("Ingreso a SelectContrato.js");

// Tom Select
document.addEventListener("DOMContentLoaded", function() {
    new TomSelect("#listInquilino", {
        create: false,
        sortField: {
            field: "text",
            direction: "asc"
        }
    });

    new TomSelect("#listInmueble", {
        create: false,
        sortField: {
            field: "text",
            direction: "asc"
        }
    });

});