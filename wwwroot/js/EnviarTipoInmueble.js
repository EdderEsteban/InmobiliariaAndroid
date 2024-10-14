console.log("Ingreso a EnviarTipoInmueble.js");

// Funcionalidad Boton Enviar
document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("btnGuardar").addEventListener("click", function (event) {
        event.preventDefault(); // Evita el comportamiento predeterminado del formulario
        enviarTipoInmueble();
    });
});

// Funcion para enviar
function enviarTipoInmueble() {
    console.log("Ingreso a enviarTipoInmueble");

    const tipo = document.getElementById("Tipo").value;
    console.log(tipo);

    if (!tipo) {
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'El campo Tipo es obligatorio.',
        });
        return;
    }

    fetch("/Inmuebles/GuardarTipo", {
        method: "POST",
        headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
        },
        body: JSON.stringify({ tipo }),
    })
        .then((response) => {
            if (!response.ok) {
                throw new Error('Error en la solicitud');
            }
            return response.json();
        })
        .then((data) => {
            console.log(data);
            Swal.fire({
                icon: 'success',
                title: 'Tipo guardado',
                text: 'El tipo de inmueble se ha guardado correctamente.',
                confirmButtonText: 'Aceptar'
            }).then(() => {
                // Redirige a la lista o limpia el formulario si es necesario
                window.location.href = '/Inmuebles/ListarTiposInmueble';
            });
        })
        .catch((error) => {
            console.error('Error:', error);
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Ocurrió un error al guardar el tipo de inmueble. Intenta nuevamente.',
            });
        });
}
