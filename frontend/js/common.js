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
    const Facade = BackendFacade.getInstance();
    Facade.logout();
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