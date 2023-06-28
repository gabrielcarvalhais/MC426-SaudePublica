# MC426-SaudePublica

Contribuições: 
Gabriel Carvalhais
Arthur Guerra
Gustavo Sacramento
Gabriel Castilho
Vitor Kenji

## Diagrama em nível de componentes
![C4-SaudeMaisBarao](https://github.com/gabrielcarvalhais/MC426-SaudePublica/assets/53404354/c6c1ab02-589a-491f-a36b-b3e23cb62c64)

## Estilos arquiteturais adotados
- Model-View-Controller (MVC)
- Representational State Transfer (REST)

## Descrição sucinta dos principais componentes
- Autenticação Controller: Autoriza o registro de usuários e entrada na aplicação
- Paciente Controller: Facilita a obtenção de informações sobre pacientes e a inclusão de novos indivíduos
- Funcionário Controller: Proporciona a visualização e modificação dos dados de colaboradores, além da inserção de novos membros
- Agendamento Controller: Viabiliza a consulta e modificação dos registros de agendamentos através de consultas ao banco de dados, filtrando de acordo com as necessidades.
- Services: Captura as solicitações provenientes dos controladores e estabelece a comunicação com o banco de dados.
- Serviço de E-mail: Envia mensagens de e-mail aos usuários.
- Serviço de notificação: Informa o usuário sobre um evento acionado pela aplicação.
