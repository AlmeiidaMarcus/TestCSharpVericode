Feature: ConsultaCorreiosSteps
	Encontrar CEPs e códigos de rastreio

Background:
	Como usuário dos correios
	Gostaria de encontrar endereços e códigos de rastreio
	Para que possa obtê-los


@CaminhoFelizCorreios
Scenario: Procurar CEP inexistente
	Given Eu acesso o site dos correios
	When Eu procuro pelo CEP ""(.*)""
	Then Confirmo que o CEP não existe
	Then Eu volto para a tela inicial

Scenario: Procurar CEP válido
	Given Eu acesso o site dos correios
	When Eu procuro pelo CEP Correto
	Then Confirmo que o resultado é "Rua Quinze de Novembro, São Paulo/SP"
	And Eu volto para a tela inicial

Scenario: Eu procuro pelo código de rastreamento incorreto
	Given Eu acesso o site dos correios
	When Eu procuro pelo código de rastreamento incorreto
	Then Confirmo que o código não está correto
	And Eu volto para a tela inicial
