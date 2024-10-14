console.log("Ingreso a EditarUsuario.js");

// Funcionalidad Boton btnPass
document.addEventListener("DOMContentLoaded", function () {
  document.getElementById("btnPass").addEventListener("click", function () {
    cambiarPass();
  });
});

// Función para cambiar la contraseña
function cambiarPass() {
  Swal.fire({
    title: "Cambiar Contraseña",
    html:
      '<input type="password" id="newPassword" class="swal2-input" placeholder="Nueva Contraseña">' +
      '<input type="password" id="confirmPassword" class="swal2-input" placeholder="Confirmar Contraseña">',
    focusConfirm: false,
    preConfirm: () => {
      const newPassword = document.getElementById("newPassword").value;
      const confirmPassword = document.getElementById("confirmPassword").value;

      // Validación de las contraseñas
      if (!newPassword || !confirmPassword) {
        Swal.showValidationMessage("Ambos campos son obligatorios");
        return false;
      }
      if (newPassword !== confirmPassword) {
        Swal.showValidationMessage("Las contraseñas no coinciden");
        return false;
      }

      // Retorna las contraseñas si todo está correcto
      return {
        newPassword: newPassword,
        confirmPassword: confirmPassword,
      };
    },
    showCancelButton: true,
    confirmButtonText: "Guardar",
    cancelButtonText: "Cancelar",
  }).then((result) => {
    if (result.isConfirmed) {
      // Si las contraseñas son válidas, se envían al backend
      const idUsuario = document.querySelector('input[id="Id_usuario"]').value;
      const { newPassword } = result.value;
      
      // Enviar solicitud al backend
      fetch(`/Usuarios/CambiarPass`, {
        method: "POST",
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
            Id_Usuario: idUsuario, 
            Password: newPassword, 
            ConfirmPassword: newPassword
        }),
      })
        .then((response) => response.json())
        .then((data) => {
          if (data.success) {
            Swal.fire({
              icon: "success",
              title: "Contraseña actualizada",
              text: "La contraseña se cambió correctamente",
            });
          } else {
            Swal.fire({
              icon: "error",
              title: "Error",
              text:
                data.message || "Hubo un problema al actualizar la contraseña",
            });
          }
        })
        .catch((error) => {
          Swal.fire({
            icon: "error",
            title: "Error",
            text: "Error en el servidor, intente nuevamente más tarde",
          });
          console.error("Error:", error);
        });
    }
  });
}

// Boton para blanquear la contraseña
document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("btnResetPass").addEventListener("click", function () {
        blanquearPassword();
    });
});
// Funcion para blanquear la contraseña
function blanquearPassword() {
    const id_usuario = document.querySelector('input[id="Id_usuario"]').value;
    const correo = document.querySelector('input[id="Email"]').value;
    fetch(`/Usuarios/BlanquearPass`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify({
            Id_usuario: id_usuario,
            Email: correo,
        }),
    })
    .then(response => response.json())
    .then(data => {
        if (data.success) {
            Swal.fire({
                icon: "success",
                title: "Contraseña blanqueada",
                text: "Contraseña blanqueada correctamente. Avise a su usuario que su nueva Contraseña es igual a su Email",
            });
            
        } else {
            Swal.fire({
                icon: "error",
                title: "Error",
                text:
                  data.message || "Hubo un problema al blanquear la contraseña",
              });
        }
    })
    .catch(error => {
        console.error("Error en el servidor: ", error);
    });
}
