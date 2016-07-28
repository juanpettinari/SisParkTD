using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using SisParkTD.Models;

namespace SisParkTD.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.SpContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        //protected override void Seed(DAL.SpContext context)
        //{
        //    var tamaños = new List<Tamaño>
        //    {
        //        new Tamaño {Descripcion = "Pequeños", ValorTamaño = 1},
        //        new Tamaño {Descripcion = "Mediano", ValorTamaño = 2},
        //        new Tamaño {Descripcion = "Grande", ValorTamaño = 3}
        //    };

        //    tamaños.ForEach(t => context.Tamaños.AddOrUpdate(p=>p.Descripcion,t));

        //    context.SaveChanges();

        //    var parcelas = new List<Parcela>
        //    {
        //        new Parcela {Disponible = true, NumeroParcela = 1, TamañoId = tamaños[0].TamañoId},
        //        new Parcela {Disponible = true, NumeroParcela = 2, TamañoId = tamaños[0].TamañoId},
        //        new Parcela {Disponible = true, NumeroParcela = 3, TamañoId = tamaños[1].TamañoId},
        //        new Parcela {Disponible = true, NumeroParcela = 4, TamañoId = tamaños[1].TamañoId},
        //        new Parcela {Disponible = true, NumeroParcela = 5, TamañoId = tamaños[1].TamañoId},
        //        new Parcela {Disponible = true, NumeroParcela = 6, TamañoId = tamaños[1].TamañoId},
        //        new Parcela {Disponible = true, NumeroParcela = 7, TamañoId = tamaños[2].TamañoId},
        //        new Parcela {Disponible = true, NumeroParcela = 8, TamañoId = tamaños[2].TamañoId}
        //    };

        //    parcelas.ForEach(p => context.Parcelas.AddOrUpdate(pa=> pa.NumeroParcela,p));



        //    var tiposDeVehiculo = new List<TipoDeVehiculo>
        //    {
        //        new TipoDeVehiculo {NombreDeTipoDeVehiculo = "Moto",TamañoId = tamaños[0].TamañoId,TarifaMensualDecimal = 800,TarifaOcasionalDecimal = 4},
        //        new TipoDeVehiculo {NombreDeTipoDeVehiculo = "Automóvil",TamañoId = tamaños[1].TamañoId,TarifaMensualDecimal = 2000,TarifaOcasionalDecimal = 10},
        //        new TipoDeVehiculo {NombreDeTipoDeVehiculo = "Camioneta",TamañoId = tamaños[2].TamañoId,TarifaMensualDecimal = 2400,TarifaOcasionalDecimal = 12}
        //    };

        //    tiposDeVehiculo.ForEach(tdv => context.TiposDeVehiculo.AddOrUpdate(p=> p.NombreDeTipoDeVehiculo,tdv));

        //    context.SaveChanges();
        //}
    }
}
