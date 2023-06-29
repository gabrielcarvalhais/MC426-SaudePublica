class BackendFacade {
    backendUrl = 'http://localhost:5001';

    static instance;
    static #initializing = false; // variavel privada que só permite que o construtor seja chamado de dentro da classe

    constructor() {
        if (!BackendFacade.#initializing) {
            throw new Error("Construtor privado, a classe é um singleton. Utilize o método 'getInstance'.")
        }

        BackendFacade.#initializing = false;
    }

    static getInstance() {
        if (!BackendFacade.instance) {
            BackendFacade.#initializing = true; // permite o construtor de ser chamado sem erro
            BackendFacade.instance = new BackendFacade();
        }
        return BackendFacade.instance;
    }

    login(data) {
        $.ajax({
            url: this.backendUrl + '/Autenticacao/Login',
            data: JSON.stringify(data),
            xhrFields: {
                withCredentials: true
            },
            crossDomain: true,
            type: 'POST',
            contentType: 'application/json',
            dataType: 'json',
            success: function (resposta) {
                if (resposta != null) {
                    if (resposta.statusCode == 400) {
                        var erro = resposta.value[0].errorMessage || resposta.value;
                        toastError(erro);
                    } else if (resposta.statusCode == 200) {
                        toastSuccess("Usuário autenticado com sucesso!")
                        
                        setTimeout(function () {
                            window.location.href = "home/home.html";
                        }, 1000);
                    }
                }
            },
            error: function (resposta) {
                toastError("Falha ao tentar autenticar este usuário!")
            }
        });
    }
  
    async cadastrar(data) {
        await $.ajax({
            url: this.backendUrl + '/' + data["tipoUsuario"] + '/Cadastro',
            data: JSON.stringify(data),
            type: 'POST',
            contentType: 'application/json',
            dataType: 'json',
            success: function (res) {
                if (res != null) {
                    if (res.statusCode == 200) {
                        toastSuccess('Usuário cadastrado com sucesso!');
                        
                        setTimeout(function () {
                            location.href = "../index.html";
                        }, 2000);
                    }
                    else{
                        toastError('Falha ao tentar cadastrar o usuário!');
                    }
                }
            },
            error: function (res) {
                console.error(res);

                toastError('Falha ao tentar cadastrar o usuário!');
            }
        });
    }
}