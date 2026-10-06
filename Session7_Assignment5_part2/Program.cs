namespace Session7_Assignment5_part2
{
    //--------------------------------First Project:-------------------------------
    public class Point3D : IComparable<Point3D>, ICloneable
    {
        //----------------- Q1: ---------------------------
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public Point3D(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Point3D(int x, int y) : this(x, y, 0) { }
        public Point3D(int x) : this(x, 0, 0) { }
        public Point3D() : this(0, 0, 0) { }
        //----------------- Q2: ---------------------------
        public override string ToString() 
        {
            return $"Point Coordinates: ({X}, {Y}, {Z})";
        }

        //----------------- Q4: ---------------------------
        public override bool Equals(object obj)
        {
            if (obj is Point3D p)
            {
                return X == p.X && Y == p.Y && Z == p.Z;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }

        public static bool operator ==(Point3D p1, Point3D p2)
        {
            if (ReferenceEquals(p1, p2)) return true;
            if (p1 is null || p2 is null) return false;
            return p1.Equals(p2);
        }

        public static bool operator !=(Point3D p1, Point3D p2)
        {
            return !(p1 == p2);
        }

        //----------------- Q5: ---------------------------
        public int CompareTo(Point3D? other)
        {
            if (other is null) return 1;
            int result = X.CompareTo(other.X);
            if (result == 0)
            {
                result = Y.CompareTo(other.Y);
            }
            return result;
        }
        //----------------- Q5: ---------------------------
        public object Clone()
        {
            return new Point3D(X, Y, Z);
        }
    }

    // -------------------------------- Second Project --------------------------------
    public static class Maths
    {
        public static double Add(double a, double b)
        {
            return a + b;
        }
        public static double Subtract(double a, double b)
        {
            return a - b;
        }
        public static double Multiply(double a, double b)
        {
            return a * b;
        }
        public static double Divide(double a, double b)
        {
            if (b == 0)
            {
                Console.WriteLine("Error: Division by zero is not allowed.");
                return double.NaN; 
            }
            return a / b;
        }
    }
    // -------------------------------- Third  Project --------------------------------
    public class Duration
    {
        // 1. Attributes / Properties
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }

        // Helper Property to calculate total seconds
        public int TotalSeconds => (Hours * 3600) + (Minutes * 60) + Seconds;
       
        // 2. Constructors
        // Constructor 1: Hours, Minutes, Seconds
        public Duration(int hours, int minutes, int seconds)
        {
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
        }
        // Constructor 2: Converts Total Seconds to Hours, Minutes, and Seconds
        public Duration(int totalSeconds)
        {
            if (totalSeconds < 0) totalSeconds = 0;

            Hours = totalSeconds / 3600;
            totalSeconds %= 3600;

            Minutes = totalSeconds / 60;
            Seconds = totalSeconds % 60;
        }
        // Default Constructor
        public Duration() : this(0, 0, 0) { }
        
        // 3. System.Object Overrides
        public override string ToString()
        {
            if (Hours > 0)
            {
                return $"Hours: {Hours}, Minutes :{Minutes}, Seconds :{Seconds}";
            }
            return $"Minutes :{Minutes}, Seconds :{Seconds}";
        }
        public override bool Equals(object? obj)
        {
            if (obj is Duration d)
            {
                return TotalSeconds == d.TotalSeconds;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(TotalSeconds);
        }
        // 4. Operators Overloading (تحميل المعاملات إضافياً)
        // ● D3 = D1 + D2
        public static Duration operator +(Duration d1, Duration d2)
        {
            return new Duration(d1.TotalSeconds + d2.TotalSeconds);
        }
        // ● D3 = D1 + 7800
        public static Duration operator +(Duration d1, int seconds)
        {
            return new Duration(d1.TotalSeconds + seconds);
        }
        // ● D3 = 666 + D3
        public static Duration operator +(int seconds, Duration d3)
        {
            return new Duration(seconds + d3.TotalSeconds);
        }
        // ● D3 = ++D1 (Increase One Minute)
        public static Duration operator ++(Duration d)
        {
            return new Duration(d.Hours, d.Minutes + 1, d.Seconds);
        }
        // ● D3 = --D2 (Decrease One Minute)
        public static Duration operator --(Duration d)
        {
            int newTotalSeconds = d.TotalSeconds - 60;
            return new Duration(newTotalSeconds < 0 ? 0 : newTotalSeconds);
        }
        // ● D1 = D1 - D2
        public static Duration operator -(Duration d1, Duration d2)
        {
            int diff = d1.TotalSeconds - d2.TotalSeconds;
            return new Duration(diff < 0 ? 0 : diff);
        }
        // ● If (D1 > D2)
        public static bool operator >(Duration d1, Duration d2)
        {
            return d1.TotalSeconds > d2.TotalSeconds;
        }
        public static bool operator <(Duration d1, Duration d2)
        {
            return d1.TotalSeconds < d2.TotalSeconds;
        }
        // ● If (D1 <= D2)
        public static bool operator <=(Duration d1, Duration d2)
        {
            return d1.TotalSeconds <= d2.TotalSeconds;
        }
        public static bool operator >=(Duration d1, Duration d2)
        {
            return d1.TotalSeconds >= d2.TotalSeconds;
        }
        // ● If (D1)
        public static bool operator true(Duration d)
        {
            return d.TotalSeconds > 0;
        }
        public static bool operator false(Duration d)
        {
            return d.TotalSeconds <= 0;
        }
        // ● DateTime Obj = (DateTime) D1
        public static explicit operator DateTime(Duration d)
        {
            DateTime today = DateTime.Today;
            return new DateTime(today.Year, today.Month, today.Day, d.Hours, d.Minutes, d.Seconds);
        }
    }

    internal class Program
    {
        //----------------- Q3: ---------------------------
        private static Point3D ReadPointFromUser(string pointName)
        {
            Console.WriteLine($"\nEnter coordinates for {pointName}:");
            Console.Write("Enter X (using int.Parse): ");
            int x = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Enter Y (using Convert.ToInt32): ");
            int y = Convert.ToInt32(Console.ReadLine());
            int z;
            do
            {
                Console.Write("Enter Z (using int.TryParse): ");
            } while (!int.TryParse(Console.ReadLine(), out z));

            return new Point3D(x, y, z);
        }
        static void Main(string[] args)
        {
            Console.WriteLine("--------------------------------First Project:-------------------------------");
            Console.WriteLine("\n----------------------- Q2: --------------------");
            Point3D P = new Point3D(10, 10);
            Console.WriteLine(P.ToString());

            Console.WriteLine("\n----------------------- Q3: --------------------");
            Point3D P1 = ReadPointFromUser("P1");
            Point3D P2 = ReadPointFromUser("P2");
            Console.WriteLine($"\nP1: {P1}");
            Console.WriteLine($"P2: {P2}");

            Console.WriteLine("\n----------------------- Q4: --------------------");
            if (P1 == P2)
            {
                Console.WriteLine("P1 and P2  are EQUAL in values.");
            }
            else
            {
                Console.WriteLine("P1 and P2  are NOT equal.");
            }

            Console.WriteLine("\n----------------------- Q5: --------------------");
            Point3D[] points = new Point3D[]
            {
                new Point3D(10, 20, 5),
                new Point3D(5, 30, 10),
                new Point3D(10, 10, 15),
                new Point3D(2, 5, 0)
            };
            Console.WriteLine("Points Before Sorting:");
            foreach (var point in points)
            {
                Console.WriteLine(point);
            }
            Array.Sort(points);
            Console.WriteLine("\nPoints After Sorting (by X then Y):");
            foreach (var point in points)
            {
                Console.WriteLine(point);
            }

            Console.WriteLine("\n----------------------- Q6: --------------------");
            Point3D originalPoint = new Point3D(10, 20, 30);
            Point3D clonedPoint = (Point3D)originalPoint.Clone();
            Console.WriteLine($"Original Point: {originalPoint}");
            Console.WriteLine($"Cloned Point:   {clonedPoint}");
            clonedPoint.X = 99;
            Console.WriteLine("\nAfter Modifying Cloned Point (X = 99):");
            Console.WriteLine($"Original Point: {originalPoint}");
            Console.WriteLine($"Cloned Point:   {clonedPoint}");

            Console.WriteLine("\n-------------------------------- Second Project --------------------------------");
            double num1 = 10;
            double num2 = 5;
            Console.WriteLine($"{num1} + {num2} = {Maths.Add(num1, num2)}");
            Console.WriteLine($"{num1} - {num2} = {Maths.Subtract(num1, num2)}");
            Console.WriteLine($"{num1} * {num2} = {Maths.Multiply(num1, num2)}");
            Console.WriteLine($"{num1} / {num2} = {Maths.Divide(num1, num2)}");

            Console.WriteLine("\n-------------------------------- Third Project --------------------------------");
            // 1. Testing Constructors
            Duration D1 = new Duration(1, 10, 15);
            Console.WriteLine($"D1 -> {D1}");

            Duration D1_Sec = new Duration(3600);
            Console.WriteLine($"D1 (3600) -> {D1_Sec}");

            Duration D2 = new Duration(7800);
            Console.WriteLine($"D2 (7800) -> {D2}");

            Duration D3 = new Duration(666);
            Console.WriteLine($"D3 (666) -> {D3}");

            Console.WriteLine("\n--- Operators Testing ---");

            // D3 = D1 + D2
            D3 = D1 + D2;
            Console.WriteLine($"D3 = D1 + D2 -> {D3}");

            // D3 = D1 + 7800
            D3 = D1 + 7800;
            Console.WriteLine($"D3 = D1 + 7800 -> {D3}");

            // D3 = 666 + D3
            D3 = 666 + D3;
            Console.WriteLine($"D3 = 666 + D3 -> {D3}");

            // D3 = ++D1 (Increase 1 Minute)
            D3 = ++D1;
            Console.WriteLine($"D3 = ++D1 -> {D3}");

            // D3 = --D2 (Decrease 1 Minute)
            D3 = --D2;
            Console.WriteLine($"D3 = --D2 -> {D3}");

            // D1 = D1 - D2
            D1 = D1 - D2;
            Console.WriteLine($"D1 = D1 - D2 -> {D1}");

            // If (D1 > D2)
            if (D1 > D2)
                Console.WriteLine("D1 is greater than D2");
            else
                Console.WriteLine("D1 is NOT greater than D2");

            // If (D1 <= D2)
            if (D1 <= D2)
                Console.WriteLine("D1 is less than or equal to D2");

            // If (D1)
            if (D1)
                Console.WriteLine("D1 is True (Total Seconds > 0)");

            // DateTime Obj = (DateTime) D1
            DateTime Obj = (DateTime)D1;
            Console.WriteLine($"DateTime Obj = (DateTime) D1 -> {Obj}");
        }
    }
}
