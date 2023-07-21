using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using TrackingManagment.LoginViewModel;
using TrackingManagment.Models;
using TrackingManagment.Models.DTO;
using TrackingManagment.Repository;
using TrackingManagment.Services;
using TrackingManagment.ViewModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

            apps.MapGet("/GetAll", [Authorize] (IRepository _repository, IInviteUserRepository tokenHandler, IHttpContextAccessor _httpContextAccessor, ITrackingRepository trackingRepository,
             UnitOfWork unitofWork, IMapper mapper) =>
            {
                var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var getSenderId = tokenHandler.GetUserIdFromToken(token);
                if (getSenderId == null) return Results.BadRequest();
                var data = _repository.GetSpecificUserData(getSenderId);
                // now i will do mapping ......................
                IList<RealStateDTO> realStateDTOs = mapper.Map<IList<RealStateDTO>>(data);
                if (data.Count == 0) return Results.Ok(data);
                var findTracking = trackingRepository.GetAll(data.FirstOrDefault().ApplicationUserId);
                foreach (var tracking in findTracking)
                {
                    realStateDTOs.FirstOrDefault(u => u.ApplicationUserId == tracking.Id).TrackingDetails.Add(
                        new TrackingOutput()
                        {
                            TrackingId = tracking.Id,
                            realStateId = tracking.RealStateId,
                            DataChangeId = tracking.Id,
                            DataChangeUser = unitofWork.CheckPersonsId(tracking.UserId),
                            UserActions = (TrackingOutput.Action)tracking.UserActions,
                            TrackingDate = tracking.ChangeTracktime
                        });
                }
                return Results.Ok(realStateDTOs);

            });


            apps.MapPost("/createState", (UnitOfWork unit, IRepository _repository, RealStateDTO state, IHttpContextAccessor httpContextAccessor,
                IInviteUserRepository _inviteUser, IMapper _mapper, ITrackingRepository _trackingRepository) =>

            {
                var token = httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var getUserName = _inviteUser.GetUserIdFromToken(token);

                var checkSenderInDatabase = unit.CheckPersonsId(getUserName);
                if (checkSenderInDatabase == null) return Results.BadRequest();
                if (state.ApplicationUserId != "")
                {
                    var data = unit.CheckPersonsId(state.ApplicationUserId);
                    if (data == null) return Results.BadRequest();
                    state.ApplicationUserId = data.Id;
                }
                else
                {
                    state.ApplicationUserId = checkSenderInDatabase.Id;
                }
                RealState realstates = _mapper.Map<RealState>(state);
                //realstate.ApplicationUser = null;
                var addState = _repository.Add(realstates);
                if (state.ApplicationUserId == getUserName)
                {
                    // here we will perform tracking.................
                    TracingUser tracking = new TracingUser()
                    {
                        RealStateId = realstates.Id,
                        UserId = getUserName,
                        ChangeTracktime = DateTime.UtcNow,
                        UserActions = TracingUser.Action.Add,
                        DataChangeId = realstates.ApplicationUserId
                    };

                    var trackingCreate = _trackingRepository.Add(tracking);

                    if (trackingCreate == null) { return Results.StatusCode(StatusCodes.Status500InternalServerError); }
                    state.TrackingDetails.Add(new TrackingOutput()
                    {
                        TrackingId = tracking.Id,
                        realStateId = tracking.RealStateId,
                        DataChangeId = tracking.Id,
                        DataChangeUser = unit.CheckPersonsId(tracking.UserId),
                        UserActions = (TrackingOutput.Action)tracking.UserActions,
                        TrackingDate = tracking.ChangeTracktime
                    });
                }
                // now we will create a tracking here .......................
                // we will check first tracking start or not ........................

                if (state.ApplicationUserId != getUserName)
                {
                    // here we will perform tracking.................
                    TracingUser tracking = new TracingUser()
                    {
                        RealStateId = realstates.Id,
                        UserId = getUserName,
                        ChangeTracktime = DateTime.UtcNow,
                        UserActions = TracingUser.Action.Add,
                        DataChangeId = realstates.ApplicationUserId
                    };

                    var trackingCreate = _trackingRepository.Add(tracking);

                    if (trackingCreate == null) { return Results.StatusCode(StatusCodes.Status500InternalServerError); }
                    state.TrackingDetails.Add(new TrackingOutput()
                    {
                        TrackingId = tracking.Id,
                        realStateId = tracking.RealStateId,
                        DataChangeId = tracking.Id,
                        DataChangeUser = unit.CheckPersonsId(tracking.UserId),
                        UserActions = (TrackingOutput.Action)tracking.UserActions,
                        TrackingDate = tracking.ChangeTracktime
                    });
                }

                return Results.Ok(new { Status = 1, Message = "Shipping created successfully", data = realstates });

            });
            //To Create new REalState
            /*     apps.MapPost("/createState", async (UnitOfWork unit, IRepository _repository, RealState state, IHttpContextAccessor httpContextAccessor, IInviteUserRepository _inviteUser) =>

                 {
                     var token = httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                     var getUserName = _inviteUser.GetUserIdFromToken(token);

                     var checkSenderInDatabase = unit.CheckPersonsId(getUserName);
                     if (checkSenderInDatabase == null) return Results.BadRequest();
                     if (state.ApplicationUserId != "")
                     {
                         var data = unit.CheckPersonsId(state.ApplicationUserId);
                         if (data == null) return Results.BadRequest();
                         state.ApplicationUserId = data.Id;
                     }
                     else
                     {
                         state.ApplicationUserId = checkSenderInDatabase.Id;
                     }
                     if (state == null) { return Results.BadRequest(); }
                     await _repository.Add(state);
                     return Results.Ok("succesfully added");
                 });*/

            //To Update RealState
            apps.MapPut("/updateState", async (UnitOfWork unit, ITrackingRepository trackingRepo, IMapper mapper, RealStateDTO state, IRepository repository,
                IInviteUserRepository invitationRepo, IHttpContextAccessor httpContextAccessor) =>

            {
                var token = httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var getSenderId = invitationRepo.GetUserIdFromToken(token);
                if (getSenderId == null) return Results.BadRequest();
                var checkSenderInDatabase = unit.CheckPersonsId(getSenderId);
                if (checkSenderInDatabase == null) return Results.BadRequest();
                if (state.ApplicationUserId != "")
                {
                    var data = unit.CheckPersonsId(state.ApplicationUserId);
                    if (data == null) return Results.BadRequest();
                    state.ApplicationUserId = data.Id;
                }
                else
                {
                    state.ApplicationUserId = checkSenderInDatabase.Id;
                }
                RealState states = mapper.Map<RealState>(state);
                var addstates = await repository.Update(states);
                if (state.ApplicationUserId == getSenderId)
                {
                    // here we will perform tracking.................
                    TracingUser tracking = new TracingUser()
                    {
                        RealStateId = states.Id,
                        UserId = getSenderId,
                        ChangeTracktime = DateTime.UtcNow,
                        UserActions = TracingUser.Action.Update,
                        DataChangeId = states.ApplicationUserId
                    };
                    var trackingCreate = trackingRepo.Add(tracking);

                    if (trackingCreate == null) { return Results.StatusCode(StatusCodes.Status500InternalServerError); }
                    state.TrackingDetails.Add(new TrackingOutput()
                    {
                        TrackingId = tracking.Id,
                        realStateId = tracking.RealStateId,
                        DataChangeId = tracking.Id,
                        DataChangeUser = unit.CheckPersonsId(tracking.UserId),
                        UserActions = (TrackingOutput.Action)tracking.UserActions,
                        TrackingDate = tracking.ChangeTracktime
                    });


                }
                if (states.ApplicationUserId != getSenderId)
                {
                    // here we will perform tracking.................
                    TracingUser tracking = new TracingUser()
                    {
                        RealStateId = states.Id,
                        UserId = getSenderId,
                        ChangeTracktime = DateTime.UtcNow,
                        UserActions = TracingUser.Action.Update,
                        DataChangeId = states.ApplicationUserId
                    };
                    var trackingCreate = trackingRepo.Add(tracking);

                    if (trackingCreate == null) { return Results.StatusCode(StatusCodes.Status500InternalServerError); }
                    state.TrackingDetails.Add(new TrackingOutput()
                    {
                        TrackingId = tracking.Id,
                        realStateId = tracking.RealStateId,
                        DataChangeId = tracking.Id,
                        DataChangeUser = unit.CheckPersonsId(tracking.UserId),
                        UserActions = (TrackingOutput.Action)tracking.UserActions,
                        TrackingDate = tracking.ChangeTracktime
                    });
                }
                return Results.Ok(new { Status = 1, Message = "Book updated successfully", data = states });
            });

            /*            apps.MapPut("/updateState", async (UnitOfWork unit, IHttpContextAccessor httpContextAccessor, IRepository _repository, RealState state, IInviteUserRepository _inviteUser) =>

                        {
                            var token = httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                            var getUserName = _inviteUser.GetUserIdFromToken(token);
                            var checkSenderInDatabase = unit.CheckPersonsId(getUserName);
                            if (checkSenderInDatabase == null) return Results.BadRequest();
                            if (state.ApplicationUserId != "")
                            {
                                var data = unit.CheckPersonsId(state.ApplicationUserId);
                                if (data == null) return Results.BadRequest();
                                state.ApplicationUserId = data.Id;
                            }
                            else
                            {
                                state.ApplicationUserId = checkSenderInDatabase.Id;
                            }
                            if (state == null) { return Results.BadRequest(); }
                            await _repository.Update(state);
                            return Results.Ok("Updated successfully");
                        });*/

            //To Delete RealState
            apps.MapDelete("/delete/{id}", (UnitOfWork unit, ITrackingRepository trackingRepo, IRepository repository,
                IInviteUserRepository invitationRepo, IHttpContextAccessor httpContextAccessor, int id) =>

            {
                var token = httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var getSenderId = invitationRepo.GetUserIdFromToken(token);
                if (getSenderId == null) return Results.BadRequest();
                var checkSenderInDatabase = unit.CheckPersonsId(getSenderId);
                if (checkSenderInDatabase == null) return Results.BadRequest();
                if (id == 0) return Results.BadRequest();
                var getState = repository.GetbyId(id);
                if (getState.ApplicationUserId != checkSenderInDatabase.Id)
                {
                    var data = unit.CheckPersonsId(checkSenderInDatabase.Id);
                    if (data == null) return Results.BadRequest();
                    // here we will perform tracking.................
                    TracingUser tracking = new TracingUser()
                    {
                        RealStateId = getState.Id,
                        UserId = getSenderId,
                        ChangeTracktime = DateTime.UtcNow,
                        UserActions = TracingUser.Action.Delete,
                        DataChangeId = getState.ApplicationUserId
                    };
                    if (!trackingRepo.Add(tracking)) { Results.StatusCode(StatusCodes.Status500InternalServerError); }
                    getState.IsDeleted = true;
                    repository.Update(getState);
                    return Results.Ok(new { Status = 1, Message = "Shipping deleted successfully", data = getState });
                }
                var deletestate = repository.Delete(id);
                if (deletestate) return Results.Ok(new { Status = 1, Message = "Deleted Successfully!!" });
                return Results.StatusCode(StatusCodes.Status500InternalServerError);

            });

            //TO GET SPECIFIC USER DATA

            apps.MapGet("/getspecificdata", (IRepository _repository, IInviteUserRepository tokenHandler, IHttpContextAccessor _httpContextAccessor, ITrackingRepository trackingRepository,
             UnitOfWork unitofWork, IMapper mapper) =>
            {

                var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var getSenderId = tokenHandler.GetUserIdFromToken(token);
                if (getSenderId == null) return Results.BadRequest();
                var data = _repository.GetSpecificUserData(getSenderId);
                // now i will do mapping ......................
                // now i will do mapping ......................
                IList<RealStateDTO> stateDTOs = mapper.Map<IList<RealStateDTO>>(data);
                if (data.Count == 0) return Results.Ok(data);
                var findTracking = trackingRepository.GetAll(data.FirstOrDefault().ApplicationUserId);
                foreach (var tracking in findTracking)
                {
                    stateDTOs.FirstOrDefault(u => u.Id == tracking.RealStateId).TrackingDetails.Add(
                        new TrackingOutput()
                        {
                            TrackingId = tracking.Id,
                            realStateId = tracking.RealStateId,
                            DataChangeId = tracking.Id,
                            DataChangeUser = unitofWork.CheckPersonsId(tracking.UserId),
                            UserActions = (TrackingOutput.Action)tracking.UserActions,
                            TrackingDate = tracking.ChangeTracktime
                        });
                }
                return Results.Ok(stateDTOs);
            });



            //TO GET USER DATAS 
            apps.MapGet("/getuserdatas/{id}", async (string? id, IRepository repository, IInviteUserRepository tokenHandler, IHttpContextAccessor _httpContextAccessor,
            ITrackingRepository _trackingRepository,
            IMapper _mapper,
            UnitOfWork _unitofWork) =>

            {
                if (string.IsNullOrEmpty(id))
                    return Results.BadRequest();
                var data = repository.GetSpecificUserData(id);
                // now i will do mapping ......................
                IList<RealStateDTO> stateDTOs = _mapper.Map<IList<RealStateDTO>>(data);
                if (data.Count == 0) return Results.Ok(data);
                var findTracking = _trackingRepository.GetAll(data.FirstOrDefault(u => id == id).ApplicationUserId);
                foreach (var tracking in findTracking)
                {
                    stateDTOs.FirstOrDefault(u => u.Id == tracking.RealStateId).TrackingDetails.Add(
                        new TrackingOutput()
                        {
                            TrackingId = tracking.Id,
                            realStateId = tracking.RealStateId,
                            DataChangeId = tracking.Id,
                            DataChangeUser = _unitofWork.CheckPersonsId(tracking.UserId),
                            UserActions = (TrackingOutput.Action)tracking.UserActions,
                            TrackingDate = tracking.ChangeTracktime
                        });
                }
                return Results.Ok(stateDTOs);


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

            //To change status when request accept or reject
            app.MapPost("/ChangeStatus/{reciverId}/{status}", ChangingStatus);

            //To get the invited users
            app.MapGet("/Getall", InviteUserGetAll);

            //To update the action of user
            app.MapPost("/update", updateActions);

            app.MapGet("/invitationComesFromUser", InvitationfromUser);

            app.MapGet("/trackingDetails", trackingDetailsofUser);





            return app;
        }

        public static IResult Invite(string userName, IInviteUserRepository invite)
        {

            var data = invite.GetUsers(userName);
            return Results.Ok(data);
        }

        public static IResult Invitation(string senderId, IHttpContextAccessor httpContextAccessor, IInviteUserRepository _inviteUser)
        {
            var token = httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var getUserName = _inviteUser.GetUserIdFromToken(token);

            if (getUserName == null) return Results.BadRequest();
            var createMail = _inviteUser.CreateInvitation(getUserName, senderId);
            if (createMail == false) return Results.BadRequest();
            return Results.Ok();
        }

        //TO CHANGE STATUS WHEN RECIEVIER ACCEPTS THE EMAIL REQUESTS
        public static IResult ChangingStatus(string reciverId, int status, IInviteUserRepository _inviteUser, IHttpContextAccessor httpContextAccessor)
        {
            var token = httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var getSenderId = _inviteUser.GetUserIdFromToken(token);
            if (getSenderId == null || reciverId == null || status == 0) return Results.BadRequest();

            if (!_inviteUser.ChangeInvitationStatus(reciverId, getSenderId, status))
            {
                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Results.Ok(new { Status = 1, Message = "Invitation Updated Successfully" });
        }

        //TO GET ALL THE INVITED PERSON 
        public static IResult InviteUserGetAll(IHttpContextAccessor httpContextAccessor, IInviteUserRepository _inviteuser)
        {
            var token = httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var getId = _inviteuser.GetUserIdFromToken(token);
            var getAll = _inviteuser.GetAllRegisteredPersons(getId);
            return Results.Ok(getAll);
        }


        //TO UPDATE RECEIVER ACTIONS
        public static IResult updateActions(string reciverId, int action, IInviteUserRepository _inviteuserrepository, IHttpContextAccessor _httpcontextAccessor)
        {
            var token = _httpcontextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var getSenderId = _inviteuserrepository.GetUserIdFromToken(token);
            if (getSenderId == null || reciverId == null || action == 0) return Results.BadRequest();

            if (!_inviteuserrepository.UpdateInvitationAction(reciverId, getSenderId, action))
            {
                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Results.Ok(new { Status = 1, Message = "Invitation Updated Successfully" });

        }

        public static IResult InvitationfromUser(IInviteUserRepository _invitationRepository, IHttpContextAccessor httpContextAccessor)
        {
            var token = httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userId = _invitationRepository.GetUserIdFromToken(token);
            if (userId == null) return Results.BadRequest();
            var data = _invitationRepository.InvitationComesFromUser(userId);
            return Results.Ok(data);

        }

        public static IResult GetInvitationerSTATE(string? invitaionerId, IRepository repository, IInviteUserRepository tokenHandler, IHttpContextAccessor _httpContextAccessor,
            ITrackingRepository _trackingRepository,
            IMapper _mapper,
            UnitOfWork _unitofWork)
        {
            if (string.IsNullOrEmpty(invitaionerId))
                return Results.BadRequest();
            var data = repository.GetSpecificUserData(invitaionerId);
            // now i will do mapping ......................
            IList<RealStateDTO> stateDTOs = _mapper.Map<IList<RealStateDTO>>(data);
            if (data.Count == 0) return Results.Ok(data);
            var findTracking = _trackingRepository.GetAll(data.FirstOrDefault().ApplicationUserId);
            foreach (var tracking in findTracking)
            {
                stateDTOs.FirstOrDefault(u => u.Id == tracking.RealStateId).TrackingDetails.Add(
                    new TrackingOutput()
                    {
                        TrackingId = tracking.Id,
                        realStateId = tracking.RealStateId,
                        DataChangeId = tracking.Id,
                        DataChangeUser = _unitofWork.CheckPersonsId(tracking.UserId),
                        UserActions = (TrackingOutput.Action)tracking.UserActions,
                        TrackingDate = tracking.ChangeTracktime
                    });
            }
            return Results.Ok(stateDTOs);
        }

       
        public static IResult trackingDetailsofUser(string userId, int id, IRepository repository, IInviteUserRepository tokenHandler, IHttpContextAccessor _httpContextAccessor,
            ITrackingRepository _trackingRepository,
            IMapper _mapper,
            UnitOfWork _unitofWork)
        {
            if (string.IsNullOrEmpty(userId))
                return Results.BadRequest();

            var data = repository.GetSpecificUserData(userId);

            if (data.Count == 0)
                return Results.NotFound();

            var selectedData = data.FirstOrDefault(u => u.Id == id);
            if (selectedData == null)
                return Results.NotFound();

            // now I will do mapping...
            RealStateDTO stateDTO = _mapper.Map<RealStateDTO>(selectedData);

            var findTracking = _trackingRepository.GetAll(selectedData.ApplicationUserId);
            foreach (var tracking in findTracking)
            {
                if (tracking.RealStateId == selectedData.Id)
                {
                    stateDTO.TrackingDetails.Add(
                        new TrackingOutput()
                        {
                            TrackingId = tracking.Id,
                            realStateId = tracking.RealStateId,
                            DataChangeId = tracking.Id,
                            DataChangeUser = _unitofWork.CheckPersonsId(tracking.UserId),
                            UserActions = (TrackingOutput.Action)tracking.UserActions,
                            TrackingDate = tracking.ChangeTracktime
                        });
                }
            }

            return Results.Ok(stateDTO);
        } };
         


}


