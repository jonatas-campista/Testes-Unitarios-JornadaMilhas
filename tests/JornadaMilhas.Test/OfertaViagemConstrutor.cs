using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test
{
    public class OfertaViagemConstrutor
    {
        [Theory]
        [InlineData(null, null, "2024-02-01", "2024-02-05", 100.0, false, "A oferta de viagem n�o possui rota ou per�odo v�lidos.")]
        [InlineData("origemTeste", "destinoTeste", "2024-02-01", "2024-02-05", 100.0, true, "")]
        [InlineData("origemTeste", null, "2024-02-01", "2024-02-05", 100.0, false, "A oferta de viagem n�o possui rota ou per�odo v�lidos.")]
        [InlineData(null, "destinoTeste", "2024-02-01", "2024-02-05", 100.0, false, "A oferta de viagem n�o possui rota ou per�odo v�lidos.")]
        [InlineData("origemTeste", "destinoTeste", null, "2024-02-05", 100.0, false, "A oferta de viagem n�o possui rota ou per�odo v�lidos.")]
        [InlineData("origemTeste", "destinoTeste", "2024-02-01", null, 100.0, false, "A oferta de viagem n�o possui rota ou per�odo v�lidos.")]
        [InlineData("origemTeste", "destinoTeste", "2024-03-01", "2024-02-05", 100.0, false, "Erro: Data de ida n�o pode ser maior que a data de volta.")]
        [InlineData("origemTeste", "destinoTeste", "2024-02-01", "2024-02-05", -1, false, "O pre�o da oferta de viagem deve ser maior que zero.")]
        [InlineData("origemTeste", "destinoTeste", "2024-02-01", "2024-02-05", 0, false, "O pre�o da oferta de viagem deve ser maior que zero.")]
        public void RetornaEhValidoDeAcordoComDadosDeEntrada(string origem, string destino, string dataIda, string dataVolta, double preco, bool ehValido, string mensagemErro)
        {
            // cen�rio
            Rota? rota = (origem == null || destino == null) ? null : new Rota(origem, destino);
            Periodo? periodo = dataIda == null || dataVolta == null ? null : new Periodo(DateTime.Parse(dataIda), DateTime.Parse(dataVolta));

            // a��o
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            // valida��o
            if (!string.IsNullOrEmpty(mensagemErro))
            {
                Assert.Contains(mensagemErro, oferta.Erros.Sumario);
            }
            Assert.Equal(ehValido, oferta.EhValido);
        }
    }
}