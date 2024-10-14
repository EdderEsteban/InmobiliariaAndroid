console.log("Ingreso a BuscarInmuebles.js");

document.addEventListener("DOMContentLoaded", function () {
  const btnBuscar = document.getElementById("btnBuscar");

  btnBuscar.addEventListener("click", function () {
    const idInmueble = document.getElementById("Id").value;

    if (!idInmueble) {
      Swal.fire({
        title: "Sin datos",
        text: "Debe completar el campo Id",
        icon: "question",
      });
      return;
    }

    fetch(`/Inmuebles/BuscarInmueblexId?id=${idInmueble}`, {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ id: idInmueble }),
    })
      .then((response) => {
        if (!response.ok) {
          throw new Error("Error en la respuesta del servidor");
        }
        return response.json();
      })
      .then((result) => {
        if (result.success) {
          const inmueble = result.data;
          // Actualizar el contenido de la tabla con los datos recibidos
          const tableBody = document.querySelector("#tbodyInmuebles");
          tableBody.innerHTML = ""; // Limpiar el contenido actual

          const row = document.createElement("tr");
          row.innerHTML = `
            <td>${inmueble.id_inmueble}</td>
            <td>${inmueble.direccion}</td>
            <td>${inmueble.uso === 1 ? "Comercial" : "Residencial"}</td>
            <td>${inmueble.tipo.tipo}</td>
            <td>${inmueble.cantidad_Ambientes}</td>
            <td>${inmueble.precio_Alquiler}</td>
            <td><a href="https://www.google.com/maps/search/?api=1&amp;query=@inmueble?.Latitud,@inmueble?.Longitud"
                    target="_blank"><i class="material-symbols-outlined">home_pin</i></a>
            </td>
            <td>${inmueble.propietarios?.nombre} ${
            inmueble.propietarios.apellido
          }</td>
            <td name="Acciones">
                <a href="/Inmuebles/EditarInmueble/${
                  inmueble.id_inmueble
                }" class="material-symbols-outlined" title="Editar Inmueble">edit_note</a>
                <a href="#" class="material-symbols-outlined btnBorrar" title="Borrar Inmueble" data-id="${
                  inmueble.id_inmueble
                }" style="cursor: pointer;">delete</a>
                <a href="/Inmuebles/DetallesInmueble/${
                  inmueble.id_inmueble
                }" class="material-symbols-outlined" title="Detalles Inmueble">visibility</a>
            </td>
        `;
          tableBody.appendChild(row);
        } else {
          Swal.fire({
            title: "Lo siento",
            text: result.message || "No se encontraron resultados",
            icon: "info",
          });
        }
      })
      .catch((error) => {
        console.error("Error:", error);
        Swal.fire("Ocurrió un error al buscar el inmueble");
      });
  });
});

document.addEventListener("DOMContentLoaded", function () {
  const btnBuscarDir = document.getElementById("btnBuscarDir");

  btnBuscarDir.addEventListener("click", function () {
    const DirInmueble = document.getElementById("Direccion").value;

    if (!DirInmueble) {
      Swal.fire({
        title: "Sin datos",
        text: "Debe completar el campo Dirección",
        icon: "question",
      });
      return;
    }

    fetch(`/Inmuebles/BuscarInmueblexDir`, {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },

      body: JSON.stringify({ Direccion: DirInmueble }),
    })
      .then((response) => {
        if (!response.ok) {
          throw new Error("Error en la respuesta del servidor");
        }
        return response.json();
      })
      .then((result) => {
        if (result.success) {
          const inmuebles = result.data; // Esto debería ser un array de inmuebles

          // Actualizar el contenido de la tabla con los datos recibidos
          const tableBody = document.querySelector("#tbodyInmuebles");
          tableBody.innerHTML = ""; // Limpiar el contenido actual

          inmuebles.forEach((inmueble) => {
            const row = document.createElement("tr");
            row.innerHTML = `
              <td>${inmueble.id_inmueble}</td>
              <td>${inmueble.direccion}</td>
              <td>${inmueble.uso === 1 ? "Comercial" : "Residencial"}</td>
              <td>${inmueble.tipo.tipo}</td>
              <td>${inmueble.cantidad_Ambientes}</td>
              <td>${inmueble.precio_Alquiler}</td>
              <td><a href="https://www.google.com/maps/search/?api=1&query=${
                inmueble.latitud
              },${inmueble.longitud}"
                      target="_blank"><i class="material-symbols-outlined">home_pin</i></a>
              </td>
              <td>${inmueble.propietarios?.nombre} ${
              inmueble.propietarios.apellido
            }</td>
              <td name="Acciones">
                  <a href="/Inmuebles/EditarInmueble/${
                    inmueble.id_inmueble
                  }" class="material-symbols-outlined" title="Editar Inmueble">edit_note</a>
                  <a href="#" class="material-symbols-outlined btnBorrar" title="Borrar Inmueble" data-id="${
                    inmueble.id_inmueble
                  }" style="cursor: pointer;">delete</a>
                  <a href="/Inmuebles/DetallesInmueble/${
                    inmueble.id_inmueble
                  }" class="material-symbols-outlined" title="Detalles Inmueble">visibility</a>
              </td>
            `;
            tableBody.appendChild(row);
          });
        } else {
          Swal.fire({
            title: "Lo siento",
            text: result.message || "No se encontraron resultados",
            icon: "info",
          });
        }
      })
      .catch((error) => {
        console.error("Error:", error);
        Swal.fire("Ocurrió un error al buscar el inmueble");
      });
  });
});

