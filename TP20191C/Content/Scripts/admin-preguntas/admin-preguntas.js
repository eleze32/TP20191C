$(document).ready(function () {
    $('#list-preguntas-profesor').DataTable();


});

$(document).on("click", ".eliminar-pregunta", function (e) {
    e.preventDefault();
    let id = $(this).data("id");
    EliminarPregunta(id);
});

function EliminarPregunta(id) {

    let data = { id: id };

    $.ajax({
        url: "/Profesor/EliminarPregunta",
        type: "Get",
        data: data,
        success: function (data) {
            let msj = "";
            if (parseInt(data) === 1) {
                msj = "La pregunta fue eliminada.";
                $("#fila_" + id).remove();
            }
                

            if (parseInt(data) === 0)
                msj = "La pregunta no existe, actualice pagina.";

            if (parseInt(data) === -1)
                msj = "No se puede eliminar porque tiene repuestas.";


            $(".modal-title").html("<h5 class='text-white'>" + msj + "</h5>");

            
        }
    });

    return false;
    
}