using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Reflection;


namespace ShowROT
{
    class Program
    {
        [DllImport("ole32.dll")]
        private static extern int CreateBindCtx(uint reserved, out IBindCtx ppbc);

        static void Main(string[] args)
        {

            IBindCtx ctx;
            IRunningObjectTable table;
            IEnumMoniker mon;
            IMoniker[] lst = new IMoniker[1];
            Object vcObject = null;
            int retVal;

            // find the File moniker object in the ROT running object table
            // When an instance of VS Express is running and a solution is open it creates a Solution object
            // and register it in the ROT
            retVal = CreateBindCtx(0, out ctx);
            if (retVal != 0)
            {
                Console.WriteLine("Error: CreateBindCtx failed with error " + retVal);
                return;
            }
            ctx.GetRunningObjectTable(out table);
            table.EnumRunning(out mon);
            //walk through the table
            while (mon.Next(1, lst, IntPtr.Zero) == 0)
            {
                string displayName;
                lst[0].GetDisplayName(ctx, lst[0], out displayName);

                Console.WriteLine(displayName);

            }
            Console.ReadKey();
        }
    }
}
