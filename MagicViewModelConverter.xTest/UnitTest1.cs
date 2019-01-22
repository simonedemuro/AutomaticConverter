using System;
using Xunit;

namespace MagicViewModelConverter.xTest
{
    public class UnitTest1
    {
        [Theory]
        public void HowItWouldBe()
        {
            Aclass aclass = new Aclass() { attr1 = 5 };
            Bclass bclass = new Bclass();
            MagicConv<Aclass, Bclass> mConv =
                new MagicConv<Aclass, Bclass>(aclass, bclass);


            mConv.MyBehaviors = new Dictionary<string, Func<Object, Object>>();
            mConv.MyBehaviors.Add("prova", (f) => dummyBehave(f));
            mConv.DoTheMagic();

            Assert.AreEqual(
                aclass.attr1.ToString() + ".5",
                bclass.attr1.ToString());
        }
    }
}
