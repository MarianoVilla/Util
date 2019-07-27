using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rama
{

    public class Hero
    {

        public string Name { get; set; }

        public int Age { get; set; }

        public string Superpower { get; set; }


    }
    public class AlterEgo
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public string Job { get; set; }
    }
    public class Test : IHero
    {
        public string PropTest { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Superpower { get; set; }
    }
    public interface IHero
    {
        string Name { get; set; }

        int Age { get; set; }

        string Superpower { get; set; }
    }








    public class Escena
    {

        private void Test()
        {
            Hero Thor = new Hero();




        }
        



    }

    public class Villano
    {

        public string Nombre;

    }


}
