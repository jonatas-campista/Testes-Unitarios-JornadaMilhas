using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test
{
    public class OfertaViagemConstrutor
    {
        [Fact]
        public void RetornaOfertaValidaQuandoDadosValidos()
        {
            //cenario
            Rota rota = new Rota("origemTeste","destinoTeste");
            Periodo periodo = new Periodo(new DateTime(2024, 2, 1), new DateTime(2024, 2, 5));
            double preco = 100.0;
            var validacao = true;

            //a��o
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            //valida��o
            Assert.Equal(validacao,oferta.EhValido);
        }
        [Fact]
        public void RetornaErrorDeRotaInvalidaOuPeriodoInvalidoQuandoRotaNula()
        {   
            //cenario
            Rota rota = null;
            Periodo periodo = new Periodo(new DateTime(2024, 2, 1), new DateTime(2024, 2, 5));
            double preco = 100.0;

            //a��o
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            //valida��o
            Assert.Contains("A oferta de viagem n�o possui rota ou per�odo v�lidos.",oferta.Erros.Sumario);
            Assert.False(oferta.EhValido);
        }

        [Fact]
        public void RetornaErrorDataIdaNaoPodeSerMaiorQueDataVoltaQuandoDataIdaForMaiorQueDataVolta()
        {
            //cenario
            Rota rota = new Rota("origemTeste", "destinoTeste");
            Periodo periodo = new Periodo(new DateTime(2024, 2, 5), new DateTime(2024, 2, 1));
            double preco = 100.0;

            //a��o
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            //valida��o
            Assert.Contains("Erro: Data de ida n�o pode ser maior que a data de volta.", oferta.Erros.Sumario);
            Assert.False(oferta.EhValido);
        }

        [Fact]
        public void RetornaErrorDePrecoInvalidoQuandoPrecoMenorQueZero()
        {
            //cenario
            Rota rota = new Rota("origemTeste", "destinoTeste");
            Periodo periodo = new Periodo(new DateTime(2024, 2, 1), new DateTime(2024, 2, 5));
            double preco = -1;

            //a��o
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            //valida��o
            Assert.Contains("O pre�o da oferta de viagem deve ser maior que zero.", oferta.Erros.Sumario);
            Assert.False(oferta.EhValido);
        }
    }
}