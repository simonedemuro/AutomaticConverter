using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MagicViewModelConverter.Test.Mock;
using NUnit.Framework;
using MagicViewModelConverter.ConversionMethods;

namespace MagicViewModelConverter.Test
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void GenericConversions()
        {
            int a = 1; a.GetType();
            double b=0;
            object c = GenericCast.Cast(b.GetType(), a);
            Console.Write(b);
        }

        [Test]
        /// <summary>
        /// This test Aims to verify that if you provide two objects with incompatible or not explicitly handled types 
        /// it Throws an exception of UnhandledConversion type.
        /// </summary>
        public void ThrowUnhandledException()
        {
            DB_OBJ DbObject = new DB_OBJ() { Id = 1, Field01 = 55, Timestamp = DateTime.Now };
            VM_OBJ VmObject = new VM_OBJ();

            MagicConverter magicConverter = new MagicConverter("dd/MM/yyyy HH:mm:ss");
            try
            {
                magicConverter.Convert(DbObject, VmObject);
            }
            catch (UnhandledConversion ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
