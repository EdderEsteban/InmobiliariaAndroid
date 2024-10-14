console.log("Ingreso a BuscarPropietarios.js");

// Funcionalidad Boton Buscar
document.addEventListener("DOMContentLoaded", function () {
  document.getElementById("btnBuscar").addEventListener("click", function () {
    buscarPropietarios();
  });
});

// Función para buscar propietarios
function buscarPropietarios() {
  console.log("Ingreso a buscarPropietarios");

  const Nombre = document.getElementById("Nombre").value;
  const Apellido = document.getElementById("Apellido").value;
  const Dni = document.getElementById("Dni").value;
  const form = document.getElementById("formSearch");

  if (Nombre === "" && Apellido === "" && Dni === "") {
    Swal.fire({
      title: "Sin datos",
      text: "Debe completar al menos uno de los campos",
      icon: "question",
    });
    return;
  }

  const JsonSearch = { Nombre: Nombre, Apellido: Apellido, Dni: Dni };

  fetch("/Propietarios/BuscarProp", {
    method: "POST",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
    body: JSON.stringify(JsonSearch),
  })
    .then((response) => response.json())
    .then((data) => {
      if (data.length === 0) {
        Swal.fire("No existen resultados a su busqueda.");
        form.reset();
      } else {
        // Actualizar el contenido de la tabla con los datos recibidos
        const tableBody = document.querySelector("#tbodyPropietarios");
        tableBody.innerHTML = ""; // Limpiar el contenido actual

        data.forEach((propietario) => {
          const row = document.createElement("tr");
          row.innerHTML = `
            <td>${propietario.nombre}</td>
            <td>${propietario.apellido}</td>
            <td>${propietario.dni}</td>
            <td>${propietario.direccion}</td>
            <td>${propietario.telefono}</td>
            <td>${propietario.correo}</td>
            <td>
                <a href="/Propietarios/EditarPropietario/${propietario.id_Propietario}" 
                    title="Editar" class="material-symbols-outlined">edit_note</a>
                <a href="/Propietarios/EliminarPropietario/${propietario.id_Propietario}" 
                    title="Eliminar" class="material-symbols-outlined">delete</a>
                <a class="btnVerInmuebles material-symbols-outlined" style="cursor: pointer;" data-id="${propietario.id_Propietario}" 
                title="Ver Inmuebles">visibility</a>
            </td>
          `;
          tableBody.appendChild(row);
        });

        // Agregar EventListener a los nuevos botones
        document.querySelectorAll(".btnVerInmuebles").forEach((button) => {
          button.addEventListener("click", function () {
            const idPropietario = this.getAttribute("data-id");
            verInmueblesPropietario(idPropietario);
          });
        });
      }
    })
    .catch((error) => {
      console.error("Error:", error);
      Swal.fire("Ocurrió un error al buscar propietarios.");
    });
}

// Función para obtener inmuebles de un propietario
function verInmueblesPropietario(idPropietario) {
  console.log(`Obteniendo inmuebles para el propietario ID: ${idPropietario}`);

  fetch("/Propietarios/ObtenerInmueblesPorPropietario", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Accept: "application/json",
    },
    body: JSON.stringify(parseInt(idPropietario)),
  })
    .then((response) => {
      if (!response.ok) {
        throw new Error("Error al obtener inmuebles");
      }
      return response.json();
    })
    .then((data) => {
      if (data.length === 0) {
        Swal.fire("Este propietario no tiene inmuebles registrados.");
      } else {
        mostrarInmuebles(data);
      }
    })
    .catch((error) => {
      console.error("Error:", error);
      Swal.fire("Ocurrió un error al obtener los inmuebles.");
    });
}

// Función para mostrar inmuebles en un modal
function mostrarInmuebles(inmuebles) {
  console.log("Inmuebles:", inmuebles);
  let contenido = `
    <table class="table">
        <thead>
            <tr>
                <th>Id</th>
                <th>Dirección</th>
                <th>Uso</th>
                <th>Tipo</th>
                <th>Ambientes</th>
                <th>Precio</th>
                <th>Ubicación</th>
                <th>Activo</th>
                <th>Disponible</th>
                <th>Acciones</th>
            </tr>
        </thead>
        <tbody>
  `;

  inmuebles.forEach((inmueble) => {
    contenido += `
        <tr>
            <td>${inmueble.id_inmueble}</td>
            <td>${inmueble.direccion}</td>
            <td>${inmueble.uso === 1 ? 'Comercial' : 'Residencial'}</td>
            <td>${inmueble.tipo.tipo}</td>
            <td>${inmueble.cantidad_Ambientes}</td>
            <td>$${inmueble.precio_Alquiler}</td>
            <td>
              <a href="https://www.google.com/maps/search/?api=1&query=${
                inmueble.latitud
              },${inmueble.longitud}" 
                target="_blank"><i class="material-symbols-outlined">home_pin</i>
              </a>
            </td>
            <td>
              <div class="form-check form-switch">
                  <input class="form-check-input" type="checkbox" role="switch" id="switchActivo-${
                    inmueble.id_inmueble
                  }"
                      ${inmueble.activo ? "checked" : ""} disabled>
                  <label class="form-check-label" for="switchActivo-${
                    inmueble.id_inmueble
                  }"></label>
              </div>
            </td>
            <td>
              <div class="form-check form-switch">
                  <input class="form-check-input" type="checkbox" role="switch" id="switchDisponible-${
                    inmueble.id_inmueble
                  }"
                      ${inmueble.disponible ? "checked" : ""} disabled>
                  <label class="form-check-label" for="switchDisponible-${
                    inmueble.id_inmueble
                  }"></label>
              </div>
            <td>
              <a class="material-symbols-outlined" href="/Inmuebles/DetallesInmueble/${inmueble.id_inmueble}" 
                 title="Detalles Inmueble">visibility</a>
            </td>
        </tr>
        `;
  });

  contenido += `
        </tbody>
    </table>
  `;

  Swal.fire({
    title: "Inmuebles del Propietario",
    html: contenido,
    width: "80%",
    showCloseButton: true,
    focusConfirm: false,
    confirmButtonText: "Cerrar",
  });
}
