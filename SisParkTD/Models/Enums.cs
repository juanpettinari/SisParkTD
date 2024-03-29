﻿namespace SisParkTD.Models
{
    public enum EstadoDeTicket
    {
        Activo = 1,
        Inactivo,
        Historico
    }

    public enum TipoDeTicket
    {
        Ocasional = 1,
        Abono
    }

    public enum Signo
    {
        Negativo = -1,
        Positivo = 1
    }

    public enum TipoDeMovimientoDeVehiculo
    {
        Entrada = 1,
        Salida
    }

    public enum TamanioVehiculo
    {
        Chico = 1,
        Mediano,
        Grande
    }

    public enum Mes
    {
        Enero = 1,
        Febrero,
        Marzo,
        Abril,
        Mayo,
        Junio,
        Julio,
        Agosto,
        Septiembre,
        Octubre,
        Noviembre,
        Diciembre
    }

    public enum TipoDocumento
    {
        Dni = 1,
        Cuit
    }
}