using MyEdo.Core.Models;
using MyEdo.Core.Models.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyEdo.Business.Services.AppTraining
{
    public interface ITrainingService
    {
        Task<string> Create(Training inputModel);

        Task<bool> Edit(Training model);

        Task<bool> Delete(string id);

        Task<bool> Request(string id);

        Task<bool> AssignToUser(string trainingId, string userId);

        Task<bool> ChangeUserTrainingStatus(string trainingId, string userId, UserTrainingStatus status);

        Task<IEnumerable<Training>> GetAllTrainings();

        Task<IEnumerable<UserTraining>> GetCurrentUserTrainings();

        Task<IEnumerable<UserTraining>> GetAllUsersTrainings();
    }
}
