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
        private readonly MyEduDbContext context;
        private readonly IUserService userService;

        public TrainingService(
            MyEduDbContext context,
            IUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }

        public async Task<bool> Create(Training model)
        {
            Training training = new Training
            {
                Name = model.Name,
                Type = model.Type,
                DueDate = model.DueDate,
                Status = model.Status,
            };

            this.context.Trainings.Add(training);
            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Edit(Training model, string id)
        {
            var trainingForUpdate = await this.GetTrainingById(id);

            trainingForUpdate.Name = model.Name;
            trainingForUpdate.Type = model.Type;
            trainingForUpdate.Status = model.Status;
            trainingForUpdate.DueDate = model.DueDate;
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

        public async Task<bool> Request(string id, UserTraining model)
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

        public async Task<bool> AssignToUser(string trainingId, User model)
        {
            var trainigToAssign = await this.GetTrainingById(trainingId);
            var userToAssign = await this.userService.GetUserByName(model.FirstName, model.LastName);

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

        public async Task<bool> ChangeUserTrainingStatus(UserTraining model, string trainingId, string userId)
        {
            var userTrainingForUpdate = this.context.UserTrainings
                .SingleOrDefault(x => x.TrainingId == trainingId && x.UserId == userId);

            userTrainingForUpdate.Status = model.Status;

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<IList<string>> GetCurrentUserTrainingsId()
        {
            var currentUserId = await this.userService.GetCurrentUserId();

            var userTrainingssId = this.context.UserTrainings
                .Where(u => u.UserId == currentUserId)
                .Select(t => t.TrainingId)
                .ToList();

            return userTrainingssId;
        }

        public async Task<IEnumerable<UserTraining>> GetCurrentUserTrainings()
        {
            var currentUserId = await this.userService.GetCurrentUserId();

            var trainings = this.context.UserTrainings
                 .Where(ut => ut.Training.DueDate.Date >= DateTime.Now.Date)
                 .Where(ut => ut.UserId == currentUserId)
                 .Where(ut => ut.Training.IsDeleted == false)
                 .ToList();

            return trainings;
        }

        public Task<IEnumerable<UserTraining>> GetAllUsersTrainings()
        {
            var trainings = this.context.UserTrainings
                 .Where(d => d.Training.DueDate.Date >= DateTime.Now.Date)
                 .Where(s => s.Training.IsDeleted == false)
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

        public Task<UserTraining> GetUserTrainingByIds(string trainingId, string userId)
        {
            var userTrainingToUpdate = this.context.UserTrainings
                .Where(x => x.UserId == userId && x.TrainingId == trainingId)
                .FirstOrDefault();

            return Task.FromResult(userTrainingToUpdate);
        }

        public Task<Training> GetTrainingById(string id)
        {
            var training = this.context.Trainings
                .Where(t => t.Id == id)
                .SingleOrDefault();

            return Task.FromResult(training);
        }
    }
}
