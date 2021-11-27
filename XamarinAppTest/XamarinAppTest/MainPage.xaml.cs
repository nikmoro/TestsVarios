using Xamarin.Forms;

namespace XamarinAppTest
{
    /// <summary>
    /// Una página de contenido simplemente muestra su contenido.
    /// </summary>
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var layout = new StackLayout
            {
                Orientation = StackOrientation.Vertical
            };

            layout.Children.Add(new Label { Text = "Ingrese su nombre" });
            layout.Children.Add(new Entry());
            layout.Children.Add(new Button { Text = "Aceptar" });
 
            this.Content = layout;
        }
    }
}
