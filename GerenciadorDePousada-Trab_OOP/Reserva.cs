using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDePousada_Trab_OOP
{
    //Classe para gerenciamneto de datas
    class Data
    {
        private int dia;
        private int mes;
        private int ano;

        public int Dia
        {
            get { return dia; } 
            set {
                if (value > 0 && value < 32)
                {
                    dia = value;
                }
            }
        }
        public int Mes
        {
            get { return mes; }
            set
            {
                if (value > 0 && value < 13)
                {
                    mes = value;
                }
            }
        }
        public int Ano
        {
            get { return ano; }
            set
            {
                ano = value;
            }
        }

        public Data()
        { }
        public Data(int dia, int mes, int ano)
        {
            this.dia = dia;
            this.mes = mes;
            this.ano = ano;
        }
    }
    class Reserva
    {
        private Data diaInicio;
        private Data diaFim;
        private string cliente;
        private Quarto quarto;
        private char status;

        public Data DiaInicio
        {
            get { return diaInicio; }
        }
        public Data DiaFim
        {
            get { return diaFim; }
        }
        public string Cliente
        {
            get { return cliente; }
        }
        public Quarto Quarto
        {
            get { return quarto; }
        }
        public char Status
        {
            get { return status; }
            set { if (value == 'A' || value == 'C' ||
                      value == 'I' || value == 'O')
                  { status = value; } 
            }
        }

        public Reserva()
        {

        }

        //Construtor para realizar desserialização
        public Reserva(string linhaArquivo, Pousada p)
        {
            string[] array = linhaArquivo.Split(";");
            diaInicio = new Data(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));
            diaFim = new Data(int.Parse(array[3]), int.Parse(array[4]), int.Parse(array[5]));
            cliente = array[6];
            int numQuarto = int.Parse(array[7]);
            for(int i = 0; i < p.Quartos.Count; i++)
            {
                if(p.Quartos[i].Numero == numQuarto)
                {
                    quarto = p.Quartos[i]; 
                }
            }
            status = char.Parse(array[8]);
        }
        public Reserva(Data diaInicio, Data diaFim, string cliente, Quarto quarto, char status)
        {
            this.diaInicio = diaInicio;
            this.diaFim = diaFim;
            this.cliente = cliente;
            this.quarto = quarto;
            this.status = status;
        }

        public string serializar()
        {
            StringBuilder sb = new StringBuilder(this.diaInicio.Dia.ToString());
            sb.Append(";");
            sb.Append(diaInicio.Mes);
            sb.Append(";");
            sb.Append(diaInicio.Ano);
            sb.Append(";");
            sb.Append(diaFim.Dia);
            sb.Append(";");
            sb.Append(diaFim.Mes);
            sb.Append(";");
            sb.Append(diaFim.Ano);
            sb.Append(";");
            sb.Append(cliente);
            sb.Append(";");
            sb.Append(quarto.Numero);
            sb.Append(";");
            sb.Append(status);
            return sb.ToString();
        }
    }
}
