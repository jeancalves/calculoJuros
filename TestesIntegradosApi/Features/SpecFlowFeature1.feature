#language: pt-br

Funcionalidade: Realizar requisição a api 
				informado o valor inicial e a quantidade de meses e
				obter o resultado do cálculo do juros

Cenário: Requisição a api Cálculo de juros
	Dado que realizo a requisição a api
	E informo a quantidade inicial e a quantidade de meses
	| quantidadeInicial | mes |
	| 100               | 5   |
	Então recebo o juros calculado

Cenário: erro Requisição a api Cálculo de juros
	Dado que realizo a requisição a api
	E informo a quantidade inicial e a quantidade de meses
	| quantidadeInicial | mes |
	| 100               | 0   |
	Então recebo recebo que a requisição foi inválida
	| status | erro             |
	| 404    | dados incorretos |