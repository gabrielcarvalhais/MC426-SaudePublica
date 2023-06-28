(function (window, document, $, undefined) {
    "use strict";
    $(function () {
        $("#btnEntrar").click(function(e){
            btnEntrar_click(e);
        });            

        $("#form-login").validate({
            rules: {
                email: {
                    required: true,
                    email: true
                },
                password: {
                    required: true
                }
            },
            messages: {
                email: {
                    required: "E-mail obrigatório",
                    email: "O e-mail informado é inválido"
                },
                password: "Senha obrigatória"
            }
        });
    });

})(window, document, window.jQuery);

function btnEntrar_click(e) {
    e.preventDefault();
    var form = $("#form-login");
    if (form.valid()) {        
        let data = form.serializeObject();
        
        const Facade = BackendFacade.getInstance();

        Facade.login(data);
    }
}

function validate(event) {
    event.preventDefault()
    var email = document.getElementById("email").value;
    var password = document.getElementById("password").value;

    if (email === "") {
        document.getElementById("email").style.border = "2px solid red";
        document.getElementById("email").focus();
        return false;
    }

    if (password === "") {
        document.getElementById("password").style.border = "2px solid red";
        document.getElementById("password").focus();
        return false;
    }
    return true;
}
