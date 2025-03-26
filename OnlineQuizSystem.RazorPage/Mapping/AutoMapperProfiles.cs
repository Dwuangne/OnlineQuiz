using AutoMapper;
using OnlineQuiz.Repository.Entities;

//using OnlineQuiz.Repository.Entities;
using OnlineQuiz.Service.BusinessModel;
using OnlineQuizSystem.RazorPage.Models.RequestModel;
using OnlineQuizSystem.RazorPage.Models.ResponseModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OnlineQuizSystem.RazorPage.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Quiz, QuizRequest>().ReverseMap();
            CreateMap<Question, QuestionRequest>().ReverseMap();

            CreateMap<Quiz, QuizResponse>().ReverseMap();
            CreateMap<Question, QuestionResponse>().ReverseMap();
            CreateMap<Room, RoomResponse>().ReverseMap();
            CreateMap<Player, PlayerResponse>().ReverseMap();

            CreateMap<QuizRequest, QuizBusiness>().ReverseMap();
            CreateMap<QuestionRequest, QuestionBusiness>().ReverseMap();
            CreateMap<Room, RoomBusiness>().ReverseMap();

        } 
    }
}
