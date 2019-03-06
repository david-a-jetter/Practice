using System;
using System.Collections.Generic;
using System.Text;

namespace Practice.StackOverflow
{
    public interface IWasLeftOutForBrevity
    {

    }

    public interface IHandle<T>
    {
        void Handler(T param);
    }

    public abstract class AbsBase : IWasLeftOutForBrevity, IHandle<int>
    {
        public abstract void Handler(int param);

        public void SomeFunction<T>(T param)
        {
            int? intParam = param as int?;

            if (! (intParam is null))
            {
                Handler(intParam.Value);
            }
        }
    }

    public class Derived : AbsBase, IHandle<decimal>
    {
        public override void Handler(int param)
        {
            // ...
        }
        public void Handler(decimal param)
        {
            // ...
        }
    }

}
