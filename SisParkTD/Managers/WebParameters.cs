using System;
using System.Collections.Generic;


/// <summary>
/// Singleton Pattern
/// </summary>
namespace SisParkTD.Managers
{
    public class WebParameters
    {

        // PONER ACA LA string de ruta donde irían los back ups
        // PONER ACA LA string de ruta donde irían los back ups
        // PONER ACA LA string de ruta donde irían los back ups
        // PONER ACA LA string de ruta donde irían los back ups
        // PONER ACA LA string de ruta donde irían los back ups
        // PONER ACA LA string de ruta donde irían los back ups
        // PONER ACA LA string de ruta donde irían los back ups
        // PONER ACA LA string de ruta donde irían los back ups

        private static WebParameters Instance { get; set; }
        private Random _random = new Random();

        const string WebName = "SisPark";
        const float IVA = 21;
        private List<string> Parameters = new List<string>();

        // Lock synchronization object
        private static object syncLock = new object();

        // Constructor (protected)
        protected WebParameters()
        {
            // List of available servers
            Parameters.Add("ServerI");
            Parameters.Add("ServerII");
            Parameters.Add("ServerIII");
            Parameters.Add("ServerIV");
            Parameters.Add("ServerV");
        }

        public static WebParameters GetParameters()
        {
            // Support multithreaded applications through
            // 'Double checked locking' pattern which (once
            // the instance exists) avoids locking each
            // time the method is invoked
            if (Instance == null)
            {
                lock (syncLock)
                {
                    if (Instance == null)
                    {
                        Instance = new WebParameters();
                    }
                }
            }

            return Instance;
        }

        // Simple, but effective random load balancer
        public string Server
        {
            get
            {
                int r = _random.Next(Parameters.Count);
                return Parameters[r].ToString();
            }
        }
    }
}