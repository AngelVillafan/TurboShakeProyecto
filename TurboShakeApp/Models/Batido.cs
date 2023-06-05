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
    public enum Aditivos
    {
        Proteina,
        Creatina,
        Ambos
    }
    public class Batido
    {
        public string Nombre { get; set; }
        public byte Edad { get; set;}
        public double Peso { get; set; }
        public double Estatura { get; set; }
        public Sexo Sexo { get; set;}
        public byte ActividadFisicaSemanal { get; set; }
    }
}
