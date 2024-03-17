using System.Globalization;
using MyModels; 

namespace MyTools
{
    public class ColorTool
    {
        static public string Generate()
        {
            Random rnd = new Random();
            return $"#{rnd.Next(6, 13):X}{rnd.Next(6, 13):X}{rnd.Next(6, 13):X}{rnd.Next(6, 13):X}{rnd.Next(6, 13):X}{rnd.Next(6, 13):X}";
        }

        static public string Offset(String raw, int offset)
        {
            int R,G,B = 0;
            if ( int.TryParse(raw.Substring(1,2), System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture, out R)
            && int.TryParse(raw.Substring(3,2), System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture, out G)
            && int.TryParse(raw.Substring(5,2), System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture, out B))
                return  $"#{R+offset:X}{G+offset:X}{B+offset:X}";
            else
                return "error";
        }
    }
}
