using CalculoJuros;
using Flunt.Notifications;
using JurosCompostos.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Xunit;

namespace TesteCalculoJuros
{
    public class UnitTestCalculoJuros
    {
        public CalculajurosController calculo;
        public Calculo calculoModelo;
        public UnitTestCalculoJuros()
        {
            calculo = new CalculajurosController();
            calculoModelo = new Calculo();
        }

        [Theory]
        [InlineData(100,0, "Mes deve ser maior que zero")]
        [InlineData(100,-10, "Mes deve ser maior que zero")]
        [InlineData(-100,5, "Valor Inicial deve ser maior que zero")]
        [InlineData(0,10, "Valor Inicial deve ser maior que zero")]
        public void TesteCalculoJurosDadosIncorreto(float valor, int mes, string mensagem)
        {
            var resultado = calculo.Get(valor, mes);
            var badResult = resultado as BadRequestObjectResult;
            Assert.IsType<BadRequestObjectResult>(resultado);
            Assert.Equal(400, badResult.StatusCode);
            List<Notification> notificacao = (List < Notification >)badResult.Value;
            Assert.Equal(mensagem, notificacao.ElementAt(0).Message.ToString());
        }

        [Theory]
        [InlineData(0, 0, "Mes deve ser maior que zero", "Valor Inicial deve ser maior que zero")]
        public void TesteCalculoJurosTodosDadosIncorreto(float valor, int mes, string mensagem, string mensagem2)
        {
            var resultado = calculo.Get(valor, mes);
            var badResult = resultado as BadRequestObjectResult;
            Assert.IsType<BadRequestObjectResult>(resultado);
            Assert.Equal(400, badResult.StatusCode);
            List<Notification> notificacao = (List<Notification>)badResult.Value;
            Assert.Equal(mensagem, notificacao.ElementAt(0).Message.ToString());
            Assert.Equal(mensagem2, notificacao.ElementAt(1).Message.ToString());
        }

        [Theory]
        [InlineData(100, 5, 105.1)]
        [InlineData(200, 5, 210.2)]
        [InlineData(400, 5, 420.4)]
        [InlineData(100, 10, 110.46)]
        public void TesteCalculoJurosValoresCorretos(float valor, int mes, double resultado)
        {
            var retorno = calculo.Get(valor, mes);
            Assert.IsType<OkObjectResult>(retorno);
            var okResult = retorno as OkObjectResult;
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(resultado, okResult.Value);


        }

        [Theory]
        [InlineData(0, true)]
        [InlineData(-20, true)]
        public void TesteModeloCalculoJurosMensagemMesIncorreto(int mes, bool resultado)
        {
            calculoModelo.meses = mes;
            calculoModelo.Validate();
            Assert.Equal("Mes deve ser maior que zero", calculoModelo.Notifications.ElementAt(0).Message);
            Assert.Equal(resultado, calculoModelo.Invalid);
        }

        [Theory]
        [InlineData(0, true)]
        [InlineData(-20, true)]
        public void TesteModeloCalculoJurosMensagemValorInicialIncorreto(int valor, bool resultado)
        {
            calculoModelo.valorinicial = valor;
            calculoModelo.meses = 5;
            calculoModelo.Validate();
            Assert.Equal("Valor Inicial deve ser maior que zero", calculoModelo.Notifications.ElementAt(0).Message);
            Assert.Equal(resultado, calculoModelo.Invalid);
        }
    }
}
