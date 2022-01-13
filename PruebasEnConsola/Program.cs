using System;
using System.Collections.Generic;
using System.Linq;

namespace PruebasEnConsola
{
    class Program
    {
        static void Main(string[] args)
        {
            //// Unir en un arreglo de Campos
            string Campo1 = "Nombre";
            string Campo2 = "Direccion";
            string Campo3 = "Telefono";
            string CampoN = "CampoN";

            ////Unir en un arreglo de Datos
            string Dato1 = "Osiel";
            string Dato2 = "Tuxtla";
            string Dato3 = "1234567890";
            string DatoN = "DatoN";

            ////object[] Campos = new object[] {Campo1; Campo2; Campo3};

            ////string[] Datos = new string[] {Dato1, Dato2, Dato3};

            //Iniciación, asignando valores
            var Campos = new List<string>
            {
                Campo1,
                Campo2,
                Campo3,
                CampoN,
            };

            Console.WriteLine(string.Join(", ", Array.ConvertAll(Campos.ToArray(), i => i.ToString())));

            //foreach (string item in Campos)
            //    Console.WriteLine(item);

            //var Datos = new List<string>
            //{
            //    Dato1 + ",",
            //    Dato2 + ",",
            //    Dato3 + ",",
            //    DatoN + ","
            //};
            ////Añadir una lista a otra lista
            //Datos.AddRange(Campos);

            //foreach (string item in Datos)
            //    Console.WriteLine(item);

            //...

            //asumo que asientos es un arreglo
            //para el ejemplo, es de tipo int pero puede ser de cualquier tipo
            //int[] asientos = { 1, 2, 3 };
            //Console.Write(
            //    //String.Join permitirá unir todos los elementos de un arreglo/lista
            //    String.Join(
            //        ",", //colocamos cómo queremos que se separen los elementos
            //        asientos.ToList() //convertimos el arreglo en lista, si ya es una lista no necesitas aplicar este método
            //            .Select(x => "null") //aplicamos una conversión a los elementos para que sean "null" que es tu requerimiento
            //        )
            //    );

            Console.ReadKey();
        }
    }
}
