using CsvHelper.Configuration;
using System;
using System.Linq.Expressions;

namespace Console.Models.ClassMaps
{
    internal abstract class ClassMapBase<TClass> : ClassMap<TClass>
    {
        public MemberMap<TClass, TMember> MapIndex<TMember>(Expression<Func<TClass, TMember>> expression, int index, bool useExistingMap = true)
        {
            if (index < 0) return null;

            return Map(expression, useExistingMap).Index(index);
        }
    }
}
