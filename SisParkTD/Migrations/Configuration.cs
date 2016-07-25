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

        protected override void Seed(DAL.SpContext context)
        {
            var tamaños = new List<Tamaño>
            {
                new Tamaño {Descripcion = "Pequeño", ValorTamaño = 1},
                new Tamaño {Descripcion = "Mediano", ValorTamaño = 2},
                new Tamaño {Descripcion = "Grande", ValorTamaño = 3}
            };

            tamaños.ForEach(t => context.Tamaños.AddOrUpdate(t));

            var parcelas = new List<Parcela>
            {
                new Parcela {Disponible = true, NumeroParcela = 1, Tamaño = tamaños[0]},
                new Parcela {Disponible = true, NumeroParcela = 2, Tamaño = tamaños[0]},
                new Parcela {Disponible = true, NumeroParcela = 3, Tamaño = tamaños[1]},
                new Parcela {Disponible = true, NumeroParcela = 4, Tamaño = tamaños[1]},
                new Parcela {Disponible = true, NumeroParcela = 5, Tamaño = tamaños[1]},
                new Parcela {Disponible = true, NumeroParcela = 6, Tamaño = tamaños[1]},
                new Parcela {Disponible = true, NumeroParcela = 7, Tamaño = tamaños[2]},
                new Parcela {Disponible = true, NumeroParcela = 8, Tamaño = tamaños[2]}
            };

            parcelas.ForEach(p => context.Parcelas.AddOrUpdate(p));

            var tiposDeVehiculo = new List<TipoDeVehiculo>
            {
                new TipoDeVehiculo {NombreDeTipoDeVehiculo = "Moto",Tamaño = tamaños[0],TarifaMensualDecimal = 1500,TarifaOcasionalDecimal = 5M},
                new TipoDeVehiculo {NombreDeTipoDeVehiculo = "Automóvil",Tamaño = tamaños[1],TarifaMensualDecimal = 2900,TarifaOcasionalDecimal = 10M},
                new TipoDeVehiculo {NombreDeTipoDeVehiculo = "Camioneta",Tamaño = tamaños[2],TarifaMensualDecimal = 3500,TarifaOcasionalDecimal = 13M}
            };



        }
    }
}
