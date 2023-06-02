using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Win32;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TrackingManagment.LoginViewModel;
using TrackingManagment.Models;
using TrackingManagment.Repository;
using TrackingManagment.Services;
using TrackingManagment.ViewModel;

namespace TrackingManagment.Endpoints
{
    public static class Presentation
    {

        public static void UserEndPoints(this IEndpointRouteBuilder apps)
        {
            // get endpoint
            apps.MapGet("/getById", async (IRepository _repository, int id) =>
            {
                var getid = await _repository.Get(id);
                if (getid == null) { return Results.NotFound(); }
                return Results.Ok(getid);

            });

            apps.MapGet("/GetAll", [Authorize] async (IRepository _repository) =>
            {
                var getAll = await _repository.GetAll();
                return getAll;
            });

            //To Create new REalState
            apps.MapPost("/createState", async (IRepository _repository, RealState state) =>

            {
                if (state == null) { return Results.BadRequest(); }
                await _repository.Add(state);
                return Results.Ok("succesfully added");
            });

            //To Update RealState
            apps.MapPut("/udateState", async (IRepository _repository, RealState state) =>

            {
                await _repository.Update(state);
                return Results.Ok("Updated successfully");
            });

            //To Delete RealState
            apps.MapDelete("/delete/{id}", async (IRepository _repository, int id) =>

            {
                var updateUser = await _repository.Delete(id);
                return Results.Ok("Record Deleted");
            });


        }


        /*       public static void RegisterLogin(this IEndpointRouteBuilder apps)
               {

                   apps.MapPost("/register", async (IUserService _service,[FromBody] RegisterView register) =>
                   {

                   });

                   apps.MapPost("/login/", async (IUserService _service, LoginView login ) =>
                   {
                       var loginUser = await _service.AuhthenticateUser(login);
                       if(login == null) return Results.StatusCode(StatusCodes.Status500InternalServerError);
                       return Results.Ok(loginUser);

                   });

                   apps.MapPost("/Email/", async (Email email,IEmailService _service) =>
                   {
                        _service.SendEmail(email);
                       return Results.Ok(email);
                   });


           }*/
        public static RouteGroupBuilder LoginRegisterAPI(this RouteGroupBuilder app)
        {
            app.MapPost("/register", Register);

            app.MapPost("/login", Login);

           
          



            return app;
        }
        public async static Task<IResult> Register(IUserService _service, [FromBody] RegisterView register)
        {
            var registerUser = await _service.Register(register);
            if (!registerUser) return Results.StatusCode(StatusCodes.Status500InternalServerError);
            return Results.Ok(new { Message = "Register successfully!!!" });
        }

        public async static Task<IResult> Login(IUserService _service, LoginView login)
        {
            var loginUser = await _service.AuhthenticateUser(login);
            if (login == null) return Results.StatusCode(StatusCodes.Status500InternalServerError);
            return Results.Ok(loginUser);

        }

       
        //INVITATION
        public static RouteGroupBuilder INVITATION_API(this RouteGroupBuilder app)
        {
            app.MapGet("/invite/{userName}", Invite);
            //app.MapPost("/email", Email);
            app.MapPost("/CreateInvitation", Invitation);
            return app;
        }
    
     public static  IResult Invite(string userName, IInviteUserRepository invite)
        {

            var data = invite.GetUsers(userName);
            return Results.Ok(data);
        }

      /*  public static IResult Email(IEmailService _service, EmailModel email)
        {

            *//* var getSenderId = _inviteUserRepository.GetUsers(token);
             if (getSenderId == null)
                 return Results.BadRequest(new { message = "your token doesnot contain user id " });*//*
            _service.SendEmail(email);
            return Results.Ok();
        }*/

        public static IResult Invitation(string senderId, IHttpContextAccessor httpContextAccessor,IInviteUserRepository _inviteUser) 
        {
            var token = httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var getUserName = _inviteUser.GetUserIdFromToken(token);

            if (getUserName == null) return Results.BadRequest();
            var createMail = _inviteUser.CreateInvitation(getUserName, senderId);
            if(createMail ==null) return Results.BadRequest();
            return Results.Ok();
        }


    }


}


