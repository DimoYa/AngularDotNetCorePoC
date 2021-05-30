using MyEdo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEdo.Business.Services.AppTraining
{
    public interface ITrainingService
    {
        Task<bool> Create(Training inputModel);

        Task<bool> Edit(Training model, string id);

        Task<bool> Delete(string id);

        Task<bool> Request(string id, UserTraining model);

        Task<bool> AssignToUser(string trainingId, User model);

        Task<bool> ChangeUserTrainingStatus(UserTraining model, string trainingId, string userId);

        Task<IEnumerable<Training>> GetAllTrainings();

        Task<IEnumerable<UserTraining>> GetCurrentUserTrainings();

        Task<IEnumerable<UserTraining>> GetAllUsersTrainings();

        Task<UserTraining> GetUserTrainingByIds(string trainingId, string userId);

        Task<IList<string>> GetCurrentUserTrainingsId();

        Task<Training> GetTrainingById(string id);
    }
}
