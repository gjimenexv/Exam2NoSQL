using Ferreteria.Model.Modelos;
using MongoDB.Bson;
using MongoDB.Driver;
using Ninject.Activation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.AccesoADatos
{
    public class Conexion
    {
        public IMongoDatabase ConectarBD()
        {

            var client = new MongoClient("mongodb+srv://rwuser:12345@myatlascluster.tr3q4.gcp.mongodb.net/sample_supplies?retryWrites=true&w=majority");
            var database = client.GetDatabase("sample_supplies");
            return database;

        }

        public IList<Sales> ListarVentas()
        {
            var laBaseDeDatos = ConectarBD();
            var collection = laBaseDeDatos.GetCollection<Sales>("sales");
            var filter = new BsonDocument();
            var elResultado = collection.Find(filter).ToList();
            return elResultado;
        }

        public IList<Sales> BuscarVentasEntreFechas(string FechaInicio, string FechaFinal)
        {
            var laBaseDeDatos = ConectarBD();
            var collection = laBaseDeDatos.GetCollection<Sales>("sales");
            var builder = Builders<Sales>.Filter;
            var query = builder.Gte("SalesDate", ValidarFechasCore(FechaInicio)) & builder.Lte("SalesDate", ValidarFechasCore(FechaFinal));
            var elResultado = collection.Find(query);
            elResultado.Options.Limit = 5;
            var resultado = elResultado.ToListAsync().Result;
            return resultado;
        }
        private DateTime ValidarFechasCore(string Fecha)
        {
            CultureInfo culture = new CultureInfo("en-US");
            DateTime tempDate = Convert.ToDateTime(Fecha, culture);
            return tempDate;
        }


        public IList<Sales> ListarVentasPorTag(string Tag)
        {
            var laBaseDeDatos = ConectarBD();
            var collection = laBaseDeDatos.GetCollection<Sales>("sales");
            var expresssionFilter = Builders<Sales>.Filter.Regex(x => x.Items[0].Tags, Tag);
            var elResultado = collection.Find(expresssionFilter);
            elResultado.Options.Limit = 10;
            var resultado = elResultado.ToListAsync().Result;
            return resultado;
        }

        public IList<Sales> ListarVentasPorEmail(string correo)
        {
            var laBaseDeDatos = ConectarBD();
            var collection = laBaseDeDatos.GetCollection<Sales>("sales");
            var expresssionFilter = Builders<Sales>.Filter.Regex(x => x.Customer.Email, "^" + correo + ".*");
            var elResultado = collection.Find(expresssionFilter);
            elResultado.Options.Limit = 10;
            var resultado = elResultado.ToListAsync().Result;
            return resultado;
        }

        public void InsertarVenta(Sales ventas)
        {
            var laBaseDeDatos = ConectarBD();
            var collection = laBaseDeDatos.GetCollection<Sales>("sales");
            collection.InsertOne(ventas);
        }

        public void BorrarTags(ObjectId elIdDelCliente, Sales venta)
        {
            var laBaseDeDatos = ConectarBD();
            var collection = laBaseDeDatos.GetCollection<Sales>("sales");
            var filter = Builders<Sales>.Filter.Eq(x => x.SalesID, venta.SalesID);
            var update = Builders<Sales>.Update.Set("Items.$[f].Tags.$", string.Empty);
            var arrayFilters = new[]
            {
                new BsonDocumentArrayFilterDefinition<BsonDocument>(
                    new BsonDocument("f.Items",new BsonDocument("$eq", new BsonArray(new [] { venta.Items[0].Tags })))),
            };

            collection.UpdateOne(filter, update, new UpdateOptions { ArrayFilters = arrayFilters });

        }

        public void ActualizarEdadDelCliente(ObjectId elIdDelCliente, string laNuevaEdad)
        {
            var laBaseDeDatos = ConectarBD();
            var collection = laBaseDeDatos.GetCollection<Sales>("sales");
            var filter = Builders<Sales>.Filter.Eq("_id", elIdDelCliente);
            var update = Builders<Sales>.Update.Set("Customer.Age", laNuevaEdad);
            collection.UpdateOne(filter, update);
        }

    }
}
