// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('#success').css('visibility', 'hidden');
    getFechaLlegada();
    $('#tropaId').on('change', function () {
        getFechaLlegada();
    })
});

function getFechaLlegada() {
    var data = $('#tropaId option:selected').val();
    var url = "./Home/GetTropaInfo";
    var headers = GetToken();
    $.ajax({
        url: url,
        type: "POST",
        data: JSON.stringify(data),
        Accept: "application/json",
        contentType: "application/json",
        headers: headers,
        dataType: "JSON",
        async: 1,
        success: function (response) {

        },
        failure: function (response) {
            console.log("Failure" + response);
        },
        error: function (response) {
            console.log('Something went wrong', response);
        },
        complete: function (response) {
            console.log(response);
            SetFechaLlegada(response);
            SetNroTropa(response);
        }
    });
}

function SetFechaLlegada(response) {
    var textDate = response.responseJSON.fechaLlegada;
    var year = textDate.substring(0, 4);
    var month = textDate.substring(5, 7);
    var day = textDate.substring(8, 10);
    var fullDate = day + '-' + month + '-' + year;
    $('#fechaLlegada').val(fullDate);
    $('#fechaLlegadaHidden').val(response.responseJSON.fechaLlegada);
    $('#horaLlegadaHidden').val(response.responseJSON.horaLlegada);
    
}

function SetNroTropa(response) {
    $('#nroTropa').val(response.responseJSON.nroTropa);
}

function GetToken() {
    var token = $('input[name="__RequestVerificationToken"]').val();
    var headers = {};
    headers["MY-XSRF-TOKEN"] = token;
    return headers;
}

function getData() {
    var data = {
        "tropaId": $("#tropaId").val(),
        "nroTropa": $("#nroTropa").val(),
        "fechaLlegada": $('#fechaLlegadaHidden').val(),
        "horaLlegada": $('#horaLlegadaHidden').val(),
        "condicionLlegada": $("#condicionLlegada option:selected").val(),
        "corral": $("#corral option:selected").val(),
        "raza": $("#raza option:selected").val(),
        "categoria": $("#categoria option:selected").val(),
        "edad": $("#edad option:selected").val(),
        "pesoGeneral": $("#pesoGeneral option:selected").val(),
        "estadoGeneral": $("#estadoGeneral option:selected").val()
    }
    return data;
}

function SaveData(event) {
    event.stopPropagation();
    event.preventDefault();
    var data = getData();
    var url = "./Home/Store";
    var headers = GetToken();
    $.ajax({
        url: url,
        type: "POST",
        data: JSON.stringify(data),
        Accept: "application/json",
        contentType: "application/json",
        headers: headers,
        dataType: "JSON",
        async: 1,
        success: function (response) {

        },
        failure: function (response) {
            console.log("Failure" + response);
        },
        error: function (response) {
            console.log('Something went wrong', response);
        },
        complete: function (response) {
            console.log(response)
            $('#save').attr("disabled", true);
            if (response.responseJSON == 1) {
                $("#success").css("visibility", "visible").delay(1000);
            } else {
                $("#success").text("Ocurrió un problema").css({ "color": "red", "visibility": "visible"}).delay(1000);
            }
            setTimeout(function () {
                location.reload();
            }, 1005);
        }
    });
}

