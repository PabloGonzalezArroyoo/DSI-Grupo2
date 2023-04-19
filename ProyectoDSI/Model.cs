using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDSI
{
    public class Agente
    {
        public int[] estados = new int[4];

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
        public int Nivel { get; set; }
        public int Experiencia { get; set; }
        public string ArmaPrincipal { get; set; }
        public string ArmaSecundaria { get; set; }
        public int Vida { get; set; }
        public int AtaqueMelee { get; set; }
        public int AtaqueDistancia { get; set; }
        public int CasillasMovimiento { get; set; }

        public Agente(int id) {
            Id = id;
            Nombre = "Agente" + id.ToString();
            Imagen = "Assets\\Agentes\\Agente" + id.ToString() + ".png";
            Descripcion = "Descripción" + id.ToString();
            Nivel = 10;
            Experiencia = 50;
            ArmaPrincipal = "ArmaPrincipal" + id.ToString();
            ArmaSecundaria = "ArmaSecundaria" + id.ToString();
            Vida = 100;
            AtaqueMelee = 10;
            AtaqueDistancia = 50;
            CasillasMovimiento = 6;
            for (int i = 0; i < 4; i++) { estados[i] = 0; }
        }
    }

    public class Model
    {
        public static List<Agente> ListaAgentes = new List<Agente>()
        {
            new Agente(1),
            new Agente(2),
            new Agente(3),
            new Agente(4),
            new Agente(5),
            new Agente(6),
            new Agente(7),
            new Agente(8)
        };

        public static List<Agente> ListaSquad = new List<Agente>()
        {
            ListaAgentes[0],
            ListaAgentes[1],
            ListaAgentes[2],
            ListaAgentes[3]
        };

        public static IList<Agente> GetAllAgentes()
        {
            return ListaAgentes;
        }

        public static IList<Agente> GetAllSquad()
        {
            return ListaSquad;
        }

        public static Agente GetAgenteById(int id)
        {
            return ListaAgentes[id - 1];
        }
    }
}
