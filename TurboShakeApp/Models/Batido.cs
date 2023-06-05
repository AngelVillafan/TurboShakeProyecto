using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboShakeApp.Models
{
    public enum Sexo
    {
        Femenino,
        Masculino
    }

    public class Batido
    {
        public string Nombre { get; set; }
        public string Edad { get; set;}
        public string Peso { get; set; }
        public double Estatura { get; set; }
        public Sexo Sexo { get; set;}
        public byte ActividadFisicaSemanal { get; set; }
        public string Aditivos { get; set; }
    }
}
