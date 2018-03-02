namespace Core.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class UnBoxingHelper
    {
        public static TDerived ToDerived<TBase, TDerived>(TBase tBase) where TDerived : TBase, new()
        {
            TDerived tDerived = new TDerived();
            foreach (PropertyInfo propBase in typeof(TBase).GetProperties())
            {
                PropertyInfo propDerived = typeof(TDerived).GetProperty(propBase.Name);

                if (propDerived.CanWrite)
                {
                    try
                    {
                        var value = propBase.GetValue(tBase, null);

                        if (value != null)
                            propDerived.SetValue(tDerived, value, null);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }
            return tDerived;
        }

        public static void To<S, D>(S src, ref D dest, params KeyValuePair<string, string>[] hook)
        {
            foreach (var property in typeof(D).GetProperties())
            {
                PropertyInfo prop = null;
                var patch = hook.FirstOrDefault(x => x.Key == property.Name).Value;

                if (!string.IsNullOrEmpty(patch))
                    prop = typeof(S).GetProperty(patch);
                else
                    prop = typeof(S).GetProperty(property.Name);

                if (prop != null && property.CanWrite)
                    property.SetValue(dest, Convert.ChangeType(prop.GetValue(src), prop.PropertyType));
            }
        }

        public static D To<S, D>(S src, params KeyValuePair<string, string>[] hook)
        {
            D result = (D)Activator.CreateInstance(typeof(D));

            To<S, D>(src, ref result, hook);

            return result;
        }

        public static void To<S, D>(S src, ref D dest, Dictionary<string, string> map) => To<S, D>(src: src, dest: ref dest, hook: map.Select(x => x).ToArray());
    }
}
