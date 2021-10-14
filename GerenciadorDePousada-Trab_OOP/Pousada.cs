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

        //Método para carregar os dados dos arquivos
        //Faz as chamadas dos construtores de desserilização para todos os tipos de objetos das listas
        public void carregaDados()
        {
            if (File.Exists("pousada.txt") && File.Exists("quarto.txt") &&
                File.Exists("produto.txt") && File.Exists("reserva.txt"))
            {
                StreamReader sr = File.OpenText("pousada.txt");
                nome = sr.ReadLine();
                contato = sr.ReadLine();
                sr.Close();
                sr = File.OpenText("produto.txt");
                string linha;
                while (!sr.EndOfStream)
                {
                    linha = sr.ReadLine();
                    produtos.Add(new Produto(linha));
                }
                sr.Close();
                sr = File.OpenText("quarto.txt");
                while (!sr.EndOfStream)
                {
                    linha = sr.ReadLine();
                    quartos.Add(new Quarto(linha));
                }
                sr.Close();
                sr = File.OpenText("reserva.txt");
                while (!sr.EndOfStream)
                {
                    linha = sr.ReadLine();
                    reservas.Add(new Reserva(linha, this));
                }
                sr.Close();
            }
        }

        //Método para salvar os dados nos arquivos
        //Faz as chamadas dos métodos de serialização para todos os tipos de objetos das listas
        public void salvaDados()
        {
            StreamWriter sw = File.CreateText("pousada.txt");
            sw.WriteLine(nome);
            sw.WriteLine(contato);
            sw.Close();
            sw = File.CreateText("quarto.txt");
            foreach(var quarto in quartos)
            {
                sw.WriteLine(quarto.serializar());
            }
            sw.Close();
            sw = File.CreateText("reserva.txt");
            foreach (var reserva in reservas)
            {
                //Não salva as reservas canceladas e as reservas em check-out
                if(reserva.Status != 'C' && reserva.Status != 'O')
                {
                    sw.WriteLine(reserva.serializar());
                }
            }
            sw.Close();
            sw = File.CreateText("produto.txt");
            foreach (var produto in produtos)
            {
                sw.WriteLine(produto.serializar());
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
                        Console.WriteLine("Data inicial: " + reservas[i].DiaInicio.Dia.ToString() + "/" +
                            reservas[i].DiaInicio.Mes.ToString() + "/" + reservas[i].DiaInicio.Ano.ToString());
                        Console.WriteLine("Data final: " + reservas[i].DiaFim.Dia.ToString() + "/" +
                            reservas[i].DiaFim.Mes.ToString() + "/" + reservas[i].DiaFim.Ano.ToString());
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
                        Console.WriteLine("Data inicial: " + reservas[i].DiaInicio.Dia.ToString() + "/" +
                            reservas[i].DiaInicio.Mes.ToString() + "/" + reservas[i].DiaInicio.Ano.ToString());
                        Console.WriteLine("Data final: " + reservas[i].DiaFim.Dia.ToString() + "/" +
                            reservas[i].DiaFim.Mes.ToString() + "/" + reservas[i].DiaFim.Ano.ToString());
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
                        Console.WriteLine("Data inicial: " + reservas[i].DiaInicio.Dia.ToString() + "/" +
                            reservas[i].DiaInicio.Mes.ToString() + "/" + reservas[i].DiaInicio.Ano.ToString());
                        Console.WriteLine("Data final: " + reservas[i].DiaFim.Dia.ToString() + "/" +
                            reservas[i].DiaFim.Mes.ToString() + "/" + reservas[i].DiaFim.Ano.ToString());
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
                        Console.WriteLine("Data inicial: " + reservas[i].DiaInicio.Dia.ToString() + "/" +
                            reservas[i].DiaInicio.Mes.ToString() + "/" + reservas[i].DiaInicio.Ano.ToString());
                        Console.WriteLine("Data final: " + reservas[i].DiaFim.Dia.ToString() + "/" +
                            reservas[i].DiaFim.Mes.ToString() + "/" + reservas[i].DiaFim.Ano.ToString());
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
                        Console.WriteLine("Data inicial: " + reservas[i].DiaInicio.Dia.ToString() + "/" +
                            reservas[i].DiaInicio.Mes.ToString() + "/" + reservas[i].DiaInicio.Ano.ToString());
                        Console.WriteLine("Data final: " + reservas[i].DiaFim.Dia.ToString() + "/" +
                            reservas[i].DiaFim.Mes.ToString() + "/" + reservas[i].DiaFim.Ano.ToString());
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
                        Console.WriteLine("Data inicial: " + reservas[i].DiaInicio.Dia.ToString() + "/" +
                            reservas[i].DiaInicio.Mes.ToString() + "/" + reservas[i].DiaInicio.Ano.ToString());
                        Console.WriteLine("Data final: " + reservas[i].DiaFim.Dia.ToString() + "/" +
                            reservas[i].DiaFim.Mes.ToString() + "/" + reservas[i].DiaFim.Ano.ToString());
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
                        Console.WriteLine("Data inicial: " + reservas[i].DiaInicio.Dia.ToString() + "/" +
                            reservas[i].DiaInicio.Mes.ToString() + "/" + reservas[i].DiaInicio.Ano.ToString());
                        Console.WriteLine("Data final: " + reservas[i].DiaFim.Dia.ToString() + "/" +
                            reservas[i].DiaFim.Mes.ToString() + "/" + reservas[i].DiaFim.Ano.ToString());
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
            StringBuilder sw1 = new StringBuilder(dataInicial.Ano.ToString() + " " + dataInicial.Mes.ToString() +
                                     " " + dataInicial.Dia.ToString());
            StringBuilder sw2 = new StringBuilder(dataFinal.Ano.ToString() + " " + dataFinal.Mes.ToString() +
                                     " " + dataFinal.Dia.ToString());
            DateTime dataPI, dataPF;
            DateTime.TryParse(sw1.ToString(), out dataPI);
            DateTime.TryParse(sw2.ToString(), out dataPF);

            if (dataPI.CompareTo(dataPF) >= 0 ||
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

            if(quartos.Exists(x => x.Numero == quarto) && reservaLiberada)
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
            if (reservas.Exists(x=> x.Cliente == cliente))
            {
                Reserva r = reservas.Find(x => x.Cliente == cliente);
                if (r.Status == ' ')
                {
                    DateTime di = new DateTime(r.DiaInicio.Ano, r.DiaInicio.Mes, r.DiaInicio.Dia);
                    DateTime df = new DateTime(r.DiaFim.Ano, r.DiaFim.Mes, r.DiaFim.Dia);
                    string dias = df.Subtract(di).Days.ToString();
                    Console.WriteLine("Check-in realizado com sucesso.");
                    r.Status = 'I';
                    Console.WriteLine("Reserva:");
                    Console.WriteLine("Data inicial: " + di.ToString());
                    Console.WriteLine("Data final: " + df.ToString());
                    Console.WriteLine("Quantidade de dias reservados: " + dias);
                    Console.WriteLine("Número do quarto: " + r.Quarto.Numero);
                    Console.WriteLine("Categoria do quarto: " + r.Quarto.Categoria);
                    float valor = float.Parse(dias) * r.Quarto.Diaria;
                    Console.WriteLine("Valor total das diárias: R$ " + valor.ToString());
                    reservaExistente = true;
                }
            }

            if (!reservaExistente)
            {
                Console.WriteLine("Não há nenhuma reserva esperando check-in para este cliente.");
            }
        }
        public void realizaCheckOut(string cliente) 
        {
            bool reservaExistente = false;
            if (reservas.Exists(x => x.Cliente == cliente))
            {
                Reserva r = reservas.Find(x => x.Cliente == cliente);
                if (r.Status == 'A' || r.Status == 'I')
                {
                    DateTime di = new DateTime(r.DiaInicio.Ano, r.DiaInicio.Mes, r.DiaInicio.Dia);
                    DateTime df = new DateTime(r.DiaFim.Ano, r.DiaFim.Mes, r.DiaFim.Dia);
                    string dias = df.Subtract(di).Days.ToString();
                    float vt = float.Parse(dias) * r.Quarto.Diaria + r.Quarto.valorTotalConsumo(this);
                    Console.WriteLine("Check-out realizado com sucesso.");
                    Console.WriteLine("Reserva:");
                    Console.WriteLine("Data inicial: " + r.DiaInicio.Dia.ToString() + "/" +
                                     r.DiaInicio.Mes.ToString() + "/" + r.DiaInicio.Ano.ToString());
                    Console.WriteLine("Data final: " + r.DiaFim.Dia.ToString() + "/" +
                                     r.DiaFim.Mes.ToString() + "/" + r.DiaFim.Ano.ToString());
                    Console.WriteLine("Valor total das diárias: R$ " + (float.Parse(dias) * r.Quarto.Diaria));
                    Console.WriteLine("Valor total dos consumos: R$ " + r.Quarto.valorTotalConsumo(this));
                    r.Quarto.listaConsumo(this);
                    Console.WriteLine("Valor final a ser pago: R$ " + vt);
                    r.Status = 'O';
                    r.Quarto.limpaConsumo();
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
            if (reservas.Exists(x => x.Cliente == cliente))
            {
                Reserva r = reservas.Find(x => x.Cliente == cliente);
                if (r.Status == 'A' || r.Status == 'I')
                {
                    existeCheckIn = true;
                    Console.WriteLine("Produtos disponíveis: ");
                    for (int i = 0; i < produtos.Count; i++)
                    {
                        Console.WriteLine(produtos[i].Nome + ": R$ " + produtos[i].Preco);
                    }
                    Console.Write("Informe o nome produro desejado: ");
                    string nomeP = Console.ReadLine();
                    if(produtos.Exists(x=> x.Nome == nomeP))
                    {
                        Produto p = produtos.Find(x => x.Nome == nomeP);
                        r.Quarto.adicionaConsumo(p.Codigo);
                        Console.WriteLine("Produto registrado!");
                    }
                    else
                    {
                        Console.WriteLine("O produto com o nome informado não existe.");
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
