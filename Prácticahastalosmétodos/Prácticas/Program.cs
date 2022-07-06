// See https://aka.ms/new-console-template for more information
public class Práctica_Métodos
{
    static void Main()
    {
        int Opcion;
        int num1;
        int num2;
        int num3;
        const double PI=3.1416;
         float num4;
        string metodoCargado;
        num1 = num2 = 0; 
        Console.WriteLine("Eliije la opción que desee");
        Console.WriteLine("1.- Suma de dos números");
        Console.WriteLine("2.- Conversión éxplicita de números");
        Console.WriteLine("3.- Área de un círculo");
        Console.WriteLine("4.- Conversión Implicita");
        Opcion = int.Parse(Console.ReadLine());
        switch (Opcion) {
            case 1:
                Console.WriteLine("Ingrese el número 1");
                num1 = int.Parse(Console.ReadLine());
                Console.WriteLine("Ingrese el número 2");
                num2 = int.Parse(Console.ReadLine());
                Console.WriteLine("A que método llamamos??");
                metodoCargado = Console.ReadLine();
                if (metodoCargado == "1")
                {
                    Console.WriteLine(SumaNum(num1, num2));
                } else if (metodoCargado == "2")

                {
                    Console.WriteLine("Ingrese otro valor");
                    num3 = int.Parse(Console.ReadLine());
                    Console.WriteLine(SumaNum(num1, num2, num3));
                }
            
                break;
                case 2:
                Console.WriteLine("Ingrese un número entero");
                num1=int.Parse(Console.ReadLine());
                Console.WriteLine((float)num1);
                break;
            case 3:
                Console.WriteLine("Ingrese el radio del circulo");
                num1= int.Parse(Console.ReadLine());
                Console.WriteLine("El radio del circulo es:"+(num1*num1*PI));
                Console.WriteLine("Pi es:{0}",PI);
                break;
        }
    }

    public static int SumaNum(int num1, int num2) => num1 + num2;
    public static int SumaNum(int num1, int num2, int num3=0)=> num1+num2+num3;
    


}
