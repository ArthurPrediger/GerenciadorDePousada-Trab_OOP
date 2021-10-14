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

        //Construtor para realizar desserialização
        public Quarto(string linhaArquivo)
        {
            string[] array = linhaArquivo.Split(";");
            numero = int.Parse(array[0]);
            categoria =char.Parse(array[1]);
            diaria = float.Parse(array[2]);
            for (int i = 3; i < array.Length; i++)
            {
                consumo.Add(int.Parse(array[i]));
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
            StringBuilder sb = new StringBuilder(this.numero.ToString());
            sb.Append(";");
            sb.Append(categoria);
            sb.Append(";");
            sb.Append(diaria);
            for(int i = 0; i < consumo.Count; i++)
            {
                sb.Append(";");
                sb.Append(consumo[i]);
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
                Produto pro = p.Produtos.Find(x => x.Codigo == consumo[i]);
                if(i < (consumo.Count - 1))
                {
                    Console.Write(pro.Nome + ", ");
                }
                else
                {
                    Console.Write(pro.Nome + ".\n");
                }

            }
        }
        public float valorTotalConsumo(Pousada p)
        {
            float valor = 0.0f;
            for(int i = 0; i < consumo.Count; i++)
            {
                Produto pro = p.Produtos.Find(x => x.Codigo == consumo[i]);
                valor += pro.Preco;
            }
            return valor;
        }
        public void limpaConsumo()
        {
            consumo.Clear();
        }
    }
}
