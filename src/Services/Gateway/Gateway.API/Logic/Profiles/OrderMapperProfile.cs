using AutoMapper;
using MRA.Gateway.Models;
using System;

namespace MRA.Gateway.Logic.Profiles
{
    public class OrderMapperProfile : Profile
    {
        public OrderMapperProfile()
        {
            CreateMap<Order, OrderViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.MeetingRoomId, opt => opt.MapFrom(src => src.MeetingRoomId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.StartTime, opt => opt.ConvertUsing<TimeSpanToStringConverter, TimeSpan>())
                .ForMember(dest => dest.EndTime, opt => opt.ConvertUsing<TimeSpanToStringConverter, TimeSpan>()) 
                .ForMember(dest => dest.MeetingRoomName, opt => opt.MapFrom(src => string.Empty))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => string.Empty));

            CreateMap<OrderInputDto, Order>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Parse(src.Date)))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => TimeSpan.Parse(src.StartTime)))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => TimeSpan.Parse(src.EndTime)))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));

            CreateMap<OrderEditDto, Order>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Parse(src.Date)))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => TimeSpan.Parse(src.StartTime)))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => TimeSpan.Parse(src.EndTime)))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.MeetingRoomId, opt => opt.MapFrom(src => src.MeetingRoomId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));
        }
    }

    public class TimeSpanToStringConverter : IValueConverter<TimeSpan, string>
    {
        public string Convert(TimeSpan sourceMember, ResolutionContext context)
        {
            return (sourceMember.Hours < 10 ? '0' + sourceMember.Hours.ToString() : sourceMember.Hours.ToString()) + ':' + (sourceMember.Minutes < 10 ? '0' + sourceMember.Minutes.ToString() : sourceMember.Minutes.ToString());
        }
    }
}