// -----------------------------------------------------------------Funcion para los botones ------------------------------------------------------------------

document.addEventListener("DOMContentLoaded", function () {
  const btnAlquilados = document.getElementById("btnAlquilados");
  const btnDisponibles = document.getElementById("btnDisponibles");
  const btnInactivos = document.getElementById("btnInactivos");

  // Función para hacer la solicitud y actualizar la tabla
  function cargarInmuebles(url) {
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
          const inmuebles = result.data;
          const tableBody = document.querySelector("#tbodyInmuebles");
          tableBody.innerHTML = ""; // Limpiar el contenido actual

          // Iterar sobre los inmuebles recibidos
          inmuebles.forEach((inmueble) => {
            const row = document.createElement("tr");
            row.innerHTML = `
                <td>${inmueble.id_inmueble}</td>
                <td>${inmueble.direccion}</td>
                <td>${inmueble.uso === 1 ? "Comercial" : "Residencial"}</td>
                <td>${inmueble.tipo.tipo}</td>
                <td>${inmueble.cantidad_Ambientes}</td>
                <td>${inmueble.precio_Alquiler}</td>
                <td><a href="https://www.google.com/maps/search/?api=1&query=${
                  inmueble.latitud
                },${inmueble.longitud}"
                        target="_blank"><i class="material-symbols-outlined">home_pin</i></a>
                </td>
                <td>${inmueble.propietarios?.nombre} ${
              inmueble.propietarios?.apellido
            }</td>
                <td>
                    <a href="/Inmuebles/EditarInmueble/${
                      inmueble.id_inmueble
                    }" class="material-symbols-outlined" title="Editar Inmueble">edit_note</a>
                    <a href="#" class="material-symbols-outlined btnBorrar" title="Borrar Inmueble" data-id="${
                      inmueble.id_inmueble
                    }" style="cursor: pointer;">delete</a>
                    <a href="/Inmuebles/DetallesInmueble/${
                      inmueble.id_inmueble
                    }" class="material-symbols-outlined" title="Detalles Inmueble">visibility</a>
                </td>
              `;
            tableBody.appendChild(row);
          });
        } else {
          Swal.fire({
            title: "Lo siento",
            text: "No se encontraron inmuebles",
            icon: "info",
          });
        }
      })
      .catch((error) => {
        console.error("Error:", error);
        Swal.fire("Ocurrió un error al cargar los inmuebles");
      });
  }

  // Listeners para los botones
  btnAlquilados.addEventListener("click", function () {
    cargarInmuebles("/Inmuebles/ListadoInmueblesAlquilados");
  });

  btnDisponibles.addEventListener("click", function () {
    cargarInmuebles("/Inmuebles/ListadoInmueblesDisponibles");
  });

  btnInactivos.addEventListener("click", function () {
    cargarInmuebles("/Inmuebles/ListadoInmueblesInactivos");
  });
});
