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
            MagicCloneConverter<C1, C2> mConv = MagicClone
                .New<C1, C2>()
                .Create();

            C2 res = mConv.DoTheMagic(c1);

            Assert.AreEqual(c1.attr1, res.attr1);
        }

        [Test]
        public void InjectCustomConvertionBehaviorTest()
        {
            // SETUP
            Aclass aclass = new Aclass() { attr1 = 5 };

            MagicCloneConverter<Aclass, Bclass> mConv = MagicClone.New<Aclass, Bclass>()
                //.AddBehaviour<int, float>(a => a * 0.5f)
                .AddBehaviour<int, float>(a => dummyBehave(a))
                .Create();

            Dictionary<string, Func<Object, Object>>  behaviors = new Dictionary<string, Func<Object, Object>>();

            // EXERCISE
            Bclass bclass = mConv.DoTheMagic(aclass);

            // ASSERT
            Assert.AreEqual(
                aclass.attr1.ToString() + ".5",
                bclass.attr1.ToString());
        }

    }
}
