using AutoMapper;
using Blog.Domain.DTOs;
using Blog.Domain.Entities;

namespace Blog.Infrastructure.Mappings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Post, PostDto>();
            CreateMap<PostDto, Post>();
            CreateMap<Comment, CommentDto>();
            CreateMap<CommentDto, Comment>();
            CreateMap<Notification, NotificationDto>();
            CreateMap<NotificationDto, Notification>();
            CreateMap<Visualization, VisualizationDto>();
            CreateMap<VisualizationDto, Visualization>();
            CreateMap<Like, LikeDto>();
            CreateMap<LikeDto, Like>();
            CreateMap<Sharing, SharingDto>();
            CreateMap<SharingDto, Sharing>();
        }
    }
}
