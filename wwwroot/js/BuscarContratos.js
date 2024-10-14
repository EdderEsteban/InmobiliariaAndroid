console.log("Ingreso a BuscarContratos.js");

// -----------------------------------------------------------------Funcion para los botones ------------------------------------------------------------------

document.addEventListener("DOMContentLoaded", function () {
  const btnVigentes = document.getElementById("btnVigentes");
  const btnTerminados = document.getElementById("btnTerminados");

  // Función para hacer la solicitud y actualizar la tabla
  function cargarContratos(url) {
    // Destruir la instancia anterior de DataTable si existe
    if ($.fn.DataTable.isDataTable("#tblContratos")) {
      $('#tblContratos').DataTable().clear().destroy();
    }

    // Llamar al servidor para obtener los datos
    fetch(url, {
      method: "GET",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
    })
      .then((response) => {
        if (!response.ok) {
          throw new Error("Error en la respuesta del servidor");
        }
        return response.json();
      })
      .then((result) => {
        if (result.success) {
          const contratos = result.contratos;
          const inquilinos = result.inquilinos;
          const inmuebles = result.inmuebles;
          const tableBody = document.querySelector("#tblBodyContratos");
          tableBody.innerHTML = ""; // Limpiar el contenido actual

          // Actualizar el contenido de la tabla con los datos recibidos
          contratos.forEach((contrato) => {
            const inquilino = inquilinos.find(i => i.id_inquilino === contrato.id_inquilino);
            const inmueble = inmuebles.find(i => i.id_inmueble === contrato.id_inmueble);

            const nombreInquilino = inquilino ? `${inquilino.nombre} ${inquilino.apellido}` : "Inquilino no disponible";

            const direccionInmueble = inmueble ? inmueble.direccion : "Dirección no disponible";

            const precioInmueble = inmueble ? inmueble.precio_Alquiler : "Precio no disponible";

            const row = document.createElement("tr");
            row.innerHTML = `
              <td>${contrato.id_contrato}</td>
              <td>${nombreInquilino}</td>
              <td>${direccionInmueble}</td>
              <td>${new Date(contrato.fecha_inicio).toLocaleDateString("es-ES")}</td>
              <td>${new Date(contrato.fecha_fin).toLocaleDateString("es-ES")}</td>
              <td>${precioInmueble}</td>
              <td>${contrato.monto}</td>
              <td>${contrato.vigencia ? 'Vigente' : 'No Vigente'}</td>
              <td>
                <a href="/Contratos/EditarContrato/${contrato.id_contrato}" class="material-symbols-outlined" title="Editar Contrato">edit_note</a>
                
                </td>
            `;
            tableBody.appendChild(row);
          });
          // Inicializar DataTable después de cargar los datos
          $('#tblContratos').DataTable({
            destroy: true, // Destruir la instancia anterior de DataTable si existe
            language: {
              url: "//cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json" // Cambiar idioma a español
            }
          });
        } else {
          Swal.fire({
            title: "Lo siento",
            text: "No se encontraron contratos",
            icon: "info",
          });
        }
      })
      .catch((error) => {
        console.error("Error:", error);
        Swal.fire("Ocurrió un error al cargar los contratos");
      });
  }

  // Listeners para los botones
  btnVigentes.addEventListener("click", function () {
    cargarContratos("/Contratos/ContratosVigentes");
  });

  btnTerminados.addEventListener("click", function () {
    cargarContratos("/Contratos/ContratosTerminados");
  });
});
