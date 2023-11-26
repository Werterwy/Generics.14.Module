using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics._14.Module
{
    public class Karta : IKartaConvertibl
    {
        public string Mast { get; set; }
        public string Tip { get; set; }

        public Karta(string mast, string tip)
        {
            Mast = mast;
            Tip = tip;
        }
        public Karta ToKarta()
        {
            return this;
        }

    }

    public interface IKartaConvertibl
    {
        Karta ToKarta();
    }
}
