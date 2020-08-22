using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2
{
    class Program
    {
        static void Main(string[] args)
        {
            DoJob();
            
        }

        static void DoJob()
        {
            var elTrabajador = new Trabajador();
            elTrabajador.Trabajo01();
        }
    }
}
