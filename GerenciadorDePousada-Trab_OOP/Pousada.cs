using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GerenciadorDePousada_Trab_OOP
{
    class Pousada
    {
        private string nome;
        private string contato;
        private List<Quarto> quartos = new List<Quarto>();
        private List<Reserva> reservas = new List<Reserva>();
        private List<Produto> produtos = new List<Produto>();

        public List<Quarto> Quartos
        {
            get { return quartos; }
        }

        public Pousada()
        {

        }

        public Pousada(string nome, string contato)
        {
            this.nome = nome;
            this.contato = contato;
        }

        public void carregaDados()
        {
            StreamReader sr = new StreamReader("pousada.txt");
            nome = sr.ReadLine();
            contato = sr.ReadLine();
            sr.Close();

            StreamReader sr1 = new StreamReader("produto.txt");
            string linha = sr1.ReadLine();
            int indice = 0;
            while (!sr1.EndOfStream)
            {
                produtos[indice] = new Produto(linha);
                linha = sr1.ReadLine();
                indice++;
            }

            StreamReader sr2 = new StreamReader("quarto.txt");
            linha = sr2.ReadLine();
            while(!sr2.EndOfStream)
            {
                quartos[indice] = new Quarto(linha);
                linha = sr2.ReadLine();
                indice++;
            }

            StreamReader sr3 = new StreamReader("reserva.txt");
            linha = sr3.ReadLine();
            indice = 0;
            while (!sr3.EndOfStream)
            {
                reservas[indice] = new Reserva(linha, this);
                linha = sr3.ReadLine();
                indice++;
            }
        }

        public void salvaDados()
        {
            StreamWriter sw = new StreamWriter("pousada.txt");
            sw.WriteLine(nome);
            sw.WriteLine(contato);
            for (int i = 0; i < quartos.Count; i++)
            {
                sw.WriteLine(quartos[i].serializar());
            }
            for (int i = 0; i < quartos.Count; i++)
            {
                sw.WriteLine(reservas[i].serializar());
            }
            for (int i = 0; i < quartos.Count; i++)
            {
                sw.WriteLine(produtos[i].serializar());
            }
            sw.Close();
        }

        public void consultaDisponibilidade(int data, int quarto)
        {
            bool quartoLiberado = true;

            for(int i = 0; i < reservas.Count; i++)
            {
                if(reservas[i].Quarto.Numero == quarto)
                {
                    if(reservas[i].DiaInicio <= data && reservas[i].DiaFim >= data)
                    {
                        quartoLiberado = false;
                    }
                }
            }
        }
        public void consultaReserva(int data, string cliente, Quarto quarto)
        {

        }
        public void realizaReserva(int datas, string cliente, Quarto quarto) 
        { }
        public void cancelaReserva(string cliente)
        { }
        public void realizaCheckIn(string cliente)
        { }
        public void realizaCheckOut(string cliente) 
        { }
    }
}
