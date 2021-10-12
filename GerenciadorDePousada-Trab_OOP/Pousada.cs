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

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }
        public string Contato
        {
            get { return contato; }
            set { contato = value; }
        }
        public List<Quarto> Quartos
        {
            get { return quartos; }
        }
        public List<Produto> Produtos
        {
            get { return produtos; }
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
            if (File.Exists("pousada.txt") && File.Exists("quarto.txt") &&
                File.Exists("produto.txt") && File.Exists("reserva.txt"))
            {
                StreamReader sr = new StreamReader("pousada.txt");
                nome = sr.ReadLine();
                contato = sr.ReadLine();

                sr = new StreamReader("produto.txt");
                string linha = sr.ReadLine();
                int indice = 0;
                while (!sr.EndOfStream)
                {
                    produtos[indice] = new Produto(linha);
                    linha = sr.ReadLine();
                    indice++;
                }

                sr = new StreamReader("quarto.txt");
                linha = sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    quartos[indice] = new Quarto(linha);
                    linha = sr.ReadLine();
                    indice++;
                }

                sr = new StreamReader("reserva.txt");
                linha = sr.ReadLine();
                indice = 0;
                while (!sr.EndOfStream)
                {
                    reservas[indice] = new Reserva(linha, this);
                    linha = sr.ReadLine();
                    indice++;
                }
                sr.Close();
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
            sw = new StreamWriter("quarto.txt");
            for (int i = 0; i < quartos.Count; i++)
            {
                if(reservas[i].Status != 'C' && reservas[i].Status != 'O')
                {
                    sw.WriteLine(reservas[i].serializar());
                }
            }
            sw = new StreamWriter("produto.txt");
            for (int i = 0; i < quartos.Count; i++)
            {
                sw.WriteLine(produtos[i].serializar());
            }
            sw.Close();
        }

        public void consultaDisponibilidade(Data data, int quarto)
        {
            bool quartoLiberado = true;
            DateTime dataP = new DateTime(data.Ano, data.Mes, data.Dia);

            for (int i = 0; i < reservas.Count; i++)
            {
                Data di = reservas[i].DiaInicio;
                Data df = reservas[i].DiaFim;
                DateTime dataI = new DateTime(di.Ano, di.Mes, di.Dia);
                DateTime dataF = new DateTime(df.Ano, df.Mes, df.Dia);
                if (reservas[i].Quarto.Numero == quarto)
                {
                    if(DateTime.Compare(dataI, dataP) <= 0 && DateTime.Compare(dataP, dataF) <= 0 && 
                        reservas[i].Status != 'C' && reservas[i].Status != 'O')
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
        public void consultaReserva(Data data, string cliente, int quarto)
        {
            StringBuilder sw = new StringBuilder(data.Ano.ToString() + "/" + data.Mes.ToString() +
                                                 "/" + data.Dia.ToString());
            bool nenhumaR = true;
            DateTime dataP;
            //Se todos os 3 dados são informados
            if (DateTime.TryParse(sw.ToString(), out dataP) && cliente != "" && quarto != 0)
            {
                int cont = 1;
                for (int i = 0; i < reservas.Count; i++)
                {
                    Data di = reservas[i].DiaInicio;
                    Data df = reservas[i].DiaFim;
                    DateTime dataI = new DateTime(di.Ano, di.Mes, di.Dia);
                    DateTime dataF = new DateTime(df.Ano, df.Mes, df.Dia);
                    if (DateTime.Compare(dataI, dataP) <= 0 && DateTime.Compare(dataP, dataF) <= 0 &&
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
                        cont++;
                        nenhumaR = false;
                    }
                }
            }
            //Se somente o nome do cliente e número do quarto são informados
            else if (!DateTime.TryParse(sw.ToString(), out dataP) && cliente != "" && quarto != 0)
            {
                int cont = 1;
                for (int i = 0; i < reservas.Count; i++)
                {
                    if (reservas[i].Quarto.Numero == quarto && reservas[i].Cliente == cliente
                        && (reservas[i].Status == 'A' || reservas[i].Status == 'I'))
                    {
                        Console.WriteLine(cont + " - Reserva:");
                        Console.WriteLine("Data inicial: " + reservas[i].DiaInicio);
                        Console.WriteLine("Data final: " + reservas[i].DiaFim);
                        Console.WriteLine("Nome do cliente: " + reservas[i].Cliente);
                        Console.WriteLine("Número do quarto: " + reservas[i].Quarto.Numero);
                        Console.WriteLine("Categoria do quarto: " + reservas[i].Quarto.Categoria);
                        Console.WriteLine("Diária do quarto: R$ " + reservas[i].Quarto.Diaria);
                        cont++;
                        nenhumaR = false;
                    }
                }
            }
            //Se somente uma data e número do quarto são informados
            else if (DateTime.TryParse(sw.ToString(), out dataP) && cliente == "" && quarto != 0)
            {
                int cont = 1;
                for (int i = 0; i < reservas.Count; i++)
                {
                    Data di = reservas[i].DiaInicio;
                    Data df = reservas[i].DiaFim;
                    DateTime dataI = new DateTime(di.Ano, di.Mes, di.Dia);
                    DateTime dataF = new DateTime(df.Ano, df.Mes, df.Dia);
                    if (DateTime.Compare(dataI, dataP) <= 0 && DateTime.Compare(dataP, dataF) <= 0 &&
                        reservas[i].Quarto.Numero == quarto &&
                        (reservas[i].Status == 'A' || reservas[i].Status == 'I'))
                    {
                        Console.WriteLine(cont + " - Reserva:");
                        Console.WriteLine("Data inicial: " + reservas[i].DiaInicio);
                        Console.WriteLine("Data final: " + reservas[i].DiaFim);
                        Console.WriteLine("Nome do cliente: " + reservas[i].Cliente);
                        Console.WriteLine("Número do quarto: " + reservas[i].Quarto.Numero);
                        Console.WriteLine("Categoria do quarto: " + reservas[i].Quarto.Categoria);
                        Console.WriteLine("Diária do quarto: R$ " + reservas[i].Quarto.Diaria);
                        cont++;
                        nenhumaR = false;
                    }
                }
            }
            //Se somenteo nome do cliente e uma data são informados 
            else if (DateTime.TryParse(sw.ToString(), out dataP) && cliente != "" && quarto == 0)
            {
                int cont = 1;
                for (int i = 0; i < reservas.Count; i++)
                {
                    Data di = reservas[i].DiaInicio;
                    Data df = reservas[i].DiaFim;
                    DateTime dataI = new DateTime(di.Ano, di.Mes, di.Dia);
                    DateTime dataF = new DateTime(df.Ano, df.Mes, df.Dia);
                    if (DateTime.Compare(dataI, dataP) <= 0 && DateTime.Compare(dataP, dataF) <= 0 &&
                        reservas[i].Cliente == cliente && (reservas[i].Status == 'A' || reservas[i].Status == 'I'))
                    {
                        Console.WriteLine(cont + " - Reserva:");
                        Console.WriteLine("Data inicial: " + reservas[i].DiaInicio);
                        Console.WriteLine("Data final: " + reservas[i].DiaFim);
                        Console.WriteLine("Nome do cliente: " + reservas[i].Cliente);
                        Console.WriteLine("Número do quarto: " + reservas[i].Quarto.Numero);
                        Console.WriteLine("Categoria do quarto: " + reservas[i].Quarto.Categoria);
                        Console.WriteLine("Diária do quarto: R$ " + reservas[i].Quarto.Diaria);
                        cont++;
                        nenhumaR = false;
                    }
                }
            }
            //Se somente o nome do cliente é informado
            else if (!DateTime.TryParse(sw.ToString(), out dataP) && cliente != "" && quarto == 0)
            {
                int cont = 1;
                for (int i = 0; i < reservas.Count; i++)
                {
                    if (reservas[i].Cliente == cliente && (reservas[i].Status == 'A' || reservas[i].Status == 'I'))
                    {
                        Console.WriteLine(cont + " - Reserva:");
                        Console.WriteLine("Data inicial: " + reservas[i].DiaInicio);
                        Console.WriteLine("Data final: " + reservas[i].DiaFim);
                        Console.WriteLine("Nome do cliente: " + reservas[i].Cliente);
                        Console.WriteLine("Número do quarto: " + reservas[i].Quarto.Numero);
                        Console.WriteLine("Categoria do quarto: " + reservas[i].Quarto.Categoria);
                        Console.WriteLine("Diária do quarto: R$ " + reservas[i].Quarto.Diaria);
                        cont++;
                        nenhumaR = false;
                    }
                }
            }
            //Se somente uma data é informado
            else if (DateTime.TryParse(sw.ToString(), out dataP) && cliente == "" && quarto == 0)
            {
                int cont = 1;
                for (int i = 0; i < reservas.Count; i++)
                {
                    Data di = reservas[i].DiaInicio;
                    Data df = reservas[i].DiaFim;
                    DateTime dataI = new DateTime(di.Ano, di.Mes, di.Dia);
                    DateTime dataF = new DateTime(df.Ano, df.Mes, df.Dia);
                    if (DateTime.Compare(dataI, dataP) <= 0 && DateTime.Compare(dataP, dataF) <= 0 &&
                        (reservas[i].Status == 'A' || reservas[i].Status == 'I'))
                    {
                        Console.WriteLine(cont + " - Reserva:");
                        Console.WriteLine("Data inicial: " + reservas[i].DiaInicio);
                        Console.WriteLine("Data final: " + reservas[i].DiaFim);
                        Console.WriteLine("Nome do cliente: " + reservas[i].Cliente);
                        Console.WriteLine("Número do quarto: " + reservas[i].Quarto.Numero);
                        Console.WriteLine("Categoria do quarto: " + reservas[i].Quarto.Categoria);
                        Console.WriteLine("Diária do quarto: R$ " + reservas[i].Quarto.Diaria);
                        cont++;
                        nenhumaR = false;
                    }
                }
            }
            //Se somente um número do quarto é informado
            else if (!DateTime.TryParse(sw.ToString(), out dataP) && cliente == "" && quarto != 0)
            {
                int cont = 1;
                for (int i = 0; i < reservas.Count; i++)
                {
                    if (reservas[i].Quarto.Numero == quarto && (reservas[i].Status == 'A' || reservas[i].Status == 'I'))
                    {
                        Console.WriteLine(cont + " - Reserva:");
                        Console.WriteLine("Data inicial: " + reservas[i].DiaInicio);
                        Console.WriteLine("Data final: " + reservas[i].DiaFim);
                        Console.WriteLine("Nome do cliente: " + reservas[i].Cliente);
                        Console.WriteLine("Número do quarto: " + reservas[i].Quarto.Numero);
                        Console.WriteLine("Categoria do quarto: " + reservas[i].Quarto.Categoria);
                        Console.WriteLine("Diária do quarto: R$ " + reservas[i].Quarto.Diaria);
                        cont++;
                        nenhumaR = false;
                    }
                }
            }

            if (nenhumaR)
            {
                Console.WriteLine("Nenhuma reserva ativa existe para os dados informados.");
            }
        }
        public void realizaReserva(Data dataInicial, Data dataFinal, string cliente, int quarto) 
        {
            bool reservaLiberada = true;
            StringBuilder sw1 = new StringBuilder(dataInicial.Ano.ToString() + "/" + dataInicial.Mes.ToString() +
                                     "/" + dataInicial.Dia.ToString());
            StringBuilder sw2 = new StringBuilder(dataFinal.Ano.ToString() + "/" + dataFinal.Mes.ToString() +
                                     "/" + dataFinal.Dia.ToString());
            DateTime dataPI, dataPF;
            if (!DateTime.TryParse(sw1.ToString(), out dataPI))
            {
                reservaLiberada = false;
            }
            if (!DateTime.TryParse(sw2.ToString(), out dataPF) ||
                dataPI.CompareTo(dataPF) >= 0 ||
                dataPI.CompareTo(DateTime.Now) < 0)
            {
                reservaLiberada = false;
            }

            for (int i = 0; i < reservas.Count; i++)
            {
                if (reservas[i].Quarto.Numero == quarto && reservaLiberada == true)
                {
                    Data di = reservas[i].DiaInicio;
                    Data df = reservas[i].DiaFim;
                    DateTime dataI = new DateTime(di.Ano, di.Mes, di.Dia);
                    DateTime dataF = new DateTime(df.Ano, df.Mes, df.Dia);
                    if (((DateTime.Compare(dataI, dataPI) <= 0 && DateTime.Compare(dataPI, dataF) <= 0) ||
                        (DateTime.Compare(dataI, dataPF) <= 0 && DateTime.Compare(dataPF, dataF) <= 0)) &&
                        (reservas[i].Status != 'C' || reservas[i].Status != 'O'))
                    {
                        reservaLiberada = false;
                        break;
                    }
                }
                if (reservas[i].Cliente == cliente && (reservas[i].Status == 'A' || reservas[i].Status == 'I'))
                {
                    reservaLiberada = false;
                    break;
                }
            }

            if(quartos.Exists(x => x.Numero == quarto))
            {
                reservas.Add(new Reserva(dataInicial, dataFinal, cliente, quartos.Find(x => x.Numero == quarto), ' '));
            }
            else
            {
                reservaLiberada = false;
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
            for (int i = 0; i < reservas.Count; i++)
            {
                if (reservas[i].Cliente == cliente && reservas[i].Status != 'C' &&
                    reservas[i].Status != 'O')
                {
                    DateTime di = new DateTime(reservas[i].DiaInicio.Ano, reservas[i].DiaInicio.Mes,
                                                reservas[i].DiaInicio.Dia);
                    DateTime df = new DateTime(reservas[i].DiaFim.Ano, reservas[i].DiaFim.Mes,
                                                reservas[i].DiaFim.Dia);
                    string dias = df.Subtract(di).ToString();
                    Console.WriteLine("Check-in realizado com sucesso.");
                    reservas[i].Status = 'I';
                    Console.WriteLine("Reserva:");
                    Console.WriteLine("Data inicial: " + reservas[i].DiaInicio);
                    Console.WriteLine("Data final: " + reservas[i].DiaFim);
                    Console.WriteLine("Quantidade de dias reservados: " + dias);
                    Console.WriteLine("Número do quarto: " + reservas[i].Quarto.Numero);
                    Console.WriteLine("Categoria do quarto: " + reservas[i].Quarto.Categoria);
                    Console.WriteLine("Valor total das diárias: R$ " + (float.Parse(dias) * reservas[i].Quarto.Diaria));
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
            bool reservaExistente = false;
            for (int i = 0; i < reservas.Count; i++)
            {
                if (reservas[i].Cliente == cliente && reservas[i].Status == 'A' &&
                    reservas[i].Status == 'I')
                {
                    DateTime di = new DateTime(reservas[i].DiaInicio.Ano, reservas[i].DiaInicio.Mes,
                            reservas[i].DiaInicio.Dia);
                    DateTime df = new DateTime(reservas[i].DiaFim.Ano, reservas[i].DiaFim.Mes,
                                                reservas[i].DiaFim.Dia);
                    string dias = df.Subtract(di).ToString();
                    float vt = float.Parse(dias) * reservas[i].Quarto.Diaria + reservas[i].Quarto.valorTotalConsumo(this);
                    Console.WriteLine("Check-out realizado com sucesso.");
                    Console.WriteLine("Reserva:");
                    Console.WriteLine("Data inicial: " + reservas[i].DiaInicio);
                    Console.WriteLine("Data final: " + reservas[i].DiaFim);
                    Console.WriteLine("Valor total das diárias: R$ " + (float.Parse(dias) * reservas[i].Quarto.Diaria));
                    Console.WriteLine("Valor total dos consumos: R$ " + reservas[i].Quarto.valorTotalConsumo(this));
                    reservas[i].Quarto.listaConsumo(this);
                    Console.WriteLine("Valor final a ser pago: R$ " + vt);
                    reservas[i].Status = 'O';
                    reservas[i].Quarto.limpaConsumo();
                    reservaExistente = true;
                }
            }

            if (!reservaExistente)
            {
                Console.WriteLine("Não há nenhuma reserva com check-in ativo existente para este cliente.");
            }
        }
        public void registraConsumo(string cliente)
        {
            bool existeCheckIn = false;
            for(int i = 0; i < reservas.Count; i++)
            {
                if(reservas[i].Cliente == cliente && (reservas[i].Status == 'I'
                    || reservas[i].Status == 'I'))
                {
                    existeCheckIn = true;
                    Console.WriteLine("Produtos disponíveis: ");
                    for (int j = 0; j < produtos.Count; i++)
                    {
                        Console.WriteLine(produtos[i].Nome + ": R$ " + produtos[i].Preco);
                    }
                    Console.WriteLine("Informe o nome produro desejado: ");
                    string nomeP = Console.ReadLine();
                    for (int j = 0; j < produtos.Count; i++)
                    {
                        if(produtos[j].Nome == nomeP)
                        {
                            reservas[i].Quarto.adicionaConsumo(produtos[j].Codigo);
                            Console.WriteLine("Produto registrado!");
                            break;
                        }
                    }
                }
            }

            if(!existeCheckIn)
            {
                Console.WriteLine("Não existe reserva em check-in para o nome informado.");
            }
        }
    }
}
