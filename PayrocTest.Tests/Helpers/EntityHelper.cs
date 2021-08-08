using System;
using System.Linq.Expressions;
using System.Reflection;

namespace PayrocTest.Tests.Helpers
{
    internal static class EntityHelper
    {
        public static void SetProperty<TClass, TValue>(
            this TClass source,
            Expression<Func<TClass, TValue>> property,
            TValue value) where TClass : class

        {
            var prop = (PropertyInfo)((MemberExpression)property.Body).Member;
            var setter = prop.GetSetMethod(true);
            if (setter != null)
            {
                prop.SetValue(source, value);
                return;
            }


            var field = prop.DeclaringType?.GetField($"<{prop.Name}>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
            if (field == null)
            {
                throw new Exception("no setter or backing field???");
            }

            field.SetValue(source, value);
        }

        public static TValue GetPrivateField<TClass, TValue>(this TClass source, string field)
        {
            return (TValue)typeof(TClass).GetField(field, BindingFlags.NonPublic | BindingFlags.Instance)
                ?.GetValue(source);
        }
    }
}
