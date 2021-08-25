using AutoMapper;

namespace Application.Common.Mappings
{
    public interface IMapTo<T>
    {
        void Mapping(Profile profile);
    }
}
