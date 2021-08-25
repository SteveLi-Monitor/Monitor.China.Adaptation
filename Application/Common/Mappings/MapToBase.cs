using AutoMapper;

namespace Application.Common.Mappings
{
    public abstract class MapToBase<T> : IMapTo<T>
    {
        public virtual void Mapping(Profile profile)
        {
            profile.CreateMap(GetType(), typeof(T));
        }
    }
}
