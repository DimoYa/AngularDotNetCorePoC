namespace MyEdo.Business.Test.Services
{
    using MyEdo.Business.Exceptions;
    using MyEdo.Core.Models;
    using MyEdo.Core.Models.Enums;
    using NUnit.Framework;
    using System.Linq;
    using System.Threading.Tasks;

    [TestFixture]
    public class SkillServiceTests : BaseTest
    {
        [Test]
        [Property("service", "SkillService")]
        public async Task CreateSkill_ShouldReturnSkillId()
        {
            // Arrange
            var skill = new Skill()
            {
                Id = "1",
                Name = "NewSkill",
                SkillCategory = this.DummySkillCategories.FirstOrDefault()
            };

            // Act
            var actualResults = await this.SkillService.CreateSkill(skill);

            // Assert
            Assert.That(actualResults, Is.Not.Null, "should return skill id");
        }

        [Test]
        [Property("service", "SkillService")]
        public async Task EditSkill_ShouldReturnTrue()
        {
            // Arrange
            var skill = this.DummySkills.FirstOrDefault();
            skill.Name = "Updated";

            // Act
            var actualResults = await this.SkillService.EditSkill(skill);

            // Assert
            Assert.That(actualResults, Is.True, "should return true");
        }

        [Test]
        [Property("service", "SkillService")]
        public async Task DeleteActiveSkill_ShouldReturnTrue()
        {
            // Arrange
            var skill = this.DummySkills.FirstOrDefault(s=> s.IsDeleted == false);

            // Act
            var actualResults = await this.SkillService.DeleteSkill(skill.Id);

            // Assert
            Assert.That(actualResults, Is.True, "should return true");
        }

        [Test]
        [Property("service", "SkillService")]
        public async Task DeleteInactiveSkill_ShouldReturnException()
        {
            // Arrange
            var skill = this.DummySkills.FirstOrDefault(s => s.IsDeleted == true);

            // Act
            var ex = Assert.ThrowsAsync<NotFoundException>(async () => await this.SkillService.DeleteSkill(skill.Id));

            // Assert
            Assert.That(ex.Message, Is.EqualTo("Not found"));
        }

        [Test]
        [Property("service", "SkillService")]
        public async Task AddActiveSkillToMyProfile_ShouldReturnTrue()
        {
            // Arrange
            var currentUserId = this.DummyUsers.FirstOrDefault().Id;

            this.MockedUserService.Setup(u => u.GetCurrentUserId())
             .Returns(Task.FromResult(currentUserId));

            var skillToadd = this.DummySkills.LastOrDefault(s => s.IsDeleted == false);
            var userSkill = new UserSkill
            {
                SkillId = skillToadd.Id,
                Level = SkillLevel.Advanced
            };

            // Act
            var actualResults = await this.SkillService.AddSkillToMyProfile(userSkill);

            // Assert
            Assert.That(actualResults, Is.True, "should return true");
        }

        [Test]
        [Property("service", "SkillService")]
        public async Task AddInActiveSkillToMyProfile_ShouldReturnexception()
        {
            // Arrange
            var currentUserId = this.DummyUsers.FirstOrDefault().Id;

            this.MockedUserService.Setup(u => u.GetCurrentUserId())
             .Returns(Task.FromResult(currentUserId));

            var skillToadd = this.DummySkills.LastOrDefault(s => s.IsDeleted == true);
            var userSkill = new UserSkill
            {
                SkillId = skillToadd.Id,
                Level = SkillLevel.Advanced
            };

            // Act
            var ex = Assert.ThrowsAsync<NotFoundException>(async () => await this.SkillService.AddSkillToMyProfile(userSkill));

            // Assert
            Assert.That(ex.Message, Is.EqualTo("Not found"));
        }

        [Test]
        [Property("service", "SkillService")]
        public async Task RemoveSkillFromMyProfile_ShouldReturnTrue()
        {
            // Arrange
            var currentUserId = this.DummyUsers.FirstOrDefault().Id;

            this.MockedUserService.Setup(u => u.GetCurrentUserId())
             .Returns(Task.FromResult(currentUserId));

            var skillToremove = this.DummyUserSkills.FirstOrDefault(us => us.UserId == currentUserId);

            // Act
            var actualResults = await this.SkillService.RemoveSkillFromProfile(skillToremove.SkillId);

            // Assert
            Assert.That(actualResults, Is.True, "should return true");
        }

        [Test]
        [Property("service", "SkillService")]
        public async Task EditSkillLevel_ShouldReturnTrue()
        {
            // Arrange
            var currentUserId = this.DummyUsers.FirstOrDefault().Id;

            this.MockedUserService.Setup(u => u.GetCurrentUserId())
             .Returns(Task.FromResult(currentUserId));

            var skillToUpdate = this.DummyUserSkills.FirstOrDefault(us => us.UserId == currentUserId);
            skillToUpdate.Level = SkillLevel.Advanced;

            // Act
            var actualResults = await this.SkillService.EditSkillLevel(skillToUpdate);

            // Assert
            Assert.That(actualResults, Is.True, "should return true");
        }

        [Test]
        [Property("service", "SkillService")]
        public async Task GetMySkills_ShouldReturnCurrentUserSkills()
        {
            // Arrange
            var currentUserId = this.DummyUsers.FirstOrDefault().Id;

            this.MockedUserService.Setup(u => u.GetCurrentUserId())
             .Returns(Task.FromResult(currentUserId));

            // Act
            var actualResults = await this.SkillService.GetMySkills();

            // Assert
            Assert.AreEqual(this.DummyUserSkills.Where(us=> us.UserId == currentUserId).Count(),
                actualResults.Count(), "should return equal skill count");
        }

        [Test]
        [Property("service", "SkillService")]
        public async Task GetAllUserSkills_ShouldReturnAlltUsersSkills()
        {
            // Act
            var actualResults = await this.SkillService.GetAllUsersSkills();

            // Assert
            Assert.AreEqual(this.DummyUserSkills.Count(),
                actualResults.Count(), "should return equal skill count");
        }

        [Test]
        [Property("service", "SkillService")]
        public async Task GetAllSkillsByCategories_ShouldReturnAllActiveSkills()
        {
            // Act
            var actualResults = await this.SkillService.GetAllSkillsByCategories();

            // Assert
            Assert.AreEqual(this.DummySkillCategories.Where(s=> s.Skills.Any(s=> s.IsDeleted == false)).Count(),
                actualResults.Count(), "should return only categories wit at least one active skill");
        }
    }
}
