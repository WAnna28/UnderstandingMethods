using System;

namespace UnderstandingMethods
{
    class Program
    {
        static void Main(string[] args)
        {
            sayHi(new DateTime(2020, 5, 8), "Hello World!");
            sayHi(DateTime.Now, "Hi!");
            sayHi(DateTime.Now);

            UnderstandingLocalFunctions();

            #region UsingOutModifier

            int ans;
            UsingOutModifier(10, 20, out ans);
            Console.WriteLine($"\nAnswer is: {ans}");
            // Starting with C# 7.0, out parameters do not need to be declared before using them.
            // In other words, they can be declared inside the method call
            UsingOutModifier(45, 2, out int ans2);
            Console.WriteLine($"Answer is: {ans2}");

            UsingOutModifiers(out int a, out int b, out int ans3);
            Console.WriteLine($"\na is: {a}");
            Console.WriteLine($"b is: {b}");
            Console.WriteLine($"Answer is: {ans3}");

            #endregion

            UsingRefModifiers();
            UsingInfModifiers();
            UsingParamsfModifiers();

            Console.ReadLine();
        }

        // C# 6 introduced expression-bodied members that shorten the syntax for single-line methods.
        // member => expression;
        // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/expression-bodied-members
        // C# allows you to create methods that can take optional arguments - string message = "Barev"
        static void sayHi(DateTime date, string message = "Barev") => Console.WriteLine($"{message}, today is: {date}."); 

        static int aa(int a, int b)
        {
            return a + b;
        }

        static int aa2(int a, int b) => a + b;

        // A feature introduced in C# 7.0 is the ability to create methods within methods, referred to officially as local functions.
        // A local function is a function declared inside another function, must be private, with C# 8.0 can be static,
        // and does not support overloading.
        // Local functions do support nesting: a local function can have a local function declared inside it.
        static void UnderstandingLocalFunctions()
        {
            Console.WriteLine($"\nDivide 5 to 2: {DivideWrapper(5, 2)}");
            Console.WriteLine($"Divide 8 to 0: {DivideWrapper(8, 0)}");
        }

        static double DivideWrapper(double x, double y)
        {
            if (y == 0) return -1;

            return Divide(x, y);

            static double Divide(double x, double y)
            {                
                return x / y;
            }
        }

        // Methods that have been defined to take output parameters (out)
        // are under obligation to assign them to an appropriate value before exiting the method scope
        // (if you fail to do so, you will receive compiler errors).
        static void UsingOutModifier(int x, int y, out int ans)
        {
            x += 18;
            y *= 5;
            ans = x + y;
        }

        // Returning multiple output parameters.
        static void UsingOutModifiers(out int x, out int y, out int ans)
        {
            x = 18;
            y = 5;
            ans = x + y;
        }

        // The value is initially assigned by the caller and may be optionally modified by
        // the called method(as the data is also passed by reference). No compiler error is
        // generated if the called method fails to assign a ref parameter.
        static void UsingRefModifiers()
        {
            string str1 = "Flip";
            string str2 = "Flop";
            Console.WriteLine("\nBefore: {0}, {1} ", str1, str2);
            SwapStrings(ref str1, ref str2);
            Console.WriteLine("After: {0}, {1} ", str1, str2);
        }

        public static void SwapStrings(ref string s1, ref string s2)
        {
            string tempStr = s1;
            s1 = s2;
            s2 = tempStr;
        }

        // New in C# 7.2, the in modifier indicates that a ref parameter
        // is read-only by the called method.
        // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/in-parameter-modifier
        static void UsingInfModifiers()
        {
            int a = 1958;
            int b = 445;
            Console.WriteLine("\nSum is: {0}", AddReadOnly(a, b));
        }

        static int AddReadOnly(in int x, in int y)
        {
            //Error CS8331 Cannot assign to variable 'in int' because it is a readonly variable
            //x = 10000;
            //y = 88888;
            return x + y;
        }

        // This parameter modifier allows you to send in a variable number of arguments as
        // a single logical parameter. A method can have only a single params modifier, and it
        // must be the final parameter of the method.
        static void UsingParamsfModifiers()
        {
            Console.WriteLine("\n************************** Using the params Modifier **************************");

            // Pass in a comma-delimited list of doubles...
            double average = CalculateAverage(4.0, 3.2, 5.7, 64.22, 87.2);
            Console.WriteLine("Average of data is: {0}", average);

            // ...or pass an array of doubles.
            double[] data = { 4.0, 3.2, 5.7 };
            average = CalculateAverage(data);
            Console.WriteLine("Average of data is: {0}", average);

            // Average of 0 is 0!
            Console.WriteLine("Average of data is: {0}", CalculateAverage());
        }

        // Return average of "some number" of doubles.
        static double CalculateAverage(params double[] values)
        {
            Console.WriteLine("You sent me {0} doubles.", values.Length);
            double sum = 0;
            if (values.Length == 0)
            {
                return sum;
            }
            for (int i = 0; i < values.Length; i++)
            {
                sum += values[i];
            }
            return sum / values.Length;
        }
    }
}