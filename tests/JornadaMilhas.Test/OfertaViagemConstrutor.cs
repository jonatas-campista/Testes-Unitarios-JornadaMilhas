using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test
{
    public class OfertaViagemConstrutor
    {
        [Theory]
        [InlineData(null, null, "2024-02-01", "2024-02-05", 100.0, false, "A oferta de viagem não possui rota ou período válidos.")]
        [InlineData("origemTeste", "destinoTeste", "2024-02-01", "2024-02-05", 100.0, true, "")]
        [InlineData("origemTeste", null, "2024-02-01", "2024-02-05", 100.0, false, "A oferta de viagem não possui rota ou período válidos.")]
        [InlineData(null, "destinoTeste", "2024-02-01", "2024-02-05", 100.0, false, "A oferta de viagem não possui rota ou período válidos.")]
        [InlineData("origemTeste", "destinoTeste", null, "2024-02-05", 100.0, false, "A oferta de viagem não possui rota ou período válidos.")]
        [InlineData("origemTeste", "destinoTeste", "2024-02-01", null, 100.0, false, "A oferta de viagem não possui rota ou período válidos.")]
        [InlineData("origemTeste", "destinoTeste", "2024-03-01", "2024-02-05", 100.0, false, "Erro: Data de ida não pode ser maior que a data de volta.")]
        
        public void RetornaEhValidoDeAcordoComDadosDeEntrada(string origem, string destino, string dataIda, string dataVolta, double preco, bool ehValido, string mensagemErro)
        {
            // cenário
            Rota? rota = (origem == null || destino == null) ? null : new Rota(origem, destino);
            Periodo? periodo = dataIda == null || dataVolta == null ? null : new Periodo(DateTime.Parse(dataIda), DateTime.Parse(dataVolta));

            // ação
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            // validação
            if (!string.IsNullOrEmpty(mensagemErro))
            {
                Assert.Contains(mensagemErro, oferta.Erros.Sumario);
            }
            Assert.Equal(ehValido, oferta.EhValido);
        }

        [Theory]
        [InlineData( -25, false, "O preço da oferta de viagem deve ser maior que zero.")]
        [InlineData( 0, false, "O preço da oferta de viagem deve ser maior que zero.")]
        public void RetornaMensagemDeErrorDePrecoInvalidoQuandoPrecoMenorOuIgualZero(double preco,bool ehValido,string mensagemErro)
        {
            Rota rota = new Rota("origemTeste", "destinoTeste");
            Periodo periodo = new Periodo(new DateTime(2024, 2, 1), new DateTime(2024, 2, 5));

            OfertaViagem oferta = new OfertaViagem(rota,periodo,preco);

            if (!string.IsNullOrEmpty(mensagemErro))
            {
                Assert.Contains(mensagemErro, oferta.Erros.Sumario);
            }

            Assert.Equal(ehValido, oferta.EhValido);
        }

    }
}