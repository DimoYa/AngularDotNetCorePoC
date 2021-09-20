export interface SkillCategoryModel {
    categoryId: string;
    categoryName: string;
    skills: SkillModel[]
}

export interface SkillModel {
    id: string;
    name: string;
}