namespace Pudelko
{
    public class Program
    {
        static int Compare(Pudelko lhs, Pudelko rhs)
        {
            double lhsUnit = (double)lhs.Unit;
            double rhsUnit = (double)rhs.Unit;

            if (lhs.Objetosc * Math.Pow(lhsUnit, 3) > rhs.Objetosc * Math.Pow(rhsUnit, 3))
            {
                return 1;
            }
            if (lhs.Objetosc * Math.Pow(lhsUnit, 3) < rhs.Objetosc * Math.Pow(rhsUnit, 3))
            {
                Console.WriteLine($"{lhs}    {rhs}");
                Console.WriteLine($"{lhs.Objetosc} * {Math.Pow(lhsUnit, 3)} < {rhs.Objetosc} * {Math.Pow(rhsUnit, 3)}");
                return -1;
            }
            if (lhs.Pole * Math.Pow(lhsUnit, 2) > rhs.Pole * Math.Pow(rhsUnit, 2))
            {
                return 1;
            }
            if (lhs.Pole * Math.Pow(lhsUnit, 2) < rhs.Pole * Math.Pow(rhsUnit, 2))
            {
                return -1;
            }
            if ((lhs.A + lhs.B + lhs.C) * lhsUnit > (rhs.A + rhs.B + rhs.C) * rhsUnit)
            {
                return 1;
            }
            if ((lhs.A + lhs.B + lhs.C) * lhsUnit < (rhs.A + rhs.B + rhs.C) * rhsUnit)
            {
                return -1;
            }
            return 0;
        }

        public static void Main()
        {
            List<Pudelko> list = new List<Pudelko>();
            list.Add(new Pudelko(1, 2, 3));
            list.Add(new Pudelko(3, 1, 2));
            list.Add(new Pudelko(300, 400, 500, UnitOfMeasure.centimeter));
            list.Add(new Pudelko());
            list.Add(new Pudelko(21, 12, 43, UnitOfMeasure.milimeter));

            foreach(Pudelko p in list)
            {
                Console.WriteLine(p.ToString("mm"));
            }

            Comparison<Pudelko> comparison = new Comparison<Pudelko>(Compare);
            list.Sort(comparison);
            Console.WriteLine("\nPosortowane pudełka:");

            foreach (Pudelko p in list)
            {
                Console.Write($"{p} | ");
                foreach (var value in p)
                {
                    Console.Write($"{value} ");
                }
                Console.Write("| ");
                for (int i = 0; i < 3; i++)
                {
                    Console.Write($"p[{i}] = {p[i]} ");
                }
                Console.Write("\n");
            }
        }
    }
}
