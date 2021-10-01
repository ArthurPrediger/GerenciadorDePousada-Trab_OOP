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
                Console.Write("Dia: ");
                int mes = int.Parse(Console.ReadLine());
                Console.Write("Dia: ");
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
                p.consultaReserva();
                menuReservas(ref p);
            }
            else if (input == 3)
            {
                p.realizaReserva();
                menuReservas(ref p);
            }
            else if (input == 4)
            {
                p.cancelaReserva();
                menuReservas(ref p);
            }
            else if (input == 5)
            {
                p.realizaCheckIn();
                menuReservas(ref p);
            }
            else if (input == 6)
            {
                p.realizaCheckOut();
                menuReservas(ref p);
            }
            else if (input == 7)
            {
                int numQ;
                p.Quartos[numQ].adicionaConsumo();
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
