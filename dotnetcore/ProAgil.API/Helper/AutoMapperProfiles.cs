using AutoMapper;
using ProAgil.API.Dtos;
using ProAgil.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProAgil.API.Helper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Evento, EventoDto>()
                .ForMember(dto => dto.Palestrante, entity => entity.MapFrom(x=> x.PalestranteEventos.Select(o=> o.Palestante)));

            CreateMap<EventoDto, Evento>();

            CreateMap<Palestrante, PalestranteDto>()
                .ForMember(dto => dto.Eventos, entity => entity.MapFrom(x => x.PalestranteEventos.Select(o => o.Evento)));

            CreateMap<Lote, LoteDto>().ReverseMap();
            CreateMap<RedeSocial, RedeSocialDto>().ReverseMap();
        }
    }
}
