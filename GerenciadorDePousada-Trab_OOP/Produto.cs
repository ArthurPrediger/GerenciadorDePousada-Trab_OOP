using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDePousada_Trab_OOP
{
    class Produto
    {
        private int codigo;
        private string nome;
        private float preco;

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }
        public float Preco
        {
            get { return preco; }
            set
            {
                if(value > 0)
                {
                    preco = value;
                }
            }
        }

        public Produto()
        {

        }

        //Construtor para realizar desserialização
        public Produto(string linhaArquivo)
        {
            string[] array = linhaArquivo.Split(";");
            codigo = int.Parse(array[0]);
            nome = array[1];
            preco = float.Parse(array[2]);
        }
        public Produto(int codigo, string nome, float preco)
        {
            this.codigo = codigo;
            this.nome = nome;
            this.preco = preco;
        }

        public string serializar()
        {
            StringBuilder sb = new StringBuilder(this.codigo.ToString());
            sb.Append(";");
            sb.Append(nome);
            sb.Append(";");
            sb.Append(preco);
            return sb.ToString();
        }

    }
}
