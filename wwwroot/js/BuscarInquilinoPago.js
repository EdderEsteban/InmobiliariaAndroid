console.log("Ingreso a BuscarInquilinoPago.js");

// Funcionalidad Boton Buscar
document.addEventListener("DOMContentLoaded", function () {
  document.getElementById("btnBuscar").addEventListener("click", function () {
    buscarInquilinos();
  });
});

// Funcion para buscar
function buscarInquilinos() {
  console.log("Ingreso a buscarInquilinos");

  const Nombre = document.getElementById("Nombre").value.trim();
  const Apellido = document.getElementById("Apellido").value.trim();
  const Dni = document.getElementById("Dni").value.trim();
  const form = document.getElementById("formSearchPago");

  // Validar que al menos un campo esté llenado
  if (!Nombre && !Apellido && !Dni) {
    Swal.fire({
      title: "Sin datos",
      text: "Debe completar al menos uno de los campos",
      icon: "question",
    });
    return;
  }

  const JsonSearch = { Nombre, Apellido, Dni };

  fetch("/Pago/BuscarInqPago", {
    method: "POST",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
    body: JSON.stringify(JsonSearch),
  })
    .then((response) => {
      if (!response.ok) {
        throw new Error("Error en la respuesta del servidor");
      }
      return response.json();
    })
    .then((data) => {
      if (data.length === 0) {
        Swal.fire("No existen resultados a su búsqueda.");
        form.reset();
      } else {
        console.log("este es data", data);
        // Actualizar el contenido de la tabla con los datos recibidos
        const tableBody = document.querySelector("#tbodyInquilinosPago");
        tableBody.innerHTML = ""; // Limpiar el contenido actual

        data.forEach((inquilino) => {
          // Verifica si el inquilino tiene contratos
          if (
            inquilino.contratosEInmuebles &&
            inquilino.contratosEInmuebles.length > 0
          ) {
            // Iterar sobre cada contrato del inquilino
            inquilino.contratosEInmuebles.forEach((contrato) => {
              const row = document.createElement("tr");
              row.innerHTML = `
                      <td>${contrato.id_Contrato}</td>
                      <td>${inquilino.nombre} ${inquilino.apellido}</td>
                      <td>${inquilino.dni}</td>
                      <td>${
                        contrato.inmueble
                          ? contrato.inmueble.direccion
                          : "Sin inmueble"
                      }</td>
                      <td>${new Date(contrato.fecha_Inicio).toLocaleDateString(
                        "es-ES"
                      )}</td>
                      <td>${new Date(contrato.fecha_Fin).toLocaleDateString(
                        "es-ES"
                      )}</td>
                      <td>
                          <a href="/Pago/CrearPago?idInquilino=${inquilino.id_Inquilino}&idContrato=${contrato.id_Contrato}" 
                              title="Pago" class="material-symbols-outlined">paid</a>
                      </td>
                  `;
              tableBody.appendChild(row);
            });
          }
        });
        form.reset();
      }
    })
    .catch((error) => {
      console.error("Error:", error);
      Swal.fire("Ocurrió un error al buscar el contrato: " + error.message);
    });
}
