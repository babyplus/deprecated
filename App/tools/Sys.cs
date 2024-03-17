namespace MyTools
{
    public class SysTool
    {
        static public void Exit(int code, String tips)
        {
            System.Console.WriteLine(tips);
            Environment.Exit(code); 
        }
    }
}
