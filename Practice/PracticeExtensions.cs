using System;
using System.Collections.Generic;

namespace Practice
{
    public static class PracticeExtensions
    {
        public static void ThrowIfNull<T>(T value, string name)
        {
            if (EqualityComparer<T>.Default.Equals(value, default(T))
                    && default(T) == null)
            {
                throw new ArgumentNullException(name);
            }
        }
    }
}
