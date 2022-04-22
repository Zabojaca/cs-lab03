using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pudelko
{
    public sealed class Pudelko : IEquatable<Pudelko>, IFormattable, IEnumerable
    {
        private readonly double _a;
        private readonly double _b;
        private readonly double _c;
        private readonly UnitOfMeasure _unit;
        public double Objetosc { get; private set; }
        public double Pole { get; private set; }

        public double A
        {
            get
            {
                return _a * (double)Unit / 1000.0;
            }
        }

        public double B
        {
            get
            {
                return _b * (double)Unit / 1000.0;
            }
        }

        public double C
        {
            get
            {
                return _c * (double)Unit / 1000.0;
            }
        }

        public UnitOfMeasure Unit
        {
            get { return _unit; }
        }

        public Pudelko(double? a = null, double? b = null, double? c = null, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            if(a == null)
            {
                a = 100 / (double)unit;
            }
            if (b == null)
            {
                b = 100 / (double)unit;
            }
            if (c == null)
            {
                c = 100 / (double)unit;
            }

            int precision = 3;
            if(unit == UnitOfMeasure.milimeter)
            {
                precision = 0;
            }
            else if(unit == UnitOfMeasure.centimeter)
            {
                precision = 1;
            }
            a = Math.Round((double)a, precision, MidpointRounding.ToZero);
            b = Math.Round((double)b, precision, MidpointRounding.ToZero);
            c = Math.Round((double)c, precision, MidpointRounding.ToZero);

            if (a * (double)unit < 1 || b * (double)unit < 1 || c * (double)unit < 1)
            {
                throw new ArgumentOutOfRangeException("Arguments must be positive");
            }
            if (a * (double)unit > 10000 || b * (double)unit > 10000 || c * (double)unit > 10000)
            {
                throw new ArgumentOutOfRangeException("Arguments must be less than or equal to 10 m");
            }

            _a = (double)a;
            _b = (double)b;
            _c = (double)c;
            _unit = unit;
            Objetosc = Math.Round(A * B * C * (double)unit / 1000, 9);
            Pole = Math.Round((A * B * 2 / 1000 + A * C * 2 + B * C * 2) * (double)unit / 1000, 6);
        }

        public string ToString(string? format, IFormatProvider formatProvider = null)
        {
            if(format == null)
            {
                switch (Unit)
                {
                    case UnitOfMeasure.milimeter:
                        format = "mm";
                        break;
                    case UnitOfMeasure.centimeter:
                        format = "cm";
                        break;
                    case UnitOfMeasure.meter:
                        format = "m";
                        break;
                }
            }
            double a, b, c;
            string stringFormat;
            if (format == "m")
            {
                a = A * (double)Unit / 1000;
                b = B * (double)Unit / 1000;
                c = C * (double)Unit / 1000;
                stringFormat = "F3";
            }
            else if (format == "cm")
            {
                a = A * (double)Unit / 10;
                b = B * (double)Unit / 10;
                c = C * (double)Unit / 10;
                stringFormat = "F1";
            }
            else if (format == "mm")
            {
                a = A * (double)Unit;
                b = B * (double)Unit;
                c = C * (double)Unit;
                stringFormat = "F0";
            }
            else
            {
                throw new FormatException();
            }

            return $"{a.ToString(stringFormat, formatProvider)} {format} × {b.ToString(stringFormat, formatProvider)} {format} × {c.ToString(stringFormat, formatProvider)} {format}";
        }

        public bool Equals(Pudelko? obj)
        {
            if (obj == null)
            {
                return false;
            }

            double AMilimeters = A * (double)Unit;
            double BMilimeters = B * (double)Unit;
            double CMilimeters = C * (double)Unit;
            double OtherAMilimeters = obj.A * (double)obj.Unit;
            double OtherBMilimeters = obj.B * (double)obj.Unit;
            double OtherCMilimeters = obj.C * (double)obj.Unit;
            if (AMilimeters == OtherAMilimeters && BMilimeters == OtherBMilimeters && CMilimeters == OtherCMilimeters)
            {
                return true;
            }
            if (AMilimeters == OtherBMilimeters && BMilimeters == OtherCMilimeters && CMilimeters == OtherAMilimeters)
            {
                return true;
            }
            if (AMilimeters == OtherCMilimeters && BMilimeters == OtherAMilimeters && CMilimeters == OtherBMilimeters)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(Pudelko lhs, Pudelko rhs)
        {
            if(lhs == null)
            {
                if(rhs == null)
                {
                    return true;
                }
                return false;
            }
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Pudelko lhs, Pudelko rhs)
        {
            return !(lhs == rhs);
        }

        public static Pudelko operator +(Pudelko lhs, Pudelko rhs)
        {
            double LhsAMilimeters = lhs.A * (double)lhs.Unit;
            double LhsBMilimeters = lhs.B * (double)lhs.Unit;
            double LhsCMilimeters = lhs.C * (double)lhs.Unit;

            double RhsAMilimeters = rhs.A * (double)rhs.Unit;
            double RhsBMilimeters = rhs.B * (double)rhs.Unit;
            double RhsCMilimeters = rhs.C * (double)rhs.Unit;

            return new Pudelko(LhsAMilimeters + RhsAMilimeters, LhsBMilimeters + RhsBMilimeters, LhsCMilimeters + RhsCMilimeters, UnitOfMeasure.milimeter);
        }

        public static explicit operator double[](Pudelko pudelko) => new double[] { pudelko.A, pudelko.B, pudelko.C };

        public static implicit operator Pudelko(ValueTuple<int, int, int> values) => new Pudelko(values.Item1, values.Item2, values.Item3, UnitOfMeasure.milimeter);

        public double this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0:
                        return A;
                    case 1:
                        return B;
                    case 2:
                        return C;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }

        public IEnumerator GetEnumerator()
        {
            yield return A;
            yield return B;
            yield return C;
        }

        public static Pudelko Parse(string text)
        {
            String textUnit = text.Substring(text.Length - 2, 2);
            UnitOfMeasure unit;
            if (textUnit == "cm")
            {
                unit = UnitOfMeasure.centimeter;
            }
            else if (textUnit == "mm")
            {
                unit = UnitOfMeasure.milimeter;
            }
            else if (textUnit == " m")
            {
                unit = UnitOfMeasure.meter;
            }
            else
            {
                throw new ArgumentException();
            }
            string[] values = text.Split($" {textUnit.Trim()} × ");
            double a = Convert.ToDouble(values[0]);
            double b = Convert.ToDouble(values[1]);
            double c = Convert.ToDouble(values[2]);
            return new Pudelko(a, b, c, unit);
        }
    }
}