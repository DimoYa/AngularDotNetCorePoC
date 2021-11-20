namespace MyEdo.Business.Test.Services
{
    using Moq;
    using MyEdo.Business.Services.AppSkill;
    using MyEdo.Business.Services.AppSkillCategory;
    using MyEdo.Business.Services.AppUser;
    using MyEdo.Business.Test.Common;
    using MyEdo.Core.Models;
    using MyEdo.Data;
    using NUnit.Framework;
    using System.Collections.Generic;
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
    }
}
