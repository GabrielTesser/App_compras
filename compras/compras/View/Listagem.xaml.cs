using compras.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace compras.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Listagem : ContentPage
    {
        ObservableCollection<Produto> lista_produto = new ObservableCollection<Produto>();
        public Listagem()
        {
            InitializeComponent();
            lst_produtos.ItemsSource = lista_produto;
        }

        private void ToolbarItem_Clicked_Novo(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new NovoProduto());
            }
            catch (Exception ex)
            {
                DisplayAlert("Ops!", ex.Message, "OK");
            }
        }

        private void ToolbarItem_Clicked_Somar(object sender, EventArgs e)
        {
            double soma = lista_produto.Sum(i => i.Preco * i.Quantidade);

            string msg = "O total da compra é " + soma;

            DisplayAlert("total", msg, "OK");
        }

        protected override void OnAppearing()
        {
            if (lista_produto.Count == 0)
            {

                System.Threading.Tasks.Task.Run(async () =>
                {
                    List<Produto> temp = await App.Database.getAll();

                    foreach (Produto item in temp)
                    {
                        lista_produto.Add(item);
                    }

                    ref_carregando.IsRefreshing = false;
                });
            }
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            MenuItem disparador = (MenuItem) sender;

            Produto produto_selecionado = (Produto)disparador.BindingContext;

            bool confirmacao = await DisplayAlert("tem certeza?", "remover item?", "sim", "não");
            
            if(confirmacao)
            {
                App.Database.Delete(produto_selecionado.Id);

                lista_produto.Remove(produto_selecionado);
            }
        }

        private void txt_buscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string buscou = e.NewTextValue;

            System.Threading.Tasks.Task.Run(async () =>
            {
                List<Produto> temp = await App.Database.Search(buscou);

                lista_produto.Clear();

                foreach (Produto item in temp)
                {
                    lista_produto.Add(item);
                }

                ref_carregando.IsRefreshing = false;
            });
        }

        private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

           

            Navigation.PushAsync(new EditarProduto
            {
                BindingContext = (Produto)e.SelectedItem
            });
        }
    }
}