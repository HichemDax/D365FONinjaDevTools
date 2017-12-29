using System;
using System.Linq;
using Microsoft.Dynamics.AX.Metadata.MetaModel;

namespace D365FONinjaDevTools.Kernel
{
    public class NAxClass : AxClass
    {
        public static AxClass Construct(string name, dynamic baseElement = null)
        {
            AxClass myClass = new AxClass {Name = name};

            if (baseElement != null)
            {
                string baseClassInterfaceName = baseElement.Caption;
                AxClass myBaseClassInterface = LocalUtils.MetaService.GetClass(baseClassInterfaceName);

                if (myBaseClassInterface.IsInterface)
                {
                    myClass.Implements = new System.Collections.Generic.List<string> {baseClassInterfaceName};

                    foreach (var axMethod in myBaseClassInterface.Methods)
                        myClass.Methods.Add(axMethod);
                }
                else
                {
                    myClass.Extends = baseClassInterfaceName;

                    if (myBaseClassInterface.IsFinal)
                        throw new Exception("Selected class is set as final, it cannot be extended");

                    if (myBaseClassInterface.IsAbstract)
                    foreach (var axAbstractMethod in myBaseClassInterface.Methods.Where(m => m.IsAbstract))
                    {
                        axAbstractMethod.IsAbstract = false;
                        axAbstractMethod.Source = axAbstractMethod.Source.Replace("abstract ", "");
                        myClass.Methods.Add(axAbstractMethod);
                    }
                }
            }

            return myClass;
        }
    }
}