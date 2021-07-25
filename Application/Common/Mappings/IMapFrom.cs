using AutoMapper;

namespace Application.Common.Mappings
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }

    public interface IMapFrom
    {
        void Mapping(Profile profile);
    }

}
