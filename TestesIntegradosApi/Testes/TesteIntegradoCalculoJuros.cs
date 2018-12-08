using System;
using System.Net.Http;
using TechTalk.SpecFlow;
using Xunit;

namespace TestesIntegradosApi
{
    [Binding]
    public class UnitTest1
    {
        HttpClient cliente;
        string valorInicial;
        string mes;
        string url;

        [Given(@"que realizo a requisição a api")]
        public void DadoQueRealizoARequisicaoAApi()
        {
            cliente = new HttpClient();
        }

        [Given(@"informo a quantidade inicial e a quantidade de meses")]
        public void DadoInformoAQuantidadeInicialEAQuantidadeDeMeses(Table tabela)
        {
            valorInicial = tabela.Rows[0]["quantidadeInicial"];
            mes = tabela.Rows[0]["mes"];
            url = $"http://localhost:56319/api/Calculajuros?valor={valorInicial}&mes={mes}";
        }

        [Then(@"recebo o juros calculado")]
        public void EntaoReceboOJurosCalculado()
        {
            var retorno = cliente.GetAsync(url).Result;
            Assert.Equal("OK", retorno.StatusCode.ToString());
            var retornostring = cliente.GetStringAsync(url).Result;
            Assert.Equal("105.1", retornostring);
        }

   
        [Then(@"recebo recebo que a requisição foi inválida")]
        public void EntaoReceboReceboQueARequisicaoFoiInvalida(Table tabela)
        {
            var retorno = cliente.GetAsync(url).Result;
            Assert.Equal("BadRequest", retorno.StatusCode.ToString());

            string resposta = "";
            using(HttpContent content = retorno.Content)
            {
                resposta = resposta + content.ReadAsStringAsync().Result;
            }
            Assert.Equal(true, resposta.Contains("Mes deve ser maior que zero"));
        }

    }
}
