using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyEdo.Business.Exceptions;
using MyEdo.Business.Services.AppTraining;
using MyEdo.Core.Common;
using MyEdo.Core.Models;
using MyEdo.Web.ApiModels.Trainings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEdo.Controllers
{
    [Authorize()]
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingController : ControllerBase
    {
        private readonly ITrainingService trainingService;
        private readonly IMapper mapper;

        public TrainingController(
        ITrainingService trainingService,
        IMapper mapper)
        {
            this.trainingService = trainingService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," +
                          GlobalConstants.ResourceRoleName)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<TrainingApiModel>>> GetAllTrainings()
        {
            try
            {
                var trainings  = await this.trainingService
               .GetAllTrainings();

                var model = trainings
                    .Select(t => new TrainingApiModel { Id = t.Id, Name = t.Name, Status = t.Status, Type = t.Type, DueDate = t.DueDate });

                return Ok(model);
            }
            catch (NotAuthorizedException ex)
            {
                return this.Unauthorized(ex.Message);
            }
            catch (ForbiddenException ex)
            {
                return this.Forbid(ex.Message);
            }
            catch (BadRequestException ex)
            {
                return this.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [ProducesResponseType(typeof(TrainingApiModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<TrainingApiModel>> CreateTraining([FromBody] TrainingApiModel model)
        {
            var trainingId = string.Empty;

            try
            {
                var modelMap = mapper.Map<Training>(model);
                trainingId = await this.trainingService.Create(modelMap);
                model.Id = trainingId;
            }
            catch (NotAuthorizedException ex)
            {
                return this.Unauthorized(ex.Message);
            }
            catch (ForbiddenException ex)
            {
                return this.Forbid(ex.Message);
            }
            catch (BadRequestException ex)
            {
                return this.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {

                return this.BadRequest(ex.Message);
            }

            return this.CreatedAtAction(nameof(this.CreateTraining), new { trainingId }, model);
        }

        [HttpPut]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<TrainingApiModel>> EditTraining([FromBody] TrainingApiModel model)
        {
            try
            {
                var modelMap = mapper.Map<Training>(model);
                await this.trainingService.Edit(modelMap);
            }
            catch (NotAuthorizedException ex)
            {
                return this.Unauthorized(ex.Message);
            }
            catch (ForbiddenException ex)
            {
                return this.Forbid(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return this.NotFound(ex.GenerateApiError());
            }
            catch (BadRequestException ex)
            {
                return this.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {

                return this.BadRequest(ex.Message);
            }

            return this.Ok(model);
        }

        [HttpDelete]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<TrainingBaseApiModel>> Delete([FromBody] TrainingBaseApiModel model)
        {
            try
            {
                await this.trainingService.Delete(model.Id);
            }
            catch (NotAuthorizedException ex)
            {
                return this.Unauthorized(ex.Message);
            }
            catch (ForbiddenException ex)
            {
                return this.Forbid(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return this.NotFound(ex.GenerateApiError());
            }
            catch (BadRequestException ex)
            {
                return this.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }

            return this.Ok(model);
        }

        [HttpPost(nameof(RequestTraining))]
        [Authorize(Roles = GlobalConstants.ResourceRoleName)]
        [ProducesResponseType(typeof(TrainingApiModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<TrainingBaseApiModel>> RequestTraining([FromBody] TrainingBaseApiModel model)
        {
            try
            {
                var modelMap = mapper.Map<UserTraining>(model);
                await this.trainingService.Request(model.Id);
            }
            catch (NotAuthorizedException ex)
            {
                return this.Unauthorized(ex.Message);
            }
            catch (ForbiddenException ex)
            {
                return this.Forbid(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return this.Unauthorized(ex.GenerateApiError());
            }
            catch (BadRequestException ex)
            {
                return this.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }

            return this.Ok(model);
        }

        [HttpPost(nameof(AssignTraining))]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [ProducesResponseType(typeof(AddUserTrainingApiModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<AddUserTrainingApiModel>> AssignTraining([FromBody] AddUserTrainingApiModel model)
        {
            try
            {
                var modelMap = mapper.Map<UserTraining>(model);
                await this.trainingService.AssignToUser(model.TrainingId, model.UserId);
            }
            catch (NotAuthorizedException ex)
            {
                return this.Unauthorized(ex.Message);
            }
            catch (ForbiddenException ex)
            {
                return this.Forbid(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return this.Unauthorized(ex.GenerateApiError());
            }
            catch (BadRequestException ex)
            {
                return this.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }

            return this.Ok(model);
        }

        [HttpPost(nameof(UpdateUserTrainingStatus))]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [ProducesResponseType(typeof(AddUserTrainingApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<AddUserTrainingApiModel>> UpdateUserTrainingStatus([FromBody] AddUserTrainingApiModel model)
        {
            try
            {
                var modelMap = mapper.Map<UserTraining>(model);
                await this.trainingService.ChangeUserTrainingStatus(model.TrainingId, model.UserId, model.Status);
            }
            catch (NotAuthorizedException ex)
            {
                return this.Unauthorized(ex.Message);
            }
            catch (ForbiddenException ex)
            {
                return this.Forbid(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return this.Unauthorized(ex.GenerateApiError());
            }
            catch (BadRequestException ex)
            {
                return this.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }

            return this.Ok(model);
        }

        [HttpGet(nameof(GetMyTrainings))]
        [Authorize(Roles = GlobalConstants.ResourceRoleName)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<TrainingApiModel>>> GetMyTrainings()
        {
            try
            {
                var trainings = await this.trainingService
               .GetCurrentUserTrainings();

                var model = trainings
                    .Select(t => new TrainingApiModel { Id = t.TrainingId, Name = t.Training.Name, Status = t.Training.Status, Type = t.Training.Type, DueDate = t.Training.DueDate });

                return Ok(model);
            }
            catch (NotAuthorizedException ex)
            {
                return this.Unauthorized(ex.Message);
            }
            catch (ForbiddenException ex)
            {
                return this.Forbid(ex.Message);
            }
            catch (BadRequestException ex)
            {
                return this.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpGet(nameof(GetAllUserTrainings))]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<GetConsolidatedUserTrainingsApiModel>>> GetAllUserTrainings()
        {
            try
            {
                var trainings = await this.trainingService
               .GetAllUsersTrainings();

                var model = trainings.Select(t => new UserTrainingApiModel
                { 
                    TrainingId = t.TrainingId,
                    TrainingName = t.Training.Name,
                    Status = t.Status,
                    UserId = t.UserId,
                    UserName = t.User.UserName
                });

                var groupedUserTrainings = model
                .GroupBy(s => new
                {
                    s.UserId,
                    s.UserName
                })
               .Select(grp => new GetConsolidatedUserTrainingsApiModel
               {
                   UserId = grp.Key.UserId,
                   UserName = grp.Key.UserName,
                   Trainings = grp.ToList(),
               })
               .ToList();

                return Ok(groupedUserTrainings);
            }
            catch (NotAuthorizedException ex)
            {
                return this.Unauthorized(ex.Message);
            }
            catch (ForbiddenException ex)
            {
                return this.Forbid(ex.Message);
            }
            catch (BadRequestException ex)
            {
                return this.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
    }
}
