(function (window, document, $, undefined) {
    "use strict";
    $(function () {
        $("#btnCadastrar").click(function(e){
            btnCadastrar_click(e);
        });            

        $("#form-cadastro").validate({
            rules: {
                nome: {
                    required: true
                },
                cpf: {
                    required: true
                },
                dataNascimento: {
                    required: true
                },
                tipoUsuario: {
                    required: true
                },
                email: {
                    required: true,
                    email: true
                },
                password: {
                    required: true                   
                },
                confirmPassword: {
                    required: true,
                    equalTo: "#password"
                }
            },
            messages: {
                nome: "Nome obrigatório",
                cpf: "CPF obrigatório",
                dataNascimento: "Data de nascimento obrigatória",
                tipoUsuario: "Tipo de usuário obrigatório",
                email: {
                    required: "E-mail obrigatório",
                    email: "O e-mail informado é inválido"
                },
                password: "Senha obrigatória",
                confirmPassword: {
                    required: "Confirmação de senha obrigatória",
                    equalTo: "As senhas não coincidem"
                },
            }
        });

        $("#cpf").mask('000.000.000-00', { reverse: false });
        $("#telefone").mask('(00) 00000-0000', { reverse: false });
    });

})(window, document, window.jQuery);

function btnCadastrar_click(e) {
    e.preventDefault();
    var form = $("#form-cadastro");
    if (form.valid()) {        
        var formData = form.serializeArray();
        var data = {};
        formData.forEach(function (input) {
            data[input.name] = input.value;
        });

        data["cpf"] = data["cpf"].replace(/[^\d]+/g, '');

        let url = `https://localhost:5000/${data["tipoUsuario"]}/Cadastro`;

        $.ajax({
            url: url,
            data: JSON.stringify(data),
            type: 'POST',
            contentType: 'application/json',
            dataType: 'json',
            success: function (resposta) {
                if (resposta != null) {
                    if (resposta.statusCode == 200) {
                        alert("Usuário cadastrado com sucesso!");
                        setTimeout(function () {
                            window.location.href = "/Autenticacao/Login";
                        }, 1000);
                    }
                    else{
                        alert("Falha ao tentar cadastrar o usuário!");
                    }
                }
            },
            error: function (resposta) {
                alert("Falha ao tentar cadastrar o usuário!")
            }
        });
    }
}
