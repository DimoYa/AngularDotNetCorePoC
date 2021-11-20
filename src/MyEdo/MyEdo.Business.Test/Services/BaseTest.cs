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

    [TestFixture]
    public class BaseTest
    {
        protected MyEdoDbContext Context { get; private set; }

        protected ISkillService SkillService { get; private set; }

        protected ISkillCategoryService SkillCategoryService { get; private set; }

        protected List<Skill> DummySkills { get; private set; }

        protected List<SkillCategory> DummySkillCategories { get; private set; }

        protected Mock<IUserService> MockedUserService { get; private set; }

        [SetUp]
        public void Setup()
        {
            this.Context = MyEdoDbContextInMemoryFactory.InitializeContext();
            this.SkillCategoryService = new SkillCategoryService(this.Context);
            this.MockedUserService = new Mock<IUserService>();

            this.SkillService = new SkillService(Context,
                this.SkillCategoryService, this.MockedUserService.Object);

            this.DummySkillCategories = DummyData.GetDummySkillCategories();
            this.DummySkills = DummyData.GetDummySkills();
            this.Context.AddRange(this.DummySkillCategories);
            this.Context.AddRange(this.DummySkills);
            this.Context.SaveChanges();
        }
    }
}
