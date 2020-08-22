using Ferreteria.Model.Modelos;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2
{
    public class Trabajador
    {
        public void Trabajo01()
        {
            var laOpcion = string.Empty;
            while (laOpcion != "X")
            {
                DesplegarMenu();
                laOpcion = LeaLaOpcion();
                switch (laOpcion)
                {
                    case "1":
                       ListarVentasPorFecha();
                        break;
                    case "2":
                        ListarVentasPorEmail();
                        break;
                    case "3":
                        ListarVentasPorTag();
                        break;
                    case "4":
                        InsertarCliente();
                        break;
                    case "5":
                        BorradoDeTags();
                        break;
                    case "6":
                        CambiarEdadDelCliente();
                        break;
                    default:
                        break;
                }
            }

        }

        private void DesplegarMenu()
        {
            Console.Clear();
            Console.WriteLine("Bienvenido al Examen 2 de Guillermo Jimenez Varela:");
            Console.WriteLine("1.Listar Ventas Por Fecha");
            Console.WriteLine("2.Listar Ventas Por Email");
            Console.WriteLine("3.Listar Ventas Por Tag");
            Console.WriteLine("4.Insertar Cliente.");
            Console.WriteLine("5.Borrado de Tags.");
            Console.WriteLine("6.Cambiar Edad del Cliente.");
            Console.WriteLine("X.Salir");
        }

        private string LeaLaOpcion()
        {
            string elResultado = Console.ReadLine();
            return elResultado;
        }

        private void ListarVentasPorEmail()
        {
            Console.Clear();
            Console.Write("Ingrese el correo que desea buscar: ");
            var correo = Console.ReadLine();
            var client = new Ferreteria.AccesoADatos.Conexion();
            var laListaDeVentas = client.ListarVentasPorEmail(correo);
            ImprimirListadoVentas(laListaDeVentas);

            Console.WriteLine("\n");
            Console.Write("-----Fin de la Operacion------ Presione cualquier tecla para salir");
            Console.ReadKey();
        }

        private void ListarVentasPorFecha()
        {
            Console.Clear();
            Console.Write("Digite la fecha inicial de la busqueda: (formato mm/dd/yyyy hh:mm:ss PM)");
            Console.Write("\n");
            var FechaInicial = Console.ReadLine();
            Console.Write("Digite la fecha final de la busqueda: (formato mm/dd/yyyy hh:mm:ss PM");
            Console.Write("\n");
            var FechaFinal = Console.ReadLine();
            if (ValidarFechas(FechaInicial, FechaFinal))
            {
                var client = new Ferreteria.AccesoADatos.Conexion();
                var laListaDeInventario = client.BuscarVentasEntreFechas(FechaInicial, FechaFinal);
                Console.Write("\n");
                ImprimirListadoVentas(laListaDeInventario);
            }
            else
            {
                Console.WriteLine("La fecha Inicial es mayor que la fecha final de busqueda");
            }

            Console.WriteLine("\n");
            Console.Write("-----Fin de la Operacion------ Presione cualquier tecla para salir");
            Console.ReadKey();
            
        }



        //Validacion de fecha
        private bool ValidarFechas(string Fecha1, string Fecha2)
        {

            CultureInfo culture = new CultureInfo("en-US");
            DateTime tempDate1 = Convert.ToDateTime(Fecha1, culture);
            DateTime tempDate2 = Convert.ToDateTime(Fecha2, culture);
            var correcto = false;
            if (tempDate1 <= tempDate2)
            {
                correcto = true;
            }
            return correcto;
        }

        private void ListarVentasPorTag()
        {
            Console.Clear();
            Console.Write("Ingrese el tag que desea buscar: ");
            var tag = Console.ReadLine();
            if (tag != String.Empty)
            {
                var client = new Ferreteria.AccesoADatos.Conexion();
                var laListaDeVentas = client.ListarVentasPorTag(tag);
                ImprimirListadoVentas(laListaDeVentas);
            }
            else
            {
                Console.Write("Digite un valor para la buscar por tags");
            }


            Console.WriteLine("\n");
            Console.Write("-----Fin de la Operacion------ Presione cualquier tecla para salir");
            Console.ReadKey();

        }
        private DateTime ValidarFechasCore(string Fecha)
        {
            CultureInfo culture = new CultureInfo("en-US");
            DateTime tempDate = Convert.ToDateTime(Fecha, culture);
            return tempDate;
        }

        private void InsertarCliente()
        {
            var tag = String.Empty;
            var tags = new List<string>();

            Console.Write("Digite la fecha de compra: (formato mm/dd/yyyy hh:mm:ss PM)");
            Console.Write("\n");
            var salesDate = Console.ReadLine();
            Console.Write("\n");
            Console.Write("Digite el nombre del item: ");
            Console.Write("\n");
            var name = Console.ReadLine();
            Console.Write("\n");
            Console.Write("Digite el precio del item: ");
            Console.Write("\n");
            var price = Console.ReadLine();
            Console.Write("\n");
            Console.Write("Digite la cantidad del Item: ");
            Console.Write("\n");
            var quantity = Console.ReadLine();
            do
            {
                Console.Write("Digite un tag del Item (Presione x para salir)");
                Console.Write("\n");
                tag = Console.ReadLine();
                Console.Write("\n");
                tags.Add(tag);
            } while (tag.ToUpper() != "X");

            Console.Write("Digite la ubicacion de la compra: ");
            var location = Console.ReadLine();

            Console.Write("Digite el correo del cliente: ");
            Console.Write("\n");
            var email = Console.ReadLine();
            Console.Write("\n");
            Console.Write("Digite el telefono del cliente: ");
            Console.Write("\n");
            var gender = Console.ReadLine();
            Console.Write("\n");
            Console.Write("Digite la edad del cliente: ");
            Console.Write("\n");
            var age = Console.ReadLine();
            Console.Write("\n");
            Console.Write("Digite true o false si el cupon ya fue usado: ");
            Console.Write("\n");
            var couponUsed = Console.ReadLine();
            Console.Write("\n");
            Console.Write("Digite el metodo de Compra ");
            Console.Write("\n");
            var purchaseMethod = Console.ReadLine();

            var venta = new Sales();

            venta.SalesDate = ValidarFechasCore(salesDate);

            venta.Items = new List<LItem>();
            var Item = new LItem();

            Item.Name = name;
            Item.Price = Int32.Parse(price);
            Item.Quantity = Int32.Parse(quantity);
            Item.Tags = tags;

            venta.Items.Add(Item);

            venta.Location = location;

            venta.Customer = new Customer();
            venta.Customer.Email = email;
            venta.Customer.Gender = gender;
            venta.Customer.Age = Int32.Parse(age);

            venta.CouponUse = bool.Parse(couponUsed);
            venta.PurchaseMethod = purchaseMethod;

            var client = new Ferreteria.AccesoADatos.Conexion();
            client.InsertarVenta(venta);

            Console.WriteLine("\n");
            Console.Write("-----Fin de la Operacion------ Presione cualquier tecla para salir");
            Console.ReadKey();
        }

        private void BorradoDeTags()
        {
            Console.Write("Ingrese el correo que desea buscar: ");
            var correo = Console.ReadLine();
            var client = new Ferreteria.AccesoADatos.Conexion();
            var laListaDeVentas = client.ListarVentasPorEmail(correo);
            ImprimirListadoVentas(laListaDeVentas);

            Console.Write("Seleccione el número de venta cuyos tags desea eliminar: ");
            var ventaSeleccionada = Console.ReadLine();
            var elNumeroDeVenta = 0;


            if (int.TryParse(ventaSeleccionada, out elNumeroDeVenta))
            {
                if (elNumeroDeVenta >= 0 && elNumeroDeVenta < laListaDeVentas.Count)
                {
                    var tags = String.Empty;
                    var totaltags = String.Empty;
                    var elRegistroDeVentas = laListaDeVentas[Int32.Parse(ventaSeleccionada)];
                    foreach (var item in elRegistroDeVentas.Items)
                    {
                        tags += item.Name+"{";

                        foreach (var tag in item.Tags)
                        {
                            tags += tag + ",";
                        }
                        tags += " }";
                        totaltags += tags + "\n";
                    }
                   
                    Console.Write(string.Format("Seguro que desea eliminar todos los tags {0}, de registro de venta{1} Si o No?", tags, elRegistroDeVentas.SalesID));
                    Console.WriteLine("\n");
                    Console.WriteLine("\n");
                    var confirmacion = Console.ReadLine().ToUpper();
                    if (confirmacion == "SI")
                    {
                        client.BorrarTags(elRegistroDeVentas.SalesID, elRegistroDeVentas);
                        Console.WriteLine("\n");
                        Console.Write("Nueva Lista de Ventas con Tags Eliminados");
                        Console.WriteLine("\n");
                        ImprimirListadoVentas(laListaDeVentas);
                    }
                    else
                    {
                        Console.WriteLine("\n");
                        Console.Write("La operacion de borrado ha sido cancelada");
                        Console.WriteLine("\n");

                    }
                    Console.WriteLine("\n");
                    Console.Write("-----Fin de la Operacion------ Presione cualquier tecla para salir");
                    Console.ReadKey();
                    Console.Clear();

                }
            }
        }


        private void CambiarEdadDelCliente()
        {
            Console.Clear();
            Console.Write("Digite la fecha inicial de la busqueda: (formato mm/dd/yyyy hh:mm:ss PM)");
            Console.Write("\n");
            var FechaInicial = Console.ReadLine();
            Console.Write("Digite la fecha final de la busqueda: (formato mm/dd/yyyy hh:mm:ss PM");
            Console.Write("\n");
            var FechaFinal = Console.ReadLine();
            if (ValidarFechas(FechaInicial, FechaFinal))
            {
                var client = new Ferreteria.AccesoADatos.Conexion();
                var laListaDeVentas = client.BuscarVentasEntreFechas(FechaInicial, FechaFinal);
                Console.Write("\n");
                ImprimirListadoVentas(laListaDeVentas);
                Console.Write("\n");
                Console.Write("Digite el numero  de registro de la venta que desea actualizar la edad: ");
                var numeroDeRegistro = Console.ReadLine();

                var elNumeroDeCliente = 0;
                if (int.TryParse(numeroDeRegistro, out elNumeroDeCliente))
                {
                    if (elNumeroDeCliente >= 0 && elNumeroDeCliente < laListaDeVentas.Count)
                    {
                        var elRegistroDeVentas = laListaDeVentas[elNumeroDeCliente];
                        Console.Write(string.Format("La edad actual del cliente es [{0}]. Digite la nueva edad: \n ", elRegistroDeVentas.Customer.Age));
                        var laNuevaEdadDelCliente = Console.ReadLine();
                        client.ActualizarEdadDelCliente(elRegistroDeVentas.SalesID, laNuevaEdadDelCliente);
                    }
                }

            }
            else
            {
                Console.WriteLine("La fecha Inicial es mayor que la fecha final de busqueda");
            }

            Console.WriteLine("\n");
            Console.Write("-----Fin de la Operacion------ Presione cualquier tecla para salir");
            Console.ReadKey();
        }

        private void ImprimirListadoVentas(IList<Sales> ListaVentas)
        {
            if (ListaVentas.Count > 0)
            {
                Console.WriteLine("Lista de Ventas:");
                var contador = 0;
                foreach (var venta in ListaVentas)
                {
                    var objetoItem = String.Empty;
                    var objetosTotal = String.Empty;
                    foreach (var item in venta.Items)
                    {
                        var tags = String.Empty;
                        foreach (var tag in item.Tags)
                        {
                            tags += "\n" + tag + " "; 
                        }
                        objetoItem = string.Format("Name: {0} "+"\n"+ " ; Tags: {1}" + "\n" + " ; Price: {2}" + "\n" + " ; Quantity: {3}" + "\n" + "",
                        item.Name, tags, item.Price, item.Quantity);

                        objetosTotal += "\n" + objetoItem;

                    }
                    Console.WriteLine(string.Format(
                        "Numero de Venta: {0};VentaID: {1}; SaleDate: {2}; Item: {3}; Customer Email: {4}; Customer Gender:{5};  Customer Age: {6}; Store Location: {7}; Coupon Use: {8}; Purchase Method: {9}",
                        contador++.ToString(), venta.SalesID, venta.SalesDate.ToString(), objetosTotal, venta.Customer.Email, venta.Customer.Gender, venta.Customer.Age,
                        venta.Location, venta.CouponUse.ToString(), venta.PurchaseMethod
                        ));

                }
            }
            else
                Console.WriteLine("No se encontró ninguna venta.");
        }







    }
}
