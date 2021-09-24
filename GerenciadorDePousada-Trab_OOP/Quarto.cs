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
        public Quarto(int numero, char categoria, float diaria, List<int> consumo)
        {
            this.numero = numero;
            this.categoria = categoria;
            this.diaria = diaria;
            int indice = 0;
            foreach (int i in consumo)
            {
                this.consumo[indice] = i;
                indice++;
            }
        }

        public string serializar()
        {
            StringBuilder sb = new StringBuilder(this.numero);
            sb.Append(";");
            return sb.ToString();
        }
    }
}
