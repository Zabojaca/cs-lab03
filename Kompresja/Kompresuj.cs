using P = Pudelko.Pudelko;
using Pudelko;

namespace Kompresja
{
    public static class Kompresja
    {
        public static P Kompresuj(P obj)
        {            
            double a = Math.Cbrt(obj.Objetosc);
            return new P(a, a, a, UnitOfMeasure.meter);
        }

        public static void Main()
        {
            P p = new P(2, 8, 4);
            var skompresowane = Kompresuj(p);
            Console.WriteLine(skompresowane);
        }
    }
}