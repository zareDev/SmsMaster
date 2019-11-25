using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SmsMaster.DTO;
using SmsMaster.Model;

namespace SmsMaster.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Sms, SendSmsRequest>();
            CreateMap<SendSmsRequest, Sms>();
            //CreateMap<UserDto, User>();
        }
    }
}
