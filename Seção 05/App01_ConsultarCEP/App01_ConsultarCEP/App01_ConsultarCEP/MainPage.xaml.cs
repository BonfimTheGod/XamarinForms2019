using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App01_ConsultarCEP.Servico.Modelo;
using App01_ConsultarCEP.Servico;


namespace App01_ConsultarCEP
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BOTAO.Clicked += BuscarCEP;
        }

        private void BuscarCEP(object sender, EventArgs args)
        {
            //TODO - Validações.
            string cep = CEP.Text.Trim();

            if (isValidCEP(cep))
            {
                try
                {
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);
                    if (end != null)
                    {
                        RESULTADO.Text = string.Format("Endereço: {0}, {1} \n{2} {3}", end.logradouro, end.bairro, end.uf, end.localidade);
                    }
                    else
                    {
                        DisplayAlert("Erro", "CEP não encontrado", "A T A");
                    }
                }
                catch (Exception e)
                {
                    DisplayAlert("Erro Crítico!", e.Message, "OK");
                }
            }
            
        }
        private bool isValidCEP(string cep)
        {
            bool valido = true;

            if(cep.Length != 8)
            {
                DisplayAlert("ERROR", "CEP inválido!\nO CEP deve conter 8 números", "OK");
                valido = false;
            }

            int NovoCEP = 0;

            if(!int.TryParse(cep, out NovoCEP))
            {
                DisplayAlert("ERROR", "CEP inválido!\nO CEP deve compostos apenas por números", "OK");
                valido = false;
            }
            return valido;
        }
    }
}
