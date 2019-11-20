using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Pipes;
using System.Diagnostics;
using System.Configuration;


namespace Auricular
{
    class Program
    {
        static void Main(string[] args)
        {
            String mensaje;
            //contectamos el auricular con el padre
            NamedPipeServerStream npss = new NamedPipeServerStream("pipe", PipeDirection.Out);
            StreamWriter sw = new StreamWriter(npss);
            npss.WaitForConnection();

            //conexion entre auricular y micro del otro
            NamedPipeClientStream npcs = new NamedPipeClientStream("192.168.37.173", "mic-aur", PipeDirection.In);
            StreamReader sr = new StreamReader(npcs);
            npcs.Connect();
            mensaje = sr.ReadLine();
            while (mensaje.ToUpper().CompareTo("FIN")!=0)
            {
                Console.WriteLine(mensaje);
                sw.WriteLine(mensaje);
                sr.ReadLine();  
            }

            
        }
    }
}
