using Xamarin.Forms;

namespace XamarinAppTest
{
    /// <summary>
    /// La clase App representa la aplicación de Xamarin.Forms como un todo y se hereda de Xamarin.Forms.Application.
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            
            // Esta propiedad controlará la primera pantalla que ve el usuario.
            MainPage = new MainPage();
        }

        #region Métodos para controlar los eventos de ciclo de vida
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
        #endregion
    }
}
