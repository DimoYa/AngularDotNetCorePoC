interface SkillCategoryModel {
    categoryId: string;
    categoryName: string;
    skills: SkillModel[]
}

interface SkillModel {
    skillid: string;
    skillName: string;
}

export default SkillCategoryModel;