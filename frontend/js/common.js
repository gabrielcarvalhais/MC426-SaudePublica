var userName = null;
var userRole = null;
var userId = null;
var userEmail = null;
var specificUserRoleId = null; // pacienteId ou funcionarioId

(function (window, document, $, undefined) {
    "use strict";
    $("#btnLogout").click(logout);
})(window, document, window.jQuery);

$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name] !== undefined) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};

async function verificaUsuarioLogado() {
    const Facade = BackendFacade.getInstance();
    Facade.verificaUsuarioLogado();
}

function logout(){
    $.ajax({
            url: 'http://localhost:5001/Autenticacao/Logout',
            type: 'GET',
            xhrFields: {
                withCredentials: true
            },
            crossDomain: true,
            success: function (resposta) {
                if (resposta != null) {
                    if (resposta.statusCode == 400) {
                        var erro = resposta.value[0].errorMessage || resposta.value;
                        toastError(erro);
                    } else if (resposta.statusCode == 200) {                        
                        window.location.href = "../index.html";
                    }
                }
            },
            error: function (resposta) {
                toastError("Falha ao tentar realizar esta ação!")
            }
        });
}

function toastSuccess(text){
    $("#toast-content").removeClass('bg-danger');
    $("#toast-content").addClass('bg-success');
    $("#toast-content .toast-body").text(text);
    $("#toast-content").toast('show');
}

function toastError(text){
    $("#toast-content").removeClass('bg-success');
    $("#toast-content").addClass('bg-danger');
    $("#toast-content .toast-body").text(text);
    $("#toast-content").toast('show');
}