jQuery.validator.addMethod("cpf", function(value, element) {
    value = jQuery.trim(value);
    value = value.replace('.','');
    value = value.replace('.','');
    cpf = value.replace('-','');

    while(cpf.length < 11) cpf = "0"+ cpf;

    const regexp = /^0+$|^1+$|^2+$|^3+$|^4+$|^5+$|^6+$|^7+$|^8+$|^9+$/;
    const a = [];

    let b = 0;
    let c = 11;

    for (let i = 0; i < 11; i++) {
        a[i] = cpf.charAt(i);
        if (i < 9) b += (a[i] * --c);
    }

    if ((x = b % 11) < 2) {
        a[9] = 0
    } else {
        a[9] = 11 - x
    }

    b = 0;
    c = 11;

    for (let y = 0; y < 10; y++) b += (a[y] * c--);

    if ((x = b % 11) < 2) {
        a[10] = 0;
    } else {
        a[10] = 11 - x;
    }

    let ret = true;
    if ((cpf.charAt(9) != a[9]) || (cpf.charAt(10) != a[10]) || cpf.match(regexp)) ret = false;

    return this.optional(element) || ret;

}, "CPF inválido");

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
                    required: true,
                    minlength: 14,
                    cpf: true
                },
                dataNascimento: {
                    required: true,
                },
                tipoUsuario: {
                    required: true
                },
                email: {
                    required: true,
                    email: true
                },
                password: {
                    required: true,
                    minlength: 5
                },
                confirmPassword: {
                    required: true,
                    equalTo: "#password"
                }
            },
            messages: {
                nome: "Nome obrigatório",
                cpf: {
                    required: "CPF obrigatório",
                    minlength: "CPF inválido",
                },
                dataNascimento: "Data de nascimento obrigatória",
                tipoUsuario: "Tipo de usuário obrigatório",
                email: {
                    required: "E-mail obrigatório",
                    email: "O e-mail informado é inválido"
                },
                password: {
                    required: "Senha obrigatória",
                    minlength: "A senha deve ter pelo menos 5 caracteres."
                },
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
    const form = $("#form-cadastro");
    if (form.valid()) {        
        const formData = form.serializeArray();
        const data = {};
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
            success: function (res) {
                if (res != null) {
                    if (res.statusCode == 200) {
                        $("#toast-cadastro").removeClass('bg-danger');
                        $("#toast-cadastro").addClass('bg-success');
                        $("#toast-cadastro .toast-body").text("Usuário cadastrado com sucesso!");
                        $("#toast-cadastro").toast('show');
                        
                        setTimeout(function () {
                            location.href = "../index.html";
                        }, 2000);
                    }
                    else{
                        $("#toast-cadastro").removeClass('bg-success');
                        $("#toast-cadastro").addClass('bg-danger');
                        $("#toast-cadastro .toast-body").text("Falha ao tentar cadastrar o usuário!");
                        $("#toast-cadastro").toast('show');

                        form.trigger("reset");
                    }
                }
            },
            error: function (res) {
                console.error(res);

                $("#toast-cadastro").addClass('bg-danger');
                $("#toast-cadastro .toast-body").text("Falha ao tentar cadastrar o usuário!");
                $("#toast-cadastro").toast('show');

                form.trigger("reset");
            }
        });
    }
}
