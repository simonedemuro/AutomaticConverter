using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MagicViewModelConverter.Test.Mock;
using NUnit.Framework;
using MagicViewModelConverter.ConversionMethods;
using System.Diagnostics.CodeAnalysis;

namespace MagicViewModelConverter.Test
{
    [TestFixture]
    public class MethodsTest
    {
        [Test]
        /// <summary>
        /// GenericCast.Cast provides an easy way to cast every compatible type to Object 
        /// </summary> 
        public void GenericConversions()
        {
            // SETUP
            int a = 1;
            double b=0;

            // EXERCISE
            object c = GenericCast.Cast(b.GetType(), a);

            // ASSERT
            Assert.That(Convert.ToInt32(c) == 1);
        }

        [Test]
        /// <summary>
        /// This test Aims to verify that if you provide two objects with incompatible or not explicitly handled types 
        /// it Throws an exception of UnhandledConversion type.
        /// </summary>
        public void ThrowUnhandledException()
        {
            // SETUP
            DB_OBJ DbObject = new DB_OBJ() { Id = 1, Field01 = 55, Timestamp = DateTime.Now };
            VM_OBJ VmObject = new VM_OBJ();
            
            MagicConverter magicConverter = new MagicConverter("dd/MM/yyyy HH:mm:ss");

            // EXERCISE
            var ex = Assert.Throws<UnhandledConversion>(() => magicConverter.Convert(DbObject, VmObject));

            // ASSERT
            Assert.That(ex.Message, Is.EqualTo("Unhandled conversion From Type: Int32 to: Double"));
        }

    }
}
