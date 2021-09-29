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
                    if(reservas[i].DiaInicio <= data && reservas[i].DiaFim >= data && reservas[i].Status != 'C')
                    {
                        quartoLiberado = false;
                    }
                }
            }

            if (quartoLiberado)
            {
                Console.WriteLine("O quarto de número " + quarto + " está liberado neste dia.");
            }
            else
            {
                Console.WriteLine("O quarto de número " + quarto + " não está liberado neste dia.");
            }
        }
        public void consultaReserva(int data, string cliente, int quarto)
        {
            bool nenhumaR = true;
            int cont = 1;
            for(int i = 0; i < reservas.Count; i++)
            {
                if (reservas[i].DiaInicio <= data && reservas[i].DiaFim >= data && 
                    reservas[i].Quarto.Numero == quarto && reservas[i].Cliente == cliente
                    && (reservas[i].Status == 'A' || reservas[i].Status == 'I'))
                {
                    Console.WriteLine(cont + " - Reserva:");
                    Console.WriteLine("Data inicial: " + reservas[i].DiaInicio);
                    Console.WriteLine("Data final: " + reservas[i].DiaFim);
                    Console.WriteLine("Nome do cliente: " + reservas[i].Cliente);
                    Console.WriteLine("Número do quarto: " + reservas[i].Quarto.Numero);
                    Console.WriteLine("Categoria do quarto: " + reservas[i].Quarto.Categoria);
                    Console.WriteLine("Diária do quarto: R$ " + reservas[i].Quarto.Diaria);
                    nenhumaR = false;
                }
            }

            if(nenhumaR)
            {
                Console.WriteLine("Nenhuma reserva ativa existe para os dados informados.");
            }
        }
        public void realizaReserva(int dataInicial, int dataFinal, string cliente, int quarto) 
        {
            bool reservaLiberada = true;

            for (int i = 0; i < reservas.Count; i++)
            {
                if (reservas[i].Quarto.Numero == quarto)
                {
                    if (((reservas[i].DiaInicio <= dataInicial && reservas[i].DiaFim >= dataInicial) ||
                        (reservas[i].DiaInicio <= dataFinal && reservas[i].DiaFim >= dataFinal)) &&
                        (reservas[i].Status != 'C' || reservas[i].Status != 'O'))
                    {
                        reservaLiberada = false;
                    }
                }
                if (reservas[i].Cliente == cliente && (reservas[i].Status == 'A' || reservas[i].Status == 'I'))
                {
                    reservaLiberada = false;
                }
            }

            if (reservaLiberada)
            {
                Console.WriteLine("Reserva realizada com sucesso.");
            }
            else
            {
                Console.WriteLine("Falha ao realizar reserva.");
            }

        }
        public void cancelaReserva(string cliente)
        {
            bool reservaExistente = false;
            for (int i = 0; i < reservas.Count; i++)
            {
                if (reservas[i].Cliente == cliente)
                {
                    Console.WriteLine("Reserva cancelada com sucesso.");
                    reservas[i].Status = 'C';
                    reservaExistente = true;
                }
            }

            if(!reservaExistente)
            {
                Console.WriteLine("Não há nenhuma reserva ativa para este cliente.");
            }
        }
        public void realizaCheckIn(string cliente)
        {
            bool reservaExistente = false;
            int dias = 0;
            for (int i = 0; i < reservas.Count; i++)
            {
                if (reservas[i].Cliente == cliente)
                {
                    Console.WriteLine("Check-in realizado com sucesso.");
                    reservas[i].Status = 'I';
                    Console.WriteLine("Reserva:");
                    Console.WriteLine("Data inicial: " + reservas[i].DiaInicio);
                    Console.WriteLine("Data final: " + reservas[i].DiaFim);
                    Console.WriteLine("Quantidade de dias reservados: " + dias);
                    Console.WriteLine("Número do quarto: " + reservas[i].Quarto.Numero);
                    Console.WriteLine("Categoria do quarto: " + reservas[i].Quarto.Categoria);
                    Console.WriteLine("Valor total das diárias: R$ " + (dias * reservas[i].Quarto.Diaria));
                    reservaExistente = true;
                }
            }

            if (!reservaExistente)
            {
                Console.WriteLine("Não há nenhuma reserva existente para este cliente.");
            }
        }
        public void realizaCheckOut(string cliente) 
        {
            
        }
    }
}
