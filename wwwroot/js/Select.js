console.log('Ingreso a Select.js');

// Tom Select
document.addEventListener("DOMContentLoaded", function() {
    new TomSelect("#listPropietario", {
        create: false,
        sortField: {
            field: "text",
            direction: "asc"
        }
    });

    new TomSelect("#listTipo", {
        create: false,
        sortField: {
            field: "text",
            direction: "asc"
        }
    });

    
});


