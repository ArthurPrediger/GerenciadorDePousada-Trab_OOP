using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDePousada_Trab_OOP
{
    class Quarto
    {
        private int numero;
        private char categoria;
        private float diaria;
        List<int> consumo = new List<int>();

        public int Numero
        {
            get { return numero ; }
            set
            {
                if(value > 0)
                {
                    numero = value;
                }
            }
        }
        public char Categoria
        {
            get { return categoria; }
            set
            {
                if(value == 'S' || value == 'M' || value == 'P')
                {
                    categoria = value;
                }
            }
        }
        public float Diaria
        {
            get { return diaria; }
            set
            {
                if(value > 0)
                {
                    diaria = value;
                }
            }
        }
        public List<int> Consumo
        {
            get { return consumo; }
        }

        public Quarto()
        {

        }
        public Quarto(string linhaArquivo)
        {
            string[] array = linhaArquivo.Split(";");
            numero = int.Parse(array[0]);
            categoria =char.Parse(array[1]);
            diaria = float.Parse(array[2]);
            for(int i = 2; i < array.Length; i++)
            {
                consumo[i - 2] = int.Parse(array[i]);
            }
        }
        public Quarto(int numero, char categoria, float diaria)
        {
            this.numero = numero;
            this.categoria = categoria;
            this.diaria = diaria;
            int indice = 0;
        }

        public string serializar()
        {
            StringBuilder sb = new StringBuilder(this.numero);
            sb.Append(";");
            sb.Append(categoria);
            sb.Append(";");
            sb.Append(diaria);
            sb.Append(";");
            for(int i = 0; i < consumo.Count; i++)
            {
                sb.Append(consumo[i]);
                sb.Append(";");
            }
            return sb.ToString();
        }

        public void adicionaConsumo(int consumo)
        {
            this.consumo.Add(consumo);
        }
        public void listaConsumo(Pousada p)
        {
            Console.WriteLine("Produtos consumidos: ");
            for (int i = 0; i < consumo.Count; i++)
            {
                if(p.Produtos.Count > (i - 1))
                {
                    Console.Write(p.Produtos[i].Nome + ", ");
                }
                else
                {
                    Console.Write(p.Produtos[i].Nome + ".");
                }

            }
        }
        public float valorTotalConsumo(Pousada p)
        {
            float valor = 0.0f;
            for(int i = 0; i < consumo.Count; i++)
            {
                for (int j = 0; j < consumo.Count; j++)
                {

                    if (consumo[i] == p.Produtos[j].Codigo)
                    {
                        valor += p.Produtos[j].Preco;
                    }
                }
            }
            return valor;
        }
        public void limpaConsumo()
        {
            consumo.Clear();
        }
    }
}
