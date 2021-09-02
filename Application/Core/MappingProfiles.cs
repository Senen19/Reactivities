using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //Con CreateMap le indicamos desde donde queremos mappear hasta donde que punto queremos que mapee
            //desde Activity hasta el Activity que tenemos en EditHandler. Se deberá añadir automapper como
            //service a nuestra start up
            CreateMap<Activity, Activity>();
        }
    }
}