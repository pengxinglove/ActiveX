using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace ActiveX_Web
{
    public class Class1
    {
        [ProgId("DemoCSharpActiveX.HelloWorld")]

        [ClassInterface(ClassInterfaceType.AutoDual)]

        [Guid("EDBFDE1E-5A09-4982-A638-91788B827716")]

        [ComVisible(true)]

        public class HelloWord

        {

            [ComVisible(true)]

            public String SayHello()

            {

                return "Hello World!";

            }

        }

    }
}

