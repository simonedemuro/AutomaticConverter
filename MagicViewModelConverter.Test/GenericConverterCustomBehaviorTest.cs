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
    public class GenericConverterCustomBehaviorTest
    {
        [ExcludeFromCodeCoverage]
        class Aclass { public int attr1 { get; set; } }

        [ExcludeFromCodeCoverage]
        class Bclass { public float attr1 { get; set; } }

        private float dummyBehave(object intVal)
        {
            float f = (int)intVal;
            f = f + (float)0.5;
            return f;
        }

        [ExcludeFromCodeCoverage]
        class C1 { public int attr1 { get; set; } public string attr2 { get; set; } private int ignoredHiHIHi;}

        [ExcludeFromCodeCoverage]
        class C2 { public int attr1 { get; set; } public string attr2 { get; set; } }

        [Test]
        public void PerfomVanillaCompatibleTypeConvertion()
        {
            C1 c1 = new C1() { attr1 = 2, attr2 = "ciao Andrea" };
            MagicClone<C1, C2> mConv = new MagicClone<C1, C2>(c1, new C2());

            C2 res = mConv.DoTheMagic();

            Assert.AreEqual(c1.attr1, res.attr1);
        }

        [Test]
        public void InjectCustomConvertionBehaviorTest()
        {
            // SETUP
            Aclass aclass = new Aclass() { attr1 = 5 };
            Bclass bclass;

            MagicClone<Aclass, Bclass> mConv = new MagicClone<Aclass, Bclass>(aclass, new Bclass());

            Dictionary<string, Func<Object, Object>>  behaviors = new Dictionary<string, Func<Object, Object>>();
            mConv.AddConvertionBehavior("System.Int32->System.Single", (f) => dummyBehave(f));

            // EXERCISE
            bclass = mConv.DoTheMagic();

            // ASSERT
            Assert.AreEqual(
                aclass.attr1.ToString() + ".5",
                bclass.attr1.ToString());
        }

    }
}
