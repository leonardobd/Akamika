$('button[id = btnEliminar]').on('click', function () {
    $('#IdCurso').val($(this).data("idcurso"));
    $('#IdSesion').val($(this).data("idsesion"));
});