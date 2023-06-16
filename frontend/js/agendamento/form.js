var operacao = null;

async function setFormConfiguration(id) {    

    operacao = (id == '') ? "insert" : "update";    

    $("#form-agendamento").validate({
        rules: {
            statusAgendamento: {
                required: true
            },
            medicoId: {
                required: true
            },
            pacienteId: {
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
            statusAgendamento: "Status obrigatório",
            medicoId: "Médico obrigatório",
            pacienteId: "Paciente obrigatório",
            dataInicio: "Data obrigatória",
            horaInicio: "Horário inicial obrigatório",
            horaFinal: "Horário final obrigatório",
            frequencia: "Frequência obrigatória",
            dataFinal: "Data final obrigatória",
        },
        errorPlacement: function (error, element) {
            if (element.attr("name") == 'PacienteId')
                error.insertAfter($("#PacienteError"));
            else if (element.attr("name") == 'MedicoId')
                error.insertAfter($("#MedicoError"));
            else error.insertAfter(element);
        },
    });

    setInputChange();
    await Promise.all([getPacientes(), getFuncionarios()]);

    if (id && id > 0)
        getAgendamentoById(id);

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
                console.log(resposta);
                if (resposta.statusCode == 400) {
                    let erro = resposta.value[0].errorMessage || resposta.value;
                    alert(erro);                                               
                } else if (resposta.statusCode == 200) {   
                    autoMapper(resposta.value);
                    $("#dataInicio").val(resposta.value.dataInicioFormatada);
                    $("#dataFinal").val(resposta.value.dataFinalFormatada);
                }
            },
            error: function (resposta) {  
                alert("Falha ao resgatar os dados do agendamento!");          
            }
        });
    } catch (err) {
        alert(err);
    }
}

async function getPacientes(){
    $("#pacienteId").empty();
    $("#pacienteId").append($('<option>', {
        value: "",
        text: "Selecione o paciente",
    }));
    try {    
        await $.ajax({
            url: 'https://localhost:5000/Paciente/GetAll',
            type: 'GET',
            success: function (resposta) {
                if (resposta.statusCode == 400) {
                    let erro = resposta.value[0].errorMessage || resposta.value;
                    alert(erro);                                               
                } else if (resposta.statusCode == 200) {   
                    let pacientes = resposta.value; 
                    for (let i = 0; i < pacientes.length; i++) {
                        $('#pacienteId').append($('<option>', {
                            value: pacientes[i].id,
                            text: pacientes[i].nome
                        }));
                    }
                }
            },
            error: function (resposta) {  
                alert("Falha ao buscar os pacientes cadastrados!");          
            }
        });
    } catch (err) {
        alert(err);
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
                    alert(erro);                                               
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
                alert("Falha ao buscar os médicos cadastrados!");          
            }
        });
    } catch (err) {
        alert(err);
    }
}

function btnSalvar_Click() {
    if ($("#form-agendamento").valid()) {        
        let data = $("#form-agendamento").serializeObject();
        
        if (operacao == "insert"){
            delete data.id;
            delete data.chave;
            delete data.vinculoId;
        }

        console.log(data);

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
                        alert(erro);                                               
                    } else if (resposta.statusCode == 200) {    
                        var message = "";
                        if (operacao == "insert")
                            message = "O agendamento foi cadastrado com sucesso!"
                        else 
                            message = "O agendamento foi modificado com sucesso!";                                         
                        alert(message);

                        setTimeout(function () {
                            window.location.href = "index.html";
                        }, 1500);
                    }
    
                },
                error: function (resposta) {  
                    alert("Falha ao realizar esta operação!");          
                }
            });
    
        } catch (err) {
            alert(err);
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