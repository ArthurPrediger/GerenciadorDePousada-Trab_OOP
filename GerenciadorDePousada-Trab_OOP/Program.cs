using System;

namespace GerenciadorDePousada_Trab_OOP
{
    class Program
    {
        static void menuCarregamento(ref Pousada p)
        {
            Console.WriteLine("\tGerenciador da Pousada");
            Console.WriteLine("Informe a opção desejada: ");
            Console.WriteLine("1 - Cadastrar nova pousada;");
            Console.WriteLine("2 - Carregar o último arquivo da pousada.");
            int input;
            do
            {
                input = int.Parse(Console.ReadLine());
            } while (input < 1 || input > 2);

            if(input == 1)
            {
                Console.WriteLine("Informe o nome da pousada: ");
                string nome = Console.ReadLine();
                Console.WriteLine("Informe o contato da pousada: ");
                string contato = Console.ReadLine();
                p = new Pousada(nome, contato);
                menuCadastros(ref p);
            }
            else
            {
                p.carregaDados();
                menuReservas(ref p);
            }

        }
        static void menuCadastros(ref Pousada p)
        {
            Console.Clear();
            Console.WriteLine("\tCadastros da pousada");
            Console.WriteLine("Informe a opção desejada de cadastro: ");
            Console.WriteLine("1 - Adicionar nome da pousada;");
            Console.WriteLine("2 - Adicionar contato da pousada;") ;
            Console.WriteLine("3 - Adicionar quarto da pousada;");
            Console.WriteLine("4 - Adicionar novo produto ao menu;");
            Console.WriteLine("5 - Salvar;");
            Console.WriteLine("6 - Ir para o menu de reservas da pousada;");
            Console.WriteLine("0 - Sair.");
            int input;

            do
            {
                input = int.Parse(Console.ReadLine());
            } while (input < 0 || input > 6);

            Console.Clear();
            if (input == 0)
            {
                p.salvaDados();
            }
            else if (input == 1)
            {
                Console.WriteLine("Informe o nome da pousada: ");
                p.Nome = Console.ReadLine();
                Console.WriteLine("Pressione qualquer tecla para voltar ao menu. ");
                Console.ReadLine();
                menuCadastros(ref p);
            }
            else if (input == 2)
            {
                Console.WriteLine("Informe o contato da pousada: ");
                p.Contato = Console.ReadLine();
                Console.WriteLine("Pressione qualquer tecla para voltar ao menu. ");
                Console.ReadLine();
                menuCadastros(ref p);
            }
            else if (input == 3)
            {
                Console.WriteLine("Informe o número do quarto a ser cadastrado: ");
                int numero = int.Parse(Console.ReadLine());
                if (!p.Quartos.Exists(x => x.Numero == numero))
                {
                    char cat;
                    float diaria;
                    Console.WriteLine("Informe a categoria do quarto (S, M ou P): ");
                    do
                    {
                        cat = char.Parse(Console.ReadLine());
                        cat = char.Parse(cat.ToString().ToUpper());
                    } while (cat != 'S' || cat != 'M' || cat != 'P');
                    Console.WriteLine("Informe o valor da diária do quartos em R$: ");
                    do
                    {
                        diaria = float.Parse(Console.ReadLine());
                    } while (diaria < 0);
                    p.Quartos.Add(new Quarto(numero, cat, diaria));
                }
                else
                {
                    Console.WriteLine("Um quarto com esse número já existe.");
                }
                Console.WriteLine("Pressione qualquer tecla para voltar ao menu. ");
                Console.ReadLine();
                menuCadastros(ref p);
            }
            else if (input == 4)
            {
                Console.WriteLine("Informe o nome do produto a ser cadastrado: ");
                string nome = Console.ReadLine();
                Console.WriteLine("Informe o código do produto a ser cadastrado: ");
                int cod = int.Parse(Console.ReadLine());
                if (!p.Produtos.Exists(x => x.Nome == nome) &&
                    !p.Produtos.Exists(x => x.Codigo == cod))
                {
                    float preco;
                    Console.WriteLine("Informe o preco do produto cadastrado em R$: ");
                    do
                    {
                        preco = float.Parse(Console.ReadLine());
                    } while (preco < 0);
                    p.Produtos.Add(new Produto(cod, nome, preco));
                }
                else
                {
                    Console.WriteLine("O nome e/ou o código do produto já existem.");
                }
                Console.WriteLine("Pressione qualquer tecla para voltar ao menu. ");
                Console.ReadLine();
                menuCadastros(ref p);
            }
            else if (input == 5)
            {
                p.salvaDados();
            }
            else if (input == 6)
            {
                menuReservas(ref p);
            }
        }
        static void menuReservas(ref Pousada p)
        {
            Console.Clear();
            Console.WriteLine("\tGerenciador de Reservas Pousada");
            Console.WriteLine("Informe a opção desejada: ");
            Console.WriteLine("1 - Consultar disponibilidade;");
            Console.WriteLine("2 - Consultar reserva;");
            Console.WriteLine("3 - Realizar reserva;");
            Console.WriteLine("4 - Cancelar reserva;");
            Console.WriteLine("5 - Realizar check-in;");
            Console.WriteLine("6 - Realizar check-out;");
            Console.WriteLine("7 - Registrar consumo;");
            Console.WriteLine("8 - Salvar;");
            Console.WriteLine("9 - Ir para o menu de cadastros da pousada;");
            Console.WriteLine("0 - Sair.");
            int input;
            do
            {
                input = int.Parse(Console.ReadLine());
            } while (input < 0 || input > 9);

            Console.Clear();
            if (input == 0)
            {
                p.salvaDados();
            }
            else if (input == 1)
            {
                Console.WriteLine("Informe uma data: ");
                Console.Write("Dia: ");
                int dia = int.Parse(Console.ReadLine());
                Console.Write("Mês: ");
                int mes = int.Parse(Console.ReadLine());
                Console.Write("Ano: ");
                int ano = int.Parse(Console.ReadLine());
                Console.WriteLine("Informe um número de quarto: ");
                int numQ = int.Parse(Console.ReadLine());
                p.consultaDisponibilidade(new Data(dia, mes, ano), numQ);
                Console.WriteLine("Pressione qualquer tecla para voltar ao menu. ");
                Console.ReadLine();
                menuReservas(ref p);
            }
            else if (input == 2)
            {
                Data data = new Data();
                string nomeC = "";
                int numQ = 0;
                Console.WriteLine("Escolha qual informação deseja passar para consulta:");
                Console.WriteLine("1 - Data específica;");
                Console.WriteLine("2 - Nome de cliente;");
                Console.WriteLine("3 - Número de quarto;");
                Console.WriteLine("4 - Continuar com as informações já passadas ou sair sem informar nada.");
                int i = int.Parse(Console.ReadLine());
                do
                {
                    if(i == 1)
                    {
                        Console.WriteLine("Escolha qual informação deseja passar para consulta:");
                        Console.WriteLine("Informe uma data: ");
                        Console.Write("Dia: ");
                        data.Dia = int.Parse(Console.ReadLine());
                        Console.Write("Mês: ");
                        data.Mes = int.Parse(Console.ReadLine());
                        Console.Write("Ano: ");
                        data.Ano = int.Parse(Console.ReadLine());
                    }
                    else if(i == 2)
                    {
                        Console.WriteLine("Informe um nome de cliente: ");
                        nomeC = Console.ReadLine();
                    }
                    else if(i == 3)
                    {
                        Console.WriteLine("Informe um número de quarto: ");
                        numQ = int.Parse(Console.ReadLine());
                    }
                } while (i != 4);

                p.consultaReserva(data, nomeC, numQ);
                Console.WriteLine("Pressione qualquer tecla para voltar ao menu. ");
                Console.ReadLine();
                menuReservas(ref p);
            }
            else if (input == 3)
            {
                Data dataI = new();
                Data dataF = new();
                Console.WriteLine("Informe uma data inicial: ");
                Console.Write("Dia: ");
                dataI.Dia = int.Parse(Console.ReadLine());
                Console.Write("Mês: ");
                dataI.Mes = int.Parse(Console.ReadLine());
                Console.Write("Ano: ");
                dataI.Ano = int.Parse(Console.ReadLine());
                Console.WriteLine("Informe uma data final: ");
                Console.Write("Dia: ");
                dataF.Dia = int.Parse(Console.ReadLine());
                Console.Write("Mês: ");
                dataF.Mes = int.Parse(Console.ReadLine());
                Console.Write("Ano: ");
                dataF.Ano = int.Parse(Console.ReadLine());
                Console.WriteLine("Informe o nome do cliente: ");
                string nomeC = Console.ReadLine();
                Console.WriteLine("Informe o número do quarto: ");
                int numQ = int.Parse(Console.ReadLine());
                p.realizaReserva(dataI, dataF, nomeC, numQ);
                Console.WriteLine("Pressione qualquer tecla para voltar ao menu. ");
                Console.ReadLine();
                menuReservas(ref p);
            }
            else if (input == 4)
            {
                Console.WriteLine("Informe o nome do cliente: ");
                string nomeC = Console.ReadLine();
                p.cancelaReserva(nomeC);
                Console.WriteLine("Pressione qualquer tecla para voltar ao menu. ");
                Console.ReadLine();
                menuReservas(ref p);
            }
            else if (input == 5)
            {
                Console.WriteLine("Informe o nome do cliente: ");
                string nomeC = Console.ReadLine();
                p.realizaCheckIn(nomeC);
                Console.WriteLine("Pressione qualquer tecla para voltar ao menu. ");
                Console.ReadLine();
                menuReservas(ref p);
            }
            else if (input == 6)
            {
                Console.WriteLine("Informe o nome do cliente: ");
                string nomeC = Console.ReadLine();
                p.realizaCheckOut(nomeC);
                Console.WriteLine("Pressione qualquer tecla para voltar ao menu. ");
                Console.ReadLine();
                menuReservas(ref p);
            }
            else if (input == 7)
            {
                Console.WriteLine("Informe o nome do cliente: ");
                string nomeC = Console.ReadLine();
                p.registraConsumo(nomeC);
                Console.WriteLine("Pressione qualquer tecla para voltar ao menu. ");
                Console.ReadLine();
                menuReservas(ref p);
            }
            else if (input == 8)
            {
                p.salvaDados();
                menuReservas(ref p);
            }
            else if (input == 9)
            {
                menuCadastros(ref p);
            }
        }
        static void Main()
        {
            Pousada p = new();
            menuCarregamento(ref p);
        }
    }
}
