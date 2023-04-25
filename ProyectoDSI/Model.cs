using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDSI
{
    public class Agente
    {
        static Random random = new Random();

        public int[] estados = new int[4];

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public string Clase { get; set; }
        public string Descripcion { get; set; }
        public int Nivel { get; set; }
        public int Experiencia { get; set; }
        public string ArmaPrincipal { get; set; }
        public string ArmaSecundaria { get; set; }
        public int Vida { get; set; }
        public int AtaqueMelee { get; set; }
        public int AtaqueDistancia { get; set; }
        public int CasillasMovimiento { get; set; }
        public int Precio { get; set; }

        public Agente(int id) {
            Id = id;
            Clase= "Clase " + id.ToString();
            Nombre = "Agente " + id.ToString();
            Imagen = "ms-appx:///Assets/Agentes/Agente" + id.ToString() + ".png";
            Descripcion = "Descripción " + id.ToString();
            Nivel = random.Next(1, Constants.MAX_LEVEL);
            Experiencia = random.Next(0, 100);
            ArmaPrincipal = "ArmaPrincipal " + id.ToString();
            ArmaSecundaria = "ArmaSecundaria " + id.ToString();
            Vida = random.Next(Constants.MIN_LIFE, 100);
            AtaqueMelee = random.Next(1, Constants.MAX_MELEE_ATTACK);
            AtaqueDistancia = random.Next(1, Constants.MAX_DIST_ATTACK);
            CasillasMovimiento = random.Next(2, Constants.MAX_MOVEMENT);
            Precio = random.Next(150, 251);
            for (int i = 0; i < 4; i++) { estados[i] = 0; }
        }
    }

    public class Casos
    {
        static Random random = new Random();

        public int Id;
        public string Nombre;
        public string Descripcion1;
        public string Descripcion2;
        public int Dificultad;

        public Casos(int id, string caso) {
            Id = id;
            Nombre = "Caso " + caso;
            Descripcion1 = "Este caso es el caso " + caso + ". Han habido un atraco a un banco y parece ser que no hay" + 
                "rastro del sospechoso. ¿Cómo puedo escapar tan rápido?";
            Descripcion2 = "No sabemos qué arma se ha usado en el caso " + caso + ". Hay balas de pistola en el suelo pero" +
                "no hay daños de bala en la escena del crimen. Hm...";
            Dificultad = random.Next(0, 6);
        }
    }

    public class Model
    {
        public static int money = 400;

        public static int getMoney() { return money; }

        public static List<Agente> ListaReclutas = new List<Agente>();

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
            new Agente(1),
            new Agente(2),
            new Agente(3),
            new Agente(4)
        };

        public static List<Casos> ListaCasos = new List<Casos>()
        {
            new Casos(1, "MEOWAL"),
            new Casos(2, "MEOWDELL"),
            new Casos(3, "MIAUNGUEN"),
            new Casos(4, "FRINMIAU"),
            new Casos(5, "COLMEOW"),
            new Casos(6, "MIAURPLE"),
            new Casos(7, "HALF-MEOW"),
            new Casos(8, "MIAUCITY")
        };

        public static IList<Agente> shuffleReclutas()
        {
            int i = 0;
            int random = 0;
            Random random1 = new Random();

            ListaReclutas.Clear();

            while (i < 10)
            {
                random = random1.Next(1, 9);
                ListaReclutas.Add(new Agente(random));
                ; i++;
            }

            return ListaReclutas;
        }

        public static IList<Agente> GetAllAgentes()
        {
            return ListaAgentes;
        }

        public static IList<Agente> GetAllSquad()
        {
            return ListaSquad;
        }

        public static IList<Agente> GetAllReclutas()
        {
            return ListaReclutas;
        }

        public static Agente GetAgenteById(int id)
        {
            return ListaAgentes[id - 1];
        }

        public static Casos GetCasoById(int id)
        {
            return ListaCasos[id - 1];
        }
    }
}
