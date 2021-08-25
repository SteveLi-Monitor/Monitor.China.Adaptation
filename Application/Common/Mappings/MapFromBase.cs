using AutoMapper;

namespace Application.Common.Mappings
{
    public abstract class MapFromBase<T> : IMapFrom<T>
    {
        public virtual void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(T), GetType());
        }
    }
}
