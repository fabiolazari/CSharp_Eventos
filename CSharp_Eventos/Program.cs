using System;

namespace CSharp_Eventos
{
    class Program
    {
        static void Main(string[] args)
        {

            Carro carro = new Carro();
            carro.ExcedeuVelocidadeSeguranca += new VelocidadeSegurancaExcedidaEventHandler(CarroLimiteVelocidadeExcedida);

            for (int i = 0; i < 3; i++)
            {
                carro.Acelerar(30);
                Console.WriteLine(" Velocidade atual : {0} Kmh ", carro.Velocidade);
            }
            Console.ReadKey();
        }

        static void CarroLimiteVelocidadeExcedida(object source, EventArgs e)
        {
            Carro carro = (Carro)source;
            Console.WriteLine("O limite de velocidade foi excedido ({0}Kmh)", carro.Velocidade);
        }
    }

    delegate void VelocidadeSegurancaExcedidaEventHandler(object source, EventArgs e);

    class Carro
    {
        public event VelocidadeSegurancaExcedidaEventHandler ExcedeuVelocidadeSeguranca;

        private int _velocidade = 0;
        private int _velocidadeSeguranca = 70;

        public int Velocidade
        {
            get
            {
                return _velocidade;
            }
        }

        public void Acelerar(int kmh)
        {
            int velocidadeAnterior = _velocidade;
            _velocidade += kmh;

            if (velocidadeAnterior <= _velocidadeSeguranca && _velocidade > _velocidadeSeguranca)
                OnVelocidadeSegurancaExcedida(new EventArgs());
        }

        public virtual void OnVelocidadeSegurancaExcedida(EventArgs e)
        {
            if (ExcedeuVelocidadeSeguranca != null)
                ExcedeuVelocidadeSeguranca(this, e);
        }
    }
}
