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
        var formData = form.serializeArray();
        var data = {};
        formData.forEach(function (input) {
            data[input.name] = input.value;
        });

        $.ajax({
            url: 'https://localhost:5000/Autenticacao/Login',
            data: JSON.stringify(data),
            type: 'POST',
            contentType: 'application/json',
            dataType: 'json',
            success: function (resposta) {
                if (resposta != null) {
                    if (resposta.statusCode == 400) {
                        var erro = resposta.value[0].errorMessage || resposta.value;
                        alert(erro);
                    } else if (resposta.statusCode == 200) {
                        alert("Usuário autenticado com sucesso!")
                        setTimeout(function () {
                            window.location.href = "/Home";
                        }, 1000);
                    }
                }
            },
            error: function (resposta) {
                alert("Falha ao tentar autenticar este usuário!")
            }
        });
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
