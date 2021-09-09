export interface SkillCategoryModel {
    categoryId: string;
    categoryName: string;
    skills: SkillModel[]
}

export interface SkillModel {
    skillid: string;
    skillName: string;
}