using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDePousada_Trab_OOP
{
    class Reserva
    {
        private int diaInicio;
        private int diaFim;
        private string cliente;
        private Quarto quarto;
        private char status;

        public int DiaInicio
        {
            get { return diaInicio; }
        }
        public int DiaFim
        {
            get { return DiaFim; }
        }
        public Quarto Quarto
        {
            get { return quarto; }
        }

        public Reserva()
        {

        }
        public Reserva(string linhaArquivo, Pousada p)
        {
            string[] array = linhaArquivo.Split(";");
            diaInicio = int.Parse(array[0]);
            diaFim = int.Parse(array[0]);
            cliente = array[1];
            int numQuarto = int.Parse(array[2]);
            for(int i = 0; i < p.Quartos.Count; i++)
            {
                if(p.Quartos[i].Numero == numQuarto)
                {
                    quarto = p.Quartos[i]; 
                }
            }
            status = char.Parse(array[3]);
        }
        public Reserva(int diaInicio, int diaFim, string cliente, Quarto quarto, char status)
        {
            this.diaInicio = diaInicio;
            this.diaFim = diaFim;
            this.cliente = cliente;
            this.quarto = quarto;
            this.status = status;
        }

        public string serializar()
        {
            StringBuilder sb = new StringBuilder(this.diaInicio);
            sb.Append(";");
            return sb.ToString();
        }
    }
}
