using Microsoft.EntityFrameworkCore;
using MyEdo.Business.Exceptions;
using MyEdo.Business.Services.AppUser;
using MyEdo.Core.Models;
using MyEdo.Core.Models.Enums;
using MyEdo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEdo.Business.Services.AppTraining
{
    public class TrainingService : ITrainingService
    {
        private readonly MyEdoDbContext context;
        private readonly IUserService userService;

        public TrainingService(
            MyEdoDbContext context,
            IUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }

        public async Task<string> Create(Training model)
        {
            Training training = new Training
            {
                Name = model.Name,
                Type = model.Type,
                DueDate = model.DueDate.AddDays(1),
                Status = model.Status,
            };

            this.context.Trainings.Add(training);
            await this.context.SaveChangesAsync();

            return training.Id;
        }

        public async Task<bool> Edit(Training model)
        {
            var trainingForUpdate = await this.GetTrainingById(model.Id);

            trainingForUpdate.Name = model.Name;
            trainingForUpdate.Type = model.Type;
            trainingForUpdate.Status = model.Status;
            trainingForUpdate.DueDate = model.DueDate.AddDays(1);
            trainingForUpdate.ModifiedOn = DateTime.UtcNow;

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Delete(string id)
        {
            var trainingForDelete = await this.GetTrainingById(id);

            trainingForDelete.IsDeleted = true;
            trainingForDelete.DeletedOn = DateTime.Now;

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Request(string id)
        {
            var trainigToRequest = await this.GetTrainingById(id);
            var currentUser = await this.userService.GetCurrentUserId();

            UserTraining userTraining = new UserTraining
            {
                UserId = currentUser,
                TrainingId = trainigToRequest.Id,
                Status = UserTrainingStatus.Requested,
            };

            this.context.UserTrainings.Add(userTraining);

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> AssignToUser(string trainingId, string userId)
        {
            var trainigToAssign = await this.GetTrainingById(trainingId);
            var userToAssign = await this.userService.GetUserById(userId);

            UserTraining userTraining = new UserTraining
            {
                UserId = userToAssign.Id,
                TrainingId = trainigToAssign.Id,
                Status = UserTrainingStatus.Assigned,
            };

            this.context.UserTrainings.Add(userTraining);

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> ChangeUserTrainingStatus(string trainingId, string userId, UserTrainingStatus status)
        {
            var userTrainingForUpdate = this.context.UserTrainings
                .SingleOrDefault(x => x.TrainingId == trainingId && x.UserId == userId);

            if (userTrainingForUpdate == null)
            {
                throw new NotFoundException();
            }

            userTrainingForUpdate.Status = status;

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<IEnumerable<UserTraining>> GetCurrentUserTrainings()
        {
            var currentUserId = await this.userService.GetCurrentUserId();

            var trainings = this.context.UserTrainings
                 .Where(
                 ut => ut.Training.DueDate.Date >= DateTime.Now.Date &&
                 ut.Training.IsDeleted == false &&
                 ut.UserId == currentUserId)
                 .Include(t => t.Training)
                 .ToList();

            return trainings;
        }

        public Task<IEnumerable<UserTraining>> GetAllUsersTrainings()
        {
            var trainings = this.context.UserTrainings
                 .Where(t => t.Training.DueDate.Date >= DateTime.Now.Date && t.Training.IsDeleted == false)
                 .Include(t => t.Training)
                 .Include(t=> t.User)
                 .ToList();

            return Task.FromResult(trainings.AsEnumerable());
        }

        public Task<IEnumerable<Training>> GetAllTrainings()
        {
            var trainings = this.context.Trainings
                .Where(t => t.IsDeleted == false)
                .OrderBy(t => t.Name)
                .ToList();

            return Task.FromResult(trainings.AsEnumerable());
        }

        private Task<Training> GetTrainingById(string id)
        {
            var training = this.context.Trainings
                .Where(t => t.DueDate.Date >= DateTime.Now.Date && t.IsDeleted == false)
                .SingleOrDefault(t => t.Id == id);

            if (training == null)
            {
                throw new NotFoundException();
            }

            return Task.FromResult(training);
        }
    }
}
