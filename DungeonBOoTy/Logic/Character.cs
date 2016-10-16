using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonBOoTy
{
    class Character
    {
        public int PV { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int Dex { get; set; }
        public int Str { get; set; }
        public int Lck { get; set; }
        public int Int { get; set; }
        public int Pow { get; set; }
        public int Spd { get; set; }
        public int Exp { get; set; }
        public string Description { get; internal set; }

        //private Class specialization;
        //private Abilities abilities

        public Character()
        {
            Name = "John Doe";
            Description = "No history";
            Level = 1;
            FillAttributes();
            Exp = 0;
        }

        private void FillAttributes()
        {
            Random r = new Random();
            Dex = r.Next(3,10);
            Str = r.Next(3, 10);
            Int = r.Next(3, 10);
            Lck = r.Next(3, 10);
            Pow = r.Next(3, 10);
            Spd = (Dex + Str) * 10;
            PV = (Str + Pow) * 10;
        }

        override
        public string ToString()
        {
            return "Nombre: " + Name + " Nivel: " + Level + " PV: "+PV + " EXP: " + Exp + " \nDescription: "+ Description;
        }

        public string Stats()
        {
            return "Dex: " + Dex + " Str: " + Str + " Int: " + Int + " Pow: " + Pow + " Lck: " + Lck + " Spd: " + Spd;
        }
    
    }
}
