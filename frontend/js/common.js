var userName = null;
var userRole = null;
var userId = null;

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
    await $.ajax({
        url: 'http://localhost:5001/Autenticacao/UsuarioLogado',
        type: 'GET',
        xhrFields: {
            withCredentials: true
        },    
        crossDomain: true,    
        success: function(resposta) {
            if (resposta.statusCode == 400) {
                let erro = resposta.value[0].errorMessage || resposta.value;
                alert(erro);                                               
            } else if (resposta.statusCode == 200) { 
                if (resposta.value.usuarioLogado === false) {                
                    window.location.href = "../index.html";
                }

                userName = resposta.value.claims[1].value;
                userRole = resposta.value.claims[3].value
                if (userRole == "Paciente")
                    userId = resposta.value.claims[0].value;
                $(".userName").text(userName);
                $(".userRole").text(userRole);
            }
        },
        error: function(resposta) {
            window.location.href = "../index.html";     
        }
    });
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