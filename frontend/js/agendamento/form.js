var operacao = null;
var idPacienteOperacao = null;

async function setFormConfiguration(id) {    

    operacao = !id ? "insert" : "update";

    $("#form-agendamento").validate({
        rules: {
            medicoId: {
                required: true
            },
            dataInicio: {
                required: true
            },
            horaInicio: {
                required: true
            },
            horaFinal: {
                required: true
            },
            frequencia: {
                required: true
            },     
            dataFinal: {
                required: true
            }
        },
        messages: {
            medicoId: "Médico obrigatório",
            dataInicio: "Data obrigatória",
            horaInicio: "Horário inicial obrigatório",
            horaFinal: "Horário final obrigatório",
            frequencia: "Frequência obrigatória",
            dataFinal: "Data final obrigatória",
        },
        errorPlacement: function (error, element) {
            if (element.attr("name") == 'MedicoId')
                error.insertAfter($("#MedicoError"));
            else error.insertAfter(element);
        },
    });

    $("#form-agendamento").trigger("reset");

    if (operacao === "update" && userRole === "Funcionário")
      $("#statusAgendamentoField").css("display", "block")
    
    setInputChange();
    await getFuncionarios();

    if (id && id > 0)
        getAgendamentoById(id);

    frequenciaChange();

    $("#btnSalvarAgendamento").unbind("click").bind("click", btnSalvar_Click);
    $("#btnSalvarAgendamento").prop("disabled", true);

    $("#frequencia").unbind("change").bind("change", frequenciaChange);
}

function getAgendamentoById(id){
    try {    
        $.ajax({
            url: `https://localhost:5000/Agendamento/GetById/${id}`,
            type: 'GET',
            success: function (resposta) {
                if (resposta.statusCode == 400) {
                    let erro = resposta.value[0].errorMessage || resposta.value;
                    toastError(erro);                                               
                } else if (resposta.statusCode == 200) {
                    autoMapper(resposta.value);
                    idPacienteOperacao = resposta.value.pacienteId;
                    $("#dataInicio").val(resposta.value.dataInicioFormatada);
                    $("#dataFinal").val(resposta.value.dataFinalFormatada);
                    frequenciaChange();
                }
            },
            error: function (resposta) {  
                toastError("Falha ao resgatar os dados do agendamento!");          
            }
        });
    } catch (err) {
        toastError(err);
    }
}

async function getFuncionarios(){
    $("#medicoId").empty();
    $("#medicoId").append($('<option>', {
        value: "",
        text: "Selecione o médico",
    }));
    try {    
        await $.ajax({
            url: 'https://localhost:5000/Funcionario/GetAll',
            type: 'GET',
            success: function (resposta) {
                if (resposta.statusCode == 400) {
                    let erro = resposta.value[0].errorMessage || resposta.value;
                    toastError(erro);                                               
                } else if (resposta.statusCode == 200) {   
                    let medicos = resposta.value; 
                    for (let i = 0; i < medicos.length; i++) {
                        $('#medicoId').append($('<option>', {
                            value: medicos[i].id,
                            text: medicos[i].nome
                        }));
                    }
                }
            },
            error: function (resposta) {  
                toastError("Falha ao buscar os médicos cadastrados!");          
            }
        });
    } catch (err) {
        toastError(err);
    }
}

function btnSalvar_Click() {
    if ($("#form-agendamento").valid()) {        
        let data = $("#form-agendamento").serializeObject();
        
        if (operacao == "insert"){
            delete data.id;
            delete data.chave;
            delete data.vinculoId;
            delete data.statusAgendamento;
            data.pacienteId = specificUserRoleId;
        } else {
            data.pacienteId = idPacienteOperacao;
        }

        try {    
            $.ajax({
                url: 'https://localhost:5000/Agendamento/Salvar',
                data: JSON.stringify(data),
                type: 'POST',
                contentType: 'application/json',
                dataType: 'json',
                success: function (resposta) {
    
                    if (resposta.statusCode == 400) {
                        var erro = resposta.value[0].errorMessage || resposta.value;
                        toastError(erro);                                               
                    } else if (resposta.statusCode == 200) {    
                        var message = "";
                        if (operacao == "insert")
                            message = "O agendamento foi cadastrado com sucesso!"
                        else 
                            message = "O agendamento foi modificado com sucesso!";                                         
                        toastSuccess(message);

                        setTimeout(function () {
                            window.location.href = "index.html";
                        }, 1500);
                    }
    
                },
                error: function (resposta) {  
                    toastError("Falha ao realizar esta operação!");          
                }
            });
    
        } catch (err) {
            toastError(err);
        }                  
    }
}

function setInputChange() {
    $("input").unbind("input").bind("input", function () {
        $("#btnSalvarAgendamento").prop("disabled", false);        
    });
    $(".form-select").unbind("input").bind("input", function () {
        $("#btnSalvarAgendamento").prop("disabled", false);
    });
}

function frequenciaChange() {
    var frequencia = $("#frequencia").val();
    if (frequencia != null && frequencia != "" && frequencia != 1) {
        $("#dataFinal").prop("disabled", false);
    }
    else {
        $("#dataFinal").prop("disabled", true);
    }
}

function autoMapper(model){
    $.each(model, function(key, value) {
        $('#' + key).val(value);
      });
}