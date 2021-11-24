using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace MvcMovie.Controllers
{
    public class HelloWorldController : Controller
    {
        // GET: /HelloWorld/

        public string Index()
        {
            return "This is my default action...";
        }

        // GET: /HelloWorld/Welcome/ 

        public string Welcome(string name, int ID = 1) // int age = 1 : Valor default, int? age : permitiría valores nulos
        {
            // Usa HtmlEncoder.Default.Encode para proteger la aplicación de entradas malintencionadas, por ejemplo, mediante JavaScript.

            return HtmlEncoder.Default.Encode($"Hello my name is: {name}, and my Id is: {ID}");
        }
    }
}