//Archivo Principal de Javascript proyecto LEGASY -- Desarrollado por Daniel Martinez --  dantrochez88@gmail.com

//#region Parametros Iniciales de Plantilla AdminLTE



//Produccion
//var Url = "";
//Local
var Url = "localhost:44369";
var Direccion = "https://" + Url;
var SocketDireccion = "ws://" + Url;

var Sesion;
var Token;

function Inicia() {
    try {
        CookiesArray = document.cookie.split('; ');
        if (CookiesArray.length >= 2) {
            for (i = 0; i < CookiesArray.length; i++) {
                Valor = CookiesArray[i].split("=");
                if (Valor[0] === "Sesion" || Valor[0] === "Token") {
                    if (Valor[0] === "Sesion") {
                        Sesion = Valor[1];
                    }
                    if (Valor[0] === "Token") {
                        Token = Valor[1];
                    }
                }
            }
      

        } else {
            Sesion = undefined;
            Token = undefined;
        }
    } catch (ex) {
        Sesion = undefined;
        Token = undefined;
    }
}


var ReEvaluarCredenciales = Inicia;
function Online() {
    ReEvaluarCredenciales();
    if (Sesion !== undefined && Token !== undefined && Sesion !== "" && Token !== "") {
        setTimeout(Online, 5000);
    } else {
       
            //Muestra el mensaje solo si la direccion actual es distinta a la direccion raiz
        //bootbox.alert(

        //        "<i class='fa fa-exclamation-triangle fa-5x text-danger'></i> Su sesion ha expirado por favor, reingrese sus credenciales para continuar.",
        //        function () {
        //            window.open(Direccion, '_blank')
        //        }
        //    );

        //bootbox.alert(
        //    "<i class='fa fa-exclamation-triangle fa-5x text-danger'></i> Su sesion ha expirado por favor, reingrese sus credenciales para continuar.",
           
        //    function () {
        //        window.open(Direccion, '_blank');
        //    }
        //);

        bootbox.alert({
            message: '<p>&nbsp;&nbsp; <i class="fa fa-exclamation-triangle fa-5x text-danger"></i> &nbsp;&nbsp; Su sesion ha expirado por favor, reingrese sus credenciales para continuar </p>',
            size: 'large',
            callback: function () {
                window.open(Direccion, '_blank')
            }
        });

    }
}

function ControlSesiones(Obj) {
    $(Obj).click(function () {
        obj = $(this);
        id = obj.attr("id");
        bootbox.confirm("Cerrar la sesion '" + id + "'?", function (result) {
            if (result) {
                $.ajax({
                    data: { SesionId: id },
                    method: "POST",
                    url: Direccion + "/Principal/CerrarSesion/",
                    success: function (r) {
                        if (r.estado) {
                            window.location.reload();
                        }
                    }
                });
            }
        });
    });
}


Inicia();
Online();
if (Sesion !== undefined && Token !== undefined && Sesion !== "" && Token !== "") {
   /* Data = new DataUsuario();*/
   
    this.ObjControlSesiones = new ControlSesiones();
   

}
//#endregion